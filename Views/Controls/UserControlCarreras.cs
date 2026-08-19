using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemadeGestiondeRallyUniversitario.Data;
using SistemadeGestiondeRallyUniversitario.Models;
using SistemadeGestiondeRallyUniversitario.Services;
using SistemadeGestiondeRallyUniversitario.Views.Forms.Modals;

namespace SistemadeGestiondeRallyUniversitario
{
    public partial class UserControlCarreras : UserControl
    {
        private readonly CarreraRepository _carreraRepo;
        private readonly EstudianteRepository _estudianteRepo;
        private readonly CalculadorEstadisticasRally _calculador;

        private List<Carrera> _todasLasCarreras;
        private List<Estudiante> _todosLosEstudiantes;
        private List<EquipoRallyItem> _itemsRoster;
        private EquipoRallyItem _carreraSeleccionada = null;

        public UserControlCarreras()
        {
            InitializeComponent();
            _carreraRepo = new CarreraRepository();
            _estudianteRepo = new EstudianteRepository();
            _calculador = new CalculadorEstadisticasRally();
            _todasLasCarreras = new List<Carrera>();
            _todosLosEstudiantes = new List<Estudiante>();
            _itemsRoster = new List<EquipoRallyItem>();
        }

        private async void UserControlCarreras_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;
            await CargarCarrerasAsync();
        }

        public async Task CargarCarrerasAsync()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                _todasLasCarreras = await Task.Run(() => _carreraRepo.ObtenerTodas());
                _todosLosEstudiantes = await Task.Run(() => _estudianteRepo.ObtenerTodos());

                _itemsRoster = await Task.Run(() => _calculador.CalcularEstadoEquipos(_todasLasCarreras, _todosLosEstudiantes));

                FiltrarCarreras();
                ActualizarBotonesSeleccion(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar carreras: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void FiltrarCarreras()
        {
            string busq = txtBuscar.Text.Trim().ToLowerInvariant();

            var filtrados = _itemsRoster
                .Where(c => string.IsNullOrEmpty(busq) ||
                            (c.Carrera != null && c.Carrera.ToLowerInvariant().Contains(busq)) ||
                            (c.Facultad != null && c.Facultad.ToLowerInvariant().Contains(busq)) ||
                            (c.EstadoEquipo != null && c.EstadoEquipo.ToLowerInvariant().Contains(busq)))
                .OrderBy(c => c.Carrera)
                .ToList();

            dgvCarreras.DataSource = null;
            dgvCarreras.DataSource = filtrados;
            ConfigurarColumnas();
        }

        private void ConfigurarColumnas()
        {
            if (dgvCarreras.Columns.Count == 0) return;

            if (dgvCarreras.Columns.Contains("IdCarrera"))
            {
                dgvCarreras.Columns["IdCarrera"].HeaderText = "#";
                dgvCarreras.Columns["IdCarrera"].FillWeight = 20;
            }
            if (dgvCarreras.Columns.Contains("Carrera"))
            {
                dgvCarreras.Columns["Carrera"].HeaderText = "Carrera / Delegación";
                dgvCarreras.Columns["Carrera"].FillWeight = 110;
            }
            if (dgvCarreras.Columns.Contains("Facultad"))
            {
                dgvCarreras.Columns["Facultad"].HeaderText = "Facultad";
                dgvCarreras.Columns["Facultad"].FillWeight = 90;
            }
            if (dgvCarreras.Columns.Contains("Progreso"))
            {
                dgvCarreras.Columns["Progreso"].HeaderText = "Atletas en Roster";
                dgvCarreras.Columns["Progreso"].FillWeight = 55;
            }
            if (dgvCarreras.Columns.Contains("EstadoEquipo"))
            {
                dgvCarreras.Columns["EstadoEquipo"].HeaderText = "Estado del Equipo";
                dgvCarreras.Columns["EstadoEquipo"].FillWeight = 75;
            }

            if (dgvCarreras.Columns.Contains("TotalAtletas")) dgvCarreras.Columns["TotalAtletas"].Visible = false;
            if (dgvCarreras.Columns.Contains("CupoMaximo")) dgvCarreras.Columns["CupoMaximo"].Visible = false;
            if (dgvCarreras.Columns.Contains("Porcentaje")) dgvCarreras.Columns["Porcentaje"].Visible = false;

            dgvCarreras.CellFormatting -= DgvCarreras_CellFormatting;
            dgvCarreras.CellFormatting += DgvCarreras_CellFormatting;
        }

        private void DgvCarreras_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvCarreras.Rows.Count) return;

            if (dgvCarreras.Columns[e.ColumnIndex].Name == "EstadoEquipo" && e.Value != null)
            {
                string val = e.Value.ToString();
                if (val.Contains("COMPLETO"))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(20, 83, 45); // Verde
                    e.CellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                }
                else if (val.Contains("CONFORMACIÓN"))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(180, 83, 9); // Ámbar
                }
                else if (val.Contains("INCOMPLETO"))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(185, 28, 28); // Rojo
                }
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            FiltrarCarreras();
        }

        private void dgvCarreras_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvCarreras.Rows[e.RowIndex].DataBoundItem is EquipoRallyItem seleccion)
            {
                ActualizarBotonesSeleccion(seleccion);
            }
            else
            {
                ActualizarBotonesSeleccion(null);
            }
        }

        private async void dgvCarreras_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvCarreras.Rows[e.RowIndex].DataBoundItem is EquipoRallyItem seleccion)
            {
                await AbrirEditorCarreraAsync(seleccion);
            }
        }

        private void ActualizarBotonesSeleccion(EquipoRallyItem c)
        {
            _carreraSeleccionada = c;
            bool habilitado = c != null;
            btnEditar.Enabled = habilitado;
            btnEliminar.Enabled = habilitado;
        }

        private async void btnNueva_Click(object sender, EventArgs e)
        {
            await AbrirEditorCarreraAsync(null);
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (_carreraSeleccionada != null)
            {
                await AbrirEditorCarreraAsync(_carreraSeleccionada);
            }
        }

        private async Task AbrirEditorCarreraAsync(EquipoRallyItem item)
        {
            Carrera carreraObj = null;
            if (item != null)
            {
                carreraObj = new Carrera
                {
                    IdCarrera = item.IdCarrera,
                    Nombre = item.Carrera,
                    Facultad = item.Facultad
                };
            }

            using (var modal = new FormEditorCarrera(carreraObj))
            {
                if (modal.ShowDialog(this.FindForm()) == DialogResult.OK)
                {
                    await CargarCarrerasAsync();
                }
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_carreraSeleccionada == null) return;

            var confirm = MessageBox.Show($"¿Está seguro de eliminar la carrera '{_carreraSeleccionada.Carrera}'?\nTodos sus estudiantes, inscripciones y tiempos asociados también serán eliminados.",
                                          "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    if (_carreraRepo.Eliminar(_carreraSeleccionada.IdCarrera))
                    {
                        MessageBox.Show("Carrera eliminada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await CargarCarrerasAsync();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar carrera: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

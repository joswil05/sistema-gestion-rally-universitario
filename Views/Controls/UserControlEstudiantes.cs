using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemadeGestiondeRallyUniversitario.Data;
using SistemadeGestiondeRallyUniversitario.Models;
using SistemadeGestiondeRallyUniversitario.Views.Forms.Modals;

namespace SistemadeGestiondeRallyUniversitario
{
    public partial class UserControlEstudiantes : UserControl
    {
        private readonly EstudianteRepository _estudianteRepo;
        private readonly CarreraRepository _carreraRepo;

        private List<Estudiante> _todosLosEstudiantes;
        private List<Carrera> _carreras;
        private Estudiante _estudianteSeleccionado = null;

        public UserControlEstudiantes()
        {
            InitializeComponent();
            _estudianteRepo = new EstudianteRepository();
            _carreraRepo = new CarreraRepository();
            _todosLosEstudiantes = new List<Estudiante>();
            _carreras = new List<Carrera>();
        }

        private async void UserControlEstudiantes_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;
            CargarFiltroCarreras();
            await CargarEstudiantesAsync();
        }

        private void CargarFiltroCarreras()
        {
            try
            {
                _carreras = _carreraRepo.ObtenerTodas();
                cmbFiltroCarrera.Items.Clear();
                cmbFiltroCarrera.Items.Add(new CarreraItem { Id = 0, Nombre = "Todas las Carreras / Delegaciones" });
                foreach (var c in _carreras)
                {
                    cmbFiltroCarrera.Items.Add(new CarreraItem { Id = c.IdCarrera, Nombre = c.Nombre });
                }
                cmbFiltroCarrera.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar filtro de carreras: " + ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public async Task CargarEstudiantesAsync()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _todosLosEstudiantes = await Task.Run(() => _estudianteRepo.ObtenerTodos());
                FiltrarEstudiantes();
                ActualizarBotonesSeleccion(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar estudiantes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void FiltrarEstudiantes()
        {
            int idCarreraFiltro = 0;
            if (cmbFiltroCarrera.SelectedItem is CarreraItem item && item.Id > 0)
            {
                idCarreraFiltro = item.Id;
            }

            string busq = txtBuscar.Text.Trim().ToLowerInvariant();

            var filtrados = _todosLosEstudiantes
                .Where(e => (idCarreraFiltro == 0 || e.IdCarrera == idCarreraFiltro) &&
                            (string.IsNullOrEmpty(busq) ||
                             (e.Nombre != null && e.Nombre.ToLowerInvariant().Contains(busq)) ||
                             (e.Apellido != null && e.Apellido.ToLowerInvariant().Contains(busq)) ||
                             (e.CorreoULSA != null && e.CorreoULSA.ToLowerInvariant().Contains(busq)) ||
                             (e.NombreCarrera != null && e.NombreCarrera.ToLowerInvariant().Contains(busq))))
                .OrderBy(e => e.Nombre)
                .ThenBy(e => e.Apellido)
                .ToList();

            dgvEstudiantes.DataSource = null;
            dgvEstudiantes.DataSource = filtrados;
            ConfigurarColumnas();
        }

        private void ConfigurarColumnas()
        {
            if (dgvEstudiantes.Columns.Count == 0) return;

            if (dgvEstudiantes.Columns.Contains("IdEstudiante"))
            {
                dgvEstudiantes.Columns["IdEstudiante"].HeaderText = "#";
                dgvEstudiantes.Columns["IdEstudiante"].FillWeight = 20;
            }
            if (dgvEstudiantes.Columns.Contains("Nombre"))
            {
                dgvEstudiantes.Columns["Nombre"].HeaderText = "Nombres";
                dgvEstudiantes.Columns["Nombre"].FillWeight = 60;
            }
            if (dgvEstudiantes.Columns.Contains("Apellido"))
            {
                dgvEstudiantes.Columns["Apellido"].HeaderText = "Apellidos";
                dgvEstudiantes.Columns["Apellido"].FillWeight = 60;
            }
            if (dgvEstudiantes.Columns.Contains("CorreoULSA"))
            {
                dgvEstudiantes.Columns["CorreoULSA"].HeaderText = "Correo Institucional ULSA";
                dgvEstudiantes.Columns["CorreoULSA"].FillWeight = 90;
            }
            if (dgvEstudiantes.Columns.Contains("NombreCarrera"))
            {
                dgvEstudiantes.Columns["NombreCarrera"].HeaderText = "Carrera / Delegación";
                dgvEstudiantes.Columns["NombreCarrera"].FillWeight = 90;
            }

            if (dgvEstudiantes.Columns.Contains("IdCarrera")) dgvEstudiantes.Columns["IdCarrera"].Visible = false;
            if (dgvEstudiantes.Columns.Contains("NombreCompleto")) dgvEstudiantes.Columns["NombreCompleto"].Visible = false;
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            FiltrarEstudiantes();
        }

        private void cmbFiltroCarrera_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarEstudiantes();
        }

        private void dgvEstudiantes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvEstudiantes.Rows[e.RowIndex].DataBoundItem is Estudiante est)
            {
                ActualizarBotonesSeleccion(est);
            }
            else
            {
                ActualizarBotonesSeleccion(null);
            }
        }

        private async void dgvEstudiantes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvEstudiantes.Rows[e.RowIndex].DataBoundItem is Estudiante est)
            {
                await AbrirEditorEstudianteAsync(est);
            }
        }

        private void ActualizarBotonesSeleccion(Estudiante est)
        {
            _estudianteSeleccionado = est;
            bool habilitado = est != null;
            btnEditar.Enabled = habilitado;
            btnEliminar.Enabled = habilitado;
            btnInscribirReto.Enabled = habilitado;
        }

        private async void btnNuevo_Click(object sender, EventArgs e)
        {
            await AbrirEditorEstudianteAsync(null);
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (_estudianteSeleccionado != null)
            {
                await AbrirEditorEstudianteAsync(_estudianteSeleccionado);
            }
        }

        private async Task AbrirEditorEstudianteAsync(Estudiante est)
        {
            using (var modal = new FormEditorEstudiante(est))
            {
                if (modal.ShowDialog(this.FindForm()) == DialogResult.OK)
                {
                    await CargarEstudiantesAsync();
                }
            }
        }

        private async void btnInscribirReto_Click(object sender, EventArgs e)
        {
            if (_estudianteSeleccionado == null) return;

            using (var modal = new FormInscribirReto(_estudianteSeleccionado))
            {
                if (modal.ShowDialog(this.FindForm()) == DialogResult.OK)
                {
                    await CargarEstudiantesAsync();
                }
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_estudianteSeleccionado == null) return;

            var confirm = MessageBox.Show($"¿Está seguro de eliminar al estudiante '{_estudianteSeleccionado.NombreCompleto}'?\nSus inscripciones y tiempos asociados también serán removidos.",
                                          "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    if (_estudianteRepo.Eliminar(_estudianteSeleccionado.IdEstudiante))
                    {
                        MessageBox.Show("Estudiante eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await CargarEstudiantesAsync();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar estudiante: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private class CarreraItem
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public override string ToString() => Nombre;
        }
    }
}

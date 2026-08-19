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
    public partial class UserControlResultados : UserControl
    {
        private readonly ResultadoRepository _resultadoRepo;
        private readonly CarreraRepository _carreraRepo;

        private List<Resultado> _todosLosResultados;
        private List<Carrera> _carreras;
        private Resultado _resultadoSeleccionado = null;

        public UserControlResultados()
        {
            InitializeComponent();
            _resultadoRepo = new ResultadoRepository();
            _carreraRepo = new CarreraRepository();
            _todosLosResultados = new List<Resultado>();
            _carreras = new List<Carrera>();
        }

        private async void UserControlResultados_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;
            CargarFiltroCarreras();
            await CargarResultadosAsync();
        }

        private void CargarFiltroCarreras()
        {
            try
            {
                _carreras = _carreraRepo.ObtenerTodas();

                cmbFiltroReto.Items.Clear();
                cmbFiltroReto.Items.Add(new CarreraComboItem { IdCarrera = 0, Nombre = "Todas las Carreras / Delegaciones" });
                foreach (var c in _carreras)
                {
                    cmbFiltroReto.Items.Add(new CarreraComboItem { IdCarrera = c.IdCarrera, Nombre = c.Nombre });
                }
                cmbFiltroReto.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar carreras: " + ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public async Task CargarResultadosAsync()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                _todosLosResultados = await Task.Run(() => _resultadoRepo.ObtenerTodos());

                var listaMostrar = _todosLosResultados;
                if (cmbFiltroReto.SelectedItem is CarreraComboItem cci && cci.IdCarrera > 0)
                {
                    listaMostrar = _todosLosResultados
                        .Where(r => string.Equals(r.NombreCarrera, cci.Nombre, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }

                dgvResultados.DataSource = null;
                dgvResultados.DataSource = listaMostrar;
                ConfigurarColumnas();
                ActualizarBotonesSeleccion(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar resultados: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void ConfigurarColumnas()
        {
            if (dgvResultados.Columns.Count == 0) return;

            if (dgvResultados.Columns.Contains("IdResultado"))
            {
                dgvResultados.Columns["IdResultado"].HeaderText = "#";
                dgvResultados.Columns["IdResultado"].FillWeight = 20;
            }
            if (dgvResultados.Columns.Contains("NombreCarrera"))
            {
                dgvResultados.Columns["NombreCarrera"].HeaderText = "Carrera / Delegación";
                dgvResultados.Columns["NombreCarrera"].FillWeight = 90;
            }
            if (dgvResultados.Columns.Contains("NombreReto"))
            {
                dgvResultados.Columns["NombreReto"].HeaderText = "Reto / Circuito";
                dgvResultados.Columns["NombreReto"].FillWeight = 70;
            }
            if (dgvResultados.Columns.Contains("NombreEstudiante"))
            {
                dgvResultados.Columns["NombreEstudiante"].HeaderText = "Representante / Capitán";
                dgvResultados.Columns["NombreEstudiante"].FillWeight = 80;
            }
            if (dgvResultados.Columns.Contains("Tiempo"))
            {
                dgvResultados.Columns["Tiempo"].HeaderText = "Tiempo Oficial (seg)";
                dgvResultados.Columns["Tiempo"].FillWeight = 50;
            }
            if (dgvResultados.Columns.Contains("FechaRegistro"))
            {
                dgvResultados.Columns["FechaRegistro"].HeaderText = "Hora de Registro";
                dgvResultados.Columns["FechaRegistro"].FillWeight = 60;
            }

            if (dgvResultados.Columns.Contains("IdInscripcion")) dgvResultados.Columns["IdInscripcion"].Visible = false;
            if (dgvResultados.Columns.Contains("IdOrganizador")) dgvResultados.Columns["IdOrganizador"].Visible = false;
            if (dgvResultados.Columns.Contains("NombreOrganizador")) dgvResultados.Columns["NombreOrganizador"].Visible = false;
        }

        private async void cmbFiltroReto_SelectedIndexChanged(object sender, EventArgs e)
        {
            await CargarResultadosAsync();
        }

        private void dgvResultados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvResultados.Rows[e.RowIndex].DataBoundItem is Resultado seleccion)
            {
                ActualizarBotonesSeleccion(seleccion);
            }
            else
            {
                ActualizarBotonesSeleccion(null);
            }
        }

        private void ActualizarBotonesSeleccion(Resultado res)
        {
            _resultadoSeleccionado = res;
            btnEliminar.Enabled = res != null;
        }

        private async void btnAbrirMesaCrono_Click(object sender, EventArgs e)
        {
            using (var modal = new FormRegistrarTiempo())
            {
                if (modal.ShowDialog(this.FindForm()) == DialogResult.OK)
                {
                    await CargarResultadosAsync();
                }
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_resultadoSeleccionado == null) return;

            var confirm = MessageBox.Show($"¿Está seguro de eliminar esta marca oficial de {_resultadoSeleccionado.NombreCarrera}?",
                                          "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    if (_resultadoRepo.EliminarResultado(_resultadoSeleccionado.IdResultado))
                    {
                        MessageBox.Show("Marca oficial eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await CargarResultadosAsync();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar marca: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private class CarreraComboItem
        {
            public int IdCarrera { get; set; }
            public string Nombre { get; set; }
            public override string ToString() => Nombre;
        }
    }
}

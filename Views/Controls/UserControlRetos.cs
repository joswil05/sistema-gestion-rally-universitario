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
    public partial class UserControlRetos : UserControl
    {
        private readonly RetoRepository _retoRepo;
        private List<Reto> _todosLosRetos;
        private Reto _retoSeleccionado = null;

        public UserControlRetos()
        {
            InitializeComponent();
            _retoRepo = new RetoRepository();
            _todosLosRetos = new List<Reto>();
        }

        private async void UserControlRetos_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;
            await CargarRetosAsync();
        }

        public async Task CargarRetosAsync()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _todosLosRetos = await Task.Run(() => _retoRepo.ObtenerTodos());
                FiltrarRetos();
                ActualizarBotonesSeleccion(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar retos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void FiltrarRetos()
        {
            string busq = txtBuscar.Text.Trim().ToLowerInvariant();

            var filtrados = _todosLosRetos
                .Where(r => string.IsNullOrEmpty(busq) ||
                            (r.Nombre != null && r.Nombre.ToLowerInvariant().Contains(busq)) ||
                            (r.Descripcion != null && r.Descripcion.ToLowerInvariant().Contains(busq)) ||
                            (r.NombreOrganizador != null && r.NombreOrganizador.ToLowerInvariant().Contains(busq)))
                .OrderBy(r => r.IdReto)
                .ToList();

            dgvRetos.DataSource = null;
            dgvRetos.DataSource = filtrados;
            ConfigurarColumnas();
        }

        private void ConfigurarColumnas()
        {
            if (dgvRetos.Columns.Count == 0) return;

            if (dgvRetos.Columns.Contains("IdReto"))
            {
                dgvRetos.Columns["IdReto"].HeaderText = "#";
                dgvRetos.Columns["IdReto"].FillWeight = 20;
            }
            if (dgvRetos.Columns.Contains("Nombre"))
            {
                dgvRetos.Columns["Nombre"].HeaderText = "Estación / Reto del Circuito";
                dgvRetos.Columns["Nombre"].FillWeight = 90;
            }
            if (dgvRetos.Columns.Contains("Descripcion"))
            {
                dgvRetos.Columns["Descripcion"].HeaderText = "Descripción del Obstáculo";
                dgvRetos.Columns["Descripcion"].FillWeight = 110;
            }
            if (dgvRetos.Columns.Contains("CupoMaximoPorCarrera"))
            {
                dgvRetos.Columns["CupoMaximoPorCarrera"].HeaderText = "Cupo / Carrera";
                dgvRetos.Columns["CupoMaximoPorCarrera"].FillWeight = 45;
            }
            if (dgvRetos.Columns.Contains("NombreOrganizador"))
            {
                dgvRetos.Columns["NombreOrganizador"].HeaderText = "Juez / Organizador Responsable";
                dgvRetos.Columns["NombreOrganizador"].FillWeight = 85;
            }

            if (dgvRetos.Columns.Contains("IdOrganizador")) dgvRetos.Columns["IdOrganizador"].Visible = false;
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            FiltrarRetos();
        }

        private void dgvRetos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvRetos.Rows[e.RowIndex].DataBoundItem is Reto seleccion)
            {
                ActualizarBotonesSeleccion(seleccion);
            }
            else
            {
                ActualizarBotonesSeleccion(null);
            }
        }

        private async void dgvRetos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvRetos.Rows[e.RowIndex].DataBoundItem is Reto seleccion)
            {
                await AbrirEditorRetoAsync(seleccion);
            }
        }

        private void ActualizarBotonesSeleccion(Reto r)
        {
            _retoSeleccionado = r;
            bool habilitado = r != null;
            btnEditar.Enabled = habilitado;
            btnEliminar.Enabled = habilitado;
        }

        private async void btnNuevo_Click(object sender, EventArgs e)
        {
            await AbrirEditorRetoAsync(null);
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (_retoSeleccionado != null)
            {
                await AbrirEditorRetoAsync(_retoSeleccionado);
            }
        }

        private async Task AbrirEditorRetoAsync(Reto reto)
        {
            using (var modal = new FormEditorReto(reto))
            {
                if (modal.ShowDialog(this.FindForm()) == DialogResult.OK)
                {
                    await CargarRetosAsync();
                }
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_retoSeleccionado == null) return;

            var confirm = MessageBox.Show($"¿Está seguro de eliminar el reto '{_retoSeleccionado.Nombre}'?\nLas inscripciones asociadas también serán eliminadas.",
                                          "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    if (_retoRepo.Eliminar(_retoSeleccionado.IdReto))
                    {
                        MessageBox.Show("Reto eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await CargarRetosAsync();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar reto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

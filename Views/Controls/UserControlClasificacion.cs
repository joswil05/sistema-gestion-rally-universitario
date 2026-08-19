using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemadeGestiondeRallyUniversitario.Data;
using SistemadeGestiondeRallyUniversitario.Models;
using SistemadeGestiondeRallyUniversitario.Services;

namespace SistemadeGestiondeRallyUniversitario
{
    public partial class UserControlClasificacion : UserControl
    {
        private readonly ClasificacionRepository _clasificacionRepo;
        private readonly RetoRepository _retoRepo;
        private List<Reto> _retos;

        public UserControlClasificacion()
        {
            InitializeComponent();
            _clasificacionRepo = new ClasificacionRepository();
            _retoRepo = new RetoRepository();
            _retos = new List<Reto>();
        }

        private async void UserControlClasificacion_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;
            CargarOpcionesFiltro();
            await CargarTablaClasificacionAsync();
        }

        private void CargarOpcionesFiltro()
        {
            try
            {
                _retos = _retoRepo.ObtenerTodos();
                cmbFiltro.Items.Clear();
                cmbFiltro.Items.Add(new FiltroClasificacionItem { IdReto = 0, Texto = "🏆 Clasificación General (Acumulado de Retos)" });

                foreach (var r in _retos)
                {
                    cmbFiltro.Items.Add(new FiltroClasificacionItem { IdReto = r.IdReto, Texto = "⚡ Reto: " + r.Nombre });
                }

                cmbFiltro.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar filtros de clasificación: " + ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public async Task CargarTablaClasificacionAsync()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                int idReto = 0;
                if (cmbFiltro.SelectedItem is FiltroClasificacionItem fci)
                {
                    idReto = fci.IdReto;
                }

                List<ClasificacionItem> lista;
                if (idReto == 0)
                {
                    lista = await Task.Run(() => _clasificacionRepo.ObtenerClasificacionGeneral());
                }
                else
                {
                    int idRetoCopy = idReto; // Capturar para el closure
                    lista = await Task.Run(() => _clasificacionRepo.ObtenerClasificacionPorReto(idRetoCopy));
                }

                dgvClasificacion.DataSource = null;

                if (lista.Count > 0)
                {
                    dgvClasificacion.DataSource = lista;
                    ConfigurarColumnas();
                }
                else
                {
                    dgvClasificacion.Columns.Clear();
                    dgvClasificacion.Columns.Add("Info", "Estado");
                    dgvClasificacion.Rows.Add("No hay registros de tiempo válidos para la modalidad seleccionada.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al calcular clasificación: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void ConfigurarColumnas()
        {
            if (dgvClasificacion.Columns.Count == 0) return;

            if (dgvClasificacion.Columns.Contains("Posicion"))
            {
                dgvClasificacion.Columns["Posicion"].HeaderText = "Puesto";
                dgvClasificacion.Columns["Posicion"].FillWeight = 30;
            }
            if (dgvClasificacion.Columns.Contains("Carrera"))
            {
                dgvClasificacion.Columns["Carrera"].HeaderText = "Carrera Universitaria";
                dgvClasificacion.Columns["Carrera"].FillWeight = 130;
            }
            if (dgvClasificacion.Columns.Contains("Facultad"))
            {
                dgvClasificacion.Columns["Facultad"].HeaderText = "Facultad";
                dgvClasificacion.Columns["Facultad"].FillWeight = 85;
            }
            if (dgvClasificacion.Columns.Contains("Reto"))
            {
                dgvClasificacion.Columns["Reto"].HeaderText = "Prueba / Reto";
                dgvClasificacion.Columns["Reto"].FillWeight = 85;
            }
            if (dgvClasificacion.Columns.Contains("TiempoFormateado"))
            {
                dgvClasificacion.Columns["TiempoFormateado"].HeaderText = "Tiempo Registrado";
                dgvClasificacion.Columns["TiempoFormateado"].FillWeight = 75;
            }
            if (dgvClasificacion.Columns.Contains("RetosCompletados"))
            {
                dgvClasificacion.Columns["RetosCompletados"].HeaderText = "Retos Ganados/Hechos";
                dgvClasificacion.Columns["RetosCompletados"].FillWeight = 70;
            }
            if (dgvClasificacion.Columns.Contains("TotalParticipantes"))
            {
                dgvClasificacion.Columns["TotalParticipantes"].HeaderText = "Participantes";
                dgvClasificacion.Columns["TotalParticipantes"].FillWeight = 55;
            }

            // Ocultar IDs numéricos crudos
            if (dgvClasificacion.Columns.Contains("IdCarrera")) dgvClasificacion.Columns["IdCarrera"].Visible = false;
            if (dgvClasificacion.Columns.Contains("TiempoSegundos")) dgvClasificacion.Columns["TiempoSegundos"].Visible = false;
        }

        private async void cmbFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            await CargarTablaClasificacionAsync();
        }

        private async void btnRefrescar_Click(object sender, EventArgs e)
        {
            await CargarTablaClasificacionAsync();
        }

        private void dgvClasificacion_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvClasificacion.Rows[e.RowIndex].DataBoundItem is ClasificacionItem item)
            {
                if (item.Posicion == 1)
                {
                    dgvClasificacion.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(254, 249, 195); // Oro suave
                    dgvClasificacion.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.FromArgb(113, 63, 18);
                    dgvClasificacion.Rows[e.RowIndex].DefaultCellStyle.Font = new Font(dgvClasificacion.Font, FontStyle.Bold);
                }
                else if (item.Posicion == 2)
                {
                    dgvClasificacion.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(241, 245, 249); // Plata suave
                    dgvClasificacion.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.FromArgb(51, 65, 85);
                }
                else if (item.Posicion == 3)
                {
                    dgvClasificacion.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 237, 213); // Bronce suave
                    dgvClasificacion.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.FromArgb(124, 45, 18);
                }
            }
        }

        private void btnPantallaGigante_Click(object sender, EventArgs e)
        {
            try
            {
                using (var formScoreboard = new Views.Forms.FormPantallaGigante())
                {
                    formScoreboard.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir modo Pantalla Gigante: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnExportarActa_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                var clasificacion = await Task.Run(() => _clasificacionRepo.ObtenerClasificacionGeneral());
                var stats = await Task.Run(() => _clasificacionRepo.ObtenerEstadisticasDashboard());

                string nombreOrganizador = SesionActual.OrganizadorLogueado?.NombreCompleto ?? "Comité Organizador ULSA";
                string html = ReporteRallyService.GenerarActaResultadosHtml(clasificacion, stats, nombreOrganizador);

                ReporteRallyService.ExportarYAbrirReporte(html);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar acta de resultados: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private class FiltroClasificacionItem
        {
            public int IdReto { get; set; }
            public string Texto { get; set; }
            public override string ToString() => Texto;
        }
    }
}

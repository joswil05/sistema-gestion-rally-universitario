using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemadeGestiondeRallyUniversitario.Data;
using SistemadeGestiondeRallyUniversitario.Models;

namespace SistemadeGestiondeRallyUniversitario
{
    public partial class UserControlInicio : UserControl
    {
        private readonly ClasificacionRepository _clasificacionRepo;

        public UserControlInicio()
        {
            InitializeComponent();
            _clasificacionRepo = new ClasificacionRepository();
        }

        private async void UserControlInicio_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;
            await CargarDatosDashboardAsync();
        }

        public async Task CargarDatosDashboardAsync()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // Ejecutar la consulta pesada fuera del hilo de UI
                DashboardStats stats = await Task.Run(() => _clasificacionRepo.ObtenerEstadisticasDashboard());
                var clasificacion = await Task.Run(() => _clasificacionRepo.ObtenerClasificacionGeneral());

                // Actualizar controles en el hilo de UI (ya estamos aquí por el await)
                lblTotalCarreras.Text = stats.TotalCarreras.ToString();
                lblTotalEstudiantes.Text = stats.TotalEstudiantes.ToString();
                lblTotalRetos.Text = stats.TotalRetos.ToString();
                lblTotalResultados.Text = stats.TotalResultados.ToString();

                dgvPodio.DataSource = null;

                if (clasificacion.Count > 0)
                {
                    dgvPodio.DataSource = clasificacion;
                    ConfigurarColumnasPodio();
                }
                else
                {
                    dgvPodio.Columns.Clear();
                    dgvPodio.Columns.Add("Mensaje", "Estado del Rally");
                    dgvPodio.Rows.Add("Aún no se han registrado tiempos en los retos para generar la clasificación.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos del dashboard: " + ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void ConfigurarColumnasPodio()
        {
            if (dgvPodio.Columns.Count == 0) return;

            if (dgvPodio.Columns.Contains("Posicion"))
            {
                dgvPodio.Columns["Posicion"].HeaderText = "#";
                dgvPodio.Columns["Posicion"].FillWeight = 30;
            }
            if (dgvPodio.Columns.Contains("Carrera"))
            {
                dgvPodio.Columns["Carrera"].HeaderText = "Carrera / Carrera Universitaria";
                dgvPodio.Columns["Carrera"].FillWeight = 140;
            }
            if (dgvPodio.Columns.Contains("Facultad"))
            {
                dgvPodio.Columns["Facultad"].HeaderText = "Facultad";
                dgvPodio.Columns["Facultad"].FillWeight = 90;
            }
            if (dgvPodio.Columns.Contains("TiempoFormateado"))
            {
                dgvPodio.Columns["TiempoFormateado"].HeaderText = "Tiempo Total";
                dgvPodio.Columns["TiempoFormateado"].FillWeight = 70;
            }
            if (dgvPodio.Columns.Contains("RetosCompletados"))
            {
                dgvPodio.Columns["RetosCompletados"].HeaderText = "Retos Realizados";
                dgvPodio.Columns["RetosCompletados"].FillWeight = 70;
            }
            if (dgvPodio.Columns.Contains("TotalParticipantes"))
            {
                dgvPodio.Columns["TotalParticipantes"].HeaderText = "Participantes";
                dgvPodio.Columns["TotalParticipantes"].FillWeight = 60;
            }

            // Ocultar IDs y datos crudos
            if (dgvPodio.Columns.Contains("IdCarrera")) dgvPodio.Columns["IdCarrera"].Visible = false;
            if (dgvPodio.Columns.Contains("Reto")) dgvPodio.Columns["Reto"].Visible = false;
            if (dgvPodio.Columns.Contains("TiempoSegundos")) dgvPodio.Columns["TiempoSegundos"].Visible = false;

            dgvPodio.CellFormatting -= DgvPodio_CellFormatting;
            dgvPodio.CellFormatting += DgvPodio_CellFormatting;
        }

        private void DgvPodio_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvPodio.Rows.Count) return;

            // Formatear columna de posición con medallas
            if (dgvPodio.Columns[e.ColumnIndex].Name == "Posicion" && e.Value != null)
            {
                string pos = e.Value.ToString();
                if (pos == "1")
                {
                    e.Value = "🥇 1°";
                    e.FormattingApplied = true;
                }
                else if (pos == "2")
                {
                    e.Value = "🥈 2°";
                    e.FormattingApplied = true;
                }
                else if (pos == "3")
                {
                    e.Value = "🥉 3°";
                    e.FormattingApplied = true;
                }
            }

            // Resaltar suavemente las primeras 3 filas
            if (e.RowIndex == 0)
            {
                dgvPodio.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(254, 252, 232); // Amarillo oro suave
                dgvPodio.Rows[e.RowIndex].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            }
            else if (e.RowIndex == 1)
            {
                dgvPodio.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252); // Plata suave
                dgvPodio.Rows[e.RowIndex].DefaultCellStyle.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            }
            else if (e.RowIndex == 2)
            {
                dgvPodio.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 247, 237); // Bronce suave
            }
        }

        private async void btnRefrescar_Click(object sender, EventArgs e)
        {
            await CargarDatosDashboardAsync();
        }
    }
}

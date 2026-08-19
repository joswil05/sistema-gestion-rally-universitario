using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemadeGestiondeRallyUniversitario.Data;
using SistemadeGestiondeRallyUniversitario.Models;

namespace SistemadeGestiondeRallyUniversitario.Views.Forms
{
    public partial class FormPantallaGigante : Form
    {
        private readonly ClasificacionRepository _clasificacionRepo;
        private readonly Timer _timerRefresco = new Timer();
        private readonly Timer _timerReloj = new Timer();

        public FormPantallaGigante()
        {
            InitializeComponent();
            _clasificacionRepo = new ClasificacionRepository();

            _timerRefresco.Interval = 4000; // Auto-refresco en vivo cada 4 segundos
            _timerRefresco.Tick += async (s, e) => await CargarLeaderboardEnVivoAsync();

            _timerReloj.Interval = 1000;
            _timerReloj.Tick += (s, e) => lblHora.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private async void FormPantallaGigante_Load(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("HH:mm:ss");
            _timerReloj.Start();
            _timerRefresco.Start();
            await CargarLeaderboardEnVivoAsync();
        }

        private async Task CargarLeaderboardEnVivoAsync()
        {
            try
            {
                var clasificacion = await Task.Run(() => _clasificacionRepo.ObtenerClasificacionGeneral());
                ActualizarPodio(clasificacion);

                dgvLeaderboard.DataSource = null;
                if (clasificacion.Count > 0)
                {
                    dgvLeaderboard.DataSource = clasificacion;
                    ConfigurarColumnas();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en auto-refresco del scoreboard: " + ex.Message);
            }
        }

        private void ActualizarPodio(List<ClasificacionItem> clasificacion)
        {
            // 🥇 1° Lugar (Oro)
            if (clasificacion.Count > 0)
            {
                var oro = clasificacion[0];
                lblOroCarrera.Text = oro.Carrera;
                lblOroTiempo.Text = oro.TiempoFormateado;
                lblOroRetos.Text = $"Retos Completados: {oro.RetosCompletados} ({oro.TotalParticipantes} atletas)";
            }
            else
            {
                lblOroCarrera.Text = "En espera de marcas...";
                lblOroTiempo.Text = "00:00.00";
                lblOroRetos.Text = "Retos Completados: 0";
            }

            // 🥈 2° Lugar (Plata)
            if (clasificacion.Count > 1)
            {
                var plata = clasificacion[1];
                lblPlataCarrera.Text = plata.Carrera;
                lblPlataTiempo.Text = plata.TiempoFormateado;
                lblPlataRetos.Text = $"Retos Completados: {plata.RetosCompletados} ({plata.TotalParticipantes} atletas)";
            }
            else
            {
                lblPlataCarrera.Text = "Sin registro aún";
                lblPlataTiempo.Text = "00:00.00";
                lblPlataRetos.Text = "Retos Completados: 0";
            }

            // 🥉 3° Lugar (Bronce)
            if (clasificacion.Count > 2)
            {
                var bronce = clasificacion[2];
                lblBronceCarrera.Text = bronce.Carrera;
                lblBronceTiempo.Text = bronce.TiempoFormateado;
                lblBronceRetos.Text = $"Retos Completados: {bronce.RetosCompletados} ({bronce.TotalParticipantes} atletas)";
            }
            else
            {
                lblBronceCarrera.Text = "Sin registro aún";
                lblBronceTiempo.Text = "00:00.00";
                lblBronceRetos.Text = "Retos Completados: 0";
            }
        }

        private void ConfigurarColumnas()
        {
            if (dgvLeaderboard.Columns.Count == 0) return;

            if (dgvLeaderboard.Columns.Contains("Posicion"))
            {
                dgvLeaderboard.Columns["Posicion"].HeaderText = "POSICIÓN";
                dgvLeaderboard.Columns["Posicion"].FillWeight = 25;
            }
            if (dgvLeaderboard.Columns.Contains("Carrera"))
            {
                dgvLeaderboard.Columns["Carrera"].HeaderText = "CARRERA / EQUIPO REPRESENTANTE";
                dgvLeaderboard.Columns["Carrera"].FillWeight = 110;
            }
            if (dgvLeaderboard.Columns.Contains("Facultad"))
            {
                dgvLeaderboard.Columns["Facultad"].HeaderText = "FACULTAD";
                dgvLeaderboard.Columns["Facultad"].FillWeight = 80;
            }
            if (dgvLeaderboard.Columns.Contains("TiempoFormateado"))
            {
                dgvLeaderboard.Columns["TiempoFormateado"].HeaderText = "TIEMPO TOTAL OFICIAL";
                dgvLeaderboard.Columns["TiempoFormateado"].FillWeight = 60;
            }
            if (dgvLeaderboard.Columns.Contains("RetosCompletados"))
            {
                dgvLeaderboard.Columns["RetosCompletados"].HeaderText = "RETOS COMPLETADOS";
                dgvLeaderboard.Columns["RetosCompletados"].FillWeight = 55;
            }
            if (dgvLeaderboard.Columns.Contains("TotalParticipantes"))
            {
                dgvLeaderboard.Columns["TotalParticipantes"].HeaderText = "ATLETAS PARTICIPANTES";
                dgvLeaderboard.Columns["TotalParticipantes"].FillWeight = 55;
            }

            if (dgvLeaderboard.Columns.Contains("IdCarrera")) dgvLeaderboard.Columns["IdCarrera"].Visible = false;
            if (dgvLeaderboard.Columns.Contains("Reto")) dgvLeaderboard.Columns["Reto"].Visible = false;
            if (dgvLeaderboard.Columns.Contains("TiempoSegundos")) dgvLeaderboard.Columns["TiempoSegundos"].Visible = false;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            _timerRefresco.Stop();
            _timerReloj.Stop();
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _timerRefresco.Stop();
            _timerReloj.Stop();
            base.OnFormClosing(e);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SistemadeGestiondeRallyUniversitario.Data;
using SistemadeGestiondeRallyUniversitario.Models;
using SistemadeGestiondeRallyUniversitario.Services;

namespace SistemadeGestiondeRallyUniversitario.Views.Forms.Modals
{
    public partial class FormRegistrarTiempo : Form
    {
        private readonly CarreraRepository _carreraRepo;
        private readonly EstudianteRepository _estudianteRepo;
        private readonly InscripcionRepository _inscripcionRepo;
        private readonly ResultadoRepository _resultadoRepo;
        private readonly RetoRepository _retoRepo;

        private List<Carrera> _carreras;
        private List<Reto> _retos;
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private readonly Timer _timerCrono = new Timer();

        public FormRegistrarTiempo()
        {
            InitializeComponent();
            _carreraRepo = new CarreraRepository();
            _estudianteRepo = new EstudianteRepository();
            _inscripcionRepo = new InscripcionRepository();
            _resultadoRepo = new ResultadoRepository();
            _retoRepo = new RetoRepository();

            _timerCrono.Interval = 30;
            _timerCrono.Tick += TimerCrono_Tick;
        }

        private void FormRegistrarTiempo_Load(object sender, EventArgs e)
        {
            CargarPenalizaciones();
            CargarCarrerasYRetos();
            ActualizarTiempoOficialCalculado();
        }

        private void FormRegistrarTiempo_FormClosing(object sender, FormClosingEventArgs e)
        {
            _timerCrono.Stop();
            _stopwatch.Stop();
        }

        private void CargarPenalizaciones()
        {
            cmbPenalizaciones.Items.Clear();
            cmbPenalizaciones.Items.Add(new PenalizacionItem { Segundos = 0, Texto = "🟢 Sin penalizaciones extraordinarias (+0s)" });
            cmbPenalizaciones.Items.Add(new PenalizacionItem { Segundos = 5, Texto = "🟡 Falta de vestimenta o dorsal (+5s)" });
            cmbPenalizaciones.Items.Add(new PenalizacionItem { Segundos = 10, Texto = "🟠 Falta antideportiva leve (+10s)" });
            cmbPenalizaciones.Items.Add(new PenalizacionItem { Segundos = 30, Texto = "🔴 Obstrucción o invasión de carril (+30s)" });
            cmbPenalizaciones.Items.Add(new PenalizacionItem { Segundos = 60, Texto = "⛔ Sanción mayor de comité de jueces (+60s)" });
            cmbPenalizaciones.SelectedIndex = 0;
        }

        private void CargarCarrerasYRetos()
        {
            try
            {
                _carreras = _carreraRepo.ObtenerTodas();
                _retos = _retoRepo.ObtenerTodos();

                cmbCarrera.Items.Clear();
                foreach (var c in _carreras)
                {
                    cmbCarrera.Items.Add(new CarreraComboItem { IdCarrera = c.IdCarrera, Nombre = c.Nombre });
                }
                if (cmbCarrera.Items.Count > 0) cmbCarrera.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar carreras: " + ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #region Cronómetro Digital

        private void TimerCrono_Tick(object sender, EventArgs e)
        {
            TimeSpan ts = _stopwatch.Elapsed;
            lblDisplayCrono.Text = string.Format("{0:D2}:{1:D2}.{2:D2}", ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
        }

        private void BtnDarSalida_Click(object sender, EventArgs e)
        {
            if (!_stopwatch.IsRunning)
            {
                _stopwatch.Restart();
                _timerCrono.Start();
                btnDarSalida.Text = "⏱ EN PISTA...";
                btnDarSalida.BackColor = Color.FromArgb(234, 179, 8);
                btnLlegadaMeta.Enabled = true;
            }
        }

        private void BtnLlegadaMeta_Click(object sender, EventArgs e)
        {
            if (_stopwatch.IsRunning)
            {
                _stopwatch.Stop();
                _timerCrono.Stop();

                TimeSpan ts = _stopwatch.Elapsed;
                int minutos = ts.Minutes + (ts.Hours * 60);
                decimal segundos = ts.Seconds + (decimal)(ts.Milliseconds / 1000.0);

                numMinutos.Value = Math.Min(numMinutos.Maximum, minutos);
                numSegundos.Value = Math.Min(numSegundos.Maximum, Math.Round(segundos, 2));

                btnDarSalida.Text = "▶ DAR SALIDA";
                btnDarSalida.BackColor = Color.FromArgb(0, 133, 66);

                ActualizarTiempoOficialCalculado();
            }
        }

        private void BtnReiniciarCrono_Click(object sender, EventArgs e)
        {
            _stopwatch.Reset();
            _timerCrono.Stop();
            btnDarSalida.Text = "▶ DAR SALIDA";
            btnDarSalida.BackColor = Color.FromArgb(0, 133, 66);
            lblDisplayCrono.Text = "00:00.00";
        }

        #endregion

        #region Cálculo y Checklist

        private void NumTiempo_ValueChanged(object sender, EventArgs e)
        {
            ActualizarTiempoOficialCalculado();
        }

        private void Checklist_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarTiempoOficialCalculado();
        }

        private void CmbPenalizaciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarTiempoOficialCalculado();
        }

        private double CalcularPenalizacionEstaciones()
        {
            double penalizacion = 0;
            if (!chkEstacion1.Checked) penalizacion += 30.0;
            if (!chkEstacion2.Checked) penalizacion += 30.0;
            if (!chkEstacion3.Checked) penalizacion += 30.0;
            if (!chkEstacion4.Checked) penalizacion += 30.0;
            if (!chkEstacion5.Checked) penalizacion += 30.0;
            if (chkPenalizacionFalta.Checked) penalizacion += 15.0;
            return penalizacion;
        }

        private double ObtenerTiempoOficialTotal()
        {
            double minutos = (double)numMinutos.Value;
            double segundos = (double)numSegundos.Value;
            double tiempoBruto = (minutos * 60.0) + segundos;
            double penalizacionEstaciones = CalcularPenalizacionEstaciones();
            double penalizacionExtra = (cmbPenalizaciones.SelectedItem is PenalizacionItem pi) ? pi.Segundos : 0;

            return tiempoBruto + penalizacionEstaciones + penalizacionExtra;
        }

        private void ActualizarTiempoOficialCalculado()
        {
            double tiempoOficial = ObtenerTiempoOficialTotal();
            double penalizacionesTotales = CalcularPenalizacionEstaciones() + (cmbPenalizaciones.SelectedItem is PenalizacionItem pi ? pi.Segundos : 0);

            if (tiempoOficial > 0)
            {
                string detalle = penalizacionesTotales > 0 ? $" (incluye +{penalizacionesTotales:F0}s penalización)" : "";
                lblTiempoOficialCalculado.Text = string.Format("⏱ TOTAL: {0} ({1:F2} seg){2}",
                    ClasificacionItem.FormatearTiempo(tiempoOficial), tiempoOficial, detalle);
            }
            else
            {
                lblTiempoOficialCalculado.Text = "⏱ TOTAL: 00:00.00 (0.00 seg)";
            }
        }

        #endregion

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!(cmbCarrera.SelectedItem is CarreraComboItem cci) || cci.IdCarrera <= 0)
            {
                MessageBox.Show("Por favor, seleccione la Carrera en competencia.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double tiempoTotal = ObtenerTiempoOficialTotal();
            string msgTiempo;
            if (!ValidacionService.ValidarTiempo(tiempoTotal, out msgTiempo))
            {
                MessageBox.Show(msgTiempo, "Validación de Tiempo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idOrganizador = SesionActual.OrganizadorLogueado?.IdOrganizador ?? 1;

            try
            {
                var estudiantesDeCarrera = _estudianteRepo.ObtenerTodos().Where(est => est.IdCarrera == cci.IdCarrera).ToList();
                if (estudiantesDeCarrera.Count == 0)
                {
                    MessageBox.Show($"La carrera '{cci.Nombre}' no tiene atletas en su roster. Registre al menos un alumno primero.",
                        "Roster Requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idEstudianteRep = estudiantesDeCarrera.First().IdEstudiante;
                int idRetoCircuito = _retos.Count > 0 ? _retos.First().IdReto : 1;

                var inscripcionesExistentes = _inscripcionRepo.ObtenerTodas();
                var inscRep = inscripcionesExistentes.FirstOrDefault(i => i.IdEstudiante == idEstudianteRep);
                int idInscripcionObjetivo = 0;

                if (inscRep != null)
                {
                    idInscripcionObjetivo = inscRep.IdInscripcion;
                }
                else
                {
                    string errorMsg;
                    if (_inscripcionRepo.InscribirEstudiante(idEstudianteRep, idRetoCircuito, out errorMsg))
                    {
                        var nuevasInsc = _inscripcionRepo.ObtenerTodas();
                        var creada = nuevasInsc.FirstOrDefault(i => i.IdEstudiante == idEstudianteRep && i.IdReto == idRetoCircuito);
                        if (creada != null) idInscripcionObjetivo = creada.IdInscripcion;
                    }
                }

                if (idInscripcionObjetivo > 0 && _resultadoRepo.RegistrarResultado(idInscripcionObjetivo, tiempoTotal, idOrganizador))
                {
                    MessageBox.Show($"¡Tiempo oficial de {cci.Nombre} registrado con éxito!\n\nTiempo Oficial: {ClasificacionItem.FormatearTiempo(tiempoTotal)} ({tiempoTotal:F2} seg)",
                        "Carrera Registrada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar resultado oficial: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private class CarreraComboItem
        {
            public int IdCarrera { get; set; }
            public string Nombre { get; set; }
            public override string ToString() => Nombre;
        }

        private class PenalizacionItem
        {
            public double Segundos { get; set; }
            public string Texto { get; set; }
            public override string ToString() => Texto;
        }
    }
}

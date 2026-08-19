using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SistemadeGestiondeRallyUniversitario.Data;
using SistemadeGestiondeRallyUniversitario.Models;

namespace SistemadeGestiondeRallyUniversitario.Views.Forms.Modals
{
    public partial class FormInscribirReto : Form
    {
        private readonly Estudiante _estudiante;
        private readonly RetoRepository _retoRepo;
        private readonly InscripcionRepository _inscripcionRepo;
        private List<Reto> _retos;

        public FormInscribirReto(Estudiante estudiante)
        {
            InitializeComponent();
            _estudiante = estudiante ?? throw new ArgumentNullException(nameof(estudiante));
            _retoRepo = new RetoRepository();
            _inscripcionRepo = new InscripcionRepository();
        }

        private void FormInscribirReto_Load(object sender, EventArgs e)
        {
            lblAtletaNombre.Text = $"{_estudiante.Nombre} {_estudiante.Apellido}";
            lblAtletaCarrera.Text = $"Carrera: {_estudiante.NombreCarrera}";

            CargarRetos();
        }

        private void CargarRetos()
        {
            try
            {
                _retos = _retoRepo.ObtenerTodos();
                cmbReto.Items.Clear();
                foreach (var r in _retos)
                {
                    cmbReto.Items.Add(new RetoItem { Id = r.IdReto, Nombre = $"{r.Nombre} (Cupo: {r.CupoMaximoPorCarrera} por carrera)" });
                }
                if (cmbReto.Items.Count > 0) cmbReto.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar retos: " + ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnInscribir_Click(object sender, EventArgs e)
        {
            if (!(cmbReto.SelectedItem is RetoItem ri) || ri.Id <= 0)
            {
                MessageBox.Show("Por favor, seleccione un reto válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string errorMsg;
                bool ok = _inscripcionRepo.InscribirEstudiante(_estudiante.IdEstudiante, ri.Id, out errorMsg);

                if (ok)
                {
                    MessageBox.Show($"¡{_estudiante.Nombre} ha sido inscrito exitosamente en el reto!", "Inscripción Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(errorMsg, "No se pudo inscribir", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al procesar inscripción: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private class RetoItem
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public override string ToString() => Nombre;
        }
    }
}

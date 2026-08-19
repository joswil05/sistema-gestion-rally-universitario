using System;
using System.Windows.Forms;
using SistemadeGestiondeRallyUniversitario.Data;
using SistemadeGestiondeRallyUniversitario.Models;
using SistemadeGestiondeRallyUniversitario.Services;

namespace SistemadeGestiondeRallyUniversitario.Views.Forms.Modals
{
    public partial class FormEditorCarrera : Form
    {
        private readonly CarreraRepository _carreraRepo;
        private readonly Carrera _carreraEditar;

        public FormEditorCarrera(Carrera carreraEditar = null)
        {
            InitializeComponent();
            _carreraRepo = new CarreraRepository();
            _carreraEditar = carreraEditar;
        }

        private void FormEditorCarrera_Load(object sender, EventArgs e)
        {
            if (_carreraEditar != null)
            {
                lblTitulo.Text = "Editar Carrera";
                lblSubtitulo.Text = $"Modificando la carrera #{_carreraEditar.IdCarrera}";
                txtNombreCarrera.Text = _carreraEditar.Nombre;
                txtFacultad.Text = _carreraEditar.Facultad;
                btnGuardar.Text = "💾 Actualizar";
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombreCarrera.Text.Trim();
            string facultad = txtFacultad.Text.Trim();

            string errorMsg;
            if (!ValidacionService.ValidarTextoRequerido(nombre, "Nombre de Carrera", out errorMsg) ||
                !ValidacionService.ValidarTextoRequerido(facultad, "Facultad", out errorMsg))
            {
                MessageBox.Show(errorMsg, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var c = new Carrera
            {
                IdCarrera = _carreraEditar?.IdCarrera ?? 0,
                Nombre = nombre,
                Facultad = facultad
            };

            try
            {
                bool exito;
                if (_carreraEditar == null)
                {
                    exito = _carreraRepo.Insertar(c);
                }
                else
                {
                    exito = _carreraRepo.Actualizar(c);
                }

                if (exito)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar carrera: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

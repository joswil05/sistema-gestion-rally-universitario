using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SistemadeGestiondeRallyUniversitario.Data;
using SistemadeGestiondeRallyUniversitario.Models;
using SistemadeGestiondeRallyUniversitario.Services;

namespace SistemadeGestiondeRallyUniversitario.Views.Forms.Modals
{
    public partial class FormEditorEstudiante : Form
    {
        private readonly EstudianteRepository _estudianteRepo;
        private readonly CarreraRepository _carreraRepo;
        private readonly Estudiante _estudianteEditar;
        private List<Carrera> _carreras;

        public FormEditorEstudiante(Estudiante estudianteEditar = null)
        {
            InitializeComponent();
            _estudianteRepo = new EstudianteRepository();
            _carreraRepo = new CarreraRepository();
            _estudianteEditar = estudianteEditar;
        }

        private void FormEditorEstudiante_Load(object sender, EventArgs e)
        {
            CargarCarreras();

            if (_estudianteEditar != null)
            {
                lblTitulo.Text = "Editar Estudiante";
                lblSubtitulo.Text = $"Modificando los datos de #{_estudianteEditar.IdEstudiante}";
                txtNombre.Text = _estudianteEditar.Nombre;
                txtApellido.Text = _estudianteEditar.Apellido;
                txtCorreo.Text = _estudianteEditar.CorreoULSA;

                for (int i = 0; i < cmbCarrera.Items.Count; i++)
                {
                    if (cmbCarrera.Items[i] is CarreraItem ci && ci.Id == _estudianteEditar.IdCarrera)
                    {
                        cmbCarrera.SelectedIndex = i;
                        break;
                    }
                }

                btnGuardar.Text = "💾 Actualizar";
            }
        }

        private void CargarCarreras()
        {
            try
            {
                _carreras = _carreraRepo.ObtenerTodas();
                cmbCarrera.Items.Clear();
                foreach (var c in _carreras)
                {
                    cmbCarrera.Items.Add(new CarreraItem { Id = c.IdCarrera, Nombre = c.Nombre });
                }
                if (cmbCarrera.Items.Count > 0) cmbCarrera.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar carreras: " + ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string apellido = txtApellido.Text.Trim();
            string correo = txtCorreo.Text.Trim();

            if (!(cmbCarrera.SelectedItem is CarreraItem ci) || ci.Id <= 0)
            {
                MessageBox.Show("Por favor, seleccione una carrera válida.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var est = new Estudiante
            {
                IdEstudiante = _estudianteEditar?.IdEstudiante ?? 0,
                Nombre = nombre,
                Apellido = apellido,
                CorreoULSA = correo,
                IdCarrera = ci.Id
            };

            string errorMsg;
            if (!est.Validar(out errorMsg))
            {
                MessageBox.Show(errorMsg, "Validación de Estudiante", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool exito;
                if (_estudianteEditar == null)
                {
                    exito = _estudianteRepo.Insertar(est);
                }
                else
                {
                    exito = _estudianteRepo.Actualizar(est);
                }

                if (exito)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar estudiante: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

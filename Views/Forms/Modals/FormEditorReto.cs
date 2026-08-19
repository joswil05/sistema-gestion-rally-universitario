using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SistemadeGestiondeRallyUniversitario.Data;
using SistemadeGestiondeRallyUniversitario.Models;
using SistemadeGestiondeRallyUniversitario.Services;

namespace SistemadeGestiondeRallyUniversitario.Views.Forms.Modals
{
    public partial class FormEditorReto : Form
    {
        private readonly RetoRepository _retoRepo;
        private readonly OrganizadorRepository _organizadorRepo;
        private readonly Reto _retoEditar;
        private List<Organizador> _organizadores;

        public FormEditorReto(Reto retoEditar = null)
        {
            InitializeComponent();
            _retoRepo = new RetoRepository();
            _organizadorRepo = new OrganizadorRepository();
            _retoEditar = retoEditar;
        }

        private void FormEditorReto_Load(object sender, EventArgs e)
        {
            CargarOrganizadores();

            if (_retoEditar != null)
            {
                lblTitulo.Text = "Editar Reto";
                lblSubtitulo.Text = $"Modificando la estación #{_retoEditar.IdReto}";
                txtNombreReto.Text = _retoEditar.Nombre;
                txtDescripcion.Text = _retoEditar.Descripcion;
                numCupo.Value = Math.Max(numCupo.Minimum, Math.Min(numCupo.Maximum, _retoEditar.CupoMaximoPorCarrera));

                for (int i = 0; i < cmbOrganizador.Items.Count; i++)
                {
                    if (cmbOrganizador.Items[i] is OrganizadorItem oi && oi.Id == _retoEditar.IdOrganizador)
                    {
                        cmbOrganizador.SelectedIndex = i;
                        break;
                    }
                }

                btnGuardar.Text = "💾 Actualizar";
            }
        }

        private void CargarOrganizadores()
        {
            try
            {
                _organizadores = _organizadorRepo.ObtenerTodos();
                cmbOrganizador.Items.Clear();
                foreach (var org in _organizadores)
                {
                    cmbOrganizador.Items.Add(new OrganizadorItem { Id = org.IdOrganizador, Nombre = org.NombreCompleto });
                }
                if (cmbOrganizador.Items.Count > 0) cmbOrganizador.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar organizadores: " + ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombreReto.Text.Trim();
            string desc = txtDescripcion.Text.Trim();
            int cupo = (int)numCupo.Value;

            if (!(cmbOrganizador.SelectedItem is OrganizadorItem oi) || oi.Id <= 0)
            {
                MessageBox.Show("Por favor, seleccione el organizador responsable.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var r = new Reto
            {
                IdReto = _retoEditar?.IdReto ?? 0,
                Nombre = nombre,
                Descripcion = desc,
                CupoMaximoPorCarrera = cupo,
                IdOrganizador = oi.Id
            };

            string errorMsg;
            if (!r.Validar(out errorMsg))
            {
                MessageBox.Show(errorMsg, "Validación de Reto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool exito;
                if (_retoEditar == null)
                {
                    exito = _retoRepo.Insertar(r);
                }
                else
                {
                    exito = _retoRepo.Actualizar(r);
                }

                if (exito)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar reto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private class OrganizadorItem
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public override string ToString() => Nombre;
        }
    }
}

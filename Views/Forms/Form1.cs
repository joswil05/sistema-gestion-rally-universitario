using System;
using System.Drawing;
using System.Windows.Forms;
using SistemadeGestiondeRallyUniversitario.Data;
using SistemadeGestiondeRallyUniversitario.Models;
using SistemadeGestiondeRallyUniversitario.Services;

namespace SistemadeGestiondeRallyUniversitario
{
    public partial class Form1 : Form
    {
        private readonly OrganizadorRepository _organizadorRepo;

        public Form1()
        {
            InitializeComponent();
            _organizadorRepo = new OrganizadorRepository();

            this.StartPosition = FormStartPosition.CenterScreen;

            txtContrasena.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnIniciarSesion_Click(this, EventArgs.Empty);
                    e.SuppressKeyPress = true;
                }
            };

            txtUsuario.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtContrasena.Focus();
                    e.SuppressKeyPress = true;
                }
            };
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Text.Trim();

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contrasena))
            {
                MessageBox.Show("Por favor, ingrese su usuario/correo y contraseña.", "Campos Requeridos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Organizador organizador = _organizadorRepo.Autenticar(usuario, contrasena);

                if (organizador != null)
                {
                    SesionActual.IniciarSesion(organizador);

                    this.Hide();
                    using (PaginaInicio nuevaVentana = new PaginaInicio())
                    {
                        nuevaVentana.ShowDialog();
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Credenciales incorrectas. Verifique su usuario y contraseña.", "Error de Inicio de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtContrasena.SelectAll();
                    txtContrasena.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al intentar iniciar sesión: " + ex.Message, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

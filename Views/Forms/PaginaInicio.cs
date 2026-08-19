using System;
using System.Drawing;
using System.Windows.Forms;
using SistemadeGestiondeRallyUniversitario.Services;

namespace SistemadeGestiondeRallyUniversitario
{
    public partial class PaginaInicio : Form
    {
        private bool _menuExpandido = true;
        private Button _botonActivo = null;

        public PaginaInicio()
        {
            InitializeComponent();
        }

        private void PaginaInicio_Load(object sender, EventArgs e)
        {
            ActualizarEtiquetaSesion();
            btnInicio_Click(btnInicio, EventArgs.Empty);
        }

        private void ActualizarEtiquetaSesion()
        {
            if (SesionActual.EstaAutenticado)
            {
                lblUsuarioSesion.Text = $"👤 Organizador: {SesionActual.OrganizadorLogueado.NombreCompleto}";
            }
            else
            {
                lblUsuarioSesion.Text = "👤 Sesión: Invitado / Organizador";
            }
        }

        private void ResaltarBotonActivo(Button boton)
        {
            Color colorNormal = Color.White;
            Color colorActivo = Color.FromArgb(0, 133, 66);    // Verde Institucional (#008542)
            Color textoNormal = Color.FromArgb(71, 85, 105);   // Slate Muted (#475569)
            Color textoActivo = Color.White;                   // Blanco alto contraste

            Button[] botones = new Button[] { btnInicio, btnCarreras, btnEstudiantes, btnRetos, btnResultados, btnClasificacion };
            foreach (var b in botones)
            {
                b.BackColor = colorNormal;
                b.ForeColor = textoNormal;
            }

            if (boton != null)
            {
                boton.BackColor = colorActivo;
                boton.ForeColor = textoActivo;
                _botonActivo = boton;
            }
        }

        private void panelMenuLateral_Paint(object sender, PaintEventArgs e)
        {
            using (var pen = new Pen(Color.FromArgb(226, 232, 240), 1))
            {
                e.Graphics.DrawLine(pen, panelMenuLateral.Width - 1, 0, panelMenuLateral.Width - 1, panelMenuLateral.Height);
            }
        }

        private void CargarUserControl(UserControl userControl)
        {
            panelContenidoPrincipal.Controls.Clear();
            userControl.Dock = DockStyle.Fill;
            panelContenidoPrincipal.Controls.Add(userControl);
        }

        private void btnMenuLateral_Click(object sender, EventArgs e)
        {
            if (_menuExpandido)
            {
                panelMenuLateral.Width = 60;
                lblTituloApp.Visible = false;

                btnInicio.Text = "🏠";
                btnCarreras.Text = "🎓";
                btnEstudiantes.Text = "👥";
                btnRetos.Text = "🎯";
                btnResultados.Text = "⏱";
                btnClasificacion.Text = "🏆";
                btnSalir.Text = "🚪";

                _menuExpandido = false;
            }
            else
            {
                panelMenuLateral.Width = 210;
                lblTituloApp.Visible = true;

                btnInicio.Text = "🏠  Inicio";
                btnCarreras.Text = "🎓  Carreras";
                btnEstudiantes.Text = "👥  Estudiantes";
                btnRetos.Text = "🎯  Retos";
                btnResultados.Text = "⏱  Resultados";
                btnClasificacion.Text = "🏆  Clasificación";
                btnSalir.Text = "🚪  Cerrar Sesión";

                _menuExpandido = true;
            }
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            ResaltarBotonActivo(btnInicio);
            CargarUserControl(new UserControlInicio());
        }

        private void btnCarreras_Click(object sender, EventArgs e)
        {
            ResaltarBotonActivo(btnCarreras);
            CargarUserControl(new UserControlCarreras());
        }

        private void btnEstudiantes_Click(object sender, EventArgs e)
        {
            ResaltarBotonActivo(btnEstudiantes);
            CargarUserControl(new UserControlEstudiantes());
        }

        private void btnRetos_Click(object sender, EventArgs e)
        {
            ResaltarBotonActivo(btnRetos);
            CargarUserControl(new UserControlRetos());
        }

        private void btnResultados_Click(object sender, EventArgs e)
        {
            ResaltarBotonActivo(btnResultados);
            CargarUserControl(new UserControlResultados());
        }

        private void btnClasificacion_Click(object sender, EventArgs e)
        {
            ResaltarBotonActivo(btnClasificacion);
            CargarUserControl(new UserControlClasificacion());
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("¿Desea cerrar la sesión actual?", "Cerrar Sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                SesionActual.CerrarSesion();
                this.Close();
            }
        }
    }
}

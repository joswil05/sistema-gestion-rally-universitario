using System;
using System.Windows.Forms;
using SistemadeGestiondeRallyUniversitario.Data;

namespace SistemadeGestiondeRallyUniversitario
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                // Inicializar y reparar esquema de base de datos si es necesario
                DatabaseHelper.InicializarBaseDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al inicializar la base de datos: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Application.Run(new Form1());
        }
    }
}


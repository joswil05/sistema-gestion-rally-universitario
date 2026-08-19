using System;

namespace SistemadeGestiondeRallyUniversitario.Models
{
    public class Resultado
    {
        public int IdResultado { get; set; }
        public double Tiempo { get; set; }
        public string FechaRegistro { get; set; }
        public int IdInscripcion { get; set; }
        public int IdOrganizador { get; set; }

        // Campos auxiliares para visualización
        public string NombreEstudiante { get; set; }
        public string NombreCarrera { get; set; }
        public string NombreReto { get; set; }
        public string NombreOrganizador { get; set; }

        public Resultado()
        {
            FechaRegistro = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public Resultado(int idResultado, double tiempo, string fechaRegistro, int idInscripcion, int idOrganizador)
        {
            IdResultado = idResultado;
            Tiempo = tiempo;
            FechaRegistro = fechaRegistro;
            IdInscripcion = idInscripcion;
            IdOrganizador = idOrganizador;
        }
    }
}

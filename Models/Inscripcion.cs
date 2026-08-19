using System;

namespace SistemadeGestiondeRallyUniversitario.Models
{
    public class Inscripcion
    {
        public int IdInscripcion { get; set; }
        public string FechaInscripcion { get; set; }
        public int IdEstudiante { get; set; }
        public int IdReto { get; set; }

        // Campos auxiliares para vistas y reportes
        public string NombreEstudiante { get; set; }
        public string NombreCarrera { get; set; }
        public string NombreReto { get; set; }

        public Inscripcion()
        {
            FechaInscripcion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public Inscripcion(int idInscripcion, string fechaInscripcion, int idEstudiante, int idReto)
        {
            IdInscripcion = idInscripcion;
            FechaInscripcion = fechaInscripcion;
            IdEstudiante = idEstudiante;
            IdReto = idReto;
        }
    }
}

using System;

namespace SistemadeGestiondeRallyUniversitario.Models
{
    /// <summary>
    /// Representa el estado de conformación y roster de una Carrera como Equipo en el Rally Universitario.
    /// </summary>
    public class EquipoRallyItem
    {
        public int IdCarrera { get; set; }
        public string Carrera { get; set; }
        public string Facultad { get; set; }
        public int TotalAtletas { get; set; }
        public int CupoMaximo { get; set; } = 20;
        public string Progreso { get; set; }
        public double Porcentaje { get; set; }
        public string EstadoEquipo { get; set; }

        public override string ToString()
        {
            return $"{Carrera} - {Progreso} ({EstadoEquipo})";
        }
    }
}

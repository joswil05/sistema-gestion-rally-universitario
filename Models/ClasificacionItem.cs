using System;

namespace SistemadeGestiondeRallyUniversitario.Models
{
    public class ClasificacionItem
    {
        public int Posicion { get; set; }
        public int IdCarrera { get; set; }
        public string Carrera { get; set; }
        public string Facultad { get; set; }
        public string Reto { get; set; }
        public double TiempoSegundos { get; set; }
        public string TiempoFormateado => FormatearTiempo(TiempoSegundos);
        public int RetosCompletados { get; set; }
        public int TotalParticipantes { get; set; }

        public static string FormatearTiempo(double segundos)
        {
            TimeSpan ts = TimeSpan.FromSeconds(segundos);
            if (ts.Hours > 0)
                return string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D2}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            return string.Format("{0:D2}:{1:D2}.{2:D2}", ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
        }
    }
}

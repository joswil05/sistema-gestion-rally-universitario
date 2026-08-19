using System;
using System.Collections.Generic;

namespace SistemadeGestiondeRallyUniversitario.Models
{
    public class DashboardStats
    {
        public int TotalCarreras { get; set; }
        public int TotalEstudiantes { get; set; }
        public int TotalRetos { get; set; }
        public int TotalInscripciones { get; set; }
        public int TotalResultados { get; set; }
        public double MejorTiempoGlobal { get; set; }
        public string CarreraLider { get; set; }
        public List<ClasificacionItem> TopPodio { get; set; }

        public DashboardStats()
        {
            TopPodio = new List<ClasificacionItem>();
            CarreraLider = "Sin resultados";
        }
    }
}

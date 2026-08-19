using System;
using System.Collections.Generic;
using System.Linq;
using SistemadeGestiondeRallyUniversitario.Models;

namespace SistemadeGestiondeRallyUniversitario.Services
{
    /// <summary>
    /// Motor analítico en memoria para el procesamiento de clasificaciones, podio, desempates y estadísticas del Rally.
    /// Implementa operaciones declarativas avanzadas con LINQ sobre colecciones IEnumerable y List.
    /// </summary>
    public class CalculadorEstadisticasRally
    {
        /// <summary>
        /// Calcula la Clasificación General del Rally utilizando LINQ puro en memoria.
        /// Criterios de Desempate: 1) Más retos completados (Desc), 2) Menor tiempo acumulado (Asc), 3) Mayor participación de estudiantes únicos (Desc).
        /// </summary>
        public List<ClasificacionItem> CalcularClasificacionGeneral(
            IEnumerable<Carrera> carreras,
            IEnumerable<Inscripcion> inscripciones,
            IEnumerable<Resultado> resultados)
        {
            if (carreras == null || resultados == null)
                return new List<ClasificacionItem>();

            var listaInscripciones = inscripciones?.ToList() ?? new List<Inscripcion>();
            var listaResultados = resultados.ToList();

            // 1. LINQ: Para cada carrera y reto, obtener la mejor marca individual representativa (Mínimo tiempo)
            var mejoresMarcas = listaResultados
                .Where(r => r.Tiempo > 0)
                .GroupBy(r => new { r.NombreCarrera, r.NombreReto })
                .Select(g => new
                {
                    Carrera = g.Key.NombreCarrera,
                    Reto = g.Key.NombreReto,
                    MejorTiempo = g.Min(x => x.Tiempo)
                })
                .ToList();

            // 2. LINQ: Agrupar por Carrera, sumar tiempos y contar retos completados
            var tablaClasificacion = carreras
                .Select(c =>
                {
                    var marcasCarrera = mejoresMarcas
                        .Where(m => string.Equals(m.Carrera, c.Nombre, StringComparison.OrdinalIgnoreCase))
                        .ToList();

                    int retosCompletados = marcasCarrera.Select(m => m.Reto).Distinct().Count();
                    double tiempoTotal = marcasCarrera.Aggregate(0.0, (acum, m) => acum + m.MejorTiempo);

                    // Contar estudiantes únicos de esta carrera que tienen al menos una inscripción
                    int participantesUnicos = listaInscripciones
                        .Where(i => string.Equals(i.NombreCarrera, c.Nombre, StringComparison.OrdinalIgnoreCase))
                        .Select(i => i.IdEstudiante)
                        .Distinct()
                        .Count();

                    return new ClasificacionItem
                    {
                        IdCarrera = c.IdCarrera,
                        Carrera = c.Nombre,
                        Facultad = c.Facultad,
                        Reto = "General (Acumulado)",
                        TiempoSegundos = tiempoTotal,
                        RetosCompletados = retosCompletados,
                        TotalParticipantes = participantesUnicos
                    };
                })
                .Where(ci => ci.RetosCompletados > 0) // Solo carreras con al menos un reto completado
                .OrderByDescending(ci => ci.RetosCompletados)
                .ThenBy(ci => ci.TiempoSegundos)
                .ThenByDescending(ci => ci.TotalParticipantes)
                .Select((item, indice) =>
                {
                    item.Posicion = indice + 1; // Asignación de puesto de podio mediante indexación LINQ
                    return item;
                })
                .ToList();

            return tablaClasificacion;
        }

        /// <summary>
        /// Calcula la Clasificación para un Reto específico utilizando LINQ.
        /// </summary>
        public List<ClasificacionItem> CalcularClasificacionPorReto(
            int idReto,
            string nombreReto,
            IEnumerable<Carrera> carreras,
            IEnumerable<Inscripcion> inscripcionesDelReto,
            IEnumerable<Resultado> resultadosDelReto)
        {
            if (resultadosDelReto == null || carreras == null)
                return new List<ClasificacionItem>();

            var listaResultados = resultadosDelReto.Where(r => r.Tiempo > 0).ToList();

            var clasificacionReto = listaResultados
                .GroupBy(r => r.NombreCarrera)
                .Select(g =>
                {
                    var carreraObj = carreras.FirstOrDefault(c => string.Equals(c.Nombre, g.Key, StringComparison.OrdinalIgnoreCase));
                    double mejorTiempo = g.Min(r => r.Tiempo);
                    int totalParticipantes = g.Select(r => r.NombreEstudiante).Distinct().Count();

                    return new ClasificacionItem
                    {
                        IdCarrera = carreraObj?.IdCarrera ?? 0,
                        Carrera = g.Key,
                        Facultad = carreraObj?.Facultad ?? "ULSA",
                        Reto = nombreReto,
                        TiempoSegundos = mejorTiempo,
                        RetosCompletados = 1,
                        TotalParticipantes = totalParticipantes
                    };
                })
                .OrderBy(ci => ci.TiempoSegundos)
                .ThenByDescending(ci => ci.TotalParticipantes)
                .Select((item, indice) =>
                {
                    item.Posicion = indice + 1;
                    return item;
                })
                .ToList();

            return clasificacionReto;
        }

        /// <summary>
        /// Consolida las métricas e indicadores del Dashboard usando LINQ en memoria.
        /// </summary>
        public DashboardStats CalcularEstadisticasDashboard(
            IEnumerable<Carrera> carreras,
            IEnumerable<Estudiante> estudiantes,
            IEnumerable<Reto> retos,
            IEnumerable<Inscripcion> inscripciones,
            IEnumerable<Resultado> resultados,
            List<ClasificacionItem> clasificacionGeneral)
        {
            var stats = new DashboardStats
            {
                TotalCarreras = carreras?.Count() ?? 0,
                TotalEstudiantes = estudiantes?.Count() ?? 0,
                TotalRetos = retos?.Count() ?? 0,
                TotalInscripciones = inscripciones?.Count() ?? 0,
                TotalResultados = resultados?.Count() ?? 0,
                MejorTiempoGlobal = (resultados != null && resultados.Any(r => r.Tiempo > 0))
                    ? resultados.Where(r => r.Tiempo > 0).Min(r => r.Tiempo)
                    : 0.0
            };

            if (clasificacionGeneral != null && clasificacionGeneral.Count > 0)
            {
                stats.CarreraLider = clasificacionGeneral.First().Carrera;
                stats.TopPodio = clasificacionGeneral.Take(3).ToList();
            }

            return stats;
        }

        /// <summary>
        /// Calcula el estado del Roster oficial de cada carrera (límite de 20 atletas) usando LINQ.
        /// </summary>
        public List<EquipoRallyItem> CalcularEstadoEquipos(
            IEnumerable<Carrera> carreras,
            IEnumerable<Estudiante> estudiantes)
        {
            if (carreras == null) return new List<EquipoRallyItem>();
            var listaEst = estudiantes?.ToList() ?? new List<Estudiante>();

            return carreras
                .Select(c =>
                {
                    int totalAtletas = listaEst.Count(e => e.IdCarrera == c.IdCarrera);
                    double pct = Math.Min(100.0, (totalAtletas / (double)ValidacionService.MAXIMO_ESTUDIANTES_POR_CARRERA_RALLY) * 100.0);

                    string estado;
                    if (totalAtletas >= ValidacionService.MAXIMO_ESTUDIANTES_POR_CARRERA_RALLY)
                    {
                        estado = "🟢 COMPLETO (Listo)";
                    }
                    else if (totalAtletas >= 10)
                    {
                        estado = $"🟡 EN CONFORMACIÓN ({totalAtletas}/{ValidacionService.MAXIMO_ESTUDIANTES_POR_CARRERA_RALLY})";
                    }
                    else if (totalAtletas > 0)
                    {
                        estado = $"🔴 INCOMPLETO ({totalAtletas}/{ValidacionService.MAXIMO_ESTUDIANTES_POR_CARRERA_RALLY})";
                    }
                    else
                    {
                        estado = "⚪ SIN ATLETAS (0/20)";
                    }

                    return new EquipoRallyItem
                    {
                        IdCarrera = c.IdCarrera,
                        Carrera = c.Nombre,
                        Facultad = c.Facultad,
                        TotalAtletas = totalAtletas,
                        CupoMaximo = ValidacionService.MAXIMO_ESTUDIANTES_POR_CARRERA_RALLY,
                        Progreso = $"{totalAtletas} / {ValidacionService.MAXIMO_ESTUDIANTES_POR_CARRERA_RALLY}",
                        Porcentaje = Math.Round(pct, 1),
                        EstadoEquipo = estado
                    };
                })
                .OrderByDescending(e => e.TotalAtletas)
                .ThenBy(e => e.Carrera)
                .ToList();
        }
    }
}

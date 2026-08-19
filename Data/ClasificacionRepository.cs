using System;
using System.Collections.Generic;
using System.Linq;
using SistemadeGestiondeRallyUniversitario.Models;
using SistemadeGestiondeRallyUniversitario.Services;

namespace SistemadeGestiondeRallyUniversitario.Data
{
    public class ClasificacionRepository
    {
        private readonly CarreraRepository _carreraRepo;
        private readonly RetoRepository _retoRepo;
        private readonly InscripcionRepository _inscripcionRepo;
        private readonly ResultadoRepository _resultadoRepo;
        private readonly EstudianteRepository _estudianteRepo;
        private readonly CalculadorEstadisticasRally _calculador;

        public ClasificacionRepository()
        {
            _carreraRepo = new CarreraRepository();
            _retoRepo = new RetoRepository();
            _inscripcionRepo = new InscripcionRepository();
            _resultadoRepo = new ResultadoRepository();
            _estudianteRepo = new EstudianteRepository();
            _calculador = new CalculadorEstadisticasRally();
        }

        /// <summary>
        /// Obtiene la clasificación general por Carrera utilizando LINQ sobre colecciones en memoria.
        /// </summary>
        public List<ClasificacionItem> ObtenerClasificacionGeneral()
        {
            var carreras = _carreraRepo.ObtenerTodas();
            var inscripciones = _inscripcionRepo.ObtenerTodas();
            var resultados = _resultadoRepo.ObtenerTodos();

            return _calculador.CalcularClasificacionGeneral(carreras, inscripciones, resultados);
        }

        /// <summary>
        /// Obtiene la clasificación filtrada por un Reto específico utilizando LINQ.
        /// </summary>
        public List<ClasificacionItem> ObtenerClasificacionPorReto(int idReto)
        {
            var carreras = _carreraRepo.ObtenerTodas();
            var retos = _retoRepo.ObtenerTodos();
            var retoSeleccionado = retos.FirstOrDefault(r => r.IdReto == idReto);
            string nombreReto = retoSeleccionado?.Nombre ?? "Reto #" + idReto;

            var inscripciones = _inscripcionRepo.ObtenerTodas(idReto);
            var resultados = _resultadoRepo.ObtenerTodos(idReto);

            return _calculador.CalcularClasificacionPorReto(idReto, nombreReto, carreras, inscripciones, resultados);
        }

        /// <summary>
        /// Calcula los indicadores agregados para la pantalla de Inicio / Dashboard en una sola carga en memoria.
        /// </summary>
        public DashboardStats ObtenerEstadisticasDashboard()
        {
            var carreras = _carreraRepo.ObtenerTodas();
            var estudiantes = _estudianteRepo.ObtenerTodos();
            var retos = _retoRepo.ObtenerTodos();
            var inscripciones = _inscripcionRepo.ObtenerTodas();
            var resultados = _resultadoRepo.ObtenerTodos();

            var clasificacionGeneral = _calculador.CalcularClasificacionGeneral(carreras, inscripciones, resultados);

            return _calculador.CalcularEstadisticasDashboard(
                carreras, estudiantes, retos, inscripciones, resultados, clasificacionGeneral);
        }
    }
}

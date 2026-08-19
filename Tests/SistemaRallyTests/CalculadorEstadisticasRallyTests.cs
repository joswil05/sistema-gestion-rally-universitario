using System.Collections.Generic;
using System.Linq;
using SistemadeGestiondeRallyUniversitario.Models;
using SistemadeGestiondeRallyUniversitario.Services;
using Xunit;

namespace SistemadeGestiondeRallyUniversitario.Tests
{
    /// <summary>
    /// Pruebas del motor analítico en memoria (LINQ) que calcula clasificaciones, podio y estadísticas.
    /// Es la lógica de negocio más densa del sistema y la más sensible a bugs de desempate / agregación.
    /// </summary>
    public class CalculadorEstadisticasRallyTests
    {
        private readonly CalculadorEstadisticasRally _calc = new CalculadorEstadisticasRally();

        private static Resultado Res(string carrera, string reto, double tiempo, string estudiante = "Est")
        {
            return new Resultado
            {
                NombreCarrera = carrera,
                NombreReto = reto,
                Tiempo = tiempo,
                NombreEstudiante = estudiante
            };
        }

        private static Inscripcion Insc(int idEstudiante, string carrera, string reto = "Reto1")
        {
            return new Inscripcion
            {
                IdEstudiante = idEstudiante,
                NombreCarrera = carrera,
                NombreReto = reto
            };
        }

        // ---------- CalcularClasificacionGeneral ----------

        [Fact]
        public void ClasificacionGeneral_PriorizaRetosCompletadosSobreElTiempo()
        {
            var carreras = new List<Carrera>
            {
                new Carrera(1, "CarreraA", "FacX"),
                new Carrera(2, "CarreraB", "FacX"),
                new Carrera(3, "CarreraC", "FacX"),
                new Carrera(4, "CarreraD", "FacX"), // sin resultados: debe quedar excluida
            };

            var resultados = new List<Resultado>
            {
                Res("CarreraA", "Reto1", 50), Res("CarreraA", "Reto2", 50), // 2 retos, total 100
                Res("CarreraB", "Reto1", 40), Res("CarreraB", "Reto2", 40), // 2 retos, total 80
                Res("CarreraC", "Reto1", 10),                               // 1 reto, total 10 (tiempo bajo pero menos retos)
            };

            var resultado = _calc.CalcularClasificacionGeneral(carreras, null, resultados);

            Assert.Equal(3, resultado.Count); // CarreraD excluida por no tener retos completados

            Assert.Equal("CarreraB", resultado[0].Carrera); // 2 retos, menor tiempo (80) -> puesto 1
            Assert.Equal(1, resultado[0].Posicion);
            Assert.Equal(80, resultado[0].TiempoSegundos);
            Assert.Equal(2, resultado[0].RetosCompletados);

            Assert.Equal("CarreraA", resultado[1].Carrera); // 2 retos, mayor tiempo (100) -> puesto 2
            Assert.Equal(2, resultado[1].Posicion);

            Assert.Equal("CarreraC", resultado[2].Carrera); // solo 1 reto -> último pese a tiempo bajo
            Assert.Equal(3, resultado[2].Posicion);
        }

        [Fact]
        public void ClasificacionGeneral_DesempataPorParticipantesUnicosCuandoRetosYTiempoEmpatan()
        {
            var carreras = new List<Carrera>
            {
                new Carrera(10, "CarreraX", "FacY"),
                new Carrera(20, "CarreraY", "FacY"),
            };

            var resultados = new List<Resultado>
            {
                Res("CarreraX", "RetoUnico", 30.0),
                Res("CarreraY", "RetoUnico", 30.0),
            };

            var inscripciones = new List<Inscripcion>();
            for (int i = 1; i <= 5; i++) inscripciones.Add(Insc(i, "CarreraX"));
            for (int i = 100; i <= 101; i++) inscripciones.Add(Insc(i, "CarreraY"));

            var resultado = _calc.CalcularClasificacionGeneral(carreras, inscripciones, resultados);

            Assert.Equal(2, resultado.Count);
            Assert.Equal("CarreraX", resultado[0].Carrera); // 5 participantes > 2 -> desempate a su favor
            Assert.Equal(5, resultado[0].TotalParticipantes);
            Assert.Equal("CarreraY", resultado[1].Carrera);
            Assert.Equal(2, resultado[1].TotalParticipantes);
        }

        [Fact]
        public void ClasificacionGeneral_TomaElMejorTiempoNoElPrimeroNiElPromedio()
        {
            var carreras = new List<Carrera> { new Carrera(1, "CarreraA", "Fac") };
            var resultados = new List<Resultado>
            {
                Res("CarreraA", "Reto1", 80), // insertado primero, pero NO es el mejor
                Res("CarreraA", "Reto1", 20), // este es el mejor tiempo del equipo en este reto
            };

            var resultado = _calc.CalcularClasificacionGeneral(carreras, null, resultados);

            Assert.Single(resultado);
            Assert.Equal(20, resultado[0].TiempoSegundos); // ni 80 (primero) ni 50 (promedio)
            Assert.Equal(1, resultado[0].RetosCompletados); // ambos resultados son del mismo reto
        }

        [Fact]
        public void ClasificacionGeneral_ExcluyeResultadosConTiempoCeroONegativo()
        {
            var carreras = new List<Carrera> { new Carrera(1, "CarreraA", "Fac") };
            var resultados = new List<Resultado>
            {
                Res("CarreraA", "RetoValido", 45),
                Res("CarreraA", "RetoInvalidoCero", 0),
                Res("CarreraA", "RetoInvalidoNegativo", -10),
            };

            var resultado = _calc.CalcularClasificacionGeneral(carreras, null, resultados);

            Assert.Single(resultado);
            Assert.Equal(1, resultado[0].RetosCompletados); // solo el reto con tiempo > 0 cuenta
            Assert.Equal(45, resultado[0].TiempoSegundos);
        }

        [Fact]
        public void ClasificacionGeneral_CarrerasONulo_DevuelveListaVacia()
        {
            Assert.Empty(_calc.CalcularClasificacionGeneral(null, null, new List<Resultado>()));
        }

        [Fact]
        public void ClasificacionGeneral_ResultadosNulo_DevuelveListaVacia()
        {
            var carreras = new List<Carrera> { new Carrera(1, "CarreraA", "Fac") };
            Assert.Empty(_calc.CalcularClasificacionGeneral(carreras, null, null));
        }

        [Fact]
        public void ClasificacionGeneral_InscripcionesNulo_NoLanzaYDejaParticipantesEnCero()
        {
            var carreras = new List<Carrera> { new Carrera(1, "CarreraA", "Fac") };
            var resultados = new List<Resultado> { Res("CarreraA", "Reto1", 30) };

            var resultado = _calc.CalcularClasificacionGeneral(carreras, null, resultados);

            Assert.Single(resultado);
            Assert.Equal(0, resultado[0].TotalParticipantes);
            Assert.Equal(1, resultado[0].RetosCompletados); // el conteo de retos NO depende de inscripciones
        }

        [Fact]
        public void ClasificacionGeneral_EmpateTotalAsignaPosicionesConsecutivasSinCompartirRango()
        {
            var carreras = new List<Carrera>
            {
                new Carrera(1, "CarreraA", "Fac"),
                new Carrera(2, "CarreraB", "Fac"),
            };
            var resultados = new List<Resultado>
            {
                Res("CarreraA", "Reto1", 30),
                Res("CarreraB", "Reto1", 30),
            };

            var resultado = _calc.CalcularClasificacionGeneral(carreras, null, resultados);

            Assert.Equal(2, resultado.Count);
            Assert.Equal(1, resultado[0].Posicion);
            Assert.Equal(2, resultado[1].Posicion); // no hay "empate compartido": siempre consecutivo
        }

        [Fact]
        public void ClasificacionGeneral_ComparacionDeCarreraPorNombreEsInsensibleAMayusculas()
        {
            var carreras = new List<Carrera> { new Carrera(1, "carrera mixta", "Fac") };
            var resultados = new List<Resultado> { Res("CARRERA MIXTA", "Reto1", 30) };

            var resultado = _calc.CalcularClasificacionGeneral(carreras, null, resultados);

            Assert.Single(resultado);
            Assert.Equal(1, resultado[0].RetosCompletados);
        }

        // ---------- CalcularClasificacionPorReto ----------

        [Fact]
        public void ClasificacionPorReto_OrdenaPorTiempoAscendenteYDesempataPorParticipantes()
        {
            var carreras = new List<Carrera>
            {
                new Carrera(1, "CarreraA", "Fac"),
                new Carrera(2, "CarreraB", "Fac"),
            };
            var resultados = new List<Resultado>
            {
                Res("CarreraB", "RetoX", 20, "Est1"),
                Res("CarreraA", "RetoX", 10, "Est2"),
            };

            var resultado = _calc.CalcularClasificacionPorReto(1, "RetoX", carreras, new List<Inscripcion>(), resultados);

            Assert.Equal(2, resultado.Count);
            Assert.Equal("CarreraA", resultado[0].Carrera); // menor tiempo primero
            Assert.Equal(1, resultado[0].Posicion);
            Assert.Equal("CarreraB", resultado[1].Carrera);
            Assert.Equal(2, resultado[1].Posicion);
        }

        [Fact]
        public void ClasificacionPorReto_CarreraDesconocida_UsaValoresPorDefecto()
        {
            var carreras = new List<Carrera> { new Carrera(1, "CarreraConocida", "Fac") };
            var resultados = new List<Resultado> { Res("CarreraFantasma", "RetoX", 15) };

            var resultado = _calc.CalcularClasificacionPorReto(1, "RetoX", carreras, new List<Inscripcion>(), resultados);

            Assert.Single(resultado);
            Assert.Equal(0, resultado[0].IdCarrera);
            Assert.Equal("ULSA", resultado[0].Facultad);
        }

        [Fact]
        public void ClasificacionPorReto_CuentaParticipantesUnicosPorEstudiante()
        {
            var carreras = new List<Carrera> { new Carrera(1, "CarreraA", "Fac") };
            var resultados = new List<Resultado>
            {
                Res("CarreraA", "RetoX", 30, "Estudiante1"),
                Res("CarreraA", "RetoX", 25, "Estudiante2"),
            };

            var resultado = _calc.CalcularClasificacionPorReto(1, "RetoX", carreras, new List<Inscripcion>(), resultados);

            Assert.Single(resultado);
            Assert.Equal(2, resultado[0].TotalParticipantes);
            Assert.Equal(25, resultado[0].TiempoSegundos); // el mejor de los dos
        }

        [Fact]
        public void ClasificacionPorReto_ResultadosONuloOCarrerasNulo_DevuelveListaVacia()
        {
            var carreras = new List<Carrera> { new Carrera(1, "CarreraA", "Fac") };
            Assert.Empty(_calc.CalcularClasificacionPorReto(1, "RetoX", carreras, null, null));
            Assert.Empty(_calc.CalcularClasificacionPorReto(1, "RetoX", null, null, new List<Resultado>()));
        }

        // ---------- CalcularEstadisticasDashboard ----------

        [Fact]
        public void Dashboard_CuentaTotalesYManejaColeccionesNulas()
        {
            var stats = _calc.CalcularEstadisticasDashboard(null, null, null, null, null, null);

            Assert.Equal(0, stats.TotalCarreras);
            Assert.Equal(0, stats.TotalEstudiantes);
            Assert.Equal(0, stats.TotalRetos);
            Assert.Equal(0, stats.TotalInscripciones);
            Assert.Equal(0, stats.TotalResultados);
            Assert.Equal(0.0, stats.MejorTiempoGlobal);
            Assert.Equal("Sin resultados", stats.CarreraLider); // valor por defecto del constructor
            Assert.Empty(stats.TopPodio);
        }

        [Fact]
        public void Dashboard_MejorTiempoGlobal_IgnoraResultadosNoPositivos()
        {
            var resultados = new List<Resultado>
            {
                Res("A", "R1", 0),
                Res("A", "R1", -5),
                Res("A", "R1", 33.5),
                Res("A", "R1", 12.25),
            };

            var stats = _calc.CalcularEstadisticasDashboard(null, null, null, null, resultados, null);

            Assert.Equal(12.25, stats.MejorTiempoGlobal);
        }

        [Fact]
        public void Dashboard_MejorTiempoGlobal_CeroCuandoNoHayTiemposPositivos()
        {
            var resultados = new List<Resultado> { Res("A", "R1", 0), Res("A", "R1", -1) };
            var stats = _calc.CalcularEstadisticasDashboard(null, null, null, null, resultados, null);
            Assert.Equal(0.0, stats.MejorTiempoGlobal);
        }

        [Fact]
        public void Dashboard_CarreraLiderYTopPodio_ProvienenDeLaClasificacionYaCalculada()
        {
            var carreras = new List<Carrera>
            {
                new Carrera(1, "CarreraA", "Fac"),
                new Carrera(2, "CarreraB", "Fac"),
                new Carrera(3, "CarreraC", "Fac"),
                new Carrera(4, "CarreraD", "Fac"),
            };
            var resultados = new List<Resultado>
            {
                Res("CarreraA", "R1", 40),
                Res("CarreraB", "R1", 30),
                Res("CarreraC", "R1", 20),
                Res("CarreraD", "R1", 10),
            };

            var clasificacion = _calc.CalcularClasificacionGeneral(carreras, null, resultados);
            var stats = _calc.CalcularEstadisticasDashboard(carreras, null, null, null, resultados, clasificacion);

            Assert.Equal("CarreraD", stats.CarreraLider); // menor tiempo, único reto cada una
            Assert.Equal(3, stats.TopPodio.Count); // Take(3) aunque haya 4 carreras clasificadas
            Assert.Equal("CarreraD", stats.TopPodio[0].Carrera);
            Assert.Equal("CarreraC", stats.TopPodio[1].Carrera);
            Assert.Equal("CarreraB", stats.TopPodio[2].Carrera);
        }

        // ---------- CalcularEstadoEquipos ----------

        [Theory]
        [InlineData(0, "⚪ SIN ATLETAS (0/20)")]
        [InlineData(1, "🔴 INCOMPLETO (1/20)")]
        [InlineData(9, "🔴 INCOMPLETO (9/20)")]
        [InlineData(10, "🟡 EN CONFORMACIÓN (10/20)")]
        [InlineData(19, "🟡 EN CONFORMACIÓN (19/20)")]
        [InlineData(20, "🟢 COMPLETO (Listo)")]
        [InlineData(25, "🟢 COMPLETO (Listo)")] // por encima del cupo del rally, sigue siendo "completo"
        public void EstadoEquipos_UmbralesDeEstadoPorCantidadDeAtletas(int totalAtletas, string estadoEsperado)
        {
            var carreras = new List<Carrera> { new Carrera(1, "CarreraA", "Fac") };
            var estudiantes = new List<Estudiante>();
            for (int i = 0; i < totalAtletas; i++)
                estudiantes.Add(new Estudiante(i, "Nombre" + i, "Apellido" + i, "", 1));

            var resultado = _calc.CalcularEstadoEquipos(carreras, estudiantes);

            Assert.Single(resultado);
            Assert.Equal(estadoEsperado, resultado[0].EstadoEquipo);
            Assert.Equal(totalAtletas, resultado[0].TotalAtletas);
        }

        [Fact]
        public void EstadoEquipos_PorcentajeNuncaSuperaCienAunqueHayaMasDe20Atletas()
        {
            var carreras = new List<Carrera> { new Carrera(1, "CarreraA", "Fac") };
            var estudiantes = new List<Estudiante>();
            for (int i = 0; i < 25; i++) estudiantes.Add(new Estudiante(i, "N" + i, "A" + i, "", 1));

            var resultado = _calc.CalcularEstadoEquipos(carreras, estudiantes);

            Assert.Equal(100.0, resultado[0].Porcentaje);
        }

        [Fact]
        public void EstadoEquipos_PorcentajeSeCalculaSobreCupoDe20()
        {
            var carreras = new List<Carrera> { new Carrera(1, "CarreraA", "Fac") };
            var estudiantes = new List<Estudiante>();
            for (int i = 0; i < 13; i++) estudiantes.Add(new Estudiante(i, "N" + i, "A" + i, "", 1));

            var resultado = _calc.CalcularEstadoEquipos(carreras, estudiantes);

            Assert.Equal(65.0, resultado[0].Porcentaje); // 13/20 * 100
        }

        [Fact]
        public void EstadoEquipos_OrdenaPorTotalAtletasDescendenteYLuegoPorNombre()
        {
            var carreras = new List<Carrera>
            {
                new Carrera(1, "Zoologia", "Fac"),
                new Carrera(2, "Arquitectura", "Fac"),
                new Carrera(3, "Biologia", "Fac"),
            };
            var estudiantes = new List<Estudiante>
            {
                new Estudiante(1, "A", "B", "", 1), // Zoologia: 1 atleta
                new Estudiante(2, "A", "B", "", 2), // Arquitectura: 2 atletas
                new Estudiante(3, "A", "B", "", 2),
                new Estudiante(4, "A", "B", "", 3), // Biologia: 2 atletas (empata con Arquitectura)
                new Estudiante(5, "A", "B", "", 3),
            };

            var resultado = _calc.CalcularEstadoEquipos(carreras, estudiantes);

            Assert.Equal("Arquitectura", resultado[0].Carrera); // empatan en 2, "Arquitectura" < "Biologia" alfabéticamente
            Assert.Equal("Biologia", resultado[1].Carrera);
            Assert.Equal("Zoologia", resultado[2].Carrera); // 1 atleta, va al final
        }

        [Fact]
        public void EstadoEquipos_CarrerasNulo_DevuelveListaVacia()
        {
            Assert.Empty(_calc.CalcularEstadoEquipos(null, new List<Estudiante>()));
        }

        [Fact]
        public void EstadoEquipos_EstudiantesNulo_TodasLasCarrerasQuedanEnCero()
        {
            var carreras = new List<Carrera> { new Carrera(1, "CarreraA", "Fac") };
            var resultado = _calc.CalcularEstadoEquipos(carreras, null);

            Assert.Single(resultado);
            Assert.Equal(0, resultado[0].TotalAtletas);
            Assert.Equal("⚪ SIN ATLETAS (0/20)", resultado[0].EstadoEquipo);
        }
    }
}

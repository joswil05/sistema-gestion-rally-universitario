using System.Linq;
using SistemadeGestiondeRallyUniversitario.Data;
using Xunit;

namespace SistemadeGestiondeRallyUniversitario.Tests.Data
{
    /// <summary>
    /// Prueba de extremo a extremo (end-to-end) contra SQLite real: siembra carrera, estudiantes,
    /// reto, inscripciones y resultados a través de los repositorios reales, y verifica que
    /// ClasificacionRepository (que conecta la capa de datos con CalculadorEstadisticasRally)
    /// produzca una clasificación coherente. Complementa las pruebas puras de
    /// CalculadorEstadisticasRallyTests, que no tocan la base de datos.
    /// </summary>
    [Collection("Base de datos SQLite")]
    public class ClasificacionRepositoryTests
    {
        private readonly CarreraRepository _carreraRepo = new CarreraRepository();
        private readonly EstudianteRepository _estudianteRepo = new EstudianteRepository();
        private readonly RetoRepository _retoRepo = new RetoRepository();
        private readonly InscripcionRepository _inscripcionRepo = new InscripcionRepository();
        private readonly ResultadoRepository _resultadoRepo = new ResultadoRepository();
        private readonly OrganizadorRepository _organizadorRepo = new OrganizadorRepository();
        private readonly ClasificacionRepository _clasificacionRepo = new ClasificacionRepository();

        public ClasificacionRepositoryTests(DatabaseFixture fixture) { _ = fixture; }

        [Fact]
        public void FlujoCompleto_InscripcionYResultado_SeReflejaEnClasificacionGeneralYDashboard()
        {
            var organizador = TestDataHelpers.ObtenerOrganizadorSembrado(_organizadorRepo);

            var carreraRapida = TestDataHelpers.CrearCarrera(_carreraRepo);
            var carreraLenta = TestDataHelpers.CrearCarrera(_carreraRepo);
            var reto = TestDataHelpers.CrearReto(_retoRepo, cupoMaximoPorCarrera: 10);

            var estRapido = TestDataHelpers.CrearEstudiante(_estudianteRepo, carreraRapida.IdCarrera);
            var estLento = TestDataHelpers.CrearEstudiante(_estudianteRepo, carreraLenta.IdCarrera);

            Assert.True(_inscripcionRepo.InscribirEstudiante(estRapido.IdEstudiante, reto.IdReto, out _));
            Assert.True(_inscripcionRepo.InscribirEstudiante(estLento.IdEstudiante, reto.IdReto, out _));

            int idInscRapido = _inscripcionRepo.ObtenerTodas(reto.IdReto).First(i => i.IdEstudiante == estRapido.IdEstudiante).IdInscripcion;
            int idInscLento = _inscripcionRepo.ObtenerTodas(reto.IdReto).First(i => i.IdEstudiante == estLento.IdEstudiante).IdInscripcion;

            Assert.True(_resultadoRepo.RegistrarResultado(idInscRapido, 15.0, organizador.IdOrganizador));
            Assert.True(_resultadoRepo.RegistrarResultado(idInscLento, 45.0, organizador.IdOrganizador));

            var clasificacionGeneral = _clasificacionRepo.ObtenerClasificacionGeneral();
            var posicionRapida = clasificacionGeneral.First(c => c.IdCarrera == carreraRapida.IdCarrera);
            var posicionLenta = clasificacionGeneral.First(c => c.IdCarrera == carreraLenta.IdCarrera);

            Assert.True(posicionRapida.Posicion < posicionLenta.Posicion);
            Assert.Equal(15.0, posicionRapida.TiempoSegundos);

            var clasificacionPorReto = _clasificacionRepo.ObtenerClasificacionPorReto(reto.IdReto);
            Assert.Equal(2, clasificacionPorReto.Count);
            Assert.Equal(carreraRapida.Nombre, clasificacionPorReto[0].Carrera); // menor tiempo -> primer lugar

            var dashboard = _clasificacionRepo.ObtenerEstadisticasDashboard();
            Assert.True(dashboard.TotalCarreras >= 2);
            Assert.True(dashboard.TotalResultados >= 2);
            Assert.True(dashboard.MejorTiempoGlobal <= 15.0); // no puede ser mayor que la mejor marca real registrada
        }
    }
}

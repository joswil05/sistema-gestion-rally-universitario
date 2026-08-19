using System;
using System.Linq;
using SistemadeGestiondeRallyUniversitario.Data;
using Xunit;

namespace SistemadeGestiondeRallyUniversitario.Tests.Data
{
    [Collection("Base de datos SQLite")]
    public class ResultadoRepositoryTests
    {
        private readonly CarreraRepository _carreraRepo = new CarreraRepository();
        private readonly EstudianteRepository _estudianteRepo = new EstudianteRepository();
        private readonly RetoRepository _retoRepo = new RetoRepository();
        private readonly InscripcionRepository _inscripcionRepo = new InscripcionRepository();
        private readonly ResultadoRepository _resultadoRepo = new ResultadoRepository();
        private readonly OrganizadorRepository _organizadorRepo = new OrganizadorRepository();

        public ResultadoRepositoryTests(DatabaseFixture fixture) { _ = fixture; }

        private int CrearInscripcionDePrueba()
        {
            var carrera = TestDataHelpers.CrearCarrera(_carreraRepo);
            var estudiante = TestDataHelpers.CrearEstudiante(_estudianteRepo, carrera.IdCarrera);
            var reto = TestDataHelpers.CrearReto(_retoRepo);
            Assert.True(_inscripcionRepo.InscribirEstudiante(estudiante.IdEstudiante, reto.IdReto, out _));
            return _inscripcionRepo.ObtenerTodas(reto.IdReto)
                .First(i => i.IdEstudiante == estudiante.IdEstudiante).IdInscripcion;
        }

        [Fact]
        public void RegistrarResultado_Exitoso_QuedaConsultable()
        {
            int idInscripcion = CrearInscripcionDePrueba();
            var organizador = TestDataHelpers.ObtenerOrganizadorSembrado(_organizadorRepo);

            bool ok = _resultadoRepo.RegistrarResultado(idInscripcion, 123.45, organizador.IdOrganizador);

            Assert.True(ok);
            var todos = _resultadoRepo.ObtenerTodos();
            Assert.Contains(todos, r => r.IdInscripcion == idInscripcion && r.Tiempo == 123.45);
        }

        [Fact]
        public void RegistrarResultado_SegundaVezSobreLaMismaInscripcion_ActualizaEnVezDeDuplicar()
        {
            int idInscripcion = CrearInscripcionDePrueba();
            var organizador = TestDataHelpers.ObtenerOrganizadorSembrado(_organizadorRepo);

            Assert.True(_resultadoRepo.RegistrarResultado(idInscripcion, 100.0, organizador.IdOrganizador));
            Assert.True(_resultadoRepo.RegistrarResultado(idInscripcion, 50.0, organizador.IdOrganizador));

            var resultados = _resultadoRepo.ObtenerTodos().Where(r => r.IdInscripcion == idInscripcion).ToList();

            Assert.Single(resultados); // upsert: sigue siendo una sola fila
            Assert.Equal(50.0, resultados[0].Tiempo);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        [InlineData(7200.01)]
        public void RegistrarResultado_TiempoFueraDeRango_LanzaArgumentOutOfRange(double tiempoInvalido)
        {
            int idInscripcion = CrearInscripcionDePrueba();
            var organizador = TestDataHelpers.ObtenerOrganizadorSembrado(_organizadorRepo);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                _resultadoRepo.RegistrarResultado(idInscripcion, tiempoInvalido, organizador.IdOrganizador));
        }

        [Fact]
        public void RegistrarResultado_LimiteSuperiorExacto7200_EsAceptado()
        {
            int idInscripcion = CrearInscripcionDePrueba();
            var organizador = TestDataHelpers.ObtenerOrganizadorSembrado(_organizadorRepo);

            bool ok = _resultadoRepo.RegistrarResultado(idInscripcion, 7200.0, organizador.IdOrganizador);

            Assert.True(ok);
        }

        [Fact]
        public void RegistrarResultado_InscripcionInexistente_LanzaInvalidOperation()
        {
            var organizador = TestDataHelpers.ObtenerOrganizadorSembrado(_organizadorRepo);

            Assert.Throws<InvalidOperationException>(() =>
                _resultadoRepo.RegistrarResultado(9_999_999, 60.0, organizador.IdOrganizador));
        }

        [Fact]
        public void EliminarResultado_ExistenteSeElimina_InexistenteDevuelveFalse()
        {
            int idInscripcion = CrearInscripcionDePrueba();
            var organizador = TestDataHelpers.ObtenerOrganizadorSembrado(_organizadorRepo);
            Assert.True(_resultadoRepo.RegistrarResultado(idInscripcion, 42.0, organizador.IdOrganizador));

            int idResultado = _resultadoRepo.ObtenerTodos().First(r => r.IdInscripcion == idInscripcion).IdResultado;

            Assert.True(_resultadoRepo.EliminarResultado(idResultado));
            Assert.DoesNotContain(_resultadoRepo.ObtenerTodos(), r => r.IdResultado == idResultado);
            Assert.False(_resultadoRepo.EliminarResultado(9_999_999));
        }

        [Fact]
        public void ObtenerTodos_FiltradoPorReto_SoloDevuelveResultadosDeEseReto()
        {
            var organizador = TestDataHelpers.ObtenerOrganizadorSembrado(_organizadorRepo);

            var carrera = TestDataHelpers.CrearCarrera(_carreraRepo);
            var estudianteA = TestDataHelpers.CrearEstudiante(_estudianteRepo, carrera.IdCarrera);
            var estudianteB = TestDataHelpers.CrearEstudiante(_estudianteRepo, carrera.IdCarrera);
            var retoUno = TestDataHelpers.CrearReto(_retoRepo);
            var retoDos = TestDataHelpers.CrearReto(_retoRepo);

            Assert.True(_inscripcionRepo.InscribirEstudiante(estudianteA.IdEstudiante, retoUno.IdReto, out _));
            Assert.True(_inscripcionRepo.InscribirEstudiante(estudianteB.IdEstudiante, retoDos.IdReto, out _));

            int idInscA = _inscripcionRepo.ObtenerTodas(retoUno.IdReto).First(i => i.IdEstudiante == estudianteA.IdEstudiante).IdInscripcion;
            int idInscB = _inscripcionRepo.ObtenerTodas(retoDos.IdReto).First(i => i.IdEstudiante == estudianteB.IdEstudiante).IdInscripcion;

            Assert.True(_resultadoRepo.RegistrarResultado(idInscA, 10.0, organizador.IdOrganizador));
            Assert.True(_resultadoRepo.RegistrarResultado(idInscB, 20.0, organizador.IdOrganizador));

            var soloRetoUno = _resultadoRepo.ObtenerTodos(retoUno.IdReto);

            Assert.Contains(soloRetoUno, r => r.IdInscripcion == idInscA);
            Assert.DoesNotContain(soloRetoUno, r => r.IdInscripcion == idInscB);
        }
    }
}

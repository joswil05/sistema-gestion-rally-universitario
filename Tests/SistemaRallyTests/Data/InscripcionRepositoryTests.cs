using System.Linq;
using SistemadeGestiondeRallyUniversitario.Data;
using Xunit;

namespace SistemadeGestiondeRallyUniversitario.Tests.Data
{
    /// <summary>
    /// Pruebas de integración contra SQLite real para la regla de negocio más crítica del sistema:
    /// los cupos de inscripción (por reto+carrera y el límite global de 20 por carrera en el rally).
    /// </summary>
    [Collection("Base de datos SQLite")]
    public class InscripcionRepositoryTests
    {
        private readonly CarreraRepository _carreraRepo = new CarreraRepository();
        private readonly EstudianteRepository _estudianteRepo = new EstudianteRepository();
        private readonly RetoRepository _retoRepo = new RetoRepository();
        private readonly InscripcionRepository _inscripcionRepo = new InscripcionRepository();

        public InscripcionRepositoryTests(DatabaseFixture fixture) { _ = fixture; }

        [Fact]
        public void InscribirEstudiante_ConCupoDisponible_Exitoso()
        {
            var carrera = TestDataHelpers.CrearCarrera(_carreraRepo);
            var estudiante = TestDataHelpers.CrearEstudiante(_estudianteRepo, carrera.IdCarrera);
            var reto = TestDataHelpers.CrearReto(_retoRepo, cupoMaximoPorCarrera: 5);

            bool ok = _inscripcionRepo.InscribirEstudiante(estudiante.IdEstudiante, reto.IdReto, out string mensaje);

            Assert.True(ok, mensaje);
            Assert.Equal(string.Empty, mensaje);

            var inscripciones = _inscripcionRepo.ObtenerTodas(reto.IdReto);
            Assert.Contains(inscripciones, i => i.IdEstudiante == estudiante.IdEstudiante && i.IdReto == reto.IdReto);
        }

        [Fact]
        public void InscribirEstudiante_Duplicado_Falla()
        {
            var carrera = TestDataHelpers.CrearCarrera(_carreraRepo);
            var estudiante = TestDataHelpers.CrearEstudiante(_estudianteRepo, carrera.IdCarrera);
            var reto = TestDataHelpers.CrearReto(_retoRepo);

            Assert.True(_inscripcionRepo.InscribirEstudiante(estudiante.IdEstudiante, reto.IdReto, out _));

            bool segundoIntento = _inscripcionRepo.InscribirEstudiante(estudiante.IdEstudiante, reto.IdReto, out string mensaje);

            Assert.False(segundoIntento);
            Assert.Contains("ya se encuentra inscrito", mensaje);
        }

        [Fact]
        public void InscribirEstudiante_RetoInexistente_Falla()
        {
            var carrera = TestDataHelpers.CrearCarrera(_carreraRepo);
            var estudiante = TestDataHelpers.CrearEstudiante(_estudianteRepo, carrera.IdCarrera);

            bool ok = _inscripcionRepo.InscribirEstudiante(estudiante.IdEstudiante, 9_999_999, out string mensaje);

            Assert.False(ok);
            Assert.Contains("No se encontró el reto", mensaje);
        }

        [Fact]
        public void InscribirEstudiante_EstudianteInexistente_Falla()
        {
            var reto = TestDataHelpers.CrearReto(_retoRepo);

            bool ok = _inscripcionRepo.InscribirEstudiante(9_999_999, reto.IdReto, out string mensaje);

            Assert.False(ok);
            Assert.Contains("No se encontró el estudiante", mensaje);
        }

        [Fact]
        public void InscribirEstudiante_RespetaCupoPorCarreraDentroDeUnReto()
        {
            var carrera = TestDataHelpers.CrearCarrera(_carreraRepo);
            var reto = TestDataHelpers.CrearReto(_retoRepo, cupoMaximoPorCarrera: 2);

            var est1 = TestDataHelpers.CrearEstudiante(_estudianteRepo, carrera.IdCarrera);
            var est2 = TestDataHelpers.CrearEstudiante(_estudianteRepo, carrera.IdCarrera);
            var est3 = TestDataHelpers.CrearEstudiante(_estudianteRepo, carrera.IdCarrera);

            Assert.True(_inscripcionRepo.InscribirEstudiante(est1.IdEstudiante, reto.IdReto, out _));
            Assert.True(_inscripcionRepo.InscribirEstudiante(est2.IdEstudiante, reto.IdReto, out _));

            bool tercero = _inscripcionRepo.InscribirEstudiante(est3.IdEstudiante, reto.IdReto, out string mensaje);

            Assert.False(tercero);
            Assert.Contains("cupo máximo", mensaje);
        }

        [Fact]
        public void InscribirEstudiante_CupoPorRetoEsPorCarrera_NoGlobalAlReto()
        {
            // El cupo del reto se evalúa por carrera: una carrera distinta con cupo propio
            // no debe verse bloqueada porque otra carrera ya llenó su cupo en el mismo reto.
            var carreraLlena = TestDataHelpers.CrearCarrera(_carreraRepo);
            var otraCarrera = TestDataHelpers.CrearCarrera(_carreraRepo);
            var reto = TestDataHelpers.CrearReto(_retoRepo, cupoMaximoPorCarrera: 1);

            var estLlena = TestDataHelpers.CrearEstudiante(_estudianteRepo, carreraLlena.IdCarrera);
            var estOtra = TestDataHelpers.CrearEstudiante(_estudianteRepo, otraCarrera.IdCarrera);

            Assert.True(_inscripcionRepo.InscribirEstudiante(estLlena.IdEstudiante, reto.IdReto, out _));

            bool otraCarreraOk = _inscripcionRepo.InscribirEstudiante(estOtra.IdEstudiante, reto.IdReto, out string mensaje);

            Assert.True(otraCarreraOk, mensaje);
        }

        [Fact]
        public void InscribirEstudiante_RespetaCupoGlobalDe20PorCarreraEnElRally()
        {
            var carrera = TestDataHelpers.CrearCarrera(_carreraRepo);
            // Cupo por reto amplio a propósito para que sea el límite GLOBAL (20) el que se dispare,
            // no el límite del reto individual.
            var reto = TestDataHelpers.CrearReto(_retoRepo, cupoMaximoPorCarrera: 25);

            for (int i = 0; i < 20; i++)
            {
                var estudiante = TestDataHelpers.CrearEstudiante(_estudianteRepo, carrera.IdCarrera);
                bool ok = _inscripcionRepo.InscribirEstudiante(estudiante.IdEstudiante, reto.IdReto, out string mensaje);
                Assert.True(ok, $"Falló en el estudiante #{i + 1}: {mensaje}");
            }

            var estudiante21 = TestDataHelpers.CrearEstudiante(_estudianteRepo, carrera.IdCarrera);
            bool ok21 = _inscripcionRepo.InscribirEstudiante(estudiante21.IdEstudiante, reto.IdReto, out string mensaje21);

            Assert.False(ok21);
            Assert.Contains("cupo máximo permitido", mensaje21);
        }

        [Fact]
        public void InscribirEstudiante_UnEstudianteYaParticipanteEnElRally_NoConsumeCupoGlobalOtraVez()
        {
            var carrera = TestDataHelpers.CrearCarrera(_carreraRepo);
            var retoUno = TestDataHelpers.CrearReto(_retoRepo, cupoMaximoPorCarrera: 25);
            var retoDos = TestDataHelpers.CrearReto(_retoRepo, cupoMaximoPorCarrera: 25);

            // Llenar el cupo global de 20 con estudiantes nuevos en retoUno.
            for (int i = 0; i < 20; i++)
            {
                var estudiante = TestDataHelpers.CrearEstudiante(_estudianteRepo, carrera.IdCarrera);
                Assert.True(_inscripcionRepo.InscribirEstudiante(estudiante.IdEstudiante, retoUno.IdReto, out _));
            }

            // Un estudiante que YA es participante del rally (inscrito en retoUno) debe poder
            // inscribirse también en retoDos, porque no cuenta como "nuevo" participante de la carrera.
            var todasInscripciones = _inscripcionRepo.ObtenerTodas(retoUno.IdReto);
            int idEstudianteExistente = todasInscripciones.First().IdEstudiante;

            bool ok = _inscripcionRepo.InscribirEstudiante(idEstudianteExistente, retoDos.IdReto, out string mensaje);

            Assert.True(ok, mensaje);
        }

        [Fact]
        public void EliminarInscripcion_ExistenteSeElimina_InexistenteDevuelveFalse()
        {
            var carrera = TestDataHelpers.CrearCarrera(_carreraRepo);
            var estudiante = TestDataHelpers.CrearEstudiante(_estudianteRepo, carrera.IdCarrera);
            var reto = TestDataHelpers.CrearReto(_retoRepo);

            Assert.True(_inscripcionRepo.InscribirEstudiante(estudiante.IdEstudiante, reto.IdReto, out _));
            int idInscripcion = _inscripcionRepo.ObtenerTodas(reto.IdReto)
                .First(i => i.IdEstudiante == estudiante.IdEstudiante).IdInscripcion;

            Assert.True(_inscripcionRepo.EliminarInscripcion(idInscripcion));
            Assert.DoesNotContain(_inscripcionRepo.ObtenerTodas(reto.IdReto), i => i.IdInscripcion == idInscripcion);

            Assert.False(_inscripcionRepo.EliminarInscripcion(9_999_999));
        }
    }
}

using System.Data.SQLite;
using System.Linq;
using SistemadeGestiondeRallyUniversitario.Data;
using Xunit;

namespace SistemadeGestiondeRallyUniversitario.Tests.Data
{
    [Collection("Base de datos SQLite")]
    public class DatabaseHelperTests
    {
        private readonly OrganizadorRepository _organizadorRepo = new OrganizadorRepository();

        public DatabaseHelperTests(DatabaseFixture fixture) { _ = fixture; }

        [Fact]
        public void InicializarBaseDatos_EsIdempotente_NoDuplicaElAdminSembrado()
        {
            // El proyecto principal llama InicializarBaseDatos() en cada arranque de la app
            // (Program.cs); debe poder re-ejecutarse sin efectos secundarios acumulativos.
            DatabaseHelper.InicializarBaseDatos();
            DatabaseHelper.InicializarBaseDatos();

            int totalAdmins = _organizadorRepo.ObtenerTodos().Count(o => o.Correo == "admin@ulsa.edu");
            Assert.Equal(1, totalAdmins);
        }

        [Fact]
        public void CadenaConexion_HabilitaForeignKeys()
        {
            using (var conexion = DatabaseHelper.ObtenerConexion())
            using (var cmd = new SQLiteCommand("PRAGMA foreign_keys;", conexion))
            {
                long habilitado = (long)cmd.ExecuteScalar();
                Assert.Equal(1, habilitado);
            }
        }
    }

    /// <summary>
    /// Verifica que las claves foráneas declaradas con ON DELETE CASCADE en el esquema
    /// (Data/DatabaseHelper.cs) realmente se apliquen en tiempo de ejecución: SQLite exige
    /// "PRAGMA foreign_keys = ON" por conexión para que las cascadas se ejecuten.
    /// </summary>
    [Collection("Base de datos SQLite")]
    public class IntegridadReferencialCascadaTests
    {
        private readonly CarreraRepository _carreraRepo = new CarreraRepository();
        private readonly EstudianteRepository _estudianteRepo = new EstudianteRepository();
        private readonly RetoRepository _retoRepo = new RetoRepository();
        private readonly InscripcionRepository _inscripcionRepo = new InscripcionRepository();
        private readonly ResultadoRepository _resultadoRepo = new ResultadoRepository();
        private readonly OrganizadorRepository _organizadorRepo = new OrganizadorRepository();

        public IntegridadReferencialCascadaTests(DatabaseFixture fixture) { _ = fixture; }

        [Fact]
        public void EliminarCarrera_CascadeaHaciaSusEstudiantes()
        {
            var carrera = TestDataHelpers.CrearCarrera(_carreraRepo);
            var estudiante = TestDataHelpers.CrearEstudiante(_estudianteRepo, carrera.IdCarrera);

            Assert.True(_carreraRepo.Eliminar(carrera.IdCarrera));

            var restante = _estudianteRepo.ObtenerTodos(null, null).FirstOrDefault(e => e.IdEstudiante == estudiante.IdEstudiante);
            Assert.Null(restante);
        }

        [Fact]
        public void EliminarEstudiante_CascadeaHaciaSusInscripciones()
        {
            var carrera = TestDataHelpers.CrearCarrera(_carreraRepo);
            var estudiante = TestDataHelpers.CrearEstudiante(_estudianteRepo, carrera.IdCarrera);
            var reto = TestDataHelpers.CrearReto(_retoRepo);
            Assert.True(_inscripcionRepo.InscribirEstudiante(estudiante.IdEstudiante, reto.IdReto, out _));

            Assert.True(_estudianteRepo.Eliminar(estudiante.IdEstudiante));

            Assert.DoesNotContain(_inscripcionRepo.ObtenerTodas(reto.IdReto), i => i.IdEstudiante == estudiante.IdEstudiante);
        }

        [Fact]
        public void EliminarReto_CascadeaHaciaSusInscripciones()
        {
            var carrera = TestDataHelpers.CrearCarrera(_carreraRepo);
            var estudiante = TestDataHelpers.CrearEstudiante(_estudianteRepo, carrera.IdCarrera);
            var reto = TestDataHelpers.CrearReto(_retoRepo);
            Assert.True(_inscripcionRepo.InscribirEstudiante(estudiante.IdEstudiante, reto.IdReto, out _));

            Assert.True(_retoRepo.Eliminar(reto.IdReto));

            Assert.DoesNotContain(_inscripcionRepo.ObtenerTodas(), i => i.IdReto == reto.IdReto);
        }

        [Fact]
        public void EliminarInscripcion_CascadeaHaciaSuResultado()
        {
            var carrera = TestDataHelpers.CrearCarrera(_carreraRepo);
            var estudiante = TestDataHelpers.CrearEstudiante(_estudianteRepo, carrera.IdCarrera);
            var reto = TestDataHelpers.CrearReto(_retoRepo);
            var organizador = TestDataHelpers.ObtenerOrganizadorSembrado(_organizadorRepo);

            Assert.True(_inscripcionRepo.InscribirEstudiante(estudiante.IdEstudiante, reto.IdReto, out _));
            int idInscripcion = _inscripcionRepo.ObtenerTodas(reto.IdReto)
                .First(i => i.IdEstudiante == estudiante.IdEstudiante).IdInscripcion;
            Assert.True(_resultadoRepo.RegistrarResultado(idInscripcion, 33.3, organizador.IdOrganizador));

            Assert.True(_inscripcionRepo.EliminarInscripcion(idInscripcion));

            Assert.DoesNotContain(_resultadoRepo.ObtenerTodos(), r => r.IdInscripcion == idInscripcion);
        }
    }
}

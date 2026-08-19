using System.Linq;
using SistemadeGestiondeRallyUniversitario.Data;
using SistemadeGestiondeRallyUniversitario.Models;
using Xunit;

namespace SistemadeGestiondeRallyUniversitario.Tests.Data
{
    [Collection("Base de datos SQLite")]
    public class CarreraRepositoryTests
    {
        private readonly CarreraRepository _repo = new CarreraRepository();

        public CarreraRepositoryTests(DatabaseFixture fixture) { _ = fixture; }

        [Fact]
        public void Insertar_ObtenerPorId_Actualizar_Eliminar_CicloCompleto()
        {
            string nombre = TestDataHelpers.NombreUnico("CarreraCiclo");
            Assert.True(_repo.Insertar(new Carrera(0, nombre, "Facultad Original")));

            var creada = _repo.ObtenerTodas().First(c => c.Nombre == nombre);
            Assert.Equal("Facultad Original", creada.Facultad);

            var porId = _repo.ObtenerPorId(creada.IdCarrera);
            Assert.NotNull(porId);
            Assert.Equal(nombre, porId.Nombre);

            porId.Facultad = "Facultad Actualizada";
            Assert.True(_repo.Actualizar(porId));
            Assert.Equal("Facultad Actualizada", _repo.ObtenerPorId(creada.IdCarrera).Facultad);

            Assert.True(_repo.Eliminar(creada.IdCarrera));
            Assert.Null(_repo.ObtenerPorId(creada.IdCarrera));
        }

        [Fact]
        public void ObtenerPorId_Inexistente_DevuelveNull()
        {
            Assert.Null(_repo.ObtenerPorId(9_999_999));
        }

        [Fact]
        public void Eliminar_Inexistente_DevuelveFalse()
        {
            Assert.False(_repo.Eliminar(9_999_999));
        }
    }

    [Collection("Base de datos SQLite")]
    public class EstudianteRepositoryTests
    {
        private readonly CarreraRepository _carreraRepo = new CarreraRepository();
        private readonly EstudianteRepository _repo = new EstudianteRepository();

        public EstudianteRepositoryTests(DatabaseFixture fixture) { _ = fixture; }

        [Fact]
        public void Insertar_Actualizar_Eliminar_CicloCompleto()
        {
            var carrera = TestDataHelpers.CrearCarrera(_carreraRepo);
            string nombre = TestDataHelpers.NombreUnico("EstCiclo");

            Assert.True(_repo.Insertar(new Estudiante(0, nombre, "Apellido", "correo@ulsa.edu", carrera.IdCarrera)));
            var creado = _repo.ObtenerTodos(carrera.IdCarrera, nombre).First(e => e.Nombre == nombre);
            Assert.Equal("correo@ulsa.edu", creado.CorreoULSA);
            Assert.Equal(carrera.Nombre, creado.NombreCarrera); // NombreCarrera es el nombre de la Carrera, no la Facultad

            creado.Apellido = "ApellidoNuevo";
            Assert.True(_repo.Actualizar(creado));
            var actualizado = _repo.ObtenerTodos(carrera.IdCarrera, nombre).First(e => e.IdEstudiante == creado.IdEstudiante);
            Assert.Equal("ApellidoNuevo", actualizado.Apellido);

            Assert.True(_repo.Eliminar(creado.IdEstudiante));
            Assert.DoesNotContain(_repo.ObtenerTodos(carrera.IdCarrera, nombre), e => e.IdEstudiante == creado.IdEstudiante);
        }

        [Fact]
        public void ObtenerTodos_BusquedaPorNombreApellidoOCorreo_UsaLike()
        {
            var carrera = TestDataHelpers.CrearCarrera(_carreraRepo);
            string nombre = TestDataHelpers.NombreUnico("Buscable");
            Assert.True(_repo.Insertar(new Estudiante(0, nombre, "ApellidoRaroXYZ", "correo.raro@ulsa.edu", carrera.IdCarrera)));

            Assert.NotEmpty(_repo.ObtenerTodos(null, nombre));
            Assert.NotEmpty(_repo.ObtenerTodos(null, "ApellidoRaroXYZ"));
            Assert.NotEmpty(_repo.ObtenerTodos(null, "correo.raro"));
            Assert.Empty(_repo.ObtenerTodos(null, "ExpresionQueNoDeberiaCoincidirNunca_ZZZ"));
        }

        [Fact]
        public void ObtenerTodos_FiltroPorCarrera_ExcluyeOtrasCarreras()
        {
            var carreraA = TestDataHelpers.CrearCarrera(_carreraRepo);
            var carreraB = TestDataHelpers.CrearCarrera(_carreraRepo);
            var estudianteA = TestDataHelpers.CrearEstudiante(_repo, carreraA.IdCarrera);
            TestDataHelpers.CrearEstudiante(_repo, carreraB.IdCarrera);

            var resultado = _repo.ObtenerTodos(carreraA.IdCarrera, null);

            Assert.Contains(resultado, e => e.IdEstudiante == estudianteA.IdEstudiante);
            Assert.All(resultado, e => Assert.Equal(carreraA.IdCarrera, e.IdCarrera));
        }
    }

    [Collection("Base de datos SQLite")]
    public class RetoRepositoryTests
    {
        private readonly RetoRepository _repo = new RetoRepository();

        public RetoRepositoryTests(DatabaseFixture fixture) { _ = fixture; }

        [Fact]
        public void Insertar_Actualizar_Eliminar_CicloCompleto()
        {
            string nombre = TestDataHelpers.NombreUnico("RetoCiclo");
            Assert.True(_repo.Insertar(new Reto(0, nombre, "Desc", 8, null)));

            var creado = _repo.ObtenerTodos().First(r => r.Nombre == nombre);
            Assert.Equal(8, creado.CupoMaximoPorCarrera);
            // Regresión: NombreOrganizador debía ser "No asignado" cuando idOrganizador es NULL,
            // pero por una confusión entre DBNull y null de C# en el mapeo del reader (RetoRepository.cs)
            // quedaba en "" — corregido; esta prueba evita que el error vuelva a colarse.
            Assert.Equal("No asignado", creado.NombreOrganizador);

            creado.CupoMaximoPorCarrera = 15;
            Assert.True(_repo.Actualizar(creado));
            Assert.Equal(15, _repo.ObtenerTodos().First(r => r.IdReto == creado.IdReto).CupoMaximoPorCarrera);

            Assert.True(_repo.Eliminar(creado.IdReto));
            Assert.DoesNotContain(_repo.ObtenerTodos(), r => r.IdReto == creado.IdReto);
        }

        [Fact]
        public void Insertar_ConCupoDefaultDelConstructorSinEspecificar_SePersisteComoCinco()
        {
            // El default de 5 sigue existiendo (constructor sin parámetros / new Reto { ... }
            // sin tocar CupoMaximoPorCarrera), pero ya NO se fuerza sobre un valor <=0 explícito:
            // el repositorio ahora persiste tal cual lo que traiga el modelo (ver
            // Insertar_CupoInvalido_SePersisteTalCual_ValidarEsQuienDebeRechazarloAntes).
            string nombre = TestDataHelpers.NombreUnico("RetoCupoDefault");
            var reto = new Reto { Nombre = nombre, Descripcion = "", IdOrganizador = null };
            Assert.Equal(5, reto.CupoMaximoPorCarrera);

            Assert.True(_repo.Insertar(reto));

            var creado = _repo.ObtenerTodos().First(r => r.Nombre == nombre);
            Assert.Equal(5, creado.CupoMaximoPorCarrera);
        }

        [Fact]
        public void Insertar_CupoInvalido_SePersisteTalCual_ValidarEsQuienDebeRechazarloAntes()
        {
            // El repositorio confía en que el llamador ya invocó reto.Validar() (como hace
            // FormEditorReto) antes de guardar; ya no "corrige" un cupo <= 0 a 5 por su cuenta.
            string nombre = TestDataHelpers.NombreUnico("RetoCupoInvalido");
            var reto = new Reto { Nombre = nombre, Descripcion = "", IdOrganizador = null, CupoMaximoPorCarrera = 0 };
            Assert.False(reto.Validar(out _)); // Validar() sí detecta el problema

            Assert.True(_repo.Insertar(reto)); // el repositorio no re-valida, inserta lo que le den

            var creado = _repo.ObtenerTodos().First(r => r.Nombre == nombre);
            Assert.Equal(0, creado.CupoMaximoPorCarrera);
        }
    }

    [Collection("Base de datos SQLite")]
    public class OrganizadorRepositoryTests
    {
        private readonly OrganizadorRepository _repo = new OrganizadorRepository();

        public OrganizadorRepositoryTests(DatabaseFixture fixture) { _ = fixture; }

        [Fact]
        public void Autenticar_ConCorreoYContrasenaSembradosPorDefecto_Exitoso()
        {
            var organizador = _repo.Autenticar("admin@ulsa.edu", "1234");
            Assert.NotNull(organizador);
            Assert.Equal("Admin", organizador.Nombre);
        }

        [Fact]
        public void Autenticar_ConNombreDeUsuario_EsInsensibleAMayusculas()
        {
            var organizador = _repo.Autenticar("aDmIn", "1234");
            Assert.NotNull(organizador);
        }

        [Fact]
        public void Autenticar_ContrasenaIncorrecta_DevuelveNull()
        {
            Assert.Null(_repo.Autenticar("admin@ulsa.edu", "clave-incorrecta"));
        }

        [Fact]
        public void Autenticar_UsuarioInexistente_DevuelveNull()
        {
            Assert.Null(_repo.Autenticar("no-existe@ulsa.edu", "1234"));
        }

        [Theory]
        [InlineData(null, "1234")]
        [InlineData("admin@ulsa.edu", null)]
        [InlineData("", "1234")]
        [InlineData("admin@ulsa.edu", "")]
        public void Autenticar_EntradasVaciasONulas_DevuelveNullSinConsultarLaBaseDeDatos(string usuario, string clave)
        {
            Assert.Null(_repo.Autenticar(usuario, clave));
        }
    }
}

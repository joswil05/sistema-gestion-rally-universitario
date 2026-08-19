using SistemadeGestiondeRallyUniversitario.Models;
using SistemadeGestiondeRallyUniversitario.Services;
using Xunit;

namespace SistemadeGestiondeRallyUniversitario.Tests
{
    public class EstudianteValidacionTests
    {
        private static Estudiante EstudianteValido()
        {
            return new Estudiante(0, "Juan", "Perez", "juan.perez@ulsa.edu.gt", 1, "Ingenieria");
        }

        [Fact]
        public void Estudiante_DatosCompletos_EsValido()
        {
            var e = EstudianteValido();
            bool ok = e.Validar(out string msg);
            Assert.True(ok);
            Assert.Equal(string.Empty, msg);
        }

        [Fact]
        public void Estudiante_CorreoVacio_EsValido_PorSerOpcional()
        {
            var e = EstudianteValido();
            e.CorreoULSA = "";
            Assert.True(e.Validar(out _));
        }

        [Fact]
        public void Estudiante_NombreVacio_EsInvalido()
        {
            var e = EstudianteValido();
            e.Nombre = "";
            bool ok = e.Validar(out string msg);
            Assert.False(ok);
            Assert.Contains("Nombre", msg);
        }

        [Fact]
        public void Estudiante_ApellidoDeUnCaracter_EsInvalido()
        {
            var e = EstudianteValido();
            e.Apellido = "P";
            bool ok = e.Validar(out string msg);
            Assert.False(ok);
            Assert.Contains("al menos 2 caracteres", msg);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Estudiante_IdCarreraNoPositivo_EsInvalido(int idCarrera)
        {
            var e = EstudianteValido();
            e.IdCarrera = idCarrera;
            bool ok = e.Validar(out string msg);
            Assert.False(ok);
            Assert.Contains("carrera válida", msg);
        }

        [Fact]
        public void Estudiante_CorreoConFormatoInvalido_EsInvalido()
        {
            var e = EstudianteValido();
            e.CorreoULSA = "esto-no-es-un-correo";
            bool ok = e.Validar(out string msg);
            Assert.False(ok);
            Assert.Contains("correo", msg.ToLowerInvariant());
        }

        [Fact]
        public void Estudiante_NombreYApellido_SeRecortanAutomaticamente()
        {
            var e = new Estudiante(0, "  Ana  ", "  Ruiz  ", "", 1);
            Assert.Equal("Ana", e.Nombre);
            Assert.Equal("Ruiz", e.Apellido);
            Assert.Equal("Ana Ruiz", e.NombreCompleto);
        }

        [Fact]
        public void Estudiante_ObtenerRol_DevuelveEstudiante()
        {
            Assert.Equal("Estudiante", EstudianteValido().ObtenerRol());
        }

        [Fact]
        public void Estudiante_ObtenerIdentificadorPrincipal_DevuelveCorreo()
        {
            var e = EstudianteValido();
            Assert.Equal(e.CorreoULSA, e.ObtenerIdentificadorPrincipal());
        }
    }

    public class OrganizadorValidacionTests
    {
        private static Organizador OrganizadorValido()
        {
            return new Organizador(0, "Maria", "Gomez", "maria@ulsa.edu", "clave123");
        }

        [Fact]
        public void Organizador_DatosCompletos_EsValido()
        {
            Assert.True(OrganizadorValido().Validar(out _));
        }

        [Fact]
        public void Organizador_SinCorreo_EsInvalido()
        {
            var o = OrganizadorValido();
            o.Correo = "";
            bool ok = o.Validar(out string msg);
            Assert.False(ok);
            Assert.Contains("correo del organizador", msg);
        }

        [Fact]
        public void Organizador_SinContrasena_EsInvalido()
        {
            var o = OrganizadorValido();
            o.Contrasena = "";
            bool ok = o.Validar(out string msg);
            Assert.False(ok);
            Assert.Contains("contraseña del organizador", msg);
        }

        [Fact]
        public void Organizador_SinNombre_EsInvalido_ViaValidacionBase()
        {
            var o = OrganizadorValido();
            o.Nombre = "";
            bool ok = o.Validar(out string msg);
            Assert.False(ok);
            Assert.Contains("Nombre", msg);
        }

        [Fact]
        public void Organizador_ObtenerRol_DevuelveOrganizador()
        {
            Assert.Equal("Organizador", OrganizadorValido().ObtenerRol());
        }
    }

    public class CarreraValidacionTests
    {
        [Fact]
        public void Carrera_DatosCompletos_EsValida()
        {
            var c = new Carrera(0, "Ingenieria en Sistemas", "Facultad de Ingenieria");
            Assert.True(c.Validar(out _));
        }

        [Fact]
        public void Carrera_NombreVacio_EsInvalida()
        {
            var c = new Carrera(0, "", "Facultad X");
            bool ok = c.Validar(out string msg);
            Assert.False(ok);
            Assert.Contains("Nombre de Carrera", msg);
        }

        [Fact]
        public void Carrera_FacultadVacia_EsInvalida()
        {
            var c = new Carrera(0, "Medicina", "");
            bool ok = c.Validar(out string msg);
            Assert.False(ok);
            Assert.Contains("Facultad", msg);
        }

        [Fact]
        public void Carrera_NombreSeRecortaEnElSetter()
        {
            var c = new Carrera(0, "  Derecho  ", "  Facultad de Ciencias Juridicas  ");
            Assert.Equal("Derecho", c.Nombre);
            Assert.Equal("Facultad de Ciencias Juridicas", c.Facultad);
        }
    }

    public class RetoValidacionTests
    {
        [Fact]
        public void Reto_DatosCompletos_EsValido()
        {
            var r = new Reto(0, "Carrera de Obstaculos", "Descripcion", 10, null);
            Assert.True(r.Validar(out _));
        }

        [Fact]
        public void Reto_NombreVacio_EsInvalido()
        {
            var r = new Reto(0, "", "Descripcion", 10, null);
            bool ok = r.Validar(out string msg);
            Assert.False(ok);
            Assert.Contains("Nombre del Reto", msg);
        }

        [Fact]
        public void Reto_CupoEnLimiteMaximo20_EsValido()
        {
            var r = new Reto(0, "Reto Grande", "", 20, null);
            Assert.True(r.Validar(out _));
        }

        [Fact]
        public void Reto_CupoMayorA20_EsInvalido()
        {
            var r = new Reto(0, "Reto Sobre Cupo", "", 21, null);
            bool ok = r.Validar(out string msg);
            Assert.False(ok);
            Assert.Contains("20", msg);
        }

        [Fact]
        public void Reto_CupoCero_YaNoSeSustituyePorCinco_YValidarLoRechaza()
        {
            // El setter conserva el valor tal cual (ya no lo "corrige" en silencio a 5),
            // así que Validar() ahora sí puede detectar y rechazar un cupo inválido:
            // antes esta rama de ValidacionService.ValidarCupo era inalcanzable pasando
            // por el modelo Reto.
            var r = new Reto(0, "Reto Cupo Cero", "", 0, null);
            Assert.Equal(0, r.CupoMaximoPorCarrera);

            bool ok = r.Validar(out string msg);
            Assert.False(ok);
            Assert.Contains("al menos 1", msg);
        }

        [Fact]
        public void Reto_CupoNegativo_YaNoSeSustituyePorCinco_YValidarLoRechaza()
        {
            var r = new Reto(0, "Reto Cupo Negativo", "", -7, null);
            Assert.Equal(-7, r.CupoMaximoPorCarrera);

            bool ok = r.Validar(out string msg);
            Assert.False(ok);
            Assert.Contains("al menos 1", msg);
        }

        [Fact]
        public void Reto_SinOrganizadorAsignado_UsaTextoPorDefecto()
        {
            var r = new Reto();
            Assert.Equal("No asignado", r.NombreOrganizador);
        }

        [Fact]
        public void Reto_ConstructorPorDefecto_CupoEsCinco()
        {
            var r = new Reto();
            Assert.Equal(5, r.CupoMaximoPorCarrera);
        }
    }
}

using System;
using System.Linq;
using SistemadeGestiondeRallyUniversitario.Data;
using SistemadeGestiondeRallyUniversitario.Models;
using Xunit;

namespace SistemadeGestiondeRallyUniversitario.Tests.Data
{
    /// <summary>
    /// Fábricas de datos de prueba con nombres únicos (sufijo GUID) para que cada test que
    /// corre sobre la base SQLite compartida quede aislado de los demás sin necesidad de
    /// truncar tablas entre pruebas.
    /// </summary>
    internal static class TestDataHelpers
    {
        public static string NombreUnico(string prefijo) => prefijo + "_" + Guid.NewGuid().ToString("N").Substring(0, 8);

        public static Carrera CrearCarrera(CarreraRepository repo, string prefijo = "CarreraTest")
        {
            string nombre = NombreUnico(prefijo);
            bool insertado = repo.Insertar(new Carrera(0, nombre, "Facultad de Pruebas"));
            Assert.True(insertado, "No se pudo insertar la carrera de prueba.");
            return repo.ObtenerTodas().First(c => c.Nombre == nombre);
        }

        public static Estudiante CrearEstudiante(EstudianteRepository repo, int idCarrera, string prefijo = "EstudianteTest")
        {
            string nombre = NombreUnico(prefijo);
            bool insertado = repo.Insertar(new Estudiante(0, nombre, "ApellidoTest", "", idCarrera));
            Assert.True(insertado, "No se pudo insertar el estudiante de prueba.");
            return repo.ObtenerTodos(idCarrera, nombre).First(e => e.Nombre == nombre);
        }

        public static Reto CrearReto(RetoRepository repo, int cupoMaximoPorCarrera = 5, string prefijo = "RetoTest")
        {
            string nombre = NombreUnico(prefijo);
            bool insertado = repo.Insertar(new Reto(0, nombre, "Descripcion de prueba", cupoMaximoPorCarrera, null));
            Assert.True(insertado, "No se pudo insertar el reto de prueba.");
            return repo.ObtenerTodos().First(r => r.Nombre == nombre);
        }

        public static Organizador ObtenerOrganizadorSembrado(OrganizadorRepository repo)
        {
            var organizador = repo.ObtenerTodos().FirstOrDefault();
            Assert.NotNull(organizador); // DatabaseHelper.InicializarBaseDatos siempre siembra un admin por defecto
            return organizador;
        }
    }
}

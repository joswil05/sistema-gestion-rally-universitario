using System;
using System.IO;
using SistemadeGestiondeRallyUniversitario.Data;
using Xunit;

namespace SistemadeGestiondeRallyUniversitario.Tests.Data
{
    /// <summary>
    /// Fixture compartido para todas las pruebas de integración contra SQLite real.
    /// Crea una base de datos limpia (aislada de rally_ulsa.db del proyecto principal, ya que
    /// DatabaseHelper resuelve la ruta relativa al directorio de salida de ESTE proyecto de pruebas)
    /// una única vez por ejecución y aplica el mismo esquema/migraciones que usa la app real.
    /// </summary>
    public class DatabaseFixture : IDisposable
    {
        public string RutaBaseDatos { get; }

        public DatabaseFixture()
        {
            RutaBaseDatos = DatabaseHelper.ObtenerRutaBaseDatos();

            BorrarSiExiste(RutaBaseDatos);
            BorrarSiExiste(RutaBaseDatos + "-wal");
            BorrarSiExiste(RutaBaseDatos + "-shm");

            DatabaseHelper.InicializarBaseDatos();
        }

        private static void BorrarSiExiste(string ruta)
        {
            try
            {
                if (File.Exists(ruta)) File.Delete(ruta);
            }
            catch
            {
                // Best-effort: si el archivo no se puede borrar (bloqueado), las pruebas igual
                // seguirán funcionando sobre datos únicos generados con GUID, sin colisionar.
            }
        }

        public void Dispose() { }
    }

    /// <summary>
    /// Todas las clases de prueba marcadas con [Collection("Base de datos SQLite")] comparten
    /// la misma instancia de DatabaseFixture y se ejecutan EN SECUENCIA (xUnit no paraleliza
    /// dentro de una misma colección), evitando así contención sobre el único archivo .db.
    /// </summary>
    [CollectionDefinition("Base de datos SQLite")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
    }
}

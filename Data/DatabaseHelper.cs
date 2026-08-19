using System;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace SistemadeGestiondeRallyUniversitario.Data
{
    public static class DatabaseHelper
    {
        private static string _rutaBaseDatos;

        public static string ObtenerRutaBaseDatos()
        {
            if (!string.IsNullOrEmpty(_rutaBaseDatos) && File.Exists(_rutaBaseDatos))
                return _rutaBaseDatos;

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string nombreDb = "rally_ulsa.db";

            // Buscar en bin/Debug o carpeta del ejecutable
            string ruta1 = Path.Combine(baseDir, nombreDb);
            if (File.Exists(ruta1))
            {
                _rutaBaseDatos = ruta1;
                return _rutaBaseDatos;
            }

            // Buscar en la raíz del proyecto
            string ruta2 = Path.Combine(baseDir, "..", "..", nombreDb);
            if (File.Exists(ruta2))
            {
                _rutaBaseDatos = Path.GetFullPath(ruta2);
                return _rutaBaseDatos;
            }

            // Buscar en el directorio actual de trabajo
            string ruta3 = Path.Combine(Directory.GetCurrentDirectory(), nombreDb);
            if (File.Exists(ruta3))
            {
                _rutaBaseDatos = Path.GetFullPath(ruta3);
                return _rutaBaseDatos;
            }

            // Por defecto, usar en el directorio de ejecución
            _rutaBaseDatos = ruta1;
            return _rutaBaseDatos;
        }

        public static string CadenaConexion => $"Data Source={ObtenerRutaBaseDatos()};Version=3;Foreign Keys=True;BusyTimeout=5000;";

        public static SQLiteConnection ObtenerConexion()
        {
            SQLiteConnection conexion = new SQLiteConnection(CadenaConexion);
            conexion.Open();
            return conexion;
        }

        /// <summary>
        /// Inicializa y repara el esquema de la base de datos si existen claves foráneas corruptas o tablas faltantes.
        /// </summary>
        public static void InicializarBaseDatos()
        {
            using (var conexion = ObtenerConexion())
            {
                // Habilitar foreign keys y wal mode
                using (var cmdPragma = new SQLiteCommand("PRAGMA foreign_keys = ON; PRAGMA journal_mode = WAL;", conexion))
                {
                    cmdPragma.ExecuteNonQuery();
                }

                // Crear tablas si no existen
                string ddl = @"
                    CREATE TABLE IF NOT EXISTS Carrera (
                        idCarrera INTEGER PRIMARY KEY AUTOINCREMENT,
                        nombre TEXT NOT NULL,
                        facultad TEXT NOT NULL
                    );

                    CREATE TABLE IF NOT EXISTS Estudiante (
                        idEstudiante INTEGER PRIMARY KEY AUTOINCREMENT,
                        nombre TEXT NOT NULL,
                        apellido TEXT NOT NULL,
                        correoULSA TEXT,
                        idCarrera INTEGER NOT NULL,
                        FOREIGN KEY(idCarrera) REFERENCES Carrera(idCarrera) ON DELETE CASCADE
                    );

                    CREATE TABLE IF NOT EXISTS Organizador (
                        idOrganizador INTEGER PRIMARY KEY AUTOINCREMENT,
                        nombre TEXT NOT NULL,
                        apellido TEXT NOT NULL,
                        correo TEXT NOT NULL UNIQUE,
                        contrasena TEXT NOT NULL
                    );

                    CREATE TABLE IF NOT EXISTS Reto (
                        idReto INTEGER PRIMARY KEY AUTOINCREMENT,
                        nombre TEXT NOT NULL,
                        descripcion TEXT,
                        cupoMaximoPorCarrera INTEGER DEFAULT 5,
                        idOrganizador INTEGER,
                        FOREIGN KEY(idOrganizador) REFERENCES Organizador(idOrganizador) ON DELETE SET NULL
                    );

                    CREATE TABLE IF NOT EXISTS Inscripcion (
                        idInscripcion INTEGER PRIMARY KEY AUTOINCREMENT,
                        fechaInscripcion TEXT NOT NULL,
                        idEstudiante INTEGER NOT NULL,
                        idReto INTEGER NOT NULL,
                        FOREIGN KEY(idEstudiante) REFERENCES Estudiante(idEstudiante) ON DELETE CASCADE,
                        FOREIGN KEY(idReto) REFERENCES Reto(idReto) ON DELETE CASCADE,
                        UNIQUE(idEstudiante, idReto)
                    );

                    CREATE TABLE IF NOT EXISTS Resultado (
                        idResultado INTEGER PRIMARY KEY AUTOINCREMENT,
                        tiempo REAL NOT NULL,
                        fechaRegistro TEXT NOT NULL,
                        idInscripcion INTEGER NOT NULL,
                        idOrganizador INTEGER NOT NULL,
                        FOREIGN KEY(idInscripcion) REFERENCES Inscripcion(idInscripcion) ON DELETE CASCADE,
                        FOREIGN KEY(idOrganizador) REFERENCES Organizador(idOrganizador)
                    );
                ";

                using (var cmd = new SQLiteCommand(ddl, conexion))
                {
                    cmd.ExecuteNonQuery();
                }

                // Verificar si hay organizadores iniciales, de lo contrario sembrar organizador por defecto
                using (var cmdCheck = new SQLiteCommand("SELECT COUNT(*) FROM Organizador;", conexion))
                {
                    long count = (long)cmdCheck.ExecuteScalar();
                    if (count == 0)
                    {
                        using (var cmdInsert = new SQLiteCommand(@"
                            INSERT INTO Organizador (nombre, apellido, correo, contrasena) 
                            VALUES ('Admin', 'LaSalle', 'admin@ulsa.edu', '1234');", conexion))
                        {
                            cmdInsert.ExecuteNonQuery();
                        }
                    }
                }

                // ── Migraciones aditivas (seguras para re-ejecución) ──

                // 1. UNIQUE en Resultado.idInscripcion — evita resultados duplicados por inscripción
                string migUniqueResultado = @"
                    CREATE UNIQUE INDEX IF NOT EXISTS IX_Resultado_idInscripcion 
                    ON Resultado(idInscripcion);";
                try
                {
                    using (var cmdMig = new SQLiteCommand(migUniqueResultado, conexion))
                    {
                        cmdMig.ExecuteNonQuery();
                    }
                }
                catch (SQLiteException)
                {
                    // El índice ya existe o hay datos duplicados previos — continuar
                }

                // 2. Índice compuesto para acelerar consultas de inscripciones por reto+carrera
                string migIdxInscripcion = @"
                    CREATE INDEX IF NOT EXISTS IX_Inscripcion_Reto_Estudiante 
                    ON Inscripcion(idReto, idEstudiante);";
                using (var cmdIdx = new SQLiteCommand(migIdxInscripcion, conexion))
                {
                    cmdIdx.ExecuteNonQuery();
                }

                // 3. Índice para acelerar clasificación (joins frecuentes)
                string migIdxResultadoInscripcion = @"
                    CREATE INDEX IF NOT EXISTS IX_Resultado_Inscripcion 
                    ON Resultado(idInscripcion, tiempo);";
                using (var cmdIdx2 = new SQLiteCommand(migIdxResultadoInscripcion, conexion))
                {
                    cmdIdx2.ExecuteNonQuery();
                }
            }
        }
    }
}

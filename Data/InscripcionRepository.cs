using System;
using System.Collections.Generic;
using System.Data.SQLite;
using SistemadeGestiondeRallyUniversitario.Models;
using SistemadeGestiondeRallyUniversitario.Services;

namespace SistemadeGestiondeRallyUniversitario.Data
{
    public class InscripcionRepository
    {
        public List<Inscripcion> ObtenerTodas(int? idRetoFiltro = null)
        {
            var lista = new List<Inscripcion>();
            using (var conexion = DatabaseHelper.ObtenerConexion())
            {
                string sql = @"
                    SELECT i.idInscripcion, i.fechaInscripcion, i.idEstudiante, i.idReto,
                           e.nombre || ' ' || e.apellido AS nombreEstudiante,
                           c.nombre AS nombreCarrera,
                           r.nombre AS nombreReto
                    FROM Inscripcion i
                    JOIN Estudiante e ON i.idEstudiante = e.idEstudiante
                    JOIN Carrera c ON e.idCarrera = c.idCarrera
                    JOIN Reto r ON i.idReto = r.idReto
                    WHERE 1=1 ";

                if (idRetoFiltro.HasValue && idRetoFiltro.Value > 0)
                {
                    sql += " AND i.idReto = @idReto ";
                }

                sql += " ORDER BY r.nombre, c.nombre, e.nombre;";

                using (var cmd = new SQLiteCommand(sql, conexion))
                {
                    if (idRetoFiltro.HasValue && idRetoFiltro.Value > 0)
                    {
                        cmd.Parameters.AddWithValue("@idReto", idRetoFiltro.Value);
                    }

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Inscripcion
                            {
                                IdInscripcion = Convert.ToInt32(reader["idInscripcion"]),
                                FechaInscripcion = reader["fechaInscripcion"]?.ToString() ?? "",
                                IdEstudiante = Convert.ToInt32(reader["idEstudiante"]),
                                IdReto = Convert.ToInt32(reader["idReto"]),
                                NombreEstudiante = reader["nombreEstudiante"]?.ToString() ?? "",
                                NombreCarrera = reader["nombreCarrera"]?.ToString() ?? "",
                                NombreReto = reader["nombreReto"]?.ToString() ?? ""
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public bool InscribirEstudiante(int idEstudiante, int idReto, out string mensajeError)
        {
            mensajeError = string.Empty;

            using (var conexion = DatabaseHelper.ObtenerConexion())
            using (var transaccion = conexion.BeginTransaction(System.Data.IsolationLevel.Serializable))
            {
                try
                {
                    // 1. Obtener idCarrera del estudiante
                    int idCarrera = 0;
                    string sqlCarrera = "SELECT idCarrera FROM Estudiante WHERE idEstudiante = @idEstudiante;";
                    using (var cmdCarrera = new SQLiteCommand(sqlCarrera, conexion, transaccion))
                    {
                        cmdCarrera.Parameters.AddWithValue("@idEstudiante", idEstudiante);
                        object resultCarrera = cmdCarrera.ExecuteScalar();
                        if (resultCarrera == null || resultCarrera == DBNull.Value)
                        {
                            mensajeError = "No se encontró el estudiante especificado.";
                            transaccion.Rollback();
                            return false;
                        }
                        idCarrera = Convert.ToInt32(resultCarrera);
                    }

                    // 2. Verificar si ya está inscrito en este reto
                    string sqlCheck = "SELECT COUNT(*) FROM Inscripcion WHERE idEstudiante = @idEstudiante AND idReto = @idReto;";
                    using (var cmdCheck = new SQLiteCommand(sqlCheck, conexion, transaccion))
                    {
                        cmdCheck.Parameters.AddWithValue("@idEstudiante", idEstudiante);
                        cmdCheck.Parameters.AddWithValue("@idReto", idReto);
                        long count = (long)cmdCheck.ExecuteScalar();
                        if (count > 0)
                        {
                            mensajeError = "El estudiante ya se encuentra inscrito en este reto.";
                            transaccion.Rollback();
                            return false;
                        }
                    }

                    // 3. Regla de Negocio: Restricción de cupo global por carrera (máximo 20 estudiantes únicos en el rally)
                    string sqlParticipacionesPrevias = "SELECT COUNT(*) FROM Inscripcion WHERE idEstudiante = @idEstudiante;";
                    using (var cmdPrevias = new SQLiteCommand(sqlParticipacionesPrevias, conexion, transaccion))
                    {
                        cmdPrevias.Parameters.AddWithValue("@idEstudiante", idEstudiante);
                        long participaciones = (long)cmdPrevias.ExecuteScalar();

                        // Si es un nuevo participante para la carrera en el rally, verificar que no supere 20 integrantes
                        if (participaciones == 0)
                        {
                            string sqlCupoGlobal = @"
                                SELECT COUNT(DISTINCT i.idEstudiante) 
                                FROM Inscripcion i
                                JOIN Estudiante e ON i.idEstudiante = e.idEstudiante
                                WHERE e.idCarrera = @idCarrera;";

                            using (var cmdGlobal = new SQLiteCommand(sqlCupoGlobal, conexion, transaccion))
                            {
                                cmdGlobal.Parameters.AddWithValue("@idCarrera", idCarrera);
                                long estudiantesInscritosEnRally = (long)cmdGlobal.ExecuteScalar();

                                if (estudiantesInscritosEnRally >= ValidacionService.MAXIMO_ESTUDIANTES_POR_CARRERA_RALLY)
                                {
                                    mensajeError = $"La carrera del estudiante ya alcanzó el cupo máximo permitido ({ValidacionService.MAXIMO_ESTUDIANTES_POR_CARRERA_RALLY} participantes) en el rally general.";
                                    transaccion.Rollback();
                                    return false;
                                }
                            }
                        }
                    }

                    // 4. Verificar cupo máximo de la carrera en este reto específico
                    string sqlCupo = @"
                        SELECT r.cupoMaximoPorCarrera, 
                               (SELECT COUNT(*) 
                                FROM Inscripcion i2 
                                JOIN Estudiante e2 ON i2.idEstudiante = e2.idEstudiante 
                                WHERE i2.idReto = r.idReto 
                                  AND e2.idCarrera = @idCarrera
                               ) AS inscritosCarrera
                        FROM Reto r
                        WHERE r.idReto = @idReto;";

                    using (var cmdCupo = new SQLiteCommand(sqlCupo, conexion, transaccion))
                    {
                        cmdCupo.Parameters.AddWithValue("@idCarrera", idCarrera);
                        cmdCupo.Parameters.AddWithValue("@idReto", idReto);

                        using (var reader = cmdCupo.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int cupoMax = reader["cupoMaximoPorCarrera"] != DBNull.Value
                                    ? Convert.ToInt32(reader["cupoMaximoPorCarrera"]) : 5;
                                int inscritos = Convert.ToInt32(reader["inscritosCarrera"]);

                                if (inscritos >= cupoMax)
                                {
                                    mensajeError = $"Se ha alcanzado el cupo máximo ({cupoMax}) para la carrera de este estudiante en este reto.";
                                    transaccion.Rollback();
                                    return false;
                                }
                            }
                            else
                            {
                                mensajeError = "No se encontró el reto especificado.";
                                transaccion.Rollback();
                                return false;
                            }
                        }
                    }

                    // 5. Insertar la inscripción (protegida por la transacción)
                    string sqlInsert = "INSERT INTO Inscripcion (fechaInscripcion, idEstudiante, idReto) VALUES (@fecha, @idEstudiante, @idReto);";
                    using (var cmdInsert = new SQLiteCommand(sqlInsert, conexion, transaccion))
                    {
                        cmdInsert.Parameters.AddWithValue("@fecha", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmdInsert.Parameters.AddWithValue("@idEstudiante", idEstudiante);
                        cmdInsert.Parameters.AddWithValue("@idReto", idReto);
                        int filas = cmdInsert.ExecuteNonQuery();

                        if (filas > 0)
                        {
                            transaccion.Commit();
                            return true;
                        }
                        else
                        {
                            mensajeError = "No se pudo registrar la inscripción.";
                            transaccion.Rollback();
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    try { transaccion.Rollback(); } catch { }
                    mensajeError = "Error durante la inscripción: " + ex.Message;
                    return false;
                }
            }
        }

        public bool EliminarInscripcion(int idInscripcion)
        {
            using (var conexion = DatabaseHelper.ObtenerConexion())
            {
                string sql = "DELETE FROM Inscripcion WHERE idInscripcion = @idInscripcion;";
                using (var cmd = new SQLiteCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue("@idInscripcion", idInscripcion);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}

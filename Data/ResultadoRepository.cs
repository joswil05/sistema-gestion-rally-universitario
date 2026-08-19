using System;
using System.Collections.Generic;
using System.Data.SQLite;
using SistemadeGestiondeRallyUniversitario.Models;

namespace SistemadeGestiondeRallyUniversitario.Data
{
    public class ResultadoRepository
    {
        public List<Resultado> ObtenerTodos(int? idRetoFiltro = null)
        {
            var lista = new List<Resultado>();
            using (var conexion = DatabaseHelper.ObtenerConexion())
            {
                string sql = @"
                    SELECT res.idResultado, res.tiempo, res.fechaRegistro, res.idInscripcion, res.idOrganizador,
                           e.nombre || ' ' || e.apellido AS nombreEstudiante,
                           c.nombre AS nombreCarrera,
                           r.nombre AS nombreReto,
                           o.nombre || ' ' || o.apellido AS nombreOrganizador
                    FROM Resultado res
                    JOIN Inscripcion i ON res.idInscripcion = i.idInscripcion
                    JOIN Estudiante e ON i.idEstudiante = e.idEstudiante
                    JOIN Carrera c ON e.idCarrera = c.idCarrera
                    JOIN Reto r ON i.idReto = r.idReto
                    LEFT JOIN Organizador o ON res.idOrganizador = o.idOrganizador
                    WHERE 1=1 ";

                if (idRetoFiltro.HasValue && idRetoFiltro.Value > 0)
                {
                    sql += " AND r.idReto = @idReto ";
                }

                sql += " ORDER BY res.tiempo ASC, res.fechaRegistro DESC;";

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
                            lista.Add(new Resultado
                            {
                                IdResultado = Convert.ToInt32(reader["idResultado"]),
                                Tiempo = Convert.ToDouble(reader["tiempo"]),
                                FechaRegistro = reader["fechaRegistro"]?.ToString() ?? "",
                                IdInscripcion = Convert.ToInt32(reader["idInscripcion"]),
                                IdOrganizador = reader["idOrganizador"] != DBNull.Value ? Convert.ToInt32(reader["idOrganizador"]) : 0,
                                NombreEstudiante = reader["nombreEstudiante"]?.ToString() ?? "",
                                NombreCarrera = reader["nombreCarrera"]?.ToString() ?? "",
                                NombreReto = reader["nombreReto"]?.ToString() ?? "",
                                // Mismo cuidado que en RetoRepository: DBNull.Value no es un null de C#,
                                // así que `?.ToString() ?? "Sistema"` nunca cae al valor por defecto.
                                NombreOrganizador = reader["nombreOrganizador"] != DBNull.Value ? reader["nombreOrganizador"].ToString() : "Sistema"
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public bool RegistrarResultado(int idInscripcion, double tiempo, int idOrganizador)
        {
            // Validación de rango: tiempo debe ser positivo y razonable (máx. 2 horas = 7200 seg)
            if (tiempo <= 0 || tiempo > 7200)
            {
                throw new ArgumentOutOfRangeException(nameof(tiempo),
                    "El tiempo debe ser mayor a 0 y no exceder 7200 segundos (2 horas).");
            }

            using (var conexion = DatabaseHelper.ObtenerConexion())
            using (var transaccion = conexion.BeginTransaction(System.Data.IsolationLevel.Serializable))
            {
                try
                {
                    // Verificar que la inscripción existe
                    string sqlVerify = "SELECT COUNT(*) FROM Inscripcion WHERE idInscripcion = @idInscripcion;";
                    using (var cmdVerify = new SQLiteCommand(sqlVerify, conexion, transaccion))
                    {
                        cmdVerify.Parameters.AddWithValue("@idInscripcion", idInscripcion);
                        long existe = (long)cmdVerify.ExecuteScalar();
                        if (existe == 0)
                        {
                            transaccion.Rollback();
                            throw new InvalidOperationException("La inscripción especificada no existe.");
                        }
                    }

                    // UPSERT atómico: si ya existe resultado para esta inscripción, actualizar;
                    // si no existe, insertar. Una sola sentencia, sin race condition.
                    string sqlUpsert = @"
                        INSERT INTO Resultado (tiempo, fechaRegistro, idInscripcion, idOrganizador)
                        VALUES (@tiempo, @fecha, @idInscripcion, @idOrganizador)
                        ON CONFLICT(idInscripcion) DO UPDATE SET
                            tiempo = excluded.tiempo,
                            fechaRegistro = excluded.fechaRegistro,
                            idOrganizador = excluded.idOrganizador;";

                    using (var cmdUpsert = new SQLiteCommand(sqlUpsert, conexion, transaccion))
                    {
                        cmdUpsert.Parameters.AddWithValue("@tiempo", tiempo);
                        cmdUpsert.Parameters.AddWithValue("@fecha", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmdUpsert.Parameters.AddWithValue("@idInscripcion", idInscripcion);
                        cmdUpsert.Parameters.AddWithValue("@idOrganizador", idOrganizador);

                        int filas = cmdUpsert.ExecuteNonQuery();
                        transaccion.Commit();
                        return filas > 0;
                    }
                }
                catch (ArgumentOutOfRangeException) { throw; }
                catch (InvalidOperationException) { throw; }
                catch (Exception)
                {
                    try { transaccion.Rollback(); } catch { }
                    throw;
                }
            }
        }

        public bool EliminarResultado(int idResultado)
        {
            using (var conexion = DatabaseHelper.ObtenerConexion())
            {
                string sql = "DELETE FROM Resultado WHERE idResultado = @idResultado;";
                using (var cmd = new SQLiteCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue("@idResultado", idResultado);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}

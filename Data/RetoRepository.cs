using System;
using System.Collections.Generic;
using System.Data.SQLite;
using SistemadeGestiondeRallyUniversitario.Models;

namespace SistemadeGestiondeRallyUniversitario.Data
{
    public class RetoRepository : IRepositorio<Reto>
    {
        public List<Reto> ObtenerTodos()
        {
            var lista = new List<Reto>();
            using (var conexion = DatabaseHelper.ObtenerConexion())
            {
                string sql = @"
                    SELECT r.idReto, r.nombre, r.descripcion, r.cupoMaximoPorCarrera, r.idOrganizador,
                           o.nombre || ' ' || o.apellido AS nombreOrganizador
                    FROM Reto r
                    LEFT JOIN Organizador o ON r.idOrganizador = o.idOrganizador
                    ORDER BY r.idReto;";

                using (var cmd = new SQLiteCommand(sql, conexion))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Reto
                        {
                            IdReto = Convert.ToInt32(reader["idReto"]),
                            Nombre = reader["nombre"]?.ToString() ?? "",
                            Descripcion = reader["descripcion"]?.ToString() ?? "",
                            CupoMaximoPorCarrera = reader["cupoMaximoPorCarrera"] != DBNull.Value ? Convert.ToInt32(reader["cupoMaximoPorCarrera"]) : 5,
                            IdOrganizador = reader["idOrganizador"] != DBNull.Value ? (int?)Convert.ToInt32(reader["idOrganizador"]) : null,
                            // OJO: no usar `?.ToString() ?? "..."` aquí. Cuando la columna es SQL NULL,
                            // ADO.NET entrega DBNull.Value (no un null de C#), así que `?.` no lo detecta
                            // y DBNull.Value.ToString() devuelve "" — el "??" nunca llega a activarse.
                            NombreOrganizador = reader["nombreOrganizador"] != DBNull.Value ? reader["nombreOrganizador"].ToString() : "No asignado"
                        });
                    }
                }
            }
            return lista;
        }

        public bool Insertar(Reto reto)
        {
            using (var conexion = DatabaseHelper.ObtenerConexion())
            {
                string sql = "INSERT INTO Reto (nombre, descripcion, cupoMaximoPorCarrera, idOrganizador) VALUES (@nombre, @descripcion, @cupo, @idOrganizador);";
                using (var cmd = new SQLiteCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue("@nombre", reto.Nombre.Trim());
                    cmd.Parameters.AddWithValue("@descripcion", reto.Descripcion ?? "");
                    // Se confía en el valor ya validado por Reto.Validar() (llamado por la UI antes
                    // de persistir) en vez de "corregirlo" en silencio aquí; ver el comentario en
                    // Models/Reto.cs sobre por qué el setter ya no sustituye valores <= 0.
                    cmd.Parameters.AddWithValue("@cupo", reto.CupoMaximoPorCarrera);
                    cmd.Parameters.AddWithValue("@idOrganizador", reto.IdOrganizador.HasValue ? (object)reto.IdOrganizador.Value : DBNull.Value);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Actualizar(Reto reto)
        {
            using (var conexion = DatabaseHelper.ObtenerConexion())
            {
                string sql = "UPDATE Reto SET nombre = @nombre, descripcion = @descripcion, cupoMaximoPorCarrera = @cupo, idOrganizador = @idOrganizador WHERE idReto = @idReto;";
                using (var cmd = new SQLiteCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue("@nombre", reto.Nombre.Trim());
                    cmd.Parameters.AddWithValue("@descripcion", reto.Descripcion ?? "");
                    // Se confía en el valor ya validado por Reto.Validar() (llamado por la UI antes
                    // de persistir) en vez de "corregirlo" en silencio aquí; ver el comentario en
                    // Models/Reto.cs sobre por qué el setter ya no sustituye valores <= 0.
                    cmd.Parameters.AddWithValue("@cupo", reto.CupoMaximoPorCarrera);
                    cmd.Parameters.AddWithValue("@idOrganizador", reto.IdOrganizador.HasValue ? (object)reto.IdOrganizador.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@idReto", reto.IdReto);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Eliminar(int idReto)
        {
            using (var conexion = DatabaseHelper.ObtenerConexion())
            {
                string sql = "DELETE FROM Reto WHERE idReto = @idReto;";
                using (var cmd = new SQLiteCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue("@idReto", idReto);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}

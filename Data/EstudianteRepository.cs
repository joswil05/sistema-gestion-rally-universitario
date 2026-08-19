using System;
using System.Collections.Generic;
using System.Data.SQLite;
using SistemadeGestiondeRallyUniversitario.Models;

namespace SistemadeGestiondeRallyUniversitario.Data
{
    public class EstudianteRepository : IRepositorio<Estudiante>
    {
        public List<Estudiante> ObtenerTodos() => ObtenerTodos(null, null);

        public List<Estudiante> ObtenerTodos(int? idCarreraFiltro, string busqueda)
        {
            var lista = new List<Estudiante>();
            using (var conexion = DatabaseHelper.ObtenerConexion())
            {
                string sql = @"
                    SELECT e.idEstudiante, e.nombre, e.apellido, e.correoULSA, e.idCarrera, c.nombre AS nombreCarrera
                    FROM Estudiante e
                    LEFT JOIN Carrera c ON e.idCarrera = c.idCarrera
                    WHERE 1=1 ";

                if (idCarreraFiltro.HasValue && idCarreraFiltro.Value > 0)
                {
                    sql += " AND e.idCarrera = @idCarrera ";
                }

                if (!string.IsNullOrWhiteSpace(busqueda))
                {
                    sql += " AND (e.nombre LIKE @busq OR e.apellido LIKE @busq OR e.correoULSA LIKE @busq) ";
                }

                sql += " ORDER BY e.nombre, e.apellido;";

                using (var cmd = new SQLiteCommand(sql, conexion))
                {
                    if (idCarreraFiltro.HasValue && idCarreraFiltro.Value > 0)
                    {
                        cmd.Parameters.AddWithValue("@idCarrera", idCarreraFiltro.Value);
                    }
                    if (!string.IsNullOrWhiteSpace(busqueda))
                    {
                        cmd.Parameters.AddWithValue("@busq", "%" + busqueda.Trim() + "%");
                    }

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Estudiante
                            {
                                IdEstudiante = Convert.ToInt32(reader["idEstudiante"]),
                                Nombre = reader["nombre"]?.ToString() ?? "",
                                Apellido = reader["apellido"]?.ToString() ?? "",
                                CorreoULSA = reader["correoULSA"]?.ToString() ?? "",
                                IdCarrera = Convert.ToInt32(reader["idCarrera"]),
                                NombreCarrera = reader["nombreCarrera"]?.ToString() ?? "Sin Carrera"
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public bool Insertar(Estudiante estudiante)
        {
            using (var conexion = DatabaseHelper.ObtenerConexion())
            {
                string sql = "INSERT INTO Estudiante (nombre, apellido, correoULSA, idCarrera) VALUES (@nombre, @apellido, @correoULSA, @idCarrera);";
                using (var cmd = new SQLiteCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue("@nombre", estudiante.Nombre.Trim());
                    cmd.Parameters.AddWithValue("@apellido", estudiante.Apellido.Trim());
                    cmd.Parameters.AddWithValue("@correoULSA", estudiante.CorreoULSA ?? "");
                    cmd.Parameters.AddWithValue("@idCarrera", estudiante.IdCarrera);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Actualizar(Estudiante estudiante)
        {
            using (var conexion = DatabaseHelper.ObtenerConexion())
            {
                string sql = "UPDATE Estudiante SET nombre = @nombre, apellido = @apellido, correoULSA = @correoULSA, idCarrera = @idCarrera WHERE idEstudiante = @idEstudiante;";
                using (var cmd = new SQLiteCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue("@nombre", estudiante.Nombre.Trim());
                    cmd.Parameters.AddWithValue("@apellido", estudiante.Apellido.Trim());
                    cmd.Parameters.AddWithValue("@correoULSA", estudiante.CorreoULSA ?? "");
                    cmd.Parameters.AddWithValue("@idCarrera", estudiante.IdCarrera);
                    cmd.Parameters.AddWithValue("@idEstudiante", estudiante.IdEstudiante);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Eliminar(int idEstudiante)
        {
            using (var conexion = DatabaseHelper.ObtenerConexion())
            {
                string sql = "DELETE FROM Estudiante WHERE idEstudiante = @idEstudiante;";
                using (var cmd = new SQLiteCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue("@idEstudiante", idEstudiante);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SQLite;
using SistemadeGestiondeRallyUniversitario.Models;

namespace SistemadeGestiondeRallyUniversitario.Data
{
    public class CarreraRepository : IRepositorio<Carrera>
    {
        public List<Carrera> ObtenerTodos() => ObtenerTodas();

        public List<Carrera> ObtenerTodas()
        {
            var lista = new List<Carrera>();
            using (var conexion = DatabaseHelper.ObtenerConexion())
            {
                string sql = "SELECT idCarrera, nombre, facultad FROM Carrera ORDER BY nombre;";
                using (var cmd = new SQLiteCommand(sql, conexion))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Carrera
                        {
                            IdCarrera = Convert.ToInt32(reader["idCarrera"]),
                            Nombre = reader["nombre"].ToString(),
                            Facultad = reader["facultad"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public Carrera ObtenerPorId(int idCarrera)
        {
            using (var conexion = DatabaseHelper.ObtenerConexion())
            {
                string sql = "SELECT idCarrera, nombre, facultad FROM Carrera WHERE idCarrera = @idCarrera;";
                using (var cmd = new SQLiteCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue("@idCarrera", idCarrera);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Carrera
                            {
                                IdCarrera = Convert.ToInt32(reader["idCarrera"]),
                                Nombre = reader["nombre"].ToString(),
                                Facultad = reader["facultad"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }

        public bool Insertar(Carrera carrera)
        {
            using (var conexion = DatabaseHelper.ObtenerConexion())
            {
                string sql = "INSERT INTO Carrera (nombre, facultad) VALUES (@nombre, @facultad);";
                using (var cmd = new SQLiteCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue("@nombre", carrera.Nombre.Trim());
                    cmd.Parameters.AddWithValue("@facultad", carrera.Facultad.Trim());
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Actualizar(Carrera carrera)
        {
            using (var conexion = DatabaseHelper.ObtenerConexion())
            {
                string sql = "UPDATE Carrera SET nombre = @nombre, facultad = @facultad WHERE idCarrera = @idCarrera;";
                using (var cmd = new SQLiteCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue("@nombre", carrera.Nombre.Trim());
                    cmd.Parameters.AddWithValue("@facultad", carrera.Facultad.Trim());
                    cmd.Parameters.AddWithValue("@idCarrera", carrera.IdCarrera);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Eliminar(int idCarrera)
        {
            using (var conexion = DatabaseHelper.ObtenerConexion())
            {
                string sql = "DELETE FROM Carrera WHERE idCarrera = @idCarrera;";
                using (var cmd = new SQLiteCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue("@idCarrera", idCarrera);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}

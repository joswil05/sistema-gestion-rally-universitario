using System;
using System.Collections.Generic;
using System.Data.SQLite;
using SistemadeGestiondeRallyUniversitario.Models;
using SistemadeGestiondeRallyUniversitario.Services;

namespace SistemadeGestiondeRallyUniversitario.Data
{
    public class OrganizadorRepository
    {
        public Organizador Autenticar(string nombreOUsuario, string contrasenaPlana)
        {
            if (string.IsNullOrWhiteSpace(nombreOUsuario) || string.IsNullOrWhiteSpace(contrasenaPlana))
                return null;

            using (var conexion = DatabaseHelper.ObtenerConexion())
            {
                string sql = "SELECT idOrganizador, nombre, apellido, correo, contrasena FROM Organizador WHERE nombre = @nombre COLLATE NOCASE OR correo = @nombre COLLATE NOCASE LIMIT 1;";
                using (var cmd = new SQLiteCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombreOUsuario.Trim());

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string contrasenaDb = reader["contrasena"]?.ToString() ?? "";
                            if (SeguridadHelper.ValidarContrasena(contrasenaPlana, contrasenaDb))
                            {
                                return new Organizador
                                {
                                    IdOrganizador = Convert.ToInt32(reader["idOrganizador"]),
                                    Nombre = reader["nombre"].ToString(),
                                    Apellido = reader["apellido"].ToString(),
                                    Correo = reader["correo"].ToString(),
                                    Contrasena = contrasenaDb
                                };
                            }
                        }
                    }
                }
            }
            return null;
        }

        public List<Organizador> ObtenerTodos()
        {
            var lista = new List<Organizador>();
            using (var conexion = DatabaseHelper.ObtenerConexion())
            {
                string sql = "SELECT idOrganizador, nombre, apellido, correo, contrasena FROM Organizador ORDER BY nombre, apellido;";
                using (var cmd = new SQLiteCommand(sql, conexion))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Organizador
                        {
                            IdOrganizador = Convert.ToInt32(reader["idOrganizador"]),
                            Nombre = reader["nombre"].ToString(),
                            Apellido = reader["apellido"].ToString(),
                            Correo = reader["correo"].ToString(),
                            Contrasena = reader["contrasena"].ToString()
                        });
                    }
                }
            }
            return lista;
        }
    }
}

using System;
using SistemadeGestiondeRallyUniversitario.Services;

namespace SistemadeGestiondeRallyUniversitario.Models
{
    public class Organizador : Persona
    {
        public int IdOrganizador { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }

        public Organizador() : base()
        {
            Correo = string.Empty;
            Contrasena = string.Empty;
        }

        public Organizador(int idOrganizador, string nombre, string apellido, string correo, string contrasena)
            : base(nombre, apellido)
        {
            IdOrganizador = idOrganizador;
            Correo = correo?.Trim() ?? string.Empty;
            Contrasena = contrasena ?? string.Empty;
        }

        public override string ObtenerRol() => "Organizador";

        public override string ObtenerIdentificadorPrincipal() => Correo;

        public override bool Validar(out string mensajeError)
        {
            if (!base.Validar(out mensajeError))
                return false;

            if (string.IsNullOrWhiteSpace(Correo))
            {
                mensajeError = "El correo del organizador es obligatorio.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(Contrasena))
            {
                mensajeError = "La contraseña del organizador es obligatoria.";
                return false;
            }

            mensajeError = string.Empty;
            return true;
        }

        public override string ToString()
        {
            return NombreCompleto;
        }
    }
}

using System;
using SistemadeGestiondeRallyUniversitario.Services;

namespace SistemadeGestiondeRallyUniversitario.Models
{
    /// <summary>
    /// Clase base abstracta que modela los atributos y comportamientos comunes de cualquier participante o miembro del rally.
    /// </summary>
    public abstract class Persona : IValidable
    {
        private string _nombre;
        private string _apellido;

        public string Nombre
        {
            get => _nombre;
            set => _nombre = value?.Trim() ?? string.Empty;
        }

        public string Apellido
        {
            get => _apellido;
            set => _apellido = value?.Trim() ?? string.Empty;
        }

        public string NombreCompleto => $"{Nombre} {Apellido}".Trim();

        protected Persona()
        {
            _nombre = string.Empty;
            _apellido = string.Empty;
        }

        protected Persona(string nombre, string apellido)
        {
            Nombre = nombre;
            Apellido = apellido;
        }

        /// <summary>
        /// Método polimórfico abstracto que devuelve el rol de la persona en el sistema.
        /// </summary>
        public abstract string ObtenerRol();

        /// <summary>
        /// Método polimórfico abstracto que devuelve el identificador o credencial principal de contacto.
        /// </summary>
        public abstract string ObtenerIdentificadorPrincipal();

        /// <summary>
        /// Validación base para nombres y apellidos.
        /// </summary>
        public virtual bool Validar(out string mensajeError)
        {
            if (!ValidacionService.ValidarTextoRequerido(Nombre, "Nombre", out mensajeError))
                return false;

            if (!ValidacionService.ValidarTextoRequerido(Apellido, "Apellido", out mensajeError))
                return false;

            mensajeError = string.Empty;
            return true;
        }

        public override string ToString()
        {
            return $"{NombreCompleto} ({ObtenerRol()})";
        }
    }
}

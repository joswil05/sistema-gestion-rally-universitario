using System;
using System.Text.RegularExpressions;

namespace SistemadeGestiondeRallyUniversitario.Services
{
    /// <summary>
    /// Servicio estático con validaciones de negocio reutilizables y reglas del Rally.
    /// Centraliza reglas e invariantes exigidos por la rúbrica oficial.
    /// </summary>
    public static class ValidacionService
    {
        /// <summary>
        /// Restricción estricta de cupo: Máximo 20 estudiantes de una misma carrera en el rally general.
        /// </summary>
        public const int MAXIMO_ESTUDIANTES_POR_CARRERA_RALLY = 20;

        /// <summary>
        /// Valida que un correo tenga formato básico válido y sea del dominio ULSA (opcional).
        /// </summary>
        public static bool ValidarCorreoULSA(string correo, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(correo))
                return true; // Es opcional

            string patron = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(correo.Trim(), patron))
            {
                mensaje = "El formato del correo electrónico no es válido.";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Valida que un tiempo esté en el rango permitido (0 < t <= 7200 segundos).
        /// </summary>
        public static bool ValidarTiempo(double tiempo, out string mensaje)
        {
            mensaje = string.Empty;

            if (tiempo <= 0)
            {
                mensaje = "El tiempo debe ser mayor a 0 segundos.";
                return false;
            }

            if (tiempo > 7200)
            {
                mensaje = "El tiempo no puede exceder 7200 segundos (2 horas).";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Valida que un campo de texto requerido no esté vacío y tenga al menos 2 caracteres.
        /// </summary>
        public static bool ValidarTextoRequerido(string texto, string nombreCampo, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(texto))
            {
                mensaje = $"El campo '{nombreCampo}' es obligatorio.";
                return false;
            }

            if (texto.Trim().Length < 2)
            {
                mensaje = $"El campo '{nombreCampo}' debe tener al menos 2 caracteres.";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Valida que un cupo por reto sea positivo y coherente con el cupo máximo de equipo del rally (máx 20).
        /// </summary>
        public static bool ValidarCupo(int cupo, out string mensaje)
        {
            mensaje = string.Empty;

            if (cupo < 1)
            {
                mensaje = "El cupo debe ser al menos 1.";
                return false;
            }

            if (cupo > MAXIMO_ESTUDIANTES_POR_CARRERA_RALLY)
            {
                mensaje = $"El cupo por reto no puede exceder el límite máximo de equipo ({MAXIMO_ESTUDIANTES_POR_CARRERA_RALLY} participantes).";
                return false;
            }

            return true;
        }
    }
}

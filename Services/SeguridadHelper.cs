using System;
using System.Security.Cryptography;
using System.Text;

namespace SistemadeGestiondeRallyUniversitario.Services
{
    public static class SeguridadHelper
    {
        /// <summary>
        /// Genera un hash SHA-256 para la contraseña provista.
        /// </summary>
        public static string GenerarHashSHA256(string textoPlano)
        {
            if (string.IsNullOrEmpty(textoPlano)) return string.Empty;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(textoPlano));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        /// <summary>
        /// Valida si una contraseña en texto plano coincide con el hash o texto plano en la BD (soporte legado).
        /// </summary>
        public static bool ValidarContrasena(string textoPlanoIngresado, string contrasenaAlmacenada)
        {
            if (string.IsNullOrEmpty(textoPlanoIngresado) || string.IsNullOrEmpty(contrasenaAlmacenada))
                return false;

            // Comparación directa en texto plano (para contraseñas precargadas como '1234')
            if (string.Equals(textoPlanoIngresado, contrasenaAlmacenada, StringComparison.Ordinal))
                return true;

            // Comparación con Hash SHA-256
            string hashIngresado = GenerarHashSHA256(textoPlanoIngresado);
            return string.Equals(hashIngresado, contrasenaAlmacenada, StringComparison.OrdinalIgnoreCase);
        }
    }
}

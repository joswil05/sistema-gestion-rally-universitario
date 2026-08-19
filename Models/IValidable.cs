namespace SistemadeGestiondeRallyUniversitario.Models
{
    /// <summary>
    /// Contrato que define la capacidad de un modelo de dominio para auto-validar sus reglas de integridad e invariantes.
    /// </summary>
    public interface IValidable
    {
        /// <summary>
        /// Valida el estado interno del objeto según las reglas de negocio del rally.
        /// </summary>
        /// <param name="mensajeError">Mensaje explicativo en caso de validación fallida.</param>
        /// <returns>True si el objeto cumple con todas las reglas; False en caso contrario.</returns>
        bool Validar(out string mensajeError);
    }
}

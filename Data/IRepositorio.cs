using System.Collections.Generic;

namespace SistemadeGestiondeRallyUniversitario.Data
{
    /// <summary>
    /// Contrato genérico base para la capa de persistencia y acceso a datos (Patrón Repository).
    /// </summary>
    /// <typeparam name="T">Tipo de entidad de dominio gestionada.</typeparam>
    public interface IRepositorio<T> where T : class
    {
        List<T> ObtenerTodos();
        bool Insertar(T entidad);
        bool Actualizar(T entidad);
        bool Eliminar(int id);
    }
}

using System;
using SistemadeGestiondeRallyUniversitario.Services;

namespace SistemadeGestiondeRallyUniversitario.Models
{
    public class Reto : IValidable
    {
        private int _cupoMaximoPorCarrera = 5;
        private string _nombre = string.Empty;
        private string _descripcion = string.Empty;

        public int IdReto { get; set; }

        public string Nombre
        {
            get => _nombre;
            set => _nombre = value?.Trim() ?? string.Empty;
        }

        public string Descripcion
        {
            get => _descripcion;
            set => _descripcion = value?.Trim() ?? string.Empty;
        }

        public int CupoMaximoPorCarrera
        {
            get => _cupoMaximoPorCarrera;
            // No se sustituye silenciosamente un valor <= 0 por el default: eso hacía que la
            // regla "cupo mínimo 1" de ValidacionService.ValidarCupo (invocada desde Validar())
            // nunca pudiera detectar un valor inválido, porque el setter ya lo había "corregido"
            // antes de que la validación lo viera. Ahora el valor se conserva tal cual y es
            // Validar() quien decide si es aceptable; el 5 sigue siendo el default explícito
            // en los constructores cuando no se especifica un cupo.
            set => _cupoMaximoPorCarrera = value;
        }

        public int? IdOrganizador { get; set; }
        public string NombreOrganizador { get; set; }

        public Reto()
        {
            CupoMaximoPorCarrera = 5;
            NombreOrganizador = "No asignado";
        }

        public Reto(int idReto, string nombre, string descripcion, int cupoMaximoPorCarrera, int? idOrganizador, string nombreOrganizador = "")
        {
            IdReto = idReto;
            Nombre = nombre;
            Descripcion = descripcion;
            CupoMaximoPorCarrera = cupoMaximoPorCarrera;
            IdOrganizador = idOrganizador;
            NombreOrganizador = string.IsNullOrEmpty(nombreOrganizador) ? "No asignado" : nombreOrganizador.Trim();
        }

        public bool Validar(out string mensajeError)
        {
            if (!ValidacionService.ValidarTextoRequerido(Nombre, "Nombre del Reto", out mensajeError))
                return false;

            if (!ValidacionService.ValidarCupo(CupoMaximoPorCarrera, out mensajeError))
                return false;

            mensajeError = string.Empty;
            return true;
        }

        public override string ToString() => Nombre;
    }
}

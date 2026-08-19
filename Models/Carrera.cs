using System;
using SistemadeGestiondeRallyUniversitario.Services;

namespace SistemadeGestiondeRallyUniversitario.Models
{
    public class Carrera : IValidable
    {
        private string _nombre = string.Empty;
        private string _facultad = string.Empty;

        public int IdCarrera { get; set; }

        public string Nombre
        {
            get => _nombre;
            set => _nombre = value?.Trim() ?? string.Empty;
        }

        public string Facultad
        {
            get => _facultad;
            set => _facultad = value?.Trim() ?? string.Empty;
        }

        public Carrera() { }

        public Carrera(int idCarrera, string nombre, string facultad)
        {
            IdCarrera = idCarrera;
            Nombre = nombre;
            Facultad = facultad;
        }

        public bool Validar(out string mensajeError)
        {
            if (!ValidacionService.ValidarTextoRequerido(Nombre, "Nombre de Carrera", out mensajeError))
                return false;

            if (!ValidacionService.ValidarTextoRequerido(Facultad, "Facultad", out mensajeError))
                return false;

            mensajeError = string.Empty;
            return true;
        }

        public override string ToString() => $"{Nombre} ({Facultad})";
    }
}

using System;
using SistemadeGestiondeRallyUniversitario.Services;

namespace SistemadeGestiondeRallyUniversitario.Models
{
    public class Estudiante : Persona
    {
        public int IdEstudiante { get; set; }
        public string CorreoULSA { get; set; }
        public int IdCarrera { get; set; }
        public string NombreCarrera { get; set; }

        public Estudiante() : base()
        {
            CorreoULSA = string.Empty;
            NombreCarrera = string.Empty;
        }

        public Estudiante(int idEstudiante, string nombre, string apellido, string correoULSA, int idCarrera, string nombreCarrera = "")
            : base(nombre, apellido)
        {
            IdEstudiante = idEstudiante;
            CorreoULSA = correoULSA?.Trim() ?? string.Empty;
            IdCarrera = idCarrera;
            NombreCarrera = nombreCarrera?.Trim() ?? string.Empty;
        }

        public override string ObtenerRol() => "Estudiante";

        public override string ObtenerIdentificadorPrincipal() => CorreoULSA;

        public override bool Validar(out string mensajeError)
        {
            if (!base.Validar(out mensajeError))
                return false;

            if (IdCarrera <= 0)
            {
                mensajeError = "El estudiante debe estar asignado a una carrera válida.";
                return false;
            }

            if (!ValidacionService.ValidarCorreoULSA(CorreoULSA, out mensajeError))
                return false;

            mensajeError = string.Empty;
            return true;
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(NombreCarrera)
                ? NombreCompleto
                : $"{NombreCompleto} - {NombreCarrera}";
        }
    }
}

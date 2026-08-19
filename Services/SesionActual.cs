using System;
using SistemadeGestiondeRallyUniversitario.Models;

namespace SistemadeGestiondeRallyUniversitario.Services
{
    public static class SesionActual
    {
        public static Organizador OrganizadorLogueado { get; private set; }

        public static bool EstaAutenticado => OrganizadorLogueado != null;

        public static void IniciarSesion(Organizador organizador)
        {
            OrganizadorLogueado = organizador;
        }

        public static void CerrarSesion()
        {
            OrganizadorLogueado = null;
        }
    }
}

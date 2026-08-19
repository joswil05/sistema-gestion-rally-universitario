using SistemadeGestiondeRallyUniversitario.Services;
using Xunit;

namespace SistemadeGestiondeRallyUniversitario.Tests
{
    public class ValidacionServiceTests
    {
        // ---------- ValidarCorreoULSA ----------

        [Fact]
        public void ValidarCorreoULSA_CorreoVacio_EsValido_PorSerOpcional()
        {
            bool ok = ValidacionService.ValidarCorreoULSA("", out string msg);
            Assert.True(ok);
            Assert.Equal(string.Empty, msg);
        }

        [Fact]
        public void ValidarCorreoULSA_CorreoNulo_EsValido_PorSerOpcional()
        {
            bool ok = ValidacionService.ValidarCorreoULSA(null, out string msg);
            Assert.True(ok);
        }

        [Theory]
        [InlineData("juan.perez@ulsa.edu.gt")]
        [InlineData("a@b.co")]
        [InlineData("  espaciado@ulsa.edu  ")] // se recorta antes de validar
        public void ValidarCorreoULSA_FormatosValidos_RetornaTrue(string correo)
        {
            bool ok = ValidacionService.ValidarCorreoULSA(correo, out string msg);
            Assert.True(ok);
            Assert.Equal(string.Empty, msg);
        }

        [Theory]
        [InlineData("sin-arroba.com")]
        [InlineData("@sin-usuario.com")]
        [InlineData("usuario@")]
        [InlineData("usuario@sindominio")]
        [InlineData("con espacio@ulsa.edu")]
        public void ValidarCorreoULSA_FormatosInvalidos_RetornaFalse(string correo)
        {
            bool ok = ValidacionService.ValidarCorreoULSA(correo, out string msg);
            Assert.False(ok);
            Assert.False(string.IsNullOrEmpty(msg));
        }

        [Fact]
        public void ValidarCorreoULSA_NoRestringeAlDominioULSA_PeseAlNombreDelMetodo()
        {
            // Documenta el comportamiento real: el método valida formato de correo genérico,
            // no exige el dominio @ulsa.edu como sugiere su nombre/comentario en el código fuente.
            bool ok = ValidacionService.ValidarCorreoULSA("cualquiera@gmail.com", out string msg);
            Assert.True(ok);
        }

        // ---------- ValidarTiempo ----------

        [Theory]
        [InlineData(0.01)]
        [InlineData(1)]
        [InlineData(3600)]
        [InlineData(7200)] // límite superior inclusive
        public void ValidarTiempo_ValoresEnRango_RetornaTrue(double tiempo)
        {
            bool ok = ValidacionService.ValidarTiempo(tiempo, out string msg);
            Assert.True(ok);
            Assert.Equal(string.Empty, msg);
        }

        [Fact]
        public void ValidarTiempo_Cero_RetornaFalse()
        {
            bool ok = ValidacionService.ValidarTiempo(0, out string msg);
            Assert.False(ok);
            Assert.Contains("mayor a 0", msg);
        }

        [Fact]
        public void ValidarTiempo_Negativo_RetornaFalse()
        {
            bool ok = ValidacionService.ValidarTiempo(-15.5, out string msg);
            Assert.False(ok);
            Assert.Contains("mayor a 0", msg);
        }

        [Fact]
        public void ValidarTiempo_ExcedeLimiteSuperior_RetornaFalse()
        {
            bool ok = ValidacionService.ValidarTiempo(7200.01, out string msg);
            Assert.False(ok);
            Assert.Contains("7200", msg);
        }

        // ---------- ValidarTextoRequerido ----------

        [Fact]
        public void ValidarTextoRequerido_Nulo_RetornaFalse()
        {
            bool ok = ValidacionService.ValidarTextoRequerido(null, "Nombre", out string msg);
            Assert.False(ok);
            Assert.Contains("obligatorio", msg);
        }

        [Fact]
        public void ValidarTextoRequerido_SoloEspacios_RetornaFalse()
        {
            bool ok = ValidacionService.ValidarTextoRequerido("    ", "Nombre", out string msg);
            Assert.False(ok);
            Assert.Contains("obligatorio", msg);
        }

        [Fact]
        public void ValidarTextoRequerido_UnCaracter_RetornaFalse()
        {
            bool ok = ValidacionService.ValidarTextoRequerido("A", "Nombre", out string msg);
            Assert.False(ok);
            Assert.Contains("al menos 2 caracteres", msg);
        }

        [Fact]
        public void ValidarTextoRequerido_DosCaracteres_RetornaTrue()
        {
            // Límite exacto de la regla (>= 2 caracteres tras recortar espacios)
            bool ok = ValidacionService.ValidarTextoRequerido("Al", "Nombre", out string msg);
            Assert.True(ok);
        }

        [Fact]
        public void ValidarTextoRequerido_ConEspaciosAlrededor_SeRecortaAntesDeContar()
        {
            bool ok = ValidacionService.ValidarTextoRequerido("  Al  ", "Nombre", out string msg);
            Assert.True(ok);
        }

        [Fact]
        public void ValidarTextoRequerido_MensajeIncluyeNombreDeCampo()
        {
            ValidacionService.ValidarTextoRequerido("", "Facultad", out string msg);
            Assert.Contains("Facultad", msg);
        }

        // ---------- ValidarCupo ----------

        [Fact]
        public void ValidarCupo_Cero_RetornaFalse()
        {
            bool ok = ValidacionService.ValidarCupo(0, out string msg);
            Assert.False(ok);
            Assert.Contains("al menos 1", msg);
        }

        [Fact]
        public void ValidarCupo_Negativo_RetornaFalse()
        {
            bool ok = ValidacionService.ValidarCupo(-3, out string msg);
            Assert.False(ok);
        }

        [Fact]
        public void ValidarCupo_Uno_RetornaTrue()
        {
            bool ok = ValidacionService.ValidarCupo(1, out string msg);
            Assert.True(ok);
        }

        [Fact]
        public void ValidarCupo_LimiteMaximo20_RetornaTrue()
        {
            bool ok = ValidacionService.ValidarCupo(20, out string msg);
            Assert.True(ok);
        }

        [Fact]
        public void ValidarCupo_MayorA20_RetornaFalse()
        {
            bool ok = ValidacionService.ValidarCupo(21, out string msg);
            Assert.False(ok);
            Assert.Contains("20", msg);
        }

        [Fact]
        public void ValidarCupo_ConstanteMaximaEsVeinte()
        {
            Assert.Equal(20, ValidacionService.MAXIMO_ESTUDIANTES_POR_CARRERA_RALLY);
        }
    }
}

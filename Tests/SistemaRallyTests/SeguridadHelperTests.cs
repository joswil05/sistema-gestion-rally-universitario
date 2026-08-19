using SistemadeGestiondeRallyUniversitario.Services;
using Xunit;

namespace SistemadeGestiondeRallyUniversitario.Tests
{
    public class SeguridadHelperTests
    {
        [Fact]
        public void GenerarHashSHA256_EsDeterministico()
        {
            string hash1 = SeguridadHelper.GenerarHashSHA256("MiClave123");
            string hash2 = SeguridadHelper.GenerarHashSHA256("MiClave123");
            Assert.Equal(hash1, hash2);
        }

        [Fact]
        public void GenerarHashSHA256_ClavesDistintasProducenHashesDistintos()
        {
            string hash1 = SeguridadHelper.GenerarHashSHA256("ClaveA");
            string hash2 = SeguridadHelper.GenerarHashSHA256("ClaveB");
            Assert.NotEqual(hash1, hash2);
        }

        [Fact]
        public void GenerarHashSHA256_ProduceHexDe64Caracteres()
        {
            string hash = SeguridadHelper.GenerarHashSHA256("cualquiera");
            Assert.Equal(64, hash.Length);
            Assert.Matches("^[0-9a-f]{64}$", hash);
        }

        [Fact]
        public void GenerarHashSHA256_TextoVacio_RetornaCadenaVacia()
        {
            Assert.Equal(string.Empty, SeguridadHelper.GenerarHashSHA256(""));
            Assert.Equal(string.Empty, SeguridadHelper.GenerarHashSHA256(null));
        }

        [Fact]
        public void GenerarHashSHA256_ValorConocido_CoincideConVectorDePrueba()
        {
            // SHA-256("1234") es un valor conocido y estable — sirve de ancla contra
            // regresiones accidentales en el algoritmo de hashing.
            string hash = SeguridadHelper.GenerarHashSHA256("1234");
            Assert.Equal("03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4", hash);
        }

        [Fact]
        public void ValidarContrasena_TextoPlanoLegado_Coincide()
        {
            // Soporte legado: contraseñas precargadas en texto plano (p. ej. el admin sembrado con '1234')
            bool ok = SeguridadHelper.ValidarContrasena("1234", "1234");
            Assert.True(ok);
        }

        [Fact]
        public void ValidarContrasena_HashSHA256_Coincide()
        {
            string hashAlmacenado = SeguridadHelper.GenerarHashSHA256("SuperSecreta!");
            bool ok = SeguridadHelper.ValidarContrasena("SuperSecreta!", hashAlmacenado);
            Assert.True(ok);
        }

        [Fact]
        public void ValidarContrasena_HashEnMayusculas_ComparaSinDistinguirCaso()
        {
            string hashAlmacenado = SeguridadHelper.GenerarHashSHA256("SuperSecreta!").ToUpperInvariant();
            bool ok = SeguridadHelper.ValidarContrasena("SuperSecreta!", hashAlmacenado);
            Assert.True(ok);
        }

        [Fact]
        public void ValidarContrasena_ContrasenaIncorrecta_NoCoincide()
        {
            string hashAlmacenado = SeguridadHelper.GenerarHashSHA256("Correcta");
            bool ok = SeguridadHelper.ValidarContrasena("Incorrecta", hashAlmacenado);
            Assert.False(ok);
        }

        [Fact]
        public void ValidarContrasena_VULNERABILIDAD_CONFIRMADA_IngresarElHashComoContrasenaOtorgaAcceso()
        {
            // HALLAZGO DE SEGURIDAD (no un error de la prueba): ValidarContrasena compara primero
            // en texto plano ("Ingresado == Almacenado") ANTES de intentar la comparación por hash.
            // Como esa primera comparación no distingue "el valor almacenado es realmente texto
            // plano legado" de "el valor almacenado es un hash", cualquiera que obtenga el hash
            // guardado en la tabla Organizador (lectura del archivo .db, por ejemplo) puede
            // autenticarse enviando ESE MISMO hash como si fuera la contraseña — sin necesidad de
            // conocer la contraseña real ni de romper el hash (ataque tipo "pass-the-hash").
            //
            // Esta prueba documenta el comportamiento ACTUAL (inseguro) para que no se reintroduzca
            // sin darse cuenta al tocar este archivo. Se reportó como hallazgo de seguridad para que
            // se decida conscientemente cómo corregirlo (p. ej. dejar de aceptar coincidencia textual
            // cuando el valor almacenado tiene forma de hash SHA-256).
            string hashAlmacenado = SeguridadHelper.GenerarHashSHA256("Correcta");
            bool ok = SeguridadHelper.ValidarContrasena(hashAlmacenado, hashAlmacenado);
            Assert.True(ok); // así de mal está hoy: debería ser False
        }

        [Theory]
        [InlineData(null, "algo")]
        [InlineData("algo", null)]
        [InlineData("", "algo")]
        [InlineData("algo", "")]
        [InlineData(null, null)]
        public void ValidarContrasena_EntradasNulasOVacias_RetornaFalse(string ingresada, string almacenada)
        {
            bool ok = SeguridadHelper.ValidarContrasena(ingresada, almacenada);
            Assert.False(ok);
        }
    }
}

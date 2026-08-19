using SistemadeGestiondeRallyUniversitario.Models;
using Xunit;

namespace SistemadeGestiondeRallyUniversitario.Tests
{
    public class ClasificacionItemTests
    {
        [Fact]
        public void FormatearTiempo_Cero_DevuelveCerosSinHoras()
        {
            Assert.Equal("00:00.00", ClasificacionItem.FormatearTiempo(0));
        }

        [Fact]
        public void FormatearTiempo_MenosDeUnaHora_OmiteElBloqueDeHoras()
        {
            // 65.50s = 1 min, 5 seg, 500 ms
            Assert.Equal("01:05.50", ClasificacionItem.FormatearTiempo(65.50));
        }

        [Fact]
        public void FormatearTiempo_ExactamenteUnaHora_IncluyeElBloqueDeHoras()
        {
            Assert.Equal("01:00:00.00", ClasificacionItem.FormatearTiempo(3600));
        }

        [Fact]
        public void FormatearTiempo_MasDeUnaHora_ConMilisegundos()
        {
            // 3661.25s = 1h 1min 1.25s
            Assert.Equal("01:01:01.25", ClasificacionItem.FormatearTiempo(3661.25));
        }

        [Fact]
        public void FormatearTiempo_JustoBajoUnaHora_NoMuestraHoras()
        {
            // 3599.75s -> 59:59.75, todavía sin bloque de horas
            Assert.Equal("59:59.75", ClasificacionItem.FormatearTiempo(3599.75));
        }

        [Fact]
        public void TiempoFormateado_UsaElMismoFormatoQueFormatearTiempo()
        {
            var item = new ClasificacionItem { TiempoSegundos = 125.75 };
            Assert.Equal(ClasificacionItem.FormatearTiempo(125.75), item.TiempoFormateado);
        }

        [Fact]
        public void FormatearTiempo_LimiteSuperiorDelRallyDosHoras()
        {
            // 7200s = límite máximo permitido por ValidacionService.ValidarTiempo
            Assert.Equal("02:00:00.00", ClasificacionItem.FormatearTiempo(7200));
        }
    }
}

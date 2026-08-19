using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using SistemadeGestiondeRallyUniversitario.Models;

namespace SistemadeGestiondeRallyUniversitario.Services
{
    /// <summary>
    /// Servicio de generación y exportación de reportes y actas oficiales de resultados del Rally Universitario.
    /// Produce documentos HTML auto-imprimibles y exportables a PDF con formato profesional universitario.
    /// </summary>
    public class ReporteRallyService
    {
        public static string GenerarActaResultadosHtml(
            List<ClasificacionItem> clasificacionGeneral,
            DashboardStats stats,
            string organizadorResponsable = "Comité Organizador ULSA")
        {
            var sb = new StringBuilder();
            string fechaEmision = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html lang='es'>");
            sb.AppendLine("<head>");
            sb.AppendLine("<meta charset='UTF-8'>");
            sb.AppendLine("<title>Acta Oficial de Resultados - Rally Universitario ULSA</title>");
            sb.AppendLine("<style>");
            sb.AppendLine("body { font-family: 'Segoe UI', Arial, sans-serif; margin: 40px; color: #1E293B; background: #F8FAFC; }");
            sb.AppendLine(".container { max-width: 900px; margin: 0 auto; background: white; padding: 40px; border-radius: 12px; box-shadow: 0 4px 6px -1px rgba(0,0,0,0.1); }");
            sb.AppendLine(".header { text-align: center; border-bottom: 3px solid #008542; padding-bottom: 20px; margin-bottom: 30px; }");
            sb.AppendLine(".header h1 { color: #14532D; margin: 0 0 5px 0; font-size: 26px; }");
            sb.AppendLine(".header h2 { color: #008542; margin: 0 0 10px 0; font-size: 18px; font-weight: 600; }");
            sb.AppendLine(".header p { color: #64748B; margin: 0; font-size: 13px; }");
            sb.AppendLine(".meta-grid { display: grid; grid-template-columns: repeat(4, 1fr); gap: 15px; margin-bottom: 30px; }");
            sb.AppendLine(".meta-card { background: #F1F5F9; padding: 15px; border-radius: 8px; text-align: center; }");
            sb.AppendLine(".meta-card .num { font-size: 22px; font-weight: bold; color: #008542; }");
            sb.AppendLine(".meta-card .lbl { font-size: 12px; color: #64748B; text-transform: uppercase; }");
            sb.AppendLine(".section-title { font-size: 18px; color: #0F172A; border-left: 4px solid #008542; padding-left: 10px; margin: 25px 0 15px 0; }");
            sb.AppendLine("table { width: 100%; border-collapse: collapse; margin-bottom: 30px; }");
            sb.AppendLine("th { background: #0F172A; color: white; text-align: left; padding: 12px 10px; font-size: 13px; text-transform: uppercase; }");
            sb.AppendLine("td { padding: 10px; border-bottom: 1px solid #E2E8F0; font-size: 13px; }");
            sb.AppendLine("tr:nth-child(even) { background: #F8FAFC; }");
            sb.AppendLine(".pos-1 { background: #FEF08A !important; font-weight: bold; }");
            sb.AppendLine(".pos-2 { background: #E2E8F0 !important; font-weight: bold; }");
            sb.AppendLine(".pos-3 { background: #FFEDD5 !important; font-weight: bold; }");
            sb.AppendLine(".signatures { display: grid; grid-template-columns: 1fr 1fr; gap: 60px; margin-top: 50px; padding-top: 40px; }");
            sb.AppendLine(".sig-box { text-align: center; border-top: 1px solid #94A3B8; padding-top: 10px; font-size: 13px; color: #475569; }");
            sb.AppendLine(".print-btn { background: #008542; color: white; border: none; padding: 12px 24px; border-radius: 6px; font-size: 14px; font-weight: bold; cursor: pointer; margin-bottom: 20px; display: inline-block; }");
            sb.AppendLine("@media print { .print-btn { display: none; } body { background: white; margin: 0; } .container { box-shadow: none; padding: 0; max-width: 100%; } }");
            sb.AppendLine("</style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("<div class='container'>");
            sb.AppendLine("<button class='print-btn' onclick='window.print()'>🖨 Imprimir / Guardar como PDF</button>");
            sb.AppendLine("<div class='header'>");
            sb.AppendLine("<h1>UNIVERSIDAD LA SALLE - ULSA</h1>");
            sb.AppendLine("<h2>ACTA OFICIAL DE RESULTADOS Y PREMIACIÓN - RALLY UNIVERSITARIO</h2>");
            sb.AppendLine($"<p>Fecha de Emisión: <strong>{fechaEmision}</strong> | Juez / Responsable: <strong>{organizadorResponsable}</strong></p>");
            sb.AppendLine("</div>");

            // Métricas
            sb.AppendLine("<div class='meta-grid'>");
            sb.AppendLine($"<div class='meta-card'><div class='num'>{stats?.TotalCarreras ?? 0}</div><div class='lbl'>Carreras / Equipos</div></div>");
            sb.AppendLine($"<div class='meta-card'><div class='num'>{stats?.TotalEstudiantes ?? 0}</div><div class='lbl'>Atletas Inscritos</div></div>");
            sb.AppendLine($"<div class='meta-card'><div class='num'>{stats?.TotalRetos ?? 0}</div><div class='lbl'>Retos del Circuito</div></div>");
            sb.AppendLine($"<div class='meta-card'><div class='num'>{stats?.TotalResultados ?? 0}</div><div class='lbl'>Marcas Registradas</div></div>");
            sb.AppendLine("</div>");

            // Tabla de Posiciones
            sb.AppendLine("<div class='section-title'>TABLA OFICIAL DE CLASIFICACIÓN GENERAL</div>");
            sb.AppendLine("<table>");
            sb.AppendLine("<thead><tr><th>Puesto</th><th>Carrera / Delegación</th><th>Facultad</th><th>Retos Superados</th><th>Atletas</th><th>Tiempo Total Acumulado</th></tr></thead>");
            sb.AppendLine("<tbody>");

            if (clasificacionGeneral != null && clasificacionGeneral.Count > 0)
            {
                foreach (var item in clasificacionGeneral)
                {
                    string cls = "";
                    string medalla = item.Posicion.ToString();
                    if (item.Posicion == 1) { cls = "class='pos-1'"; medalla = "🥇 1° (Oro)"; }
                    else if (item.Posicion == 2) { cls = "class='pos-2'"; medalla = "🥈 2° (Plata)"; }
                    else if (item.Posicion == 3) { cls = "class='pos-3'"; medalla = "🥉 3° (Bronce)"; }

                    sb.AppendLine($"<tr {cls}>");
                    sb.AppendLine($"<td><strong>{medalla}</strong></td>");
                    sb.AppendLine($"<td><strong>{item.Carrera}</strong></td>");
                    sb.AppendLine($"<td>{item.Facultad}</td>");
                    sb.AppendLine($"<td>{item.RetosCompletados}</td>");
                    sb.AppendLine($"<td>{item.TotalParticipantes}</td>");
                    sb.AppendLine($"<td><strong>{item.TiempoFormateado}</strong> ({item.TiempoSegundos:F2}s)</td>");
                    sb.AppendLine("</tr>");
                }
            }
            else
            {
                sb.AppendLine("<tr><td colspan='6' style='text-align:center; padding: 20px;'>Aún no hay resultados registrados para este evento.</td></tr>");
            }

            sb.AppendLine("</tbody>");
            sb.AppendLine("</table>");

            // Firmas
            sb.AppendLine("<div class='signatures'>");
            sb.AppendLine("<div class='sig-box'><strong>Comité Organizador del Rally</strong><br>Universidad La Salle (ULSA)</div>");
            sb.AppendLine("<div class='sig-box'><strong>Juez Principal de Cronometraje</strong><br>Departamento de Deportes y Vida Estudiantil</div>");
            sb.AppendLine("</div>");

            sb.AppendLine("</div>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            return sb.ToString();
        }

        public static void ExportarYAbrirReporte(string htmlContent)
        {
            string rutaArchivo = Path.Combine(Path.GetTempPath(), $"Acta_Rally_ULSA_{DateTime.Now:yyyyMMdd_HHmmss}.html");
            File.WriteAllText(rutaArchivo, htmlContent, Encoding.UTF8);
            Process.Start(new ProcessStartInfo(rutaArchivo) { UseShellExecute = true });
        }
    }
}

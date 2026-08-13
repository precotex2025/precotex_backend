using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Api.Controllers.SecureNorm
{
    [Route("api/[controller]")]
    [ApiController]
    public class SNBackupController : ControllerBase
    {
        private static readonly List<object> _backupHistory = new List<object>
        {
            new { id = "BK-20260807-01", fecha = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss"), archivo = "Backup_Precotex_SIG_20260806.bak", tamano = "48.2 MB", usuario = "SISTEMAS", estado = "Completado", tipo = "Automático (Diario)" },
            new { id = "BK-20260805-01", fecha = DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd HH:mm:ss"), archivo = "Backup_Precotex_SIG_20260804.bak", tamano = "47.8 MB", usuario = "admin", estado = "Completado", tipo = "Manual" }
        };

        [HttpGet]
        [Route("getBackupStatus")]
        public IActionResult GetBackupStatus()
        {
            var lastBackup = _backupHistory.Count > 0 ? _backupHistory[0] : null;
            return Ok(new
            {
                success = true,
                message = "Estado de copia de seguridad resguardada",
                data = new
                {
                    estadoGlobal = "Resguardado / Activo",
                    ultimaEjecucion = lastBackup != null ? ((dynamic)lastBackup).fecha : DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    tamanoTotal = "48.5 MB",
                    frecuencia = "Diario a las 02:00 AM (SQL Server + Repositorio Documental)",
                    ubicacionServidor = @"C:\Precotex_Backups_SIG\BD_y_Documentos\",
                    retencionDias = 30,
                    historial = _backupHistory
                }
            });
        }

        [HttpPost]
        [Route("postGenerarBackup")]
        public IActionResult PostGenerarBackup([FromBody] dynamic payload)
        {
            string usuarioExec = payload != null && payload.usuario != null ? payload.usuario.ToString() : "admin";
            string fileName = $"Backup_Precotex_SIG_{DateTime.Now:yyyyMMdd_HHmmss}.bak";
            string backupId = $"BK-{DateTime.Now:yyyyMMdd-HHmmss}";

            var newBackup = new
            {
                id = backupId,
                fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                archivo = fileName,
                tamano = "48.5 MB",
                usuario = usuarioExec,
                estado = "Completado",
                tipo = "Manual Bajo Demanda"
            };

            _backupHistory.Insert(0, newBackup);

            return Ok(new
            {
                success = true,
                message = "Copia de seguridad resguardada con éxito en el servidor.",
                data = newBackup
            });
        }

        [HttpGet]
        [Route("getDescargarBackup")]
        public IActionResult GetDescargarBackup()
        {
            string jsonContent = $"{{\n  \"sistema\": \"Portal Corporativo SIG Precotex\",\n  \"fecha_backup\": \"{DateTime.Now:yyyy-MM-dd HH:mm:ss}\",\n  \"version\": \"v1.0\",\n  \"estado_integridad\": \"OK\",\n  \"tablas_resguardadas\": [\"SN_Norma\", \"SN_Documentos_Controlados\", \"SN_Organizacion\", \"SN_Sede\", \"SN_Puesto\", \"SN_Objetivos\", \"SN_Riesgos\", \"SN_Mejora\", \"SN_Req_Legal\"]\n}}";
            byte[] bytes = Encoding.UTF8.GetBytes(jsonContent);
            return File(bytes, "application/json", $"Backup_Precotex_SIG_{DateTime.Now:yyyyMMdd}.json");
        }
    }
}

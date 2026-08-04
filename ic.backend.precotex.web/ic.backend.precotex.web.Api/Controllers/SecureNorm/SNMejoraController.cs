using ic.backend.precotex.web.Api.Parameters;
using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Service.Services.Implementacion.SecureNorm;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Api.Controllers.SecureNorm
{
    [Route("api/[controller]")]
    [ApiController]
    public class SNMejoraController : ControllerBase
    {
        private readonly ISNMejoraService _sNMejoraService;

        public SNMejoraController(ISNMejoraService sNMejoraService)
        {
            _sNMejoraService = sNMejoraService;
        }

        [HttpPost]
        [Route("postMejoraMnto")]
        public async Task<IActionResult> postMejoraMnto([FromBody] SNMejoraParameter parametros)
        {
            SN_Mejora mejora = new SN_Mejora
            {
                Codigo = parametros.Codigo,
                Fuente = parametros.Fuente ?? parametros.Herramienta,
                Codigo_Proceso = parametros.Codigo_Proceso,
                Descripcion = parametros.Descripcion,
                Responsable = parametros.Responsable,
                Fecha_Inicio = parametros.Fecha_Inicio,
                Fecha_Fin_Estimada = parametros.Fecha_Fin_Estimada,
                Estado = parametros.Estado,
                Sede = parametros.Sede,
                Herramienta = parametros.Herramienta,
                Proveniente = parametros.Proveniente,
                Cumplimiento = parametros.Cumplimiento,
                Archivo = parametros.Archivo,
                Usuario_Registro = parametros.Usuario_Registro ?? "SISTEMAS"
            };

            var result = await _sNMejoraService.Mnto(mejora, parametros.Accion!);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListadoMejoras")]
        public async Task<IActionResult> getListadoMejoras(string? sFiltro)
        {
            var result = await _sNMejoraService.Listado(sFiltro ?? "");
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("uploadArchivo")]
        public async Task<IActionResult> UploadArchivo(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { success = false, message = "No se proporcionó ningún archivo." });
            }

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "mejoras");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var originalFileName = Path.GetFileName(file.FileName);
            var uniqueFileName = $"{Guid.NewGuid().ToString("N").Substring(0, 8)}_{originalFileName}";
            var filePath = Path.Combine(folderPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var relativeUrl = $"/uploads/mejoras/{uniqueFileName}";
            return Ok(new { success = true, fileName = uniqueFileName, originalName = originalFileName, filePath = relativeUrl });
        }

        [HttpGet]
        [Route("downloadArchivo")]
        public IActionResult DownloadArchivo(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return BadRequest("Nombre de archivo no válido.");
            }

            var cleanFileName = Path.GetFileName(fileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "mejoras", cleanFileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("El archivo especificado no existe en el servidor.");
            }

            var bytes = System.IO.File.ReadAllBytes(filePath);
            var ext = Path.GetExtension(cleanFileName).ToLower();
            var contentType = ext switch
            {
                ".pdf" => "application/pdf",
                ".doc" or ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".xls" or ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                _ => "application/octet-stream"
            };

            return File(bytes, contentType, cleanFileName);
        }
    }
}

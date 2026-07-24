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
    public class SNManualController : ControllerBase
    {
        private readonly ISNManualService _sNManualService;

        public SNManualController(ISNManualService sNManualService)
        {
            _sNManualService = sNManualService;
        }

        [HttpPost]
        [Route("postManualMnto")]
        public async Task<IActionResult> postManualMnto([FromBody] SNManualParameter parametros)
        {
            SN_Manual manual = new SN_Manual
            {
                Id_Manual = parametros.Id_Manual ?? 0,
                Codigo = parametros.Codigo,
                Titulo = parametros.Titulo,
                Subtitulo = parametros.Subtitulo,
                Descripcion = parametros.Descripcion,
                Autor = parametros.Autor,
                Fecha_Publicacion = parametros.Fecha_Publicacion,
                Version = parametros.Version,
                Color = parametros.Color,
                Icono = parametros.Icono,
                Archivo = parametros.Archivo,
                Usuario_Registro = parametros.Usuario_Registro ?? "SISTEMAS"
            };

            var result = await _sNManualService.Mnto(manual, parametros.Accion!);
            if (result!.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListadoManuales")]
        public async Task<IActionResult> getListadoManuales(string? sFiltro)
        {
            var result = await _sNManualService.Listado(sFiltro ?? "");
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("uploadManual")]
        public async Task<IActionResult> uploadManual(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest(new { success = false, message = "No se ha seleccionado ningún archivo PDF." });
                }

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "manuales");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return Ok(new
                {
                    success = true,
                    message = "Archivo PDF de manual subido correctamente.",
                    fileName = fileName,
                    filePath = $"/uploads/manuales/{fileName}"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error al subir manual: " + ex.Message });
            }
        }

        [HttpGet]
        [Route("downloadManual")]
        public IActionResult downloadManual(string fileName)
        {
            try
            {
                if (string.IsNullOrEmpty(fileName))
                {
                    return BadRequest("Nombre de archivo no válido.");
                }

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "manuales", fileName);
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound(new { success = false, message = "El manual no existe en el servidor." });
                }

                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                var contentType = "application/pdf";

                return File(fileBytes, contentType, fileName);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}

using ic.backend.precotex.web.Api.Parameters;
using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Service.Services.Implementacion.SecureNorm;
using ic.backend.precotex.web.Service.Services.SecureNorm;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ic.backend.precotex.web.Api.Controllers.SecureNorm
{
    [Route("api/[controller]")]
    [ApiController]
    public class SNDocumentosControladosController : ControllerBase
    {
        private readonly ISNDocumentosControladosService _sNDocumentosControladosService;
        public SNDocumentosControladosController(ISNDocumentosControladosService sNDocumentosControladosService)
        {
            _sNDocumentosControladosService = sNDocumentosControladosService;
        }


        [HttpGet]
        [Route("getListadoDocumentosControlados")]
        public async Task<IActionResult> GetListadoDocumentosControlados(string sCodigo_Organizacion = "001", string sCodigo_Sede = "001", string sCodigo_Puesto = "", string sCodigo_Proceso = "")
        {
            var result = await _sNDocumentosControladosService.Listado(sCodigo_Organizacion, sCodigo_Sede, sCodigo_Puesto, sCodigo_Proceso);
            if (result != null && result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postProcesoMnto")]
        public async Task<IActionResult> postProcesoMnto([FromBody] SNDocumentosControladosParameter parametros)
        {
            SN_Documentos_Controlados documento = new SN_Documentos_Controlados
            {


                Codigo_Documentos_Controlados = parametros.Codigo_Documentos_Controlados ?? "",
                Codigo_Proceso = parametros.Codigo_Proceso,
                Codigo_Carpeta_Control = parametros.Codigo_Carpeta_Control ?? "",
                Codigo_Normas = parametros.Codigo_Normas ?? "",
                Codigo_Tiempo_Conservacion = parametros.Codigo_Tiempo_Conservacion ?? "",
                Codigo_Tipo_Descarga = parametros.Codigo_Tipo_Descarga ?? "",
                Denominacion = parametros.Denominacion ?? "",
                Codigo_Documento = parametros.Codigo_Documento ?? "",
                Version_Documento = parametros.Version_Documento ?? "",
                Ruta_Adjunto = parametros.Ruta_Adjunto ?? "",
                Descripcion = parametros.Descripcion ?? "",
                bRegistroAsociado = parametros.bRegistroAsociado,
                bRequiereRevision = parametros.bRequiereRevision,
                Flg_Estado = parametros.Flg_Estado,
                Fec_Vencimiento = !string.IsNullOrEmpty(parametros.Fec_Vencimiento) && DateTime.TryParse(parametros.Fec_Vencimiento, out var fVenc) ? fVenc : null,
                Flg_Activo = parametros.Flg_Activo,
                Cod_Usuario = parametros.Cod_Usuario ?? ""
            };

            var result = await _sNDocumentosControladosService.ProcesoMnto(documento, parametros.Accion!);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
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

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "documentos_controlados");
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

            var relativeUrl = $"/uploads/documentos_controlados/{uniqueFileName}";
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
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "documentos_controlados", cleanFileName);

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

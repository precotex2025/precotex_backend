using ic.backend.precotex.web.Api.Parameters;
using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Service.common;
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
    public class SNProveedorController : ControllerBase
    {
        private readonly ISNProveedorService _sNProveedorService;
        
        public SNProveedorController(ISNProveedorService sNProveedorService)
        {
            _sNProveedorService = sNProveedorService;
        }

        private IActionResult HandleList<T>(ServiceResponseList<T>? result)
        {
            if (result == null)
            {
                return BadRequest(new { Success = false, Message = "No se obtuvo respuesta del servicio." });
            }

            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            return result.CodeResult == StatusCodes.Status500InternalServerError
                ? StatusCode(StatusCodes.Status500InternalServerError, result)
                : BadRequest(result);
        }

        private IActionResult HandleItem<T>(ServiceResponse<T>? result)
        {
            if (result == null)
            {
                return BadRequest(new { Success = false, Message = "No se obtuvo respuesta del servicio." });
            }

            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            return result.CodeResult == StatusCodes.Status500InternalServerError
                ? StatusCode(StatusCodes.Status500InternalServerError, result)
                : BadRequest(result);
        }

        // =========================================================================
        // 1. CATÁLOGO MAESTRO DE PROVEEDORES
        // =========================================================================

        [HttpGet]
        [Route("getListadoProveedores")]
        public async Task<IActionResult> getListadoProveedores(
            [FromQuery] int numCiclo = 0,
            [FromQuery] int idArea = 0,
            [FromQuery] string? sFiltro = "",
            [FromQuery] int soloActivos = 1)
        {
            var result = await _sNProveedorService.Listado(numCiclo, idArea, sFiltro ?? string.Empty, soloActivos);
            return HandleList(result);
        }

        [HttpGet]
        [Route("getHistorialCambiosProveedores")]
        public async Task<IActionResult> getHistorialCambiosProveedores([FromQuery] int top = 50)
        {
            var result = await _sNProveedorService.HistorialCambios(top);
            return HandleList(result);
        }

        [HttpPost]
        [Route("postProveedorMnto")]
        public async Task<IActionResult> postProveedorMnto([FromBody] SNProveedorParameter parametros)
        {
            if (parametros == null)
            {
                return BadRequest(new { Success = false, Message = "Los parámetros enviados son nulos." });
            }

            var proveedor = new SN_Proveedor
            {
                Id_Proveedor = parametros.Id_Proveedor,
                Ruc = parametros.Ruc,
                Razon_Social = parametros.Razon_Social ?? string.Empty,
                Bien_Servicio = parametros.Bien_Servicio,
                Tipo_Proveedor = parametros.Tipo_Proveedor,
                Proceso = parametros.Proceso,
                Ids_Areas = parametros.Ids_Areas,
                Contacto_Nombre = parametros.Contacto_Nombre,
                Contacto_Telefono = parametros.Contacto_Telefono,
                Contacto_Correo = parametros.Contacto_Correo,
                Flg_Homologado = parametros.Flg_Homologado,
                Cod_Usuario = parametros.Cod_Usuario ?? "SISTEMAS"
            };

            var result = await _sNProveedorService.Mnto(proveedor, parametros.Accion ?? "I");
            return HandleItem(result);
        }

        // =========================================================================
        // 2. EVALUACIONES FOR-LOG-LOG-003
        // =========================================================================

        [HttpGet]
        [Route("getListadoEvaluaciones")]
        public async Task<IActionResult> getListadoEvaluaciones(
            [FromQuery] int numCiclo = 0,
            [FromQuery] int idArea = 0,
            [FromQuery] int idProveedor = 0,
            [FromQuery] byte numSemestre = 0)
        {
            var result = await _sNProveedorService.ListadoEvaluaciones(numCiclo, idArea, idProveedor, numSemestre);
            return HandleList(result);
        }

        [HttpPost]
        [Route("postGuardarEvaluacion")]
        public async Task<IActionResult> postGuardarEvaluacion([FromBody] SNProveedorEvaluacionParameter parametros)
        {
            if (parametros == null)
            {
                return BadRequest(new { Success = false, Message = "Los parámetros de evaluación son nulos." });
            }

            var evaluacion = new SN_Proveedor_Evaluacion
            {
                Id_Proveedor = parametros.Id_Proveedor,
                Id_Area = parametros.Id_Area,
                Cod_Evaluador = parametros.Cod_Evaluador ?? string.Empty,
                Fec_Evaluacion = parametros.Fec_Evaluacion ?? DateTime.Today,
                Nota_Calidad = parametros.Nota_Calidad,
                Flg_Na_Calidad = parametros.Flg_Na_Calidad,
                Obs_Calidad = parametros.Obs_Calidad,
                Nota_Tiempos = parametros.Nota_Tiempos,
                Flg_Na_Tiempos = parametros.Flg_Na_Tiempos,
                Obs_Tiempos = parametros.Obs_Tiempos,
                Nota_Seguridad = parametros.Nota_Seguridad,
                Flg_Na_Seguridad = parametros.Flg_Na_Seguridad,
                Obs_Seguridad = parametros.Obs_Seguridad,
                Nota_Atencion = parametros.Nota_Atencion,
                Flg_Na_Atencion = parametros.Flg_Na_Atencion,
                Obs_Atencion = parametros.Obs_Atencion,
                Nota_Precio = parametros.Nota_Precio,
                Flg_Na_Precio = parametros.Flg_Na_Precio,
                Obs_Precio = parametros.Obs_Precio,
                Des_Acciones = parametros.Des_Acciones,
                Cod_Usuario = parametros.Cod_Usuario ?? "SISTEMAS"
            };

            var result = await _sNProveedorService.GuardarEvaluacion(evaluacion);
            return HandleItem(result);
        }

        // =========================================================================
        // 3. CONSOLIDADO ANUAL PONDERADO
        // =========================================================================

        [HttpGet]
        [Route("getConsolidadoAnual")]
        public async Task<IActionResult> getConsolidadoAnual(
            [FromQuery] int numCiclo = 0,
            [FromQuery] int idProveedor = 0,
            [FromQuery] byte numSemestre = 0)
        {
            var result = await _sNProveedorService.ConsolidadoAnual(numCiclo, idProveedor, numSemestre);
            return HandleItem(result);
        }

        // =========================================================================
        // 4. INCIDENTES OPERACIONALES
        // =========================================================================

        [HttpGet]
        [Route("getListadoIncidentes")]
        public async Task<IActionResult> getListadoIncidentes(
            [FromQuery] int idProveedor = 0,
            [FromQuery] int idArea = 0,
            [FromQuery] int numCiclo = 0)
        {
            var result = await _sNProveedorService.ListadoIncidentes(idProveedor, idArea, numCiclo);
            return HandleList(result);
        }

        [HttpPost]
        [Route("postIncidenteMnto")]
        public async Task<IActionResult> postIncidenteMnto([FromBody] SNProveedorIncidenteParameter parametros)
        {
            if (parametros == null)
            {
                return BadRequest(new { Success = false, Message = "Los parámetros del incidente son nulos." });
            }

            var incidente = new SN_Proveedor_Incidente
            {
                Id_Incidente = parametros.Id_Incidente,
                Id_Proveedor = parametros.Id_Proveedor,
                Id_Area = parametros.Id_Area,
                Fec_Incidente = parametros.Fec_Incidente ?? DateTime.Today,
                Des_Producto = parametros.Des_Producto,
                Des_Incidente = parametros.Des_Incidente ?? string.Empty,
                Des_Acciones = parametros.Des_Acciones,
                Flg_Cerrado = parametros.Flg_Cerrado,
                Fec_Cierre = parametros.Fec_Cierre,
                Cod_Usuario_Reg = parametros.Cod_Usuario ?? "SISTEMAS"
            };

            var result = await _sNProveedorService.MntoIncidente(incidente, parametros.Accion ?? "I");
            return HandleItem(result);
        }

        // =========================================================================
        // 5. DOCUMENTOS SSOMA Y TIPOS DE DOCUMENTO
        // =========================================================================

        [HttpGet]
        [Route("getListadoDocumentos")]
        public async Task<IActionResult> getListadoDocumentos(
            [FromQuery] int idProveedor = 0,
            [FromQuery] int idTipoDoc = 0)
        {
            var result = await _sNProveedorService.ListadoDocumentos(idProveedor, idTipoDoc);
            return HandleList(result);
        }

        [HttpPost]
        [Route("postDocumentoMnto")]
        public async Task<IActionResult> postDocumentoMnto([FromBody] SNProveedorDocumentoParameter parametros)
        {
            if (parametros == null)
            {
                return BadRequest(new { Success = false, Message = "Los parámetros del documento son nulos." });
            }

            var documento = new SN_Proveedor_Documento
            {
                Id_Documento = parametros.Id_Documento,
                Id_Proveedor = parametros.Id_Proveedor,
                Id_Tipo_Doc = parametros.Id_Tipo_Doc,
                Ref_Persona = parametros.Ref_Persona,
                Num_Documento = parametros.Num_Documento,
                Fec_Emision = parametros.Fec_Emision,
                Fec_Vencimiento = parametros.Fec_Vencimiento ?? DateTime.Today,
                Ruta_Adjunto = parametros.Ruta_Adjunto,
                Observaciones = parametros.Observaciones,
                Cod_Usuario = parametros.Cod_Usuario ?? "SISTEMAS"
            };

            var result = await _sNProveedorService.MntoDocumento(documento, parametros.Accion ?? "I");
            return HandleItem(result);
        }

        [HttpGet]
        [Route("getListadoTiposDocumento")]
        public async Task<IActionResult> getListadoTiposDocumento()
        {
            var result = await _sNProveedorService.ListadoTiposDocumento();
            return HandleList(result);
        }

        [HttpPost]
        [Route("uploadDocumentoSSOMA")]
        public async Task<IActionResult> uploadDocumentoSSOMA(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { Success = false, Message = "No se proporcionó ningún archivo." });
            }

            var originalFileName = Path.GetFileName(file.FileName);
            var ext = Path.GetExtension(originalFileName).ToLowerInvariant();
            if (ext != ".pdf" && ext != ".jpg" && ext != ".jpeg" && ext != ".png" && ext != ".xlsx" && ext != ".docx")
            {
                return BadRequest(new { Success = false, Message = "Formato de archivo no permitido. Use PDF, imagen u Office." });
            }

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "proveedores");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var uniqueFileName = $"{Guid.NewGuid():N}"[..8] + $"_{originalFileName}";
            var filePath = Path.Combine(folderPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var relativeUrl = $"/uploads/proveedores/{uniqueFileName}";
            return Ok(new { Success = true, FileName = uniqueFileName, OriginalName = originalFileName, FilePath = relativeUrl });
        }

        [HttpGet]
        [Route("downloadDocumentoSSOMA")]
        public IActionResult downloadDocumentoSSOMA([FromQuery] string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return BadRequest(new { Success = false, Message = "Nombre de archivo no válido." });
            }

            var cleanFileName = Path.GetFileName(fileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "proveedores", cleanFileName);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound(new { Success = false, Message = "El archivo especificado no existe en el servidor." });
            }

            var bytes = System.IO.File.ReadAllBytes(filePath);
            var ext = Path.GetExtension(cleanFileName).ToLowerInvariant();
            var contentType = ext switch
            {
                ".pdf" => "application/pdf",
                ".png" => "image/png",
                ".jpg" or ".jpeg" => "image/jpeg",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                _ => "application/octet-stream"
            };

            return File(bytes, contentType, cleanFileName);
        }

        // =========================================================================
        // 6. ÁREAS EVALUADORAS (PESOS Y UMBRALES)
        // =========================================================================

        [HttpGet]
        [Route("getListadoAreasEvaluadoras")]
        [Route("getListadoConfigPeso")]
        public async Task<IActionResult> getListadoAreasEvaluadoras([FromQuery] bool soloActivos = false)
        {
            var result = await _sNProveedorService.ListadoConfigPeso(soloActivos);
            return HandleList(result);
        }

        [HttpPost]
        [Route("postGuardarAreaEvaluadora")]
        [Route("postGuardarConfigPeso")]
        public async Task<IActionResult> postGuardarAreaEvaluadora([FromBody] SNProveedorConfigPesoParameter parametros)
        {
            if (parametros == null || string.IsNullOrWhiteSpace(parametros.Des_Area))
            {
                return BadRequest(new { Success = false, Message = "Debe especificar el nombre del área a configurar." });
            }

            var config = new SN_Proveedor_ConfigPeso
            {
                Id_Area = parametros.Id_Area,
                Des_Area = parametros.Des_Area.Trim(),
                Num_Peso = parametros.Num_Peso,
                Num_Umbral_Minimo = parametros.Num_Umbral_Minimo,
                Flg_Activo = parametros.Flg_Activo,
                Cod_Usuario = parametros.Cod_Usuario ?? "SISTEMAS"
            };

            var result = await _sNProveedorService.GuardarConfigPeso(config);
            return HandleItem(result);
        }
    }
}

using ic.backend.precotex.web.Api.Parameters;
using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Service.Services.Implementacion.SecureNorm;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Api.Controllers.SecureNorm
{
    [ApiController]
    [Route("api/[controller]")]
    public class SNReqLegalController : ControllerBase
    {
        private readonly ISNReqLegalService _service;
        //private readonly ILogger<SNReqLegalController> _logger;

        public SNReqLegalController(
            ISNReqLegalService service,
            ILogger<SNReqLegalController> logger)
        {
            _service = service;
            //_logger = logger;
        }


        /// <summary>
        /// Obtiene el listado de requisitos legales y normativos con filtro opcional.
        /// GET: api/SNReqLegal/getListadoReqLegal?sFiltro=SST
        /// </summary>
        [HttpGet("getListadoReqLegal")]
        public async Task<IActionResult> GetListadoReqLegal([FromQuery] string sFiltro = "")
        {
            try
            {
                var data = await _service.Listado(sFiltro);
                return Ok(data);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error al obtener listado de requisitos legales");
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Mantenimiento de Requisitos Legales (Insertar, Editar o Eliminar)
        /// POST: api/SNReqLegal/postReqLegalMnto
        /// </summary>
        [HttpPost("postReqLegalMnto")]
        [HttpPost("postProcesoMntoReqLegal")]
        public async Task<IActionResult> PostReqLegalMnto([FromBody] SNReqLegalParameter request)
        {
            if (request == null)
            {
                return BadRequest(new { success = false, message = "Los datos de la solicitud no son válidos." });
            }

            try
            {
                SN_Req_Legal reqLegal = new SN_Req_Legal
                {
                    Id = request.Id > 0 ? request.Id : request.Id_Req,
                    Id_Req = request.Id > 0 ? request.Id : request.Id_Req,
                    Codigo = request.Codigo,
                    Item = request.Item,
                    Requisito = request.Requisito,
                    Tema = request.Tema,
                    Ambito = request.Ambito,
                    Tipo = request.Tipo,
                    Norma = request.Norma,
                    Articulo = request.Articulo,
                    Entidad = request.Entidad,
                    Obligacion = request.Obligacion,
                    Evidenciadoc = request.Evidenciadoc,
                    Estado = request.Estado,
                    Responsable = request.Responsable,
                    Frecuencia = request.Frecuencia,
                    Evaluacion = request.Evaluacion,
                    Proxeval = request.Proxeval,
                    Vencimiento = request.Vencimiento,
                    Observaciones = request.Observaciones,
                    Evidencia = request.Evidencia,
                    Usuario_Registro = request.Usuario ?? request.Usuario_Registro ?? "SISTEMAS"
                };

                string accion = request.Accion ?? (reqLegal.Id > 0 ? "U" : "I");
                var data = await _service.Mnto(reqLegal, accion);
                return Ok(data);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error al procesar mantenimiento de requisito legal");
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}

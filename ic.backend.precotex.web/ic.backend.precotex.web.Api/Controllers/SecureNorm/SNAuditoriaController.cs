using ic.backend.precotex.web.Api.Parameters;
using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Service.Services.Implementacion.SecureNorm;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Api.Controllers.SecureNorm
{
    [Route("api/[controller]")]
    [ApiController]
    public class SNAuditoriaController : ControllerBase
    {
        private readonly ISNAuditoriaService _sNAuditoriaService;

        public SNAuditoriaController(ISNAuditoriaService sNAuditoriaService)
        {
            _sNAuditoriaService = sNAuditoriaService;
        }

        [HttpPost]
        [Route("postProcesoMntoAuditoria")]
        public async Task<IActionResult> postProcesoMntoAuditoria([FromBody] SNAuditoriaParameter parametros)
        {
            SN_Auditoria auditoria = new SN_Auditoria
            {
                Codigo_Auditoria = parametros.Codigo_Auditoria ?? "",
                Tipo = parametros.Tipo ?? "",
                Norma = parametros.Norma ?? "",
                Responsable = parametros.Responsable ?? "",
                Areas = parametros.Areas ?? "",
                Fecha_Inicio = parametros.Fecha_Inicio,
                Fecha_Fin = parametros.Fecha_Fin,
                Frecuencia = parametros.Frecuencia ?? "",
                Alcance = parametros.Alcance ?? "",
                Estado = parametros.Estado ?? "Programada",
                Usuario_Registro = parametros.Cod_Usuario ?? "SISTEMAS"
            };

            var result = await _sNAuditoriaService.ProcesoMnto(auditoria, parametros.Accion!);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListadoAuditorias")]
        public async Task<IActionResult> getListadoAuditorias(string? sFiltro)
        {
            var result = await _sNAuditoriaService.Listado(sFiltro ?? "");
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }
    }
}

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
    public class SNReqLegalController : ControllerBase
    {
        private readonly ISNReqLegalService _sNReqLegalService;

        public SNReqLegalController(ISNReqLegalService sNReqLegalService)
        {
            _sNReqLegalService = sNReqLegalService;
        }

        [HttpPost]
        [Route("postReqLegalMnto")]
        public async Task<IActionResult> postReqLegalMnto([FromBody] SNReqLegalParameter parametros)
        {
            SN_Req_Legal reqLegal = new SN_Req_Legal
            {
                Codigo = parametros.Codigo,
                Requisito = parametros.Requisito,
                Ambito = parametros.Ambito,
                Tipo = parametros.Tipo,
                Norma = parametros.Norma,
                Entidad = parametros.Entidad,
                Obligacion = parametros.Obligacion,
                Estado = parametros.Estado,
                Responsable = parametros.Responsable,
                Evaluacion = parametros.Evaluacion,
                Proxeval = parametros.Proxeval,
                Vencimiento = parametros.Vencimiento,
                Evidencia = parametros.Evidencia,
                Usuario_Registro = parametros.Usuario_Registro ?? "SISTEMAS"
            };

            var result = await _sNReqLegalService.Mnto(reqLegal, parametros.Accion!);
            if (result!.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListadoReqLegal")]
        public async Task<IActionResult> getListadoReqLegal(string? sFiltro)
        {
            var result = await _sNReqLegalService.Listado(sFiltro ?? "");
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

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
    public class SNObjetivoController : ControllerBase
    {
        private readonly ISNObjetivoService _sNObjetivoService;

        public SNObjetivoController(ISNObjetivoService sNObjetivoService)
        {
            _sNObjetivoService = sNObjetivoService;
        }

        [HttpPost]
        [Route("postObjetivoMnto")]
        public async Task<IActionResult> postObjetivoMnto([FromBody] SNObjetivoParameter parametros)
        {
            SN_Objetivo objetivo = new SN_Objetivo
            {
                Codigo = parametros.Codigo,
                Nombre = parametros.Nombre,
                Proceso = parametros.Proceso,
                Meta = parametros.Meta ?? 0,
                Usuario_Registro = parametros.Usuario_Registro ?? "SISTEMAS"
            };

            var result = await _sNObjetivoService.Mnto(objetivo, parametros.Accion!);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListadoObjetivos")]
        public async Task<IActionResult> getListadoObjetivos(string? sFiltro)
        {
            var result = await _sNObjetivoService.Listado(sFiltro ?? "");
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListadoObjetivoMediciones")]
        public async Task<IActionResult> getListadoObjetivoMediciones(int? idObjetivo, string? sFiltro)
        {
            var result = await _sNObjetivoService.ListadoMediciones(idObjetivo, sFiltro ?? "");
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postProcesoMntoObjetivoMedicion")]
        public async Task<IActionResult> postProcesoMntoObjetivoMedicion([FromBody] SNObjetivoMedicionParameter parametros)
        {
            SN_Objetivo_Medicion medicion = new SN_Objetivo_Medicion
            {
                Id_Obj_Medicion = parametros.Id_Obj_Medicion ?? 0,
                Id_Objetivo = parametros.Id_Objetivo ?? 0,
                Codigo_Objetivo = parametros.Codigo_Objetivo,
                Periodo = parametros.Periodo ?? "",
                Valor = parametros.Valor ?? 0,
                Usuario_Registro = parametros.Usuario_Registro ?? "SISTEMAS"
            };

            var result = await _sNObjetivoService.MntoMedicion(medicion, parametros.Accion!);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }
    }
}

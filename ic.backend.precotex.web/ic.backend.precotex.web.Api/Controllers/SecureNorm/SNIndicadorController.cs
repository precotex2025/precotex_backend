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
    public class SNIndicadorController : ControllerBase
    {
        private readonly ISNIndicadorService _sNIndicadorService;

        public SNIndicadorController(ISNIndicadorService sNIndicadorService)
        {
            _sNIndicadorService = sNIndicadorService;
        }

        [HttpPost]
        [Route("postIndicadorMnto")]
        public async Task<IActionResult> postIndicadorMnto([FromBody] SNIndicadorParameter parametros)
        {
            SN_Indicador indicador = new SN_Indicador
            {
                Codigo = parametros.Codigo,
                Nombre = parametros.Nombre,
                Codigo_Proceso = parametros.Codigo_Proceso,
                Unidad_Medida = parametros.Unidad_Medida,
                Meta = parametros.Meta ?? 0,
                Frecuencia = parametros.Frecuencia,
                Usuario_Registro = parametros.Usuario_Registro
            };

            var result = await _sNIndicadorService.Mnto(indicador, parametros.Accion!);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListadoIndicadores")]
        public async Task<IActionResult> getListadoIndicadores(string? sFiltro)
        {
            var result = await _sNIndicadorService.Listado(sFiltro ?? "");
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListadoIndicadorMediciones")]
        public async Task<IActionResult> getListadoIndicadorMediciones(int? idIndicador, string? sFiltro)
        {
            var result = await _sNIndicadorService.ListadoMediciones(idIndicador, sFiltro ?? "");
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postProcesoMntoIndicadorMedicion")]
        public async Task<IActionResult> postProcesoMntoIndicadorMedicion([FromBody] SNIndicadorMedicionParameter parametros)
        {
            SN_Indicador_Medicion medicion = new SN_Indicador_Medicion
            {
                Id_Medicion = parametros.Id_Medicion ?? 0,
                Id_Indicador = parametros.Id_Indicador ?? 0,
                Codigo_Indicador = parametros.Codigo_Indicador,
                Periodo = parametros.Periodo ?? "",
                Valor_Obtenido = parametros.Valor_Obtenido ?? 0,
                Comentario = parametros.Comentario ?? "",
                Usuario_Registro = parametros.Usuario_Registro ?? "SISTEMAS"
            };

            var result = await _sNIndicadorService.MntoMedicion(medicion, parametros.Accion!);
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

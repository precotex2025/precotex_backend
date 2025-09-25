using ic.backend.precotex.web.Service.Services.Implementacion.Laboratorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ic.backend.precotex.web.Api.Controllers.Laboratorio
{
    [Route("api/[controller]")]
    [ApiController]
    public class LbColaTrabajoController : ControllerBase
    {
        public readonly ILbColaTrabajoService _LbColaTrabajoService;

        public LbColaTrabajoController(ILbColaTrabajoService LbColaTrabajoService)
        {
            _LbColaTrabajoService = LbColaTrabajoService;
        }

        /*
            CABECERA
        */

        //OBTIENE LISTA DE COLA DE TRABAJO
        [HttpGet]
        [Route("getListaSDCPorEstado")]
        public async Task<IActionResult> getListaSDCPorEstado(string Flg_Est_Lab)
        {
            var result = await _LbColaTrabajoService.ListaSDCPorEstado(Flg_Est_Lab);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        /*
            DETALLE 
        */
        //OBTIENE COLORES DE UNA SDC 
        [HttpGet]
        [Route("getListaSDCDetalle")]
        public async Task<IActionResult> getListaSDCDetalle(int Corr_Carta)
        {
            var result = await _LbColaTrabajoService.ListaColoresSDC(Corr_Carta);
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

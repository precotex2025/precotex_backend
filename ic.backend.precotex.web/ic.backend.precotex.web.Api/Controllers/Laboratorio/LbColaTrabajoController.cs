using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.Laboratorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ic.backend.precotex.web.Api.Parameters;
using ic.backend.precotex.web.Entity.Entities.RetiroRepuestos;
using ic.backend.precotex.web.Entity.Entities.Laboratorio;


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
        public async Task<IActionResult> getListaSDCPorEstado(string Flg_Est_Lab, DateTime Fec_Ini, DateTime Fec_Fin)
        {
            var result = await _LbColaTrabajoService.ListaSDCPorEstado(Flg_Est_Lab, Fec_Ini, Fec_Fin);
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

        [HttpPost]
        [Route("postRegistrarDetalleColorSDC")]
        public async Task<IActionResult> RegistrarDetalleColorSDC([FromBody] LbColaTrabajoParameter_Detalle parametros)
        {
            Lb_ColTra_Det _lbColaTrabajoDet = new Lb_ColTra_Det
            {
                Corr_Carta = parametros.Corr_Carta,
                Sec = parametros.Sec
            };

            var result = await _LbColaTrabajoService.RegistrarDetalleColorSDC(_lbColaTrabajoDet);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getLlenarDesplegable")]
        public async Task<IActionResult> getLlenarDesplegable()
        {
            var result = await _LbColaTrabajoService.LlenarDesplegable();
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

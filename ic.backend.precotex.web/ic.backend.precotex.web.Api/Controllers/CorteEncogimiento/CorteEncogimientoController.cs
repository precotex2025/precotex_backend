
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.Services.Implementacion.CorteEncogimiento;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ic.backend.precotex.web.Api.Controllers.CorteEncogimiento
{
    [Route("api/[controller]")]
    [ApiController]
    public class CorteEncogimientoController : ControllerBase
    {
        private readonly ICorteEncogimientoService _ICorteEncogimiento;

        public CorteEncogimientoController(ICorteEncogimientoService txCorteEncogimientoService)
        {
            _ICorteEncogimiento = txCorteEncogimientoService;
        }

        [HttpGet]
        [Route("getListaCorteEncogimiento")]
        public async Task<IActionResult> getListaCorteEncogimiento()
        {
            var result = await _ICorteEncogimiento.ListaCorteEncogimiento();
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);

        }

        [HttpGet]
        [Route("getInsertCorteEncogimiento")]
        public async Task<IActionResult> getInsertCorteEncogimiento(string pTipo, string? pCod_Ordtra)
        {

            var result = await _ICorteEncogimiento.InsertCorteEncogimiento(pTipo, pCod_Ordtra);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListCorteEncogimientoDet")]
        public async Task<IActionResult> getListCorteEncogimientoDet(string pTipo, string? pNum_Secuencia, string? pCodPartida, decimal? pAncho_Antes_Lav,decimal? pAlto_Antes_Lav, decimal? pAncho_Despues_Lav, decimal? pAlto_Despues_Lav, decimal? pSesgadura)
        {

            var result = await _ICorteEncogimiento.ListCorteEncogimientoDet(pTipo, pNum_Secuencia, pCodPartida, pAncho_Antes_Lav, pAlto_Antes_Lav, pAncho_Despues_Lav, pAlto_Despues_Lav, pSesgadura);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getBuscarCorteEncogimiento")]
        public async Task<IActionResult> getBuscarCorteEncogimiento(string pCod_Ordtra)
        {

            var result = await _ICorteEncogimiento.BuscarCorteEncogimiento(pCod_Ordtra);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("getUpdateMedidasTablaMaestra")]
        public async Task<IActionResult> getUpdateMedidasTablaMaestra([FromBody] List<E_Corte_Encogimiento> pData)
        {

            var result = await _ICorteEncogimiento.UpdateMedidasTablaMaestra(pData);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getBuscarUsuario")]
        public async Task<IActionResult> getBuscarUsuario(string pUsuario)
        {

            var result = await _ICorteEncogimiento.BuscarUsuario(pUsuario);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

    }
}

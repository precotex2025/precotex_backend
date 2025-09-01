using ic.backend.precotex.web.Service.Services.Implementacion.Almacen;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ic.backend.precotex.web.Api.Controllers.Almacen
{
    [Route("api/[controller]")]
    [ApiController]
    public class TxBultoHiladoController : ControllerBase
    {
        private readonly ITxBultoHiladoService _txBultoHiladoService;

        public TxBultoHiladoController(ITxBultoHiladoService txBultoHiladoService)
        {
            _txBultoHiladoService = txBultoHiladoService;
        }

        [HttpGet]
        [Route("getBultoUbicacion")]
        public async Task<IActionResult> getBultoUbicacion(string sCodProveedor, string? sCodOrdProv, string? sNumSemana, string? sNomConera)
        {
            var result = await _txBultoHiladoService.ListaBultosUbicacion(sCodProveedor, sCodOrdProv, sNumSemana, sNomConera);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getProveedores")]
        public async Task<IActionResult> getProveedores()
        {
            var result = await _txBultoHiladoService.ListaProveedores();
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

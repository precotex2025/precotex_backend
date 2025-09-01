using ic.backend.precotex.web.Service.Services.Almacen;
using ic.backend.precotex.web.Service.Services.Implementacion.Almacen;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ic.backend.precotex.web.Api.Controllers.Almacen
{
    [Route("api/[controller]")]
    [ApiController]
    public class TxUbicacionController : ControllerBase
    {
        private readonly ITxUbicacionService _txUbicacionService;

        public TxUbicacionController(ITxUbicacionService txUbicacionService)
        {
            _txUbicacionService = txUbicacionService;
        }

        [HttpGet]
        [Route("getListaUbicacionByCode")]
        public async Task<IActionResult> getListaUbicacionByCode(string? Cod_Ubicacion)
        {
            var result = await _txUbicacionService.ListaByCodigoUbicacion(Cod_Ubicacion);
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

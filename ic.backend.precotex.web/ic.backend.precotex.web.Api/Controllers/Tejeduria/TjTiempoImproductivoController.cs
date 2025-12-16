using ic.backend.precotex.web.Service.Services.Implementacion.Tejeduria;
using ic.backend.precotex.web.Service.Services.Tejeduria;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ic.backend.precotex.web.Api.Controllers.Tejeduria
{
    [Route("api/[controller]")]
    [ApiController]
    public class TjTiempoImproductivoController : ControllerBase
    {

        private readonly ITjTiempoImproductivoService _tjTiempoImproductivoService;

        public TjTiempoImproductivoController(ITjTiempoImproductivoService tjTiempoImproductivoService)
        {
            _tjTiempoImproductivoService = tjTiempoImproductivoService;
        }

        [HttpGet]
        [Route("getObtieneTiempoImproductivoPendiente")]
        public async Task<IActionResult> getObtieneTiempoImproductivoPendiente(string? sCodMaquina)
        {
            var result = await _tjTiempoImproductivoService.ObtieneTiempoImproductivoPendiente(sCodMaquina);
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

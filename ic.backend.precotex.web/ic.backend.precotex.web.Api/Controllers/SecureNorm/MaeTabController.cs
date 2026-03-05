using ic.backend.precotex.web.Service.Services.Implementacion.SecureNorm;
using ic.backend.precotex.web.Service.Services.SecureNorm;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ic.backend.precotex.web.Api.Controllers.SecureNorm
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaeTabController : ControllerBase
    {
        private readonly IMaeTabService _maeTabService;

        public MaeTabController(IMaeTabService maeTabService)
        {
            _maeTabService = maeTabService;
        }

        [HttpGet]
        [Route("getListaMaeTab")]
        public async Task<IActionResult> getListaMaeTab(string? sCodigoTipo)
        {
            var result = await _maeTabService.Lista(sCodigoTipo!);
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

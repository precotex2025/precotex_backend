using ic.backend.precotex.web.Entity.Entities.SecureNorm.Parameters;
using ic.backend.precotex.web.Service.Services.Implementacion.SecureNorm;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Api.Controllers.SecureNorm
{
    [Route("api/[controller]")]
    [ApiController]
    public class SNRiesgoController : ControllerBase
    {
        private readonly ISNRiesgoService _service;

        public SNRiesgoController(ISNRiesgoService service)
        {
            _service = service;
        }

        [HttpGet("getListadoRiesgos")]
        public async Task<IActionResult> GetListadoRiesgos([FromQuery] string sFiltro = "")
        {
            var response = await _service.GetListadoRiesgos(sFiltro);
            return Ok(response);
        }

        [HttpPost("postProcesoMntoRiesgo")]
        public async Task<IActionResult> PostProcesoMntoRiesgo([FromBody] SNRiesgoParameter request)
        {
            var response = await _service.PostProcesoMntoRiesgo(request);
            return Ok(response);
        }
    }
}

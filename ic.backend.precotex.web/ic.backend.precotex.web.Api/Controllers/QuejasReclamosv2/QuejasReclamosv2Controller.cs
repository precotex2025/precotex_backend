
using ic.backend.precotex.web.Service.Services.Implementacion.QuejasReclamosv2Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ic.backend.precotex.web.Api.Controllers.QuejasReclamosv2
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuejasReclamosv2Controller : ControllerBase
    {

        private readonly IQuejasReclamosv2Service _IClientes;
        private readonly IWebHostEnvironment _environment;

        public QuejasReclamosv2Controller(IQuejasReclamosv2Service txtIClientes, IWebHostEnvironment environment)
        {
            _IClientes = txtIClientes;
            _environment = environment;
        }

        [HttpGet]
        [Route("getObtenerEstado")]
        public async Task<IActionResult> obtenerEstado()
        {
            var result = await _IClientes.ObtenerEstado();
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

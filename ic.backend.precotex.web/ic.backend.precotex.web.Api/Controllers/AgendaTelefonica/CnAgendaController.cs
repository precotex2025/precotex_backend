using ic.backend.precotex.web.Service.Services.Implementacion.AgendaTelefonica;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ic.backend.precotex.web.Api.Controllers.AgendaTelefonica
{
    [Route("api/[controller]")]
    [ApiController]
    public class CnAgendaController : ControllerBase
    {
        public readonly ICnAgendaService _service;

        public CnAgendaController(ICnAgendaService service)
        {
            _service = service;
        }


        [HttpGet]
        [Route("getObtenerNumeros")]
        public async Task<IActionResult> ObtenerNumeros()
        {
            var result = await _service.ObtenerNumeros();
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

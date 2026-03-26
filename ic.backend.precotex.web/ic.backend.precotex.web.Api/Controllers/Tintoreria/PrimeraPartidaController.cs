
using ic.backend.precotex.web.Api.Parameters;
using ic.backend.precotex.web.Entity.Entities.Tintoreria;
using ic.backend.precotex.web.Service.Services.Implementacion.Tintoreria;
using ic.backend.precotex.web.Service.Services.Tintoreria;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ic.backend.precotex.web.Api.Controllers.Tintoreria
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrimeraPartidaController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IPrimeraPartidaService _primeraPartidaService;

        public PrimeraPartidaController(HttpClient httpClient, IPrimeraPartidaService primeraPartidaService)
        {
            _httpClient = httpClient;
            _primeraPartidaService = primeraPartidaService;
        }


        [HttpPost]
        [Route("postAuditoriaPrimeraPartida")]
        public async Task<IActionResult> postAuditoriaPrimeraPartida([FromBody] AuditoriaPrimeraPartida parameters)
        {

            var result = await _primeraPartidaService.ProcesoMnto(parameters);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListaPrimeraPartida")]
        public async Task<IActionResult> getListaPrimeraPartida(DateTime FecIni, DateTime FecFin)
        {
            var result = await _primeraPartidaService.Lista(FecIni, FecFin);
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

using ic.backend.precotex.web.Entity.Entities.Calidad;
using ic.backend.precotex.web.Service.Services.Implementacion.Calidad;
using Microsoft.AspNetCore.Mvc;

namespace ic.backend.precotex.web.Api.Controllers.Calidad
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoConformidadesController : ControllerBase
    {
        private readonly INoConformidadesService _service;

        public NoConformidadesController(INoConformidadesService service)
        {
            _service = service;
        }

        [HttpGet("getInformesCabecera")]
        public async Task<IActionResult> GetInformesCabecera([FromQuery] string numInforme = "", [FromQuery] string fIni = "", [FromQuery] string fFin = "", [FromQuery] string partida = "")
        {
            try
            {
                var data = await _service.MostrarCabecera(numInforme, fIni, fFin, partida);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("getDatosInformeCalidad")]
        public async Task<IActionResult> GetDatosInformeCalidad([FromQuery] string tipo = "T", [FromQuery] string cod = "")
        {
            try
            {
                var data = await _service.ListarDatosInformeCalidad(tipo, cod);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("getPartida")]
        public async Task<IActionResult> GetPartida([FromQuery] string partida, [FromQuery] string tipo = "")
        {
            try
            {
                var data = await _service.MostrarPartida(partida, tipo);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("getInformeDetalle")]
        public async Task<IActionResult> GetInformeDetalle([FromQuery] string numInforme = "", [FromQuery] string partida = "")
        {
            try
            {
                var articulos = await _service.MostrarDetalle(numInforme, partida);
                var motivos = await _service.MostrarDetalleMotivo(numInforme, partida);
                return Ok(new { articulos, motivos });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("getEvolutivo")]
        public async Task<IActionResult> GetEvolutivo()
        {
            try
            {
                var data = await _service.MostrarEvolutivo();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("getReporteNoConformidad")]
        public async Task<IActionResult> GetReporteNoConformidad([FromQuery] string fIni = "", [FromQuery] string fFin = "")
        {
            try
            {
                var data = await _service.ReporteNoConformidad(fIni, fFin);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("guardarInforme")]
        public async Task<IActionResult> GuardarInforme([FromBody] InformeGuardarRequest req)
        {
            try
            {
                var resultado = await _service.GuardarInforme(req);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}

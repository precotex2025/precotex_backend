using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ic.backend.precotex.web.Entity.Entities.Calidad;
using ic.backend.precotex.web.Service.Services.Calidad;

namespace ic.backend.precotex.web.Api.Controllers.Calidad
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class NoConformidadesController : ControllerBase
    {
        private readonly INoConformidadesService _service;
        private readonly ILogger<NoConformidadesController> _logger;

        public NoConformidadesController(INoConformidadesService service, ILogger<NoConformidadesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("health")]
        public IActionResult Health() => Ok(new { status = "Healthy", timestamp = DateTime.Now });

        [HttpGet("getDatosInformeCalidad")]
        public IActionResult GetDatosInformeCalidad([FromQuery] string tipo, [FromQuery] string cod = "")
        {
            try
            {
                var data = _service.ListarDatosInformeCalidad(tipo, cod);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetDatosInformeCalidad");
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("getInformesCabecera")]
        public IActionResult GetInformesCabecera([FromQuery] string numInforme = "", [FromQuery] string fIni = "", [FromQuery] string fFin = "", [FromQuery] string partida = "")
        {
            try
            {
                var data = _service.MostrarCabecera(numInforme, fIni, fFin, partida);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetInformesCabecera");
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("getPartida")]
        public IActionResult GetPartida([FromQuery] string partida, [FromQuery] string tipo = "")
        {
            try
            {
                var data = _service.MostrarPartida(partida, tipo);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetPartida");
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("getInformeDetalle")]
        public IActionResult GetInformeDetalle([FromQuery] string numInforme = "", [FromQuery] string partida = "")
        {
            try
            {
                var data = _service.MostrarDetalle(numInforme, partida);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetInformeDetalle");
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("getInformeDetalleMotivo")]
        public IActionResult GetInformeDetalleMotivo([FromQuery] string numInforme, [FromQuery] string partida = "")
        {
            try
            {
                var data = _service.MostrarDetalleMotivo(numInforme, partida);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetInformeDetalleMotivo");
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("guardarInforme")]
        public IActionResult GuardarInforme([FromBody] InformeGuardarRequest request)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var result = _service.GuardarInforme(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GuardarInforme");
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
    }
}

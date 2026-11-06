using ic.backend.precotex.web.Entity.Entities.Almacen;
using ic.backend.precotex.web.Service.Services.Implementacion.Almacen;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Api.Controllers.Almacen
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControlUbicacionesController : ControllerBase
    {
        private readonly IControlUbicacionesService _controlUbicacionesService;

        public ControlUbicacionesController(IControlUbicacionesService controlUbicacionesService)
        {
            _controlUbicacionesService = controlUbicacionesService;
        }

        [HttpGet]
        [Route("getMuestraDetalleUbicacion")]
        public async Task<IActionResult> GetMuestraDetalleUbicacion(
            [FromQuery] string? desUbicacion, 
            [FromQuery] string? idRollo, 
            [FromQuery] string? codUsuario)
        {
            var result = await _controlUbicacionesService.ObtenerDetalleUbicacion(desUbicacion, idRollo, codUsuario);

            if (result != null && result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost]
        [Route("limpiarTemporalGuia")]
        public async Task<IActionResult> LimpiarTemporalGuia([FromQuery] string? codUsuario)
        {
            var result = await _controlUbicacionesService.LimpiarTemporalGuia(codUsuario);
            return Ok(result);
        }

        [HttpGet]
        [Route("consultarGuia")]
        public async Task<IActionResult> ConsultarGuia([FromQuery] string numGuia, [FromQuery] string? codUsuario)
        {
            var result = await _controlUbicacionesService.ConsultarGuia(numGuia, codUsuario);
            return Ok(result);
        }

        [HttpPost]
        [Route("insertarBultoGuia")]
        public async Task<IActionResult> InsertarBultoGuia([FromBody] ControlUbicacionLecturaRequest request)
        {
            var result = await _controlUbicacionesService.InsertarBultoGuia(request);
            return Ok(result);
        }

        [HttpPost]
        [Route("guardarUbicacionGuia")]
        public async Task<IActionResult> GuardarUbicacionGuia([FromBody] ControlUbicacionGuardarRequest request)
        {
            var result = await _controlUbicacionesService.GuardarUbicacionGuia(request);
            return Ok(result);
        }

        // ENDPOINTS DE REUBICACIÓN

        [HttpPost]
        [Route("limpiarTemporalReubicacion")]
        public async Task<IActionResult> LimpiarTemporalReubicacion([FromQuery] string? codUsuario)
        {
            var result = await _controlUbicacionesService.LimpiarTemporalReubicacion(codUsuario);
            return Ok(result);
        }

        [HttpGet]
        [Route("consultarReubicacion")]
        public async Task<IActionResult> ConsultarReubicacion([FromQuery] string? codUsuario)
        {
            var result = await _controlUbicacionesService.ConsultarReubicacion(codUsuario);
            return Ok(result);
        }

        [HttpPost]
        [Route("insertarBultoReubicacion")]
        public async Task<IActionResult> InsertarBultoReubicacion([FromBody] ControlUbicacionReubicacionLecturaRequest request)
        {
            var result = await _controlUbicacionesService.InsertarBultoReubicacion(request);
            return Ok(result);
        }

        [HttpPost]
        [Route("guardarReubicacion")]
        public async Task<IActionResult> GuardarReubicacion([FromBody] ControlUbicacionGuardarReubicacionRequest request)
        {
            var result = await _controlUbicacionesService.GuardarReubicacion(request);
            return Ok(result);
        }
    }
}

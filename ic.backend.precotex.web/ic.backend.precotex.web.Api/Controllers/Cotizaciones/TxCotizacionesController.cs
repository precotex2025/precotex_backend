using ic.backend.precotex.web.Service.Services.Implementacion.Cotizaciones;
using ic.backend.precotex.web.Service.Services.Implementacion.Laboratorio;
using ic.backend.precotex.web.Service.Services.Laboratorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ic.backend.precotex.web.Api.Controllers.Cotizaciones
{
    [Route("api/[controller]")]
    [ApiController]
    public class TxCotizacionesController : ControllerBase
    {

        public readonly ITxCotizacionesService _txCotizacionesService;

        public TxCotizacionesController(ITxCotizacionesService txCotizacionesService)
        {
            _txCotizacionesService = txCotizacionesService;
        }

        [HttpGet]
        [Route("getListarProcesosExportacion")]
        public async Task<IActionResult> getListarProcesosExportacion(string Pro_Cen_Cos)
        {
            var result = await _txCotizacionesService.ListarProcesosExportacion(Pro_Cen_Cos);
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

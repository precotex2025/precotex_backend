using ic.backend.precotex.web.Service.Services.Almacen;
using ic.backend.precotex.web.Service.Services.Implementacion.Almacen;
using iTextSharp.text.pdf.codec.wmf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ic.backend.precotex.web.Api.Controllers.Almacen
{
    [Route("api/[controller]")]
    [ApiController]
    public class TmpVisorPermanenciaTelaCrudaController : ControllerBase
    {
        private readonly ITmpVisorPermanenciaTelaCrudaService _tmpVisorPermanenciaTelaCrudaService;

        public TmpVisorPermanenciaTelaCrudaController(ITmpVisorPermanenciaTelaCrudaService tmpVisorPermanenciaTelaCrudaService)
        {
            _tmpVisorPermanenciaTelaCrudaService = tmpVisorPermanenciaTelaCrudaService;
        }

        [HttpGet]
        [Route("getPermanenciaTelaCruda")]
        public async Task<IActionResult> getPermanenciaTelaCruda(int anio)
        {
            var result = await _tmpVisorPermanenciaTelaCrudaService.ObtieneListaPermanenciaTelaCruda(anio);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getEstatusRequerimientoAlmacen")]
        public async Task<IActionResult> getEstatusRequerimientoAlmacen(string sEstado)
        {
            var result = await _tmpVisorPermanenciaTelaCrudaService.EstatusRequerimientoAlmacen(sEstado);
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

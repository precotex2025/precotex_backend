using ic.backend.precotex.web.Entity.Entities.QuejasReclamos;
using ic.backend.precotex.web.Service.Services.Implementacion.Cotizaciones;
using ic.backend.precotex.web.Service.Services.Implementacion.Laboratorio;
using ic.backend.precotex.web.Service.Services.Laboratorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Drives.Item.Items.Item.Workbook.TableRowOperationResultWithKey;
using Microsoft.Graph.Security.Labels.RetentionLabels.Item.RetentionEventType;

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

        [HttpGet]
        [Route("getRutaXCodTela")]
        public async Task<IActionResult> getRutaXCodTela(string Cod_Tela)
        {
            var result = await _txCotizacionesService.RutaXCodTela(Cod_Tela);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getRutaXCodTelaDetalle")]
        public async Task<IActionResult> getRutaXCodTelaDetalle(string Cod_Tela, string Cod_Ruta)
        {
            var result = await _txCotizacionesService.RutaXCodTelaDetalle(Cod_Tela, Cod_Ruta);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListaTelas")]
        public async Task<IActionResult> getListaTelas(string Cod_Tela)
        {
            var result = await _txCotizacionesService.ListaTelas(Cod_Tela);
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

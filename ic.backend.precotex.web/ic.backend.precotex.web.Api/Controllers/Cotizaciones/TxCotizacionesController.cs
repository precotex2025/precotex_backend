using ic.backend.precotex.web.Api.Parameters;
using ic.backend.precotex.web.Entity.Entities.Cotizaciones;
using ic.backend.precotex.web.Entity.Entities.Memorandum;
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
        public async Task<IActionResult> getListarProcesosExportacion(int Pro_Cen_Cos, string Tipo, string? Cod_Cliente_Tex, string Cod_Tela, string Cod_Ruta, string? Cod_Color)
        {
            var result = await _txCotizacionesService.ListarProcesosExportacion(Pro_Cen_Cos, Tipo, Cod_Cliente_Tex!, Cod_Tela, Cod_Ruta, Cod_Color);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarProcesosExportacionFooter")]
        public async Task<IActionResult> getListarProcesosExportacionFooter(int Pro_Cen_Cos)
        {
            var result = await _txCotizacionesService.ListarProcesosExportacionFooter(Pro_Cen_Cos);
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

        [HttpGet]
        [Route("getListaCentroCosto")]
        public async Task<IActionResult> getListaCentroCosto()
        {
            var result = await _txCotizacionesService.ListaCentroCosto();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }
            
            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postProcesoCotizacion")]
        public async Task<IActionResult> postProcesoCotizacion([FromBody] CotizacionesParameter parameters)
        {
            Tx_Cotizaciones_Cab _Coti = new Tx_Cotizaciones_Cab
            {
               IdCotizacion_Cab = parameters.IdCotizacion_Cab,
               Pro_Id  = parameters.Pro_Id,
               Cen_Cos_Cod  = parameters.Cen_Cos_Cod,
               Cod_Tipo  = parameters.Cod_Tipo,
               Cod_Cliente_Tex  = parameters.Cod_Cliente_Tex,
               Cod_Tela  = parameters.Cod_Tela,
               Cod_Ruta  = parameters.Cod_Ruta,
               Cod_Color  = parameters.Cod_Color,
               Cod_RecetaAcabado = parameters.Cod_RecetaAcabado,
               Flg_Estatus  = parameters.Flg_Estatus,
               Usu_Registro = parameters.Usu_Registro
            };
            var result = await _txCotizacionesService.ProcesoCotizacion(_Coti, parameters.Detalles!, parameters.Accion!);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getValidaColorExiste")]
        public async Task<IActionResult> getValidaColorExiste(string Cod_Color)
        {
            var result = await _txCotizacionesService.ValidaColorExiste(Cod_Color);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListaUnidadNegocio")]
        public async Task<IActionResult> getListaUnidadNegocio()
        {
            var result = await _txCotizacionesService.ListaUnidadNegocio();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListaIntensidad")]
        public async Task<IActionResult> getListaIntensidad(int Id_Unidad_NegocioKey)
        {
            var result = await _txCotizacionesService.ListaIntensidad(Id_Unidad_NegocioKey);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListaHiladoxTela")]
        public async Task<IActionResult> getListaHiladoxTela(string Cod_Tela)
        {
            var result = await _txCotizacionesService.ListaHiladoxTela(Cod_Tela);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListaUnidadNegocioTipo")]
        public async Task<IActionResult> getListaUnidadNegocioTipo(int Id_Unidad_NegocioKey)
        {
            var result = await _txCotizacionesService.ListaUnidadNegocioTipo(Id_Unidad_NegocioKey);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListaColoresXCliente")]
        public async Task<IActionResult> getListaColoresXCliente(string Cod_Cliente)
        {
            var result = await _txCotizacionesService.ListaColoresXCliente(Cod_Cliente);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListaPrecioXColor")]
        public async Task<IActionResult> getListaPrecioXColor(string Cod_Color)
        {
            var result = await _txCotizacionesService.ListaPrecioXColor(Cod_Color);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListaRecetasAntipilling")]
        public async Task<IActionResult> getListaRecetasAntipilling()
        {
            var result = await _txCotizacionesService.ListaRecetasAntipilling();
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

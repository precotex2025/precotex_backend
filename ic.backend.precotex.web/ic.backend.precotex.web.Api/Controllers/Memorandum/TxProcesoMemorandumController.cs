using ic.backend.precotex.web.Api.Parameters;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.Memorandum;
using ic.backend.precotex.web.Service.Services.Implementacion.Memorandum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PdfiumViewer;
using System.Drawing.Printing;
using iTextSharp.text.pdf;
using iTextSharp.text;
using ic.backend.precotex.web.Service.Services.Implementacion.DDT;
using ZXing;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.HelpCommon;

namespace ic.backend.precotex.web.Api.Controllers.Memorandum
{
    [Route("api/[controller]")]
    [ApiController]
    public class TxProcesoMemorandumController : ControllerBase
    {
        private readonly IHelpCommonService _IHelpCommonService;
        private readonly ITxProcesoMemorandumService _txProcesoMemorandumService;
        private readonly ITxUbicacionColgadorService _txUbicacionColgadorService;

        public TxProcesoMemorandumController(ITxProcesoMemorandumService txProcesoMemorandumService, ITxUbicacionColgadorService ITxUbicacionColgadorService, IHelpCommonService IHelpCommonService)
        {
            _txProcesoMemorandumService = txProcesoMemorandumService;
            _txUbicacionColgadorService = ITxUbicacionColgadorService;
            _IHelpCommonService = IHelpCommonService;
        }

        [HttpGet]
        [Route("getObtieneInformacionMemorandum")]
        public async Task<IActionResult> getObtieneInformacionMemorandum(DateTime FecIni, DateTime FecFin, string? NumMemo, string? codUsuario, string? CodPlantaGarita)
        {
            NumMemo = NumMemo ?? "";
            var result = await _txProcesoMemorandumService.ObtieneInformacionMemorandum(FecIni, FecFin, NumMemo, codUsuario, CodPlantaGarita);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getMateriales")]
        public async Task<IActionResult> getMateriales()
        {
            var result = await _txProcesoMemorandumService.Materiales();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getPlantas")]
        public async Task<IActionResult> getPlantas()
        {
            var result = await _txProcesoMemorandumService.Plantas();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postProcesoMntoMemorandum")]
        public async Task<IActionResult> postProcesoMntoMemorandum([FromBody] TxMemorandumRegistroParameter parameters)
        {
            Tx_Memorandum _txMemorandum = new Tx_Memorandum
            {
                Num_Memo = parameters.Num_Memo,
                Cod_Usuario_Emisor = parameters.Cod_Usuario_Emisor,
                Cod_Usuario_Receptor = parameters.Cod_Usuario_Receptor,
                Num_Planta_Origen = parameters.Num_Planta_Origen,
                Num_Planta_Destino = parameters.Num_Planta_Destino,
                Cod_Usuario_Seguridad_Emisor = parameters.Cod_Usuario_Seguridad_Emisor,
                Cod_Usuario_Seguridad_Receptor = parameters.Cod_Usuario_Seguridad_Receptor,
                Cod_Tipo_Memo = parameters.Cod_Tipo_Memo,
                Cod_Motivo_Memo = parameters.Cod_Motivo_Memo,
                //nuevos campos
                Cod_Tipo_Movimiento = parameters.Cod_Tipo_Movimiento,
                Datos_Externo = parameters.Datos_Externo,
                Direccion_Externo = parameters.Direccion_Externo
            };
            var result = await _txProcesoMemorandumService.ProcesoMntoMemorandum(_txMemorandum, parameters.Detalle!, parameters.Accion!);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getTipoMemorandum")]
        public async Task<IActionResult> getTipoMemorandum()
        {
            var result = await _txProcesoMemorandumService.TipoMemorandum();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getUsuario")]
        public async Task<IActionResult> getUsuario(string Cod_Trabajador, string Tip_Trabajador)
        {
            var result = await _txProcesoMemorandumService.Usuario(Cod_Trabajador, Tip_Trabajador);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getMotivoMemorandum")]
        public async Task<IActionResult> getMotivoMemorandum()
        {
            var result = await _txProcesoMemorandumService.Motivos();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtieneDetalleMemorandumByNumMemo")]
        public async Task<IActionResult> getObtieneDetalleMemorandumByNumMemo(string? NumMemo)
        {
            NumMemo = NumMemo ?? "";
            var result = await _txProcesoMemorandumService.ObtieneDetalleMemorandumByNumMemo(NumMemo);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postAvanzaEstadoMemorandum")]
        public async Task<IActionResult> postAvanzaEstadoMemorandum([FromBody] txMemorandumAvanzaParameter parameters)
        {

            var result = await _txProcesoMemorandumService.AvanzaEstadoMemorandum(parameters.Cod_Usuario, parameters.Num_Memo!, parameters.Observaciones!);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtenerPermisosMemorandum")]
        public async Task<IActionResult> getObtenerPermisosMemorandum(string sCodUsuario, string sNumMemo)
        {
            var result = await _txProcesoMemorandumService.ObtenerPermisosMemorandum(sCodUsuario, sNumMemo);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtenerRolUsuarioMemorandum")]
        public async Task<IActionResult> getObtenerRolUsuarioMemorandum([FromQuery] string sCodUsuario, [FromQuery] string sNumMemo)
        {
            var result = await _txProcesoMemorandumService.ObtenerRolUsuarioMemorandum(sCodUsuario, sNumMemo);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRevertirEstadoMemorandum")]
        public async Task<IActionResult> postRevertirEstadoMemorandum([FromBody] txMemorandumAvanzaParameter parameters)
        {

            var result = await _txProcesoMemorandumService.RevertirEstadoMemorandum(parameters.Cod_Usuario, parameters.Num_Memo!, parameters.Observaciones!);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }


        [HttpGet]
        [Route("getHistorialMovimientoMemorandum")]
        public async Task<IActionResult> getHistorialMovimientoMemorandum([FromQuery] string sNumMemo)
        {
            var result = await _txProcesoMemorandumService.HistorialMovimientoMemorandum(sNumMemo);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postDevolverMemorandum")]
        public async Task<IActionResult> postDevolverMemorandum([FromBody] txMemorandumDevolucionParameter parameters)
        {
            Tx_Memorandum _txMemorandum = new Tx_Memorandum
            {
                Num_Memo = parameters.Num_Memo,
                Cod_Usuario_Emisor = parameters.Cod_Usuario_Emisor,
                Cod_Usuario_Receptor = parameters.Cod_Usuario_Receptor,
                Num_Planta_Origen = parameters.Num_Planta_Origen,
                Num_Planta_Destino = parameters.Num_Planta_Destino,
                Cod_Usuario_Seguridad_Emisor = parameters.Cod_Usuario_Seguridad_Emisor,
                Cod_Usuario_Seguridad_Receptor = parameters.Cod_Usuario_Seguridad_Receptor,
                Cod_Tipo_Memo = parameters.Cod_Tipo_Memo,
                Cod_Motivo_Memo = parameters.Cod_Motivo_Memo
            };
            var result = await _txProcesoMemorandumService.DevolverMemorandum(_txMemorandum);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtenerInfoUsuarioMemorandum")]
        public async Task<IActionResult> getObtenerInfoUsuarioMemorandum([FromQuery] string sCodUsuario)
        {
            var result = await _txProcesoMemorandumService.ObtenerInfoUsuarioMemorandum(sCodUsuario);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getExportarInformacionMemorandumDetalle")]
        public async Task<IActionResult> getExportarInformacionMemorandumDetalle(DateTime FecIni, DateTime FecFin)
        {
            var result = await _txProcesoMemorandumService.ExportarInformacionMemorandumDetalle(FecIni, FecFin);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtieneLineaTempoMemorandum")]
        public async Task<IActionResult> getObtieneLineaTempoMemorandum([FromQuery] string sNumMemo)
        {
            var result = await _txProcesoMemorandumService.ObtieneLineaTempoMemorandum(sNumMemo);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getDescargarMemo")]
        public async Task<IActionResult> postImprimirMemo([FromQuery] string sNumMemo, int iCantidad)
        {
            try
            {


                // 1. Obtener memo desde la función del servicio
                var memo = await _txProcesoMemorandumService.ObtieneInformacionMemorandumDetalle(sNumMemo);
                if (memo == null)
                    return NotFound("No se encontró el memo");

                ServiceResponse<string> result = null;
                var resultPrint = await _txUbicacionColgadorService.ObtenerImpresoraPredeterminada();
                result = await _IHelpCommonService.PrintA4ToPdf(memo.Elements.ToList(), iCantidad);

                if (result.Success)
                {
                    result.CodeResult = StatusCodes.Status200OK;
                    // Leer el archivo generado
                    var pdfBytes = System.IO.File.ReadAllBytes(result.Element!);
                    return File(pdfBytes, "application/pdf", Path.GetFileName(result.Element!));
                }

                result.CodeResult = StatusCodes.Status400BadRequest;
                return BadRequest(result);
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Error al descargar archivo: {ex.Message}");
            }
        }

    }
}

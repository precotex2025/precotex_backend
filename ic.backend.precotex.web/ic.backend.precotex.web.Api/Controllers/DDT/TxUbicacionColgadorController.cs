using ic.backend.precotex.web.Api.Parameters;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.Services.Implementacion.DDT;
using ic.backend.precotex.web.Service.Services.Implementacion.Tejeduria;
using ic.backend.precotex.web.Service.Services.Tejeduria;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace ic.backend.precotex.web.Api.Controllers.DDT
{
    [Route("api/[controller]")]
    [ApiController]
    public class TxUbicacionColgadorController : ControllerBase
    {
        private readonly ITxUbicacionColgadorService _txUbicacionColgadorService;
    
        public TxUbicacionColgadorController(ITxUbicacionColgadorService txUbicacionColgadorService)
        {
            _txUbicacionColgadorService = txUbicacionColgadorService;
        }

        [HttpPost]
        [Route("postCrudUbicacionColgador")]
        public async Task<IActionResult> postCrudUbicacionColgador([FromBody] txUbicacionColgadorParameter parameters)
        {
            var obj = setDataTx_Ubicacion_Colgador(parameters);
            var result = await _txUbicacionColgadorService.CrudUbicacionColgador(obj,
                parameters.Accion);

            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListadoUbicacionColgador")]
        public async Task<IActionResult> getListadoUbicacionColgador(DateTime FecIni, DateTime FecFin, int Id_Tipo_Ubicacion_Colgador)
        {
            var result = await _txUbicacionColgadorService.ListadoUbicacionColgador(FecIni, FecFin, Id_Tipo_Ubicacion_Colgador);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListadoTipoUbicacionColgador")]
        public async Task<IActionResult> getListadoTipoUbicacionColgador()
        {
            var result = await _txUbicacionColgadorService.ListadoTipoUbicacionColgador();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListadoTipoFamTela")]
        public async Task<IActionResult> getListadoTipoFamTela()
        {
            var result = await _txUbicacionColgadorService.ListadoTipoFamTela();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }


        [HttpGet]
        [Route("getObtenerCorrelativo")]
        public async Task<IActionResult> getObtenerCorrelativo(int Id_Tipo_Ubicacion_Colgador, string Cod_FamTela)
        {
            var result = await _txUbicacionColgadorService.ObtenerCorrelativo(Id_Tipo_Ubicacion_Colgador, Cod_FamTela);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postCrudUbicacionColgadorItems")]
        public async Task<IActionResult> postCrudUbicacionColgadorItems([FromBody] TxUbicacionColgadorItemsParameter parameters)
        {
            var obj = setDataTx_Ubicacion_Colgador_Item(parameters);
            var result = await _txUbicacionColgadorService.CrudUbicacionColgadorItems(obj, parameters.CodigoBarra, parameters.Accion);

            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtenerUbicacionColgadorQR")]
        public async Task<IActionResult> getObtenerUbicacionColgadorQR(string CodigoBarra)
        {
            var result = await _txUbicacionColgadorService.ObtenerUbicacionColgadorQR(CodigoBarra);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtenerUbicacionColgadorById")]
        public async Task<IActionResult> getObtenerUbicacionColgadorById(int Id_Tx_Ubicacion_Colgador)
        {
            var result = await _txUbicacionColgadorService.ObtenerUbicacionColgadorById(Id_Tx_Ubicacion_Colgador);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListadoTotalColgadoresxTipoUbicaciones")]
        public async Task<IActionResult> getListadoTotalColgadoresxTipoUbicaciones(DateTime? FecCrea)
        {
            var result = await _txUbicacionColgadorService.ListadoTotalColgadoresxTipoUbicaciones(FecCrea);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListadoColgadoresxUbicacion")]
        public async Task<IActionResult> getListadoColgadoresxUbicacion(int Id_Tx_Ubicacion_Colgador)
        {
            var result = await _txUbicacionColgadorService.ListadoColgadoresxUbicacion(Id_Tx_Ubicacion_Colgador);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListadoTotalColgadoresxCodigoBarra")]
        public async Task<IActionResult> getListadoTotalColgadoresxCodigoBarra(string CodigoBarra)
        {
            var result = await _txUbicacionColgadorService.ListadoTotalColgadoresxCodigoBarra(CodigoBarra);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListadoUbicacioFisica")]
        public async Task<IActionResult> getListadoUbicacioFisica()
        {
            var result = await _txUbicacionColgadorService.ListadoUbicacioFisica();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtenerInformacionTotalCajasxUbicacion")]
        public async Task<IActionResult> getObtenerInformacionTotalCajasxUbicacion(int Id_Tx_Ubicacion_Fisica)
        {
            var result = await _txUbicacionColgadorService.ObtenerInformacionTotalCajasxUbicacion(Id_Tx_Ubicacion_Fisica);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtenerInformacionCajasxUbicacion")]
        public async Task<IActionResult> getObtenerInformacionCajasxUbicacion(int Id_Tx_Ubicacion_Fisica)
        {
            var result = await _txUbicacionColgadorService.ObtenerInformacionCajasxUbicacion(Id_Tx_Ubicacion_Fisica);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getReporteColgadoresGralDetallado")]
        public async Task<IActionResult> getReporteColgadoresGralDetallado()
        {
            var result = await _txUbicacionColgadorService.ReporteColgadoresGralDetallado();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        #region SET VALORES
        private Tx_Ubicacion_Colgador setDataTx_Ubicacion_Colgador(txUbicacionColgadorParameter txUbicacionColgadorParameter)
        {
            return new Tx_Ubicacion_Colgador
            {

                Id_Tx_Ubicacion_Colgador = txUbicacionColgadorParameter.Id_Tx_Ubicacion_Colgador,
                Id_Tipo_Ubicacion_Colgador = txUbicacionColgadorParameter.Id_Tipo_Ubicacion_Colgador,
                Id_Tipo_Ubicacion_Colgador_Padre = txUbicacionColgadorParameter.Id_Tipo_Ubicacion_Colgador_Padre,
                CodigoBarra = txUbicacionColgadorParameter.CodigoBarra,
                Cod_FamTela = txUbicacionColgadorParameter.Cod_FamTela,
                Correlativo = txUbicacionColgadorParameter.Correlativo,
                Flg_Estatus = txUbicacionColgadorParameter.Flg_Estatus,
                Cod_Usuario = txUbicacionColgadorParameter.Cod_Usuario
                //Cod_Equipo = txUbicacionColgadorParameter.Cod_Equipo
            };
        }

        private Tx_Ubicacion_Colgador_Items setDataTx_Ubicacion_Colgador_Item(TxUbicacionColgadorItemsParameter txUbicacionColgadorItemsParameter)
        {
            return new Tx_Ubicacion_Colgador_Items
            {
                Id_Tx_Ubicacion_Colgador = txUbicacionColgadorItemsParameter.Id_Tx_Ubicacion_Colgador,
                Flg_Estatus = txUbicacionColgadorItemsParameter.Flg_Estatus,
                Usu_Registro = txUbicacionColgadorItemsParameter.Cod_Usuario,
                Accion = txUbicacionColgadorItemsParameter.Accion,
                CodigoBarra = txUbicacionColgadorItemsParameter.CodigoBarra,
                Id_Tx_Ubicacion_Fisica = txUbicacionColgadorItemsParameter.Id_Tx_Ubicacion_Fisica
            };
        }
        #endregion
    }
}

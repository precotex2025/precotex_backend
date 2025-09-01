using ic.backend.precotex.web.Api.Parameters;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.Services.DDT;
using ic.backend.precotex.web.Service.Services.Implementacion.DDT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ic.backend.precotex.web.Api.Controllers.DDT
{
    [Route("api/[controller]")]
    [ApiController]
    public class TxProcesoColgadorRegistroController : ControllerBase
    {
        private readonly ITxProcesoColgadorRegistroService _txProcesoColgadorRegistroService;

        public TxProcesoColgadorRegistroController(ITxProcesoColgadorRegistroService txProcesoColgadorRegistroService)
        {
            _txProcesoColgadorRegistroService = txProcesoColgadorRegistroService;
        }

        [HttpGet]
        [Route("getObtieneInformacionTelaColgadorDet")]
        public async Task<IActionResult> getObtieneInformacionTelaColgadorDet(int Id_Tx_Colgador_Registro_Cab)
        {
            var result = await _txProcesoColgadorRegistroService.ObtieneInformacionTelaColgadorDet(Id_Tx_Colgador_Registro_Cab);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListadoColgadoresBandeja")]
        public async Task<IActionResult> getListadoColgadoresBandeja(DateTime FecIni, DateTime FecFin, string? Cod_Tela)
        {
            Cod_Tela = Cod_Tela ?? "";
            var result = await _txProcesoColgadorRegistroService.ListadoColgadoresBandeja(FecIni, FecFin, Cod_Tela);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtieneInformacionRutaColgador")]
        public async Task<IActionResult> getObtieneInformacionRutaColgador(string Cod_Tela)
        {
            var result = await _txProcesoColgadorRegistroService.ObtieneInformacionRutaColgador(Cod_Tela);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtieneInformacionTelaColgador")]
        public async Task<IActionResult> getObtieneInformacionTelaColgador(string Cod_Tela)
        {
            var result = await _txProcesoColgadorRegistroService.ObtieneInformacionTelaColgador(Cod_Tela);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtieneInformacionClienteColgador")]
        public async Task<IActionResult> getObtieneInformacionClienteColgador()
        {
            var result = await _txProcesoColgadorRegistroService.ObtieneInformacionClienteColgador();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postProcesoMntoColgador")]
        public async Task<IActionResult> postProcesoMntoColgador([FromBody] txColgadorRegistroParameter parameters)
        {
            Tx_Colgador_Registro_Cab _tx_Colgador_Registro_Cab = new Tx_Colgador_Registro_Cab
            {
                Cod_Tela = parameters.Cod_Tela,
                Cod_OrdTra = parameters.Cod_OrdTra,
                Cod_Ruta = parameters.Cod_Ruta,
                Cod_Cliente_Tex = parameters.Cod_Cliente_Tex,
                Fabric = parameters.Fabric,
                Yarn = parameters.Yarn,
                Composicion = parameters.Composicion,
                Flg_Estatus = parameters.Flg_Estatus,
                Usu_Registro = parameters.Usu_Registro,
            };
            var result = await _txProcesoColgadorRegistroService.ProcesoMntoColgador(_tx_Colgador_Registro_Cab, parameters.Detalle!, parameters.Accion!);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postProcesoEliminarColgador")]
        public async Task<IActionResult> postProcesoEliminarColgador([FromBody] Tx_Colgador_Registro_Cab parameters)
        {
            var result = await _txProcesoColgadorRegistroService.ProcesoEliminarColgador(Convert.ToInt32(parameters.Id_Tx_Colgador_Registro_Cab), parameters.Usu_Registro!);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        #region SET VALORES
        //    private txColgadorRegistroParameter setDatatxColgadorRegistroParameter(txColgadorRegistroParameter item)
        //{
        //    return new txColgadorRegistroParameter
        //    {
        //        Cod_Tela = item.Cod_Tela,
        //        Cod_OrdTra = item.Cod_OrdTra,
        //        Cod_Ruta = item.Cod_Ruta,
        //        Cod_Cliente_Tex = item.Cod_Cliente_Tex,
        //        Fabric = item.Fabric,
        //        Yarn = item.Yarn,
        //        Composicion = item.Composicion,
        //        Flg_Estatus = item.Flg_Estatus,
        //        Usu_Registro = item.Usu_Registro,
        //        Detalle = item.Detalle
        //    };
        //}

        #endregion

    }
}

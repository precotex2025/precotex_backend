using ic.backend.precotex.web.Api.Parameters;
using ic.backend.precotex.web.Entity.Entities.RetiroRepuestos;
using ic.backend.precotex.web.Service.Services.Implementacion.RetiroRepuestos;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ic.backend.precotex.web.Api.Controllers.RetiroRepuestos
{
    [Route("api/[controller]")]
    [ApiController]
    public class TxRetiroRepuestosController : ControllerBase
    {

        public readonly ITxRetiroRepuestosService _txRetiroRepuestosService;

        public TxRetiroRepuestosController (ITxRetiroRepuestosService txRetiroRepuestosService)
        {
            _txRetiroRepuestosService = txRetiroRepuestosService;
        }

        /******************************************CABECERA************************************************************/

        //OBTIENE TODA LA LISTA DE RETIROS
        [HttpGet]
        [Route("getListaRetiros")]
        public async Task<IActionResult> getListaRetiros(DateTime FecIni, DateTime FecFin)
        {
            var result = await _txRetiroRepuestosService.ListaRetiros(FecIni, FecFin);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        //OBTIENE UN RETIRO POR NUMERO DE REQUERIMIENTO
        [HttpGet]
        [Route("getListaRetirosPorNumRequerimiento")]
        public async Task<IActionResult> getListaRetirosPorNumRequerimiento(int Num_Requerimiento)
        {
            var result = await _txRetiroRepuestosService.ListaRetirosPorNumRequerimiento(Num_Requerimiento);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        //REGISTRA UN RETIRO
        [HttpPost]
        [Route("postRegistrarRequerimiento")]
        public async Task<IActionResult> postRegistrarRequerimiento([FromBody] TxRegistroRetiroRepuestosParameter parametros)
        {
            Tx_Retiro_Repuestos _txRetiroRepuestos = new Tx_Retiro_Repuestos
            {
                Num_Requerimiento = parametros.Num_Requerimiento,
                Fec_Requerimiento = parametros.Fec_Requerimiento,
                Cod_Seguridad = parametros.Cod_Seguridad,
                Cod_Mantenimiento = parametros.Cod_Mantenimiento,
                Nro_Precinto_Apertura = parametros.Nro_Precinto_Apertura,
                Nro_Precinto_Cierre = parametros.Nro_Precinto_Cierre
            };

            var result = await _txRetiroRepuestosService.RegistrarRequerimiento(_txRetiroRepuestos);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        //ACTUALIZA CABECERA REQUERIMIENTO
        [HttpPatch]
        [Route ("patchActualizarRequerimiento")]
        public async Task<IActionResult> patchActualizarRequerimiento([FromBody] TxRegistroRetiroRepuestosParameter parametros)
        {
            Tx_Retiro_Repuestos _txRetiroRepuestos = new Tx_Retiro_Repuestos
            {
                Num_Requerimiento = parametros.Num_Requerimiento,
                Cod_Seguridad = parametros.Cod_Seguridad,
                Cod_Mantenimiento = parametros.Cod_Mantenimiento,
                Nro_Precinto_Apertura = parametros.Nro_Precinto_Apertura
            };

            var result = await _txRetiroRepuestosService.ActualizarRequerimiento(_txRetiroRepuestos);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        //ACTUALIZA CABECERA REQUERIMIENTO -> AGREGA PRECINTO CIERRE
        [HttpPatch]
        [Route ("patchActualizarRequerimientoPrecintoCierre")]
        public async Task<IActionResult> patchActualizarRequerimientoPrecintoCierre([FromBody] TxRegistroRetiroRepuestosParameter parametros)
        {
            Tx_Retiro_Repuestos _txRetiroRepuestos = new Tx_Retiro_Repuestos
            {
                Num_Requerimiento = parametros.Num_Requerimiento,
                Nro_Precinto_Cierre = parametros.Nro_Precinto_Cierre
            };

            var result = await _txRetiroRepuestosService.ActualizarRequerimientoPrecintoCierre(_txRetiroRepuestos);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }


        /******************************************DETALLE************************************************************/
        //OBTIENE EL DETALLE DE UN RETIRO
        [HttpGet]
        [Route("getListaRetiroDetallePorNumRequerimiento")]
        public async Task<IActionResult> getListaRetiroDetallePorNumRequerimiento(int Num_Requerimiento)
        {
            var result = await _txRetiroRepuestosService.ListaRetiroDetallePorNumRequerimiento(Num_Requerimiento);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        //OBTIENE LOS DATOS DE UN DETALLE DE UN RETIRO
        [HttpGet]
        [Route("getListaRetiroDetallePorNumReqySecuencia")]
        public async Task<IActionResult> getListaRetiroDetallePorNumReqySecuencia(int Num_Requerimiento, int Nro_Secuencia)
        {
            var result = await _txRetiroRepuestosService.ListaRetiroDetallePorNumReqySecuencia(Num_Requerimiento, Nro_Secuencia);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        //REGISTRA DETALLE DE UN RETIRO
        [HttpPost]
        [Route("postRegistrarRequerimientoDetalle")]
        public async Task<IActionResult> postRegistrarRequerimientoDetalle([FromBody] TxRegistroRetiroRepuestosParameter_Detalle parametros)
        {
            Tx_Retiro_Repuestos_Detalle _txRetiroRepuestosDetalle = new Tx_Retiro_Repuestos_Detalle
            {
                Num_Requerimiento = parametros.Num_Requerimiento,
                Itm_Codigo = parametros.Itm_Codigo,
                Itm_Descripcion = parametros.Itm_Descripcion,
                Itm_Cantidad = parametros.Itm_Cantidad,
                Itm_Unidad_Medida = parametros.Itm_Unidad_Medida,
                Rpt_Cambio = parametros.Rpt_Cambio,
                Itm_Foto = parametros.Itm_Foto
            };

            var result = await _txRetiroRepuestosService.RegistrarRequerimientoDetalle(_txRetiroRepuestosDetalle);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        //ACTUALIZA DETALLE DE UN RETIRO
        [HttpPatch]
        [Route("patchActualizarRequerimientoDetalle")]
        public async Task<IActionResult> RegistrarRequerimientoDetalle([FromBody] TxRegistroRetiroRepuestosParameter_Detalle parametros)
        {
            Tx_Retiro_Repuestos_Detalle _txRetiroRepuestosDetalle = new Tx_Retiro_Repuestos_Detalle
            {
                Num_Requerimiento = parametros.Num_Requerimiento,
                Nro_Secuencia = parametros.Nro_Secuencia,
                Itm_Codigo = parametros.Itm_Codigo,
                Itm_Descripcion = parametros.Itm_Descripcion,
                Itm_Cantidad = parametros.Itm_Cantidad,
                Itm_Unidad_Medida = parametros.Itm_Unidad_Medida,
                Rpt_Cambio = parametros.Rpt_Cambio,
                Itm_Foto = parametros.Itm_Foto
            };

            var result = await _txRetiroRepuestosService.ActualizarRequerimientoDetalle(_txRetiroRepuestosDetalle);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        /******************************************COMPLEMENTARIOS************************************************************/

        //OBTIENE LOS DATOS DE UN ITEM
        [HttpGet]
        [Route("getListaItems")]
        public async Task<IActionResult> getListaItems()
        {
            var result = await _txRetiroRepuestosService.ListaItems();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        //OBTIENE USUARIO Y CODIGO



    }
}

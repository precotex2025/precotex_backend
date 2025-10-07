using ic.backend.precotex.web.Api.Parameters;
using ic.backend.precotex.web.Entity.Entities.ReporteNC;
using ic.backend.precotex.web.Entity.Entities.RetiroRepuestos;
using ic.backend.precotex.web.Service.Services.Implementacion.Laboratorio;
using ic.backend.precotex.web.Service.Services.Implementacion.ReporteNC;
using ic.backend.precotex.web.Service.Services.Laboratorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ic.backend.precotex.web.Api.Controllers.ReporteNC
{
    [Route("api/[controller]")]
    [ApiController]
    public class TxReporteNCController : ControllerBase
    {
        public readonly ITxReporteNCService _txReporteNCService;

        public TxReporteNCController(ITxReporteNCService txReporteNCService)
        {
            _txReporteNCService = txReporteNCService;
        }

        //OBTIENE LISTA REPORTES
        [HttpGet]
        [Route("getListarRegistro")]
        public async Task<IActionResult> getListarRegistro(int Rep_ID)
        {
            var result = await _txReporteNCService.ListarRegistro(Rep_ID);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        //REGISTRA REPORTE NC
        [HttpPost]
        [Route("postRegistrarReporteNC")]
        public async Task<IActionResult> RegistrarReporteNC([FromBody] Tx_ReporteNC parametros)
        {
            Tx_ReporteNC _lb_AgrOpc_Colorantes = new Tx_ReporteNC
            {
                //Rep_Id = parametros.Rep_Id,
                //Rep_FecObs = parametros.Rep_FecObs,
                //Rep_HorObs = parametros.Rep_HorObs,
                Cod_Planta_Tg = parametros.Cod_Planta_Tg,
                Are_Id = parametros.Are_Id,
                Rep_Esp = parametros.Rep_Esp,
                Rep_Clas = parametros.Rep_Clas,
                Rep_DesNC = parametros.Rep_DesNC,
                Rep_NivRgo = parametros.Rep_NivRgo,
                Rep_AccCor = parametros.Rep_AccCor,
                Resp_Id = parametros.Resp_Id,
                Rep_RepPor = parametros.Rep_RepPor,
                //Codigo = parametros.Codigo,
                //sMsj = parametros.sMsj
            };

            var result = await _txReporteNCService.RegistrarReporteNC(parametros);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarPlantas")]
        public async Task<IActionResult> getListarPlantas()
        {
            var result = await _txReporteNCService.ListarPlantas();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarClasificaciones")]
        public async Task<IActionResult> getListarClasificaciones()
        {
            var result = await _txReporteNCService.ListarClasificaciones();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarEstado")]
        public async Task<IActionResult> patchActualizarEstado([FromBody] Tx_ReporteNCParameter parametros)
        {
            Tx_ReporteNC _txReporteNC = new Tx_ReporteNC
            {
                Rep_Id = parametros.Rep_Id,
                Rep_Est = parametros.Rep_Est

            };

            var result = await _txReporteNCService.ActualizarEstado(_txReporteNC);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarDatosResolvedor")]
        public async Task<IActionResult> getListarDatosResolvedor(int Rep_ID)
        {
            var result = await _txReporteNCService.ListarDatosResolvedor(Rep_ID);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarReporteNC")]
        public async Task<IActionResult> patchActualizarReporteNC([FromBody] Tx_ReporteNCParameter parametros)
        {
            Tx_ReporteNC _txReporteNC = new Tx_ReporteNC
            {
                Rep_Aceptado = parametros.Rep_Aceptado,
                Rep_Resp_Levantamiento = parametros.Rep_Resp_Levantamiento,
                Rep_AccCor_Tom = parametros.Rep_AccCor_Tom,
                Rep_Est = parametros.Rep_Est,
                Rep_DetObs = parametros.Rep_DetObs

            };

            var result = await _txReporteNCService.ActualizarReporteNC(_txReporteNC);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarEstados")]
        public async Task<IActionResult> getListarEstados()
        {
            var result = await _txReporteNCService.ListarEstados();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        //ActualizarReporteNCOriginal(Tx_ReporteNC tx_ReporteNC)
        //ACTUALIZAR REPORTE NC ORIGINAL
        [HttpPost]
        [Route("patchActualizarReporteNCOriginal")]
        public async Task<IActionResult> patchActualizarReporteNCOriginal([FromBody] Tx_ReporteNC parametros)
        {
            Tx_ReporteNC _lb_AgrOpc_Colorantes = new Tx_ReporteNC
            {
                Rep_Id = parametros.Rep_Id,
                Cod_Planta_Tg = parametros.Cod_Planta_Tg,
                Are_Id = parametros.Are_Id,
                Rep_Esp = parametros.Rep_Esp,
                Rep_Clas = parametros.Rep_Clas,
                Rep_DesNC = parametros.Rep_DesNC,
                Rep_NivRgo = parametros.Rep_NivRgo,
                Rep_AccCor = parametros.Rep_AccCor,
                Resp_Id = parametros.Resp_Id,
                Rep_RepPor = parametros.Rep_RepPor,
            };

            var result = await _txReporteNCService.ActualizarReporteNCOriginal(parametros);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

    }
}

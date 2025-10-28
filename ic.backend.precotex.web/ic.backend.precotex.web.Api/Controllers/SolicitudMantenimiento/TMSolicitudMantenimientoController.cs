using ic.backend.precotex.web.Api.Parameters;
using ic.backend.precotex.web.Entity.Entities.Memorandum;
using ic.backend.precotex.web.Entity.Entities.SolicitudMantenimiento;
using ic.backend.precotex.web.Service.Services.Implementacion.SolicitudMantenimiento;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ic.backend.precotex.web.Api.Controllers.SolicitudMantenimiento
{
    [Route("api/[controller]")]
    [ApiController]

    public class TMSolicitudMantenimientoController : ControllerBase
    {
        private readonly ITMSolicitudMantenimientoService _tMSolicitudMantenimientoService;

        public TMSolicitudMantenimientoController(ITMSolicitudMantenimientoService tMSolicitudMantenimientoService)
        {
            _tMSolicitudMantenimientoService = tMSolicitudMantenimientoService;
        }

        [HttpPost]
        [Route("postProcesoMntoSolicitudMantenimiento")]
        public async Task<IActionResult> postProcesoMntoSolicitudMantenimiento([FromBody] TmSolicitudMantenimientoParameter parameters)
        {
            TM_Solicitud_Mantenimiento _tmSolicitudMantenimiento = new TM_Solicitud_Mantenimiento
            {
                Cod_Solicitud = parameters.Cod_Solicitud,
                Cod_Area = parameters.Cod_Area,
                Cod_Maquina = parameters.Cod_Maquina,
                Observacion = parameters.Observacion,
                Prioridad = parameters.Prioridad,
                Paro_Maquina = parameters.Paro_Maquina == "1"? true: false,
                Ruta_Fotografia = parameters.Ruta_Fotografia,
                Hora_Inicio = parameters.Hora_Inicio,
                Usu_Registro = parameters.Usu_Registro
            };
            var result = await _tMSolicitudMantenimientoService.ProcesoMntoSolicitudMantenimiento(_tmSolicitudMantenimiento, parameters.Accion!);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtieneInformacionMaquinas")]
        public async Task<IActionResult> getObtieneInformacionMaquinas([FromQuery] string sCodMaquina)
        {
            var result = await _tMSolicitudMantenimientoService.ObtieneInformacionMaquinas(sCodMaquina);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtieneInformacionSolicitudMantenimiento")]
        public async Task<IActionResult> getObtieneInformacionSolicitudMantenimiento([FromQuery] DateTime FecIni, DateTime FecFin, string codUsuario)
        {
            var result = await _tMSolicitudMantenimientoService.ObtieneInformacionSolicitudMantenimiento(FecIni, FecFin, codUsuario);
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

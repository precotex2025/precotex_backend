using ic.backend.precotex.web.Api.Parameters;
using ic.backend.precotex.web.Entity.Entities.Memorandum;
using ic.backend.precotex.web.Entity.Entities.Tejeduria;
using ic.backend.precotex.web.Service.Services.Implementacion.Tejeduria;
using ic.backend.precotex.web.Service.Services.Tejeduria;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ic.backend.precotex.web.Api.Controllers.Tejeduria
{
    [Route("api/[controller]")]
    [ApiController]
    public class TjSolicitudDevolucionAuditoriaController:ControllerBase
    {
        private readonly ITjSolicitudDevolucionAuditoriaService _tjSolicitudDevolucionAuditoriaService;

        public TjSolicitudDevolucionAuditoriaController(ITjSolicitudDevolucionAuditoriaService tjSolicitudDevolucionAuditoriaService)
        {
            _tjSolicitudDevolucionAuditoriaService = tjSolicitudDevolucionAuditoriaService;
        }

        [HttpGet]
        [Route("getListaSolicitudAuditoria")]

        public async Task<IActionResult> getListaSolicitudAuditoria(int Num_Solicitud, string? Lote, DateTime FechaIni, DateTime FechaFin, string? Estado)
        {
            if (Lote == null)
            {
                Lote = "";
            }

            if (Estado == null)
            {
                Estado = "";
            }

            var result = await _tjSolicitudDevolucionAuditoriaService.ListaSolicitudDevolucion(Num_Solicitud, Lote, FechaIni, FechaFin, Estado);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListaSolicitudAuditoriaBultos")]

        public async Task<IActionResult> getListaSolicitudAuditoriaBultos(int Num_Solicitud, string Lote, string? Semana, string? Color, string? Marca, string? Conera)
        {
            var result = await _tjSolicitudDevolucionAuditoriaService.ListaSolicitudDevolucionBultos(Num_Solicitud, Lote, Semana, Color, Marca, Conera);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postProceso")]
        public async Task<IActionResult> postProceso([FromBody] TjSolicitudDevolucionAuditoriaParameter parameters)
        {
            Tj_Mantenimiento_Solicitud_Devolucion _Tj_Mantenimiento_Solicitud_Devolucion = new Tj_Mantenimiento_Solicitud_Devolucion
            {
                Num_Solicitud = parameters.Num_Solicitud,
                Lote = parameters.Lote,
                Semana = parameters.Semana,
                Color = parameters.Color,
                Marca = parameters.Marca,
                Conera = parameters.Conera,
                Estado = parameters.Estado,
                Tipo = parameters.Tipo,
                Cod_Usuario = parameters.Cod_Usuario
            };

            var result = await _tjSolicitudDevolucionAuditoriaService.Proceso(_Tj_Mantenimiento_Solicitud_Devolucion, parameters.Accion!);
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

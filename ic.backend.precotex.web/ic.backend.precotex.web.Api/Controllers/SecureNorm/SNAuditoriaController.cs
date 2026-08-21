using ic.backend.precotex.web.Api.Parameters;
using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Service.Services.Implementacion.SecureNorm;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Api.Controllers.SecureNorm
{
    [Route("api/[controller]")]
    [ApiController]
    public class SNAuditoriaController : ControllerBase
    {
        private readonly ISNAuditoriaService _sNAuditoriaService;

        public SNAuditoriaController(ISNAuditoriaService sNAuditoriaService)
        {
            _sNAuditoriaService = sNAuditoriaService;
        }

        [HttpPost]
        [Route("postProcesoMntoAuditoria")]
        public async Task<IActionResult> postProcesoMntoAuditoria([FromBody] SNAuditoriaParameter parametros)
        {
            SN_Auditoria auditoria = new SN_Auditoria
            {
                Codigo_Auditoria = parametros.Codigo_Auditoria ?? "",
                Tipo = parametros.Tipo ?? "",
                Norma = parametros.Norma ?? "",
                Responsable = parametros.Responsable ?? "",
                Areas = parametros.Areas ?? "",
                Fecha_Inicio = parametros.Fecha_Inicio,
                Fecha_Fin = parametros.Fecha_Fin,
                Frecuencia = parametros.Frecuencia ?? "",
                Alcance = parametros.Alcance ?? "",
                Estado = parametros.Estado ?? "Programada",
                Usuario_Registro = parametros.Cod_Usuario ?? "SISTEMAS"
            };

            var result = await _sNAuditoriaService.ProcesoMnto(auditoria, parametros.Accion!);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListadoAuditorias")]
        public async Task<IActionResult> getListadoAuditorias(string? sFiltro)
        {
            var result = await _sNAuditoriaService.Listado(sFiltro ?? "");
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListadoEjecucionAuditorias")]
        public async Task<IActionResult> getListadoEjecucionAuditorias(string? sFiltro)
        {
            var result = await _sNAuditoriaService.ListadoEjecucion(sFiltro ?? "");
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postProcesoMntoEjecucion")]
        public async Task<IActionResult> postProcesoMntoEjecucion([FromBody] SNAuditoriaEjecucionParameter parametros)
        {
            SN_Auditoria_Ejecucion ejecucion = new SN_Auditoria_Ejecucion
            {
                Id_Ejecucion = parametros.Id_Ejecucion ?? 0,
                Codigo_Ejecucion = parametros.Codigo_Ejecucion ?? "",
                Codigo_Auditoria = parametros.Codigo_Auditoria ?? parametros.Auditoria ?? "",
                Fecha_Ejecucion = parametros.Fecha_Ejecucion,
                Auditados = parametros.Auditados ?? "",
                Tipo_Hallazgo = parametros.Tipo_Hallazgo ?? parametros.Tipo ?? "Observación",
                Descripcion_Hallazgo = parametros.Descripcion_Hallazgo ?? parametros.Descripcion ?? "",
                Codigo_NC = parametros.Codigo_NC ?? parametros.Nc ?? "",
                Responsable_Auditor = parametros.Responsable_Auditor ?? parametros.Responsable ?? "",
                Estado = parametros.Estado ?? "Abierto",
                Ruta_Archivo_Evidencia = parametros.Ruta_Archivo_Evidencia ?? parametros.Archivo ?? "",
                Notas_Adicionales = parametros.Notas_Adicionales ?? parametros.Notas ?? "",
                Cod_Usuario = parametros.Cod_Usuario ?? "SISTEMAS"
            };

            var result = await _sNAuditoriaService.ProcesoMntoEjecucion(ejecucion, parametros.Accion!);
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

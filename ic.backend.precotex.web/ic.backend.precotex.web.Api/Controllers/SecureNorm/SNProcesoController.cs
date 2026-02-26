using ic.backend.precotex.web.Api.Parameters;
using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Service.Services.Implementacion.SecureNorm;
using ic.backend.precotex.web.Service.Services.SecureNorm;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ic.backend.precotex.web.Api.Controllers.SecureNorm
{
    [Route("api/[controller]")]
    [ApiController]
    public class SNProcesoController : ControllerBase
    {
        private readonly ISNProcesoService _sNProcesoService;
        public SNProcesoController(ISNProcesoService sNProcesoService)
        {
            _sNProcesoService = sNProcesoService;
        }


        [HttpPost]
        [Route("postProcesoMntoProcesos")]
        public async Task<IActionResult> postProcesoMntoProcesos([FromBody] SNProcesoParameter parametros)
        {
            SN_Proceso proceso = new SN_Proceso
            {
                Codigo_Proceso = parametros.Codigo_Proceso ?? "",
                Codigo_Sede = parametros.Codigo_Sede ?? "",
                Proceso = parametros.Proceso ?? "",
                Codigo_Tipo_Proceso = parametros.Codigo_Tipo_Proceso ?? "",
                Descripcion = parametros.Descripcion ?? "",
                Nombre_Adjunto = parametros.Nombre_Adjunto ?? "",
                Ruta_Adjunto = parametros.Ruta_Adjunto ?? "",
                Flg_Activo = parametros.Flg_Activo ?? "",
                Cod_Usuario = parametros.Cod_Usuario ?? ""
            };

            var result = await _sNProcesoService.ProcesoMnto(proceso, parametros.Accion!);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListadoProcesos")]
        public async Task<IActionResult> getListadoProcesos(string? sCodigoOrganizacion, string? sEstado)
        {
            var result = await _sNProcesoService.Listado(sCodigoOrganizacion!, sEstado!);
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

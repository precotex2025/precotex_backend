using ic.backend.precotex.web.Api.Parameters;
using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Service.Services.Implementacion.SecureNorm;
using ic.backend.precotex.web.Service.Services.SecureNorm;
using Microsoft.AspNetCore.Mvc;

namespace ic.backend.precotex.web.Api.Controllers.SecureNorm
{
    [Route("api/[controller]")]
    [ApiController]
    public class SNPuestoController : ControllerBase
    {
        private readonly ISNPuestoService _sNPuestoService;
        public SNPuestoController(ISNPuestoService sNPuestoService)
        {
            _sNPuestoService = sNPuestoService;
        }

        [HttpPost]
        [Route("postProcesoMntoPuesto")]
        public async Task<IActionResult> postProcesoMntoPuesto([FromBody] SNPuestoParameter parametros)
        {
            SN_Puesto puesto = new SN_Puesto
            {
                Codigo_Puesto = parametros.Codigo_Puesto ?? "",
                Codigo_Organizacion = parametros.Codigo_Organizacion ?? "",
                Codigo_Sede = parametros.Codigo_Sede ?? "",
                Denominacion = parametros.Denominacion ?? "",
                Codigo_Nivel_Riesgo = parametros.Codigo_Nivel_Riesgo ?? "",
                Validacion_Periodica = parametros.Validacion_Periodica ?? "",
                Puesto_Descripcion = parametros.Puesto_Descripcion ?? "",
                Puesto_Funciones = parametros.Puesto_Funciones ?? "",
                Puesto_Requisitos = parametros.Puesto_Requisitos ?? "",
                Puesto_Caracteristicas = parametros.Puesto_Caracteristicas ?? "",
                Caracteristicas_Visible = parametros.Caracteristicas_Visible ?? "",
                Flg_Activo = parametros.Flg_Activo ?? "",
                Cod_Usuario = parametros.Cod_Usuario ?? ""
            };

            var result = await _sNPuestoService.ProcesoMnto(puesto, parametros.Accion!);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListadoPuesto")]
        public async Task<IActionResult> getListadoPuesto(string sCodigo_Organizacion, string sCodigo_Sede, string sCodigo_Nivel_Riesgo)
        {
            var result = await _sNPuestoService.Listado(sCodigo_Organizacion!, sCodigo_Sede!, sCodigo_Nivel_Riesgo!);
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

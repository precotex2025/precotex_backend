using ic.backend.precotex.web.Api.Parameters;
using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Service.Services.Implementacion.SecureNorm;
using ic.backend.precotex.web.Service.Services.SecureNorm;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ic.backend.precotex.web.Api.Controllers.SecureNorm
{
    [Route("api/[controller]")]
    [ApiController]
    public class SNOrganizacionController : ControllerBase
    {
        private readonly ISNOrganizacionService _sNOrganizacionService ;

        public SNOrganizacionController(ISNOrganizacionService sNOrganizacionService)
        {
            _sNOrganizacionService = sNOrganizacionService;
        }

        [HttpPost]
        [Route("postProcesoMntoOrganizacion")]
        public async Task<IActionResult> postProcesoMntoOrganizacion([FromBody] SNOrganizacionParameter parametros)
        {
            SN_Organizacion organizacion = new SN_Organizacion
            {
                Codigo_Organizacion = parametros.Codigo_Organizacion ?? "",
                Denominacion = parametros.Denominacion ?? "",
                Direccion = parametros.Direccion ?? "",
                Localidad = parametros.Localidad ?? "",
                Provincia = parametros.Provincia ?? "",
                Pais = parametros.Pais ?? "",
                //Denominacion_Sede_Principal =parametros.Denominacion_Sede_Principal ?? "",
                //Acronimo = parametros.Acronimo ?? "",
                //Sede_Direccion = parametros.Sede_Direccion ?? "",
                //Sede_Localidad = parametros.Sede_Localidad ?? "",
                //Sede_Provincia = parametros.Sede_Provincia ?? "",
                //Sede_Pais = parametros.Sede_Pais ?? "",
                Flg_Activo = parametros.Flg_Activo ?? "",
                Cod_Usuario = parametros.Cod_Usuario ?? "",                 
            };

            var result = await _sNOrganizacionService.ProcesoMnto(organizacion, parametros.Accion!);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListadoOrganizacion")]
        public async Task<IActionResult> getListadoOrganizacion(string? sEstado)
        {
            var result = await _sNOrganizacionService.Listado(sEstado!);
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

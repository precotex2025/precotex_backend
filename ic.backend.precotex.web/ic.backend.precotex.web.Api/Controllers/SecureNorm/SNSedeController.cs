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
    public class SNSedeController : ControllerBase
    {
        private readonly ISNSedeService _sNSedeService;

        public SNSedeController(ISNSedeService sNSedeService)
        {
            _sNSedeService = sNSedeService;
        }

        [HttpPost]
        [Route("postProcesoMntoSedes")]
        public async Task<IActionResult> postProcesoMntoSedes([FromBody] SNSedeParameter parametros)
        {
            SN_Sede sede = new SN_Sede
            {
     
                Codigo_Sede = parametros.Codigo_Sede ?? "",
                Codigo_Organizacion = parametros.Codigo_Organizacion ?? "",
                Denominacion = parametros.Denominacion ?? "",
                Acronimo = parametros.Acronimo ?? "",
                Direccion = parametros.Direccion ?? "",
                Localidad = parametros.Localidad ?? "",
                Provincia = parametros.Provincia ?? "",
                Pais = parametros.Pais ?? "",
                Flg_Activo = parametros.Flg_Activo ?? "",
                Cod_Usuario = parametros.Cod_Usuario ?? ""
            };

            var result = await _sNSedeService.ProcesoMnto(sede, parametros.Accion!);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }


        [HttpGet]
        [Route("getListadoSedes")]
        public async Task<IActionResult> getListadoSedes(string sCodigoOrganizacion, string? sEstado)
        {
            var result = await _sNSedeService.Listado(sCodigoOrganizacion!, sEstado!);
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

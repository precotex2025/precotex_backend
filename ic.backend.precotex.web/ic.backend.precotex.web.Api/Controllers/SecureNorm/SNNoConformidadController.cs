using ic.backend.precotex.web.Api.Parameters;
using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Service.Services.SecureNorm;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Api.Controllers.SecureNorm
{
    [Route("api/[controller]")]
    [ApiController]
    public class SNNoConformidadController : ControllerBase
    {
        private readonly ISNNoConformidadService _sNNoConformidadService;

        public SNNoConformidadController(ISNNoConformidadService sNNoConformidadService)
        {
            _sNNoConformidadService = sNNoConformidadService;
        }

        [HttpPost]
        [Route("postProcesoMntoNoConformidad")]
        public async Task<IActionResult> postProcesoMntoNoConformidad([FromBody] SNNoConformidadParameter parametros)
        {
            SN_No_Conformidad noConformidad = new SN_No_Conformidad
            {
                NC = parametros.NC ?? "",
                Tipo = parametros.Tipo ?? "",
                Accion = parametros.Accion_Desc ?? "",
                Proceso = parametros.Proceso ?? "",
                Responsable = parametros.Responsable ?? "",
                Fecha_Inicio = parametros.Fecha_Inicio,
                Fecha_Limite = parametros.Fecha_Limite,
                Estado = parametros.Estado ?? "Pendiente",
                Descripcion = parametros.Descripcion ?? "",
                Codigo_Auditoria = parametros.Codigo_Auditoria ?? "",
                Usuario_Registro = parametros.Cod_Usuario ?? "SISTEMAS"
            };

            var result = await _sNNoConformidadService.ProcesoMnto(noConformidad, parametros.Accion!);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListadoNoConformidades")]
        public async Task<IActionResult> getListadoNoConformidades(string? sFiltro)
        {
            var result = await _sNNoConformidadService.Listado(sFiltro ?? "");
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

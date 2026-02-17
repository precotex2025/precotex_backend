using ic.backend.precotex.web.Api.Parameters;
using ic.backend.precotex.web.Entity.Entities.Laboratorio;
using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Service.Services.Implementacion.SecureNorm;
using ic.backend.precotex.web.Service.Services.Implementacion.Tejeduria;
using ic.backend.precotex.web.Service.Services.Tejeduria;
using Microsoft.AspNetCore.Mvc;

namespace ic.backend.precotex.web.Api.Controllers.SecureNorm
{
    [Route("api/[controller]")]
    [ApiController]
    public class SNNormaController : ControllerBase
    {
        private readonly ISNNormaService _sNNormaService;

        public SNNormaController(ISNNormaService sNNormaService)
        {
            _sNNormaService = sNNormaService;
        }

        [HttpPost]
        [Route("postProcesoMntoNormas")]
        public async Task<IActionResult> postProcesoMntoNormas([FromBody] SNNormaParameter parametros)
        {
            SN_Norma norma = new SN_Norma
            {
                Codigo_Norma = parametros.Codigo_Norma ?? "",
                Norma = parametros.Norma ?? "",
                Descripcion = parametros.Descripcion ?? "",
                Flg_Activo = parametros.Flg_Activo ?? "",
                Cod_Usuario = parametros.Cod_Usuario ?? ""
            };

            var result = await _sNNormaService.ProcesoMnto(norma, parametros.Accion!);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListadoNormas")]
        public async Task<IActionResult> getListadoNormas(string? sEstado)
        {
            var result = await _sNNormaService.Listado(sEstado!);
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

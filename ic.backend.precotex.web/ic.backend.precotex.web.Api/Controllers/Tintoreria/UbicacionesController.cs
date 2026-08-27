using ic.backend.precotex.web.Api.Parameters;
using ic.backend.precotex.web.Entity.Entities.Tintoreria;
using ic.backend.precotex.web.Service.Services.Implementacion.Tintoreria;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ic.backend.precotex.web.Api.Controllers.Tintoreria
{
    [Route("api/[controller]")]
    [ApiController]
    public class UbicacionesController : ControllerBase
    {
        private readonly IUbicacionesService _IUbicacionesService;
        public UbicacionesController(IUbicacionesService IUbicacionesService)
        {
            _IUbicacionesService = IUbicacionesService;
        }

        [HttpGet]
        [Route("getListaBultoUbicaciones")]
        public async Task<IActionResult> getListaBultoUbicaciones(string? Cod_Almacen, string? Cod_Item)
        {
            var result = await _IUbicacionesService.ListaBultoUbicaciones(Cod_Almacen, Cod_Item);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postInsertarBultoGrupo")]
        public async Task<IActionResult> postInsertarBultoGrupo([FromBody] UbicacionesInsertarBultoGrupoParameter parameters)
        {
            var ubicaciones = setDataInsertarBultoGrupo(parameters);
            var result = await _IUbicacionesService.InsertarBultoGrupo(ubicaciones);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc > 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        #region SET VALORES
        private Ubicaciones.InsertarBultoGrupo setDataInsertarBultoGrupo(UbicacionesInsertarBultoGrupoParameter parameters)
        {
            return new Ubicaciones.InsertarBultoGrupo
            {
                Accion = parameters.Accion,
                Id_Bulto_Hilado_Grupo = parameters.Id_Bulto_Hilado_Grupo,
                Num_Corre = parameters.Num_Corre,
                Cod_Usuario = parameters.Cod_Usuario
            };
        }
        #endregion
    }
}

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
        public async Task<IActionResult> getListaBultoUbicaciones(string? Cod_Almacen, string? Codigo_Barra_Grupo)
        {
            var result = await _IUbicacionesService.ListaBultoUbicaciones(Cod_Almacen, Codigo_Barra_Grupo);
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
                //result.CodeResult = result.CodeTransacc > 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                result.CodeResult = result.CodeTransacc == 2 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postUbicarGrupoOBulto")]
        public async Task<IActionResult> postUbicarGrupoOBulto([FromBody] UbicacionesUbicarGrupoOBultoParameter parameters)
        {
            var ubicaciones = setDataUbicarGrupoOBulto(parameters);
            var result = await _IUbicacionesService.UbicarGrupoOBulto(ubicaciones);
            if (result.Success)
            {
                //result.CodeResult = StatusCodes.Status200OK;
                result.CodeResult = result.CodeTransacc == 2 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListaAgrupamientosDelDia")]
        public async Task<IActionResult> getListaAgrupamientosDelDia(DateTime? Fec_Creacion, string? Codigo_Barra_Grupo)
        {
            var result = await _IUbicacionesService.ListaAgrupamientosDelDia(Fec_Creacion, Codigo_Barra_Grupo);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListaDetalleBultosAgrupados")]
        public async Task<IActionResult> getListaDetalleBultosAgrupados(string? Cod_Almacen, int? Id_Agrupamiento, string? Codigo_Barra_Grupo)
        {
            var result = await _IUbicacionesService.ListaDetalleBultosAgrupados(Cod_Almacen, Id_Agrupamiento, Codigo_Barra_Grupo);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getConsultaKardexPda")]
        public async Task<IActionResult> getConsultaKardexPda(string? Cod_Almacen, string? Codigo_Escaneado)
        {
            var result = await _IUbicacionesService.ConsultaKardexPda(Cod_Almacen, Codigo_Escaneado);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
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

        private Ubicaciones.UbicarGrupoOBulto setDataUbicarGrupoOBulto(UbicacionesUbicarGrupoOBultoParameter parameters)
        {
            return new Ubicaciones.UbicarGrupoOBulto
            {
                Accion = parameters.Accion,
                Id_Agrupamiento = parameters.Id_Agrupamiento,
                Num_Corre = parameters.Num_Corre,
                Codigo_Ubicacion_Dest = parameters.Codigo_Ubicacion_Dest,
                Cod_Usuario = parameters.Cod_Usuario
            };
        }
        #endregion
    }
}

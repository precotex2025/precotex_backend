using ic.backend.precotex.web.Api.Parameters;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.Almacen;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ic.backend.precotex.web.Api.Controllers.Almacen
{
    [Route("api/[controller]")]
    [ApiController]
    public class TxBultoHiladoGrupoController : ControllerBase
    {

        private readonly ITxBultoHiladoGrupoService _txBultoHiladoGrupoService;

        public TxBultoHiladoGrupoController(ITxBultoHiladoGrupoService txBultoHiladoGrupoService)
        {
            _txBultoHiladoGrupoService = txBultoHiladoGrupoService;
        }

        //[HttpPost()]
        //[Produces(typeof(ServiceResponse<int>))]
        [HttpPost]
        [Route("postGenerarGrupo")]
        public async Task<IActionResult> Insertar([FromBody] txBultoHiladoGrupoParameter parameters)
        {
            var hiladoGrupo = setDataTxBultoHiladoGrupo(parameters);
            var result = await _txBultoHiladoGrupoService.Insertar(hiladoGrupo);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc > 1? StatusCodes.Status200OK: StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListaGrupos")]
        public async Task<IActionResult> getListaGrupos(DateTime? FecCrea, string? Grupo)
        {
            var result = await _txBultoHiladoGrupoService.Lista(FecCrea, Grupo);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListaGruposDet")]
        public async Task<IActionResult> getListaGruposDet(string? Grupo)
        {
            var result = await _txBultoHiladoGrupoService.ListaDet(Grupo);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListaGruposById")]
        public async Task<IActionResult> getListaGruposById(int? IdBultoHiladoGrupo)
        {
            var result = await _txBultoHiladoGrupoService.Obtener(IdBultoHiladoGrupo);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getValidarGrupo")]
        public async Task<IActionResult> getValidarGrupo(string? Grupo)
        {
            var result = await _txBultoHiladoGrupoService.Validar(Grupo);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListaGruposByCode")]
        public async Task<IActionResult> getListaGruposByCode(string? Grupo)
        {
            var result = await _txBultoHiladoGrupoService.ObtenerByCode(Grupo);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListaGruposDetById")]
        public async Task<IActionResult> getListaGruposDetById(int? IdBultoHiladoGrupo)
        {
            var result = await _txBultoHiladoGrupoService.ListaDetById(IdBultoHiladoGrupo);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListaGruposByCodUbicacion")]
        public async Task<IActionResult> getListaGruposByCodUbicacion(string? CodUbicacion)
        {
            var result = await _txBultoHiladoGrupoService.ListaByIdUbicacion(CodUbicacion);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postUbicarReubicarGrupo")]
        public async Task<IActionResult> UbicarReubicar([FromBody] txBultoHiladoGrupoUbicaReubicaParameter parameters)
        {
            var hiladoGrupoUbicaReubica = setDataTxBultoHiladoGrupoUbicaReubica(parameters);
            var result = await _txBultoHiladoGrupoService.UbicarReubicar(hiladoGrupoUbicaReubica);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 2 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        #region SET VALORES
        private Tx_Bulto_Hilado_Grupo setDataTxBultoHiladoGrupo(txBultoHiladoGrupoParameter txBultoHiladoGrupoParameter)
        {
            return new Tx_Bulto_Hilado_Grupo
            {
                Accion = txBultoHiladoGrupoParameter.Accion,
                Id_Bulto_Hilado_Grupo = txBultoHiladoGrupoParameter.Id_Bulto_Hilado_Grupo,
                Num_Corre = txBultoHiladoGrupoParameter.Num_Corre,
                Usu_Registro = txBultoHiladoGrupoParameter.Cod_Usuario
            };

        }

        private Tx_Bulto_Hilado_Grupo setDataTxBultoHiladoGrupoUbicaReubica(txBultoHiladoGrupoUbicaReubicaParameter txBultoHiladoGrupoUbicaReubicaParameter)
        {
            return new Tx_Bulto_Hilado_Grupo
            {
                Accion = txBultoHiladoGrupoUbicaReubicaParameter.Accion,
                Grupo = txBultoHiladoGrupoUbicaReubicaParameter.Grupo,
                Cod_Ubicacion = txBultoHiladoGrupoUbicaReubicaParameter.Cod_Ubicacion,
                Usu_Registro = txBultoHiladoGrupoUbicaReubicaParameter.Cod_Usuario
            };

        }

        #endregion
    }
}

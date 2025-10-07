using System.Collections;
using System.Data.SqlClient;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.Services.Implementacion.CorteEncogimiento;
using ic.backend.precotex.web.Service.Services.Implementacion.RegistroPartidaParihuela;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using static ic.backend.precotex.web.Api.Controllers.RegistroPartidaParihuela.RegistroPartidaParihuelaController;

namespace ic.backend.precotex.web.Api.Controllers.RegistroPartidaParihuela
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroPartidaParihuelaController : ControllerBase
    {
        private readonly IRegistroPartidaParihuelaService _IRegistroPartidaParihuela;

        public RegistroPartidaParihuelaController(IRegistroPartidaParihuelaService txIRegistroPartidaParihuela)
        {
            _IRegistroPartidaParihuela = txIRegistroPartidaParihuela;
        }

        [HttpGet]
        [Route("getObtenerDetPartida")]
        public async Task<IActionResult> getObtenerDetPartida(string pCod_Partida, string pOpcion)
        {

            var result = await _IRegistroPartidaParihuela.ObtenerDetPartida(pCod_Partida, pOpcion);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postUpdateDetPartida")]
        public async Task<IActionResult> UpdateDetPartida([FromBody] UpdateDetPartidaRequest request)
        {

            var result = await _IRegistroPartidaParihuela.UpdateDetPartida(request.pData, request.pCod_Usuario, request.pEstadoParihuela);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            //result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);

        }


        [HttpGet]
        [Route("GetCategoriasById")]
        public async Task<IActionResult> GetCategoriasById([FromQuery] string idPartida)
        {
            if (string.IsNullOrEmpty(idPartida))
            {
                return BadRequest("El ID es requerido");
            }

            var categorias = await _IRegistroPartidaParihuela.ObtenerCategoriasPorId(idPartida);

            if (categorias.Success)
            {
                categorias.CodeResult = StatusCodes.Status200OK;
                return Ok(categorias);
            }

            categorias.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(categorias);
        }


        [HttpGet]
        [Route("getValidarMermaPartida")]
        public async Task<IActionResult> getValidarMermaPartida(string pCod_Partida)
        {

            var result = await _IRegistroPartidaParihuela.ValidarMerma(pCod_Partida);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                //return Ok(result);
            }
            if(result.Elements is null)
            {
                await _IRegistroPartidaParihuela.UpdateEstadoMermaAsync(pCod_Partida);
            result.CodeResult = StatusCodes.Status400BadRequest;
            }
            //result.CodeResult = StatusCodes.Status400BadRequest;
            return Ok(result);
        }

        [HttpPost]
        [Route("getEnviarDespacho")]
        public async Task<IActionResult> getEnviarDespacho([FromQuery] string pCod_Partida)
        {

            var result = await _IRegistroPartidaParihuela.EnviarDespacho(pCod_Partida);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return Ok(result);

        }

        [HttpPost]
        [Route("postEnviarCabecera")]
        public async Task<IActionResult> postEnviarCabecera([FromBody] string pCod_Partida)
        {

            var result = await _IRegistroPartidaParihuela.EnviarCabecera(pCod_Partida);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return Ok(result);

        }



        // Modelo para recibir los datos
        public class UpdateDetPartidaRequest
        {
            public List<E_RegistroPartidaParihuela> pData { get; set; }  // Cambiar el tipo de dato según lo que realmente sea pData
            public string pCod_Usuario { get; set; }
            public string pEstadoParihuela { get; set; }
        }

    }
}

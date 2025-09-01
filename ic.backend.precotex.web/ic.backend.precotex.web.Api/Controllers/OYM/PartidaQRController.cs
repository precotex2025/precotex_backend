using ic.backend.precotex.web.Api.Parameters;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.Services.DDT;
using ic.backend.precotex.web.Service.Services.Implementacion.DDT;
using ic.backend.precotex.web.Service.Services.Implementacion.OYM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ic.backend.precotex.web.Api.Controllers.OYM
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartidaQRController : ControllerBase
    {
        private readonly IPartidaQRService _partidaQRService;

        public PartidaQRController(IPartidaQRService partidaQRService)
        {
            _partidaQRService = partidaQRService;
        }

        [HttpGet]
        [Route("getObtieneInformacionPartidaQR")]
        public async Task<IActionResult> getObtieneInformacionPartidaQR(string Cod_OrdTra, int Num_Secuencia)
        {
            var result = await _partidaQRService.ObtieneInformacionPartidaQR(Cod_OrdTra, Num_Secuencia);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postProcesoMntoColgador")]
        public async Task<IActionResult> postProcesoMntoColgador([FromBody] PartidaQRParameter parameters)
        {
            Tx_Partida_IA _tx_Partida_IA = new Tx_Partida_IA
            {
                Cod_OrdTra = parameters.Cod_OrdTra,
                Num_Secuencia = parameters.Num_Secuencia,
                Largo = parameters.Largo,
                Ancho = parameters.Ancho,
                Usu_Registro = parameters.Cod_Usuario
            };
            var result = await _partidaQRService.ProcesoInsertarPartidaQR(_tx_Partida_IA, parameters.Accion!);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

    }
}

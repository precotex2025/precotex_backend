using ic.backend.precotex.web.Service.Services.Implementacion.Mantto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ic.backend.precotex.web.Api.Controllers.Mantto
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioSedeController : ControllerBase
    {
        private readonly ITxUsuarioSedeService _txUsuarioSedeService;

        public UsuarioSedeController(ITxUsuarioSedeService txUsuarioSedeService)
        {
            _txUsuarioSedeService = txUsuarioSedeService;
        }

        [HttpGet]
        [Route("getListaUsuarioSedeByUser")]
        public async Task<IActionResult> getListaUsuarioSedeByUser(string pCodUsuario)
        {

            var result = await _txUsuarioSedeService.ListaUsuarioSedeByUser(pCodUsuario);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }
    }
}

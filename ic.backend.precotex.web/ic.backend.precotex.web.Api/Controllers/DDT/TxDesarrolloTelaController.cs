using Azure;
using ic.backend.precotex.web.Api.Parameters;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.Services.DDT;
using ic.backend.precotex.web.Service.Services.Implementacion.DDT;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ic.backend.precotex.web.Api.Controllers.DDT
{
    [Route("api/[controller]")]
    [ApiController]
    public class TxDesarrolloTelaController : ControllerBase
    {

        private readonly ITxDesarrolloTelaService _txDesarrolloTelaService;

        public TxDesarrolloTelaController(ITxDesarrolloTelaService txDesarrolloTelaService)
        {
            _txDesarrolloTelaService = txDesarrolloTelaService;
        }

        [HttpPost]
        [Route("postListadoDesarrolloTelas")]
        public async Task<IActionResult> postListadoDesarrolloTelas([FromBody] txDesarrolloTelasParameter filtro)
        {
      
            var result = await _txDesarrolloTelaService.ListadoDesarrolloTelas(filtro.Accion!,filtro.Cod_Tela!,filtro.Cod_Version!, filtro.Nom_Version!,
                                                                               filtro.Comentario!, filtro.Ruta_Archivo!, filtro.Cod_Motivo_Solicitud!, 
                                                                               filtro.Comentario_Solicitud!, filtro.Cod_Usuario!);
            if (result == null || !result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost]
        [Route("postProcesoDesarrolloTela")]
        public async Task<IActionResult> postProcesoDesarrolloTela([FromBody] txDesarrolloTelasParameter filtro)
        {
            var result = await _txDesarrolloTelaService.ProcesoDesarrolloTela(filtro.Accion!, filtro.Cod_Tela!, filtro.Cod_Version!, filtro.Nom_Version!,
                                                                               filtro.Comentario!, filtro.Ruta_Archivo!, filtro.Cod_Motivo_Solicitud!,
                                                                               filtro.Comentario_Solicitud!, filtro.Cod_Usuario!);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getPdf")]
        public IActionResult getPdf(string ruta)
        {
            string baseFolder = @"\\SERVERDATA\Estilos";
            string fileName = ruta;

            var fullPath = Path.Combine(baseFolder, fileName);

            if (!System.IO.File.Exists(fullPath))
                return NotFound("Archivo no encontrado");

            var bytes = System.IO.File.ReadAllBytes(fullPath);
            return File(bytes, "application/pdf");
        }

    }
}

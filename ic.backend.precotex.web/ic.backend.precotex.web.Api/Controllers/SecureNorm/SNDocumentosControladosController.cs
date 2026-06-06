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
    public class SNDocumentosControladosController : ControllerBase
    {
        private readonly ISNDocumentosControladosService _sNDocumentosControladosService;
        public SNDocumentosControladosController(ISNDocumentosControladosService sNDocumentosControladosService)
        {
            _sNDocumentosControladosService = sNDocumentosControladosService;
        }


        [HttpPost]
        [Route("postProcesoMnto")]
        public async Task<IActionResult> postProcesoMnto([FromBody] SNDocumentosControladosParameter parametros)
        {
            SN_Documentos_Controlados documento = new SN_Documentos_Controlados
            {


                Codigo_Documentos_Controlados = parametros.Codigo_Documentos_Controlados ?? "",
                Codigo_Proceso = parametros.Codigo_Proceso,
                Codigo_Carpeta_Control = parametros.Codigo_Carpeta_Control ?? "",
                Codigo_Normas = parametros.Codigo_Normas ?? "",
                Codigo_Tiempo_Conservacion = parametros.Codigo_Tiempo_Conservacion ?? "",
                Codigo_Tipo_Descarga = parametros.Codigo_Tipo_Descarga ?? "",
                Denominacion = parametros.Denominacion ?? "",
                Codigo_Documento = parametros.Codigo_Documento ?? "",
                Version_Documento = parametros.Version_Documento ?? "",
                Ruta_Adjunto = parametros.Ruta_Adjunto ?? "",
                Descripcion = parametros.Descripcion ?? "",
                bRegistroAsociado = parametros.bRegistroAsociado,
                bRequiereRevision = parametros.bRequiereRevision,
                Flg_Estado = parametros.Flg_Estado,
                Flg_Activo = parametros.Flg_Activo,
                Cod_Usuario = parametros.Cod_Usuario ?? ""
            };

            var result = await _sNDocumentosControladosService.ProcesoMnto(documento, parametros.Accion!);
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

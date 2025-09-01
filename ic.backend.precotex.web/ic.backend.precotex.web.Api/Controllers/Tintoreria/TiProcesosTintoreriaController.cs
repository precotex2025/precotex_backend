using ic.backend.precotex.web.Service.Services.Almacen;
using ic.backend.precotex.web.Service.Services.Implementacion.Almacen;
using ic.backend.precotex.web.Service.Services.Implementacion.Tintoreria;
using ic.backend.precotex.web.Service.Services.Tintoreria;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace ic.backend.precotex.web.Api.Controllers.Tintoreria
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiProcesosTintoreriaController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly ITiProcesosTintoreriaService _ITiProcesosTintoreriaService;
        public TiProcesosTintoreriaController(ITiProcesosTintoreriaService ITiProcesosTintoreriaService,
            HttpClient httpClient)
        {
            _ITiProcesosTintoreriaService = ITiProcesosTintoreriaService;
              _httpClient = httpClient;
        }

        [HttpGet]
        [Route("getListaEstatusControlTenido")]
        public async Task<IActionResult> getListaEstatusControlTenido(string? Cod_Ordtra, DateTime? Fecha_Ini, DateTime? Fecha_Fin, string Cod_Usuario)
        {
            var result = await _ITiProcesosTintoreriaService.ListaEstatusControlTenido(Cod_Ordtra, Fecha_Ini, Fecha_Fin, Cod_Usuario);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtieneMuestraControlProceso")]
        public async Task<IActionResult> getObtieneMuestraControlProceso(string? Cod_Ordtra, DateTime? Fecha_Ini, DateTime? Fecha_Fin)
        {
            var result = await _ITiProcesosTintoreriaService.ListaControlProcesosTintoreria(Cod_Ordtra, Fecha_Ini, Fecha_Fin);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListaDetalleToberaPruebaTenido")]
        public async Task<IActionResult> getListaDetalleToberaPruebaTenido(string Cod_Ordtra, string IdOrgatexUnico)
        {
            var result = await _ITiProcesosTintoreriaService.ListaDetalleToberaPruebaTenido(Cod_Ordtra, IdOrgatexUnico);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }


        [HttpGet]
        [Route("GetImageBase64FromUrlAsync")]
        public async Task<IActionResult> GetImageBase64FromUrlAsync(string imageUrl)
        {
            try
            {
                // Realiza la solicitud HTTP para obtener la imagen
                var imageBytes = await _httpClient.GetByteArrayAsync(imageUrl);

                // Convierte los bytes de la imagen a Base64
                var base64String = Convert.ToBase64String(imageBytes);

                return Ok(new { Base64Image = base64String }); ;
            }
            catch (Exception ex)
            {
                // En caso de error, lanzar una excepción
                return BadRequest(ex);
            }
        }

    }
}

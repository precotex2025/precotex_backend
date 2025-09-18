using ic.backend.precotex.web.Api.Parameters;
using ic.backend.precotex.web.Entity.Entities.QuejasReclamos;
using ic.backend.precotex.web.Entity.Entities.RetiroRepuestos;
using ic.backend.precotex.web.Service.Services.Implementacion.RetiroRepuestos;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ic.backend.precotex.web.Api.Controllers.RetiroRepuestos
{
    [Route("api/[controller]")]
    [ApiController]
    public class TxRetiroRepuestosController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        public readonly ITxRetiroRepuestosService _txRetiroRepuestosService;

        public TxRetiroRepuestosController(ITxRetiroRepuestosService txRetiroRepuestosService, HttpClient httpClient)
        {
            _txRetiroRepuestosService = txRetiroRepuestosService;
            _httpClient = httpClient;
        }

        /******************************************CABECERA************************************************************/

        //OBTIENE TODA LA LISTA DE RETIROS
        [HttpGet]
        [Route("getListaRetiros")]
        public async Task<IActionResult> getListaRetiros(DateTime FecIni, DateTime FecFin)
        {
            var result = await _txRetiroRepuestosService.ListaRetiros(FecIni, FecFin);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        //OBTIENE UN RETIRO POR NUMERO DE REQUERIMIENTO
        [HttpGet]
        [Route("getListaRetirosPorNumRequerimiento")]
        public async Task<IActionResult> getListaRetirosPorNumRequerimiento(int Num_Requerimiento)
        {
            var result = await _txRetiroRepuestosService.ListaRetirosPorNumRequerimiento(Num_Requerimiento);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        //REGISTRA UN RETIRO
        [HttpPost]
        [Route("postRegistrarRequerimiento")]
        public async Task<IActionResult> postRegistrarRequerimiento([FromBody] TxRegistroRetiroRepuestosParameter parametros)
        {
            Tx_Retiro_Repuestos _txRetiroRepuestos = new Tx_Retiro_Repuestos
            {
                Num_Requerimiento = parametros.Num_Requerimiento,
                //Fec_Requerimiento = parametros.Fec_Requerimiento,
                Cod_Seguridad = parametros.Cod_Seguridad,
                Cod_Mantenimiento = parametros.Cod_Mantenimiento,
                Nro_Precinto_Apertura = parametros.Nro_Precinto_Apertura,
                Nro_Precinto_Cierre = parametros.Nro_Precinto_Cierre
            };

            var result = await _txRetiroRepuestosService.RegistrarRequerimiento(_txRetiroRepuestos);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        //ACTUALIZA CABECERA REQUERIMIENTO
        [HttpPatch]
        [Route ("patchActualizarRequerimiento")]
        public async Task<IActionResult> patchActualizarRequerimiento([FromBody] TxRegistroRetiroRepuestosParameter parametros)
        {
            Tx_Retiro_Repuestos _txRetiroRepuestos = new Tx_Retiro_Repuestos
            {
                Num_Requerimiento = parametros.Num_Requerimiento,
                Cod_Seguridad = parametros.Cod_Seguridad,
                Cod_Mantenimiento = parametros.Cod_Mantenimiento,
                Nro_Precinto_Apertura = parametros.Nro_Precinto_Apertura
            };

            var result = await _txRetiroRepuestosService.ActualizarRequerimiento(_txRetiroRepuestos);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        //ACTUALIZA CABECERA REQUERIMIENTO -> AGREGA PRECINTO CIERRE
        [HttpPatch]
        [Route ("patchActualizarRequerimientoPrecintoCierre")]
        public async Task<IActionResult> patchActualizarRequerimientoPrecintoCierre([FromBody] TxRegistroRetiroRepuestosParameter parametros)
        {
            Tx_Retiro_Repuestos _txRetiroRepuestos = new Tx_Retiro_Repuestos
            {
                Num_Requerimiento = parametros.Num_Requerimiento,
                Nro_Precinto_Cierre = parametros.Nro_Precinto_Cierre
            };

            var result = await _txRetiroRepuestosService.ActualizarRequerimientoPrecintoCierre(_txRetiroRepuestos);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }


        /******************************************DETALLE************************************************************/
        //OBTIENE EL DETALLE DE UN RETIRO
        [HttpGet]
        [Route("getListaRetiroDetallePorNumRequerimiento")]
        public async Task<IActionResult> getListaRetiroDetallePorNumRequerimiento(int Num_Requerimiento)
        {
            var result = await _txRetiroRepuestosService.ListaRetiroDetallePorNumRequerimiento(Num_Requerimiento);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        //OBTIENE LOS DATOS DE UN DETALLE DE UN RETIRO
        [HttpGet]
        [Route("getListaRetiroDetallePorNumReqySecuencia")]
        public async Task<IActionResult> getListaRetiroDetallePorNumReqySecuencia(int Num_Requerimiento, int Nro_Secuencia)
        {
            var result = await _txRetiroRepuestosService.ListaRetiroDetallePorNumReqySecuencia(Num_Requerimiento, Nro_Secuencia);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        //REGISTRA DETALLE DE UN RETIRO
        //[HttpPost]
        //[Route("postRegistrarRequerimientoDetalle")]
        //public async Task<IActionResult> postRegistrarRequerimientoDetalle([FromBody] TxRegistroRetiroRepuestosParameter_Detalle parametros)
        //{
        //    Tx_Retiro_Repuestos_Detalle _txRetiroRepuestosDetalle = new Tx_Retiro_Repuestos_Detalle
        //    {
        //        Num_Requerimiento = parametros.Num_Requerimiento,
        //        Cod_Item = parametros.Cod_Item,
        //        //Itm_Descripcion = parametros.Itm_Descripcion,
        //        Can_Requerida = parametros.Can_Requerida,
        //        //Itm_Unidad_Medida = parametros.Itm_Unidad_Medida,
        //        Rpt_Cambio = parametros.Rpt_Cambio,
        //        Itm_Foto = parametros.Itm_Foto
        //    };

        //    var result = await _txRetiroRepuestosService.RegistrarRequerimientoDetalle(_txRetiroRepuestosDetalle);
        //    if (result.Success)
        //    {
        //        result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
        //        return Ok(result);
        //    }

        //    result.CodeResult = StatusCodes.Status400BadRequest;
        //    return BadRequest(result);
        //}

        //ACTUALIZA DETALLE DE UN RETIRO
        //[HttpPatch]
        //[Route("patchActualizarRequerimientoDetalle")]
        //public async Task<IActionResult> RegistrarRequerimientoDetalle([FromBody] TxRegistroRetiroRepuestosParameter_Detalle parametros)
        //{
        //    Tx_Retiro_Repuestos_Detalle _txRetiroRepuestosDetalle = new Tx_Retiro_Repuestos_Detalle
        //    {
        //        Num_Requerimiento = parametros.Num_Requerimiento,
        //        Nro_Secuencia = parametros.Nro_Secuencia,
        //        Cod_Item = parametros.Cod_Item,
        //        Itm_Descripcion = parametros.Itm_Descripcion,
        //        Can_Requerida = parametros.Can_Requerida,
        //        Itm_Unidad_Medida = parametros.Itm_Unidad_Medida,
        //        Rpt_Cambio = parametros.Rpt_Cambio,
        //        Itm_Foto = parametros.Itm_Foto
        //    };

        //    var result = await _txRetiroRepuestosService.ActualizarRequerimientoDetalle(_txRetiroRepuestosDetalle);
        //    if (result.Success)
        //    {
        //        result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
        //        return Ok(result);
        //    }

        //    result.CodeResult = StatusCodes.Status400BadRequest;
        //    return BadRequest(result);
        //}

        /******************************************COMPLEMENTARIOS************************************************************/

        //OBTIENE LOS DATOS DE UN ITEM
        [HttpGet]
        [Route("getListaItems")]
        public async Task<IActionResult> getListaItems(string Cod_Item)
        {
            var result = await _txRetiroRepuestosService.ListaItems(Cod_Item);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }
        
        [HttpGet]
        [Route("getListaItemsCompletos")]
        public async Task<IActionResult> getListaItemsCompletos()
        {
            var result = await _txRetiroRepuestosService.ListaItemsCompletos();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        //OBTIENE USUARIO Y CODIGO
        [HttpGet]
        [Route("getListaRetiroRepuestoUsuario")]
        public async Task<IActionResult> getListaRetiroRepuestoUsuario(int Id_Usuario)
        {
            var result = await _txRetiroRepuestosService.ListaRetiroRepuestoUsuario(Id_Usuario);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }
            
            result.CodeResult= StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        //OBTIENE DATOS DE USUARIO POR TIPO
        
        [HttpGet]
        [Route("getListaRetiroRepuestoUsuarioPorTipo")]
        public async Task<IActionResult> getListaRetiroRepuestoUsuarioPorTipo()
        {
            int Tip_Usuario = 1;
            var result = await _txRetiroRepuestosService.ListaRetiroRepuestoUsuarioPorTipo(Tip_Usuario);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListaRetiroRepuestoUsuarioSeguridadNombres")]
        public async Task<IActionResult> getListaRetiroRepuestoUsuarioSeguridadNombres()
        {
            var result = await _txRetiroRepuestosService.ListaRetiroRepuestoUsuarioSeguridadNombres();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListaRetiroRepuestoUsuarioMantenimientoNombres")]
        public async Task<IActionResult> getListaRetiroRepuestoUsuarioMantenimientoNombres()
        {
            var result = await _txRetiroRepuestosService.ListaRetiroRepuestoUsuarioMantenimientoNombres();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListaDatosItemsPorNumReqySecuencia")]
        public async Task<IActionResult> getListaDatosItemsPorNumReqySecuencia(int Num_Requerimiento, int Nro_Secuencia)
        {
            var result = await _txRetiroRepuestosService.ListaDatosItemsPorNumReqySecuencia(Num_Requerimiento, Nro_Secuencia);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListaDatosReporte")]
        public async Task<IActionResult> getListaDatosReporte(DateTime FecIni, DateTime FecFin)
        {
            var result = await _txRetiroRepuestosService.ListaDatosReporte(FecIni, FecFin);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }


        [HttpPost]
        [Route("patchActualizarRequerimientoDetalle")]
        [ApiExplorerSettings(IgnoreApi = true)] // 👈 oculta de Swagger
        //public async Task<IActionResult> postProcesoConfirmarReclamo([FromForm] ProcesoConfirmarReclamo parameters, [FromForm][Required] IFormFile archivoCalidad)
        public async Task<IActionResult> postProcesoConfirmarReclamo()
        {
            //CAPTURAMOS LA TRAZA
            var form = Request.Form;
            var nNum_Requerimiento = form["nNum_Requerimiento"];
            //var sCod_Item = form["sCod_Item"];
            var nNum_Secuencia = form["nNum_Secuencia"];
            var nCan_Requerida = form["nCan_Requerida"];
            var sRpt_Cambio = form["sRpt_Cambio"];
            var sNombreArchivo = form["sNombre_Archivo"];


            //SI TIENE PROCESO POR CONFIRMAR 
            string nombreArchivo = string.Empty;
            string rutaBase = @"\\192.168.1.36\d$\dayala\imgRetiro\"; //Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "archivosReclamos"); 
            Directory.CreateDirectory(rutaBase); // Se asegura de que el directorio exista

            var claveArchivo = $"form['itm_Foto']";
            var archivo = form.Files.FirstOrDefault();

            if (archivo != null && archivo.Length > 0)
            {

                nombreArchivo = $"{Guid.NewGuid()}_{archivo.FileName}";
                var rutaArchivo = Path.Combine(rutaBase, nombreArchivo);
                //.Replace(" ", "%20")
                // Eliminar si ya existe (raro con GUID, pero por si acaso)
                if (System.IO.File.Exists(rutaArchivo))
                {
                    System.IO.File.Delete(rutaArchivo);
                }

                using (var stream = new FileStream(rutaArchivo, FileMode.Create))
                {
                    await archivo.CopyToAsync(stream);
                }
            }

            var result = await _txRetiroRepuestosService.ActualizarRequerimientoDetalle(nNum_Requerimiento, nNum_Secuencia, nCan_Requerida, sRpt_Cambio, nombreArchivo);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
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

        [HttpPost("guardar-excel")]
        public async Task<IActionResult> GuardarExcel(int Num_Requerimiento)
        {
            try
            {
                using var memoryStream = new MemoryStream();
                await Request.Body.CopyToAsync(memoryStream);

                // Asegúrate de que el stream esté en posición 0
                memoryStream.Position = 0;

                var fileName = $"Reporte_{Num_Requerimiento}.xlsx";
                var filePath = Path.Combine(@"\\192.168.1.36\d$\dayala\Reportes-RetiroRepuestos\", fileName);

                // Guarda el archivo como binario puro
                await System.IO.File.WriteAllBytesAsync(filePath, memoryStream.ToArray());

                return Ok(new { message = "Archivo guardado correctamente", path = filePath });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar el archivo: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getListaRetiroRepuestosPorIdRequerimientoMAX")]
        public async Task<IActionResult> ListaRetiroRepuestosPorIdRequerimientoMAX()
        {
            var result = await _txRetiroRepuestosService.ListaRetiroRepuestosPorIdRequerimientoMAX();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postEnviarCorreo")]
        public async Task<IActionResult> postEnviarCorreo([FromBody] string cuerpo)
        {
            var result = await _txRetiroRepuestosService.EnviarCorreo();
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

using System.Xml;
using ic.backend.precotex.web.Entity.Entities.QuejasReclamos;
using ic.backend.precotex.web.Service.Services.Implementacion.QuejasReclamos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Data.SqlClient;
using System.Linq;
using ic.backend.precotex.web.Entity.Entities.CalificacionRollosEnProceso;
using ic.backend.precotex.web.Api.Parameters;
using System;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace ic.backend.precotex.web.Api.Controllers.QuejasReclamos
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuejasReclamosController : ControllerBase
    {
        private readonly IQuejasReclamosService _IClientes;
        private readonly IWebHostEnvironment _environment;

        public QuejasReclamosController(IQuejasReclamosService txtIClientes, IWebHostEnvironment environment)
        {
            _IClientes = txtIClientes;
            _environment = environment;
        }

        [HttpGet]
        [Route("getObtenerListaClientes")]
        public async Task<IActionResult> obtenerListaClientes()
        {
            var result = await _IClientes.ObtenerClintes();
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtenerEstado")]
        public async Task<IActionResult> obtenerEstado()
        {
            var result = await _IClientes.ObtenerEstado();
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtenerUnidadNegocio")]
        public async Task<IActionResult> obtenerUnidadNegocio()
        {
            var result = await _IClientes.ObtenerUnidadNegocio();
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtenerResponsable")]
        public async Task<IActionResult> obtenerResponsable()
        {
            var result = await _IClientes.ObtenerResponsable();
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtenerMotivo")]
        public async Task<IActionResult> obtenerMotivo()
        {
            var result = await _IClientes.ObtenerMotivo();
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postGuardarQuejasReclamos")]
        public async Task<IActionResult> GuardarReclamos()
        {
            try
            {
                var form = Request.Form;
                var reclamos = new List<ReclamoClienteDto>();
                var isNew = false;  
                int index = 0;
                string? Ncaso = string.Empty;

                while (form.ContainsKey($"reclamos[{index}][cliente]"))
                {
                    var reclamo = new ReclamoClienteDto
                    {
                        Id = form[$"reclamos[{index}][id]"],
                        NroCaso = form[$"reclamos[{index}][nroCaso]"],
                        Cliente = form[$"reclamos[{index}][cliente]"],
                        TipoRegistro = form[$"reclamos[{index}][tipoRegistro]"],
                        UnidadNegocio = form[$"reclamos[{index}][unidadNegocio]"],
                        UsuarioRegistro = form[$"reclamos[{index}][usuarioRegistro]"],
                        Responsable = form[$"reclamos[{index}][responsable]"],
                        MotivoRegistro = form[$"reclamos[{index}][motivoRegistro]"],
                        EstadoSolicitud = form[$"reclamos[{index}][estadoSolicitud]"],
                        Observacion = form[$"reclamos[{index}][observacion]"],
                        //CAMPOS NUEVOS
                        Cod_Cliente_Tex = form[$"reclamos[{index}][cod_Cliente_Tex]"],
                        Cod_Ordtra = form[$"reclamos[{index}][cod_Ordtra]"],
                        Cod_Tela = form[$"reclamos[{index}][Cod_Tela]"],
                        Cod_Color = form[$"reclamos[{index}][cod_Color]"],
                        Id_Unidad_NegocioKey = Convert.ToInt32(form[$"reclamos[{index}][cod_Unidad_Negocio]"]),
                        Cod_Motivo = form[$"reclamos[{index}][cod_Motivo]"],
                        IdArea = Convert.ToInt32(form[$"reclamos[{index}][idArea]"]),
                        IdResponsable = Convert.ToInt32(form[$"reclamos[{index}][idResponsable]"]),
                        //Campos Nuevos
                        Cod_TemCli = form[$"reclamos[{index}][Cod_TemCli]"],
                        Cod_EstCli = form[$"reclamos[{index}][Cod_EstCli]"]
                    };

                    string rutaBase = @"D:\archivosReclamos"; //Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "archivosReclamos"); 
                    Directory.CreateDirectory(rutaBase); // Se asegura de que el directorio exista

                    var claveArchivo = $"reclamos[{index}][archivoAdjunto]";
                    var archivo = form.Files.FirstOrDefault(f => f.Name == claveArchivo);

                    if (archivo != null && archivo.Length > 0)
                    {
                        //reclamo.archivoAdjunto = archivo;

                        var nombreArchivo = $"{Guid.NewGuid()}_{archivo.FileName}";
                        // $"{Guid.NewGuid()}_{Path.GetFileName(archivo.FileName)}";
                        var rutaArchivo = Path.Combine(rutaBase, nombreArchivo);

                        // Eliminar si ya existe (raro con GUID, pero por si acaso)
                        if (System.IO.File.Exists(rutaArchivo))
                        {
                            System.IO.File.Delete(rutaArchivo);
                        }

                        using (var stream = new FileStream(rutaArchivo, FileMode.Create))
                        {
                            await archivo.CopyToAsync(stream);
                        }

                        Console.WriteLine($"Archivo guardado en: {rutaArchivo}");
                        // Aquí podrías guardar la ruta o nombre en el DTO si deseas
                        reclamo.archivoAdjunto = nombreArchivo;
                    }
                    
                    //if (reclamo.NroCaso == "undefined")
                    //{
                    //    isNew = true;
                    //}

                    reclamos.Add(reclamo);
                    index++;
                }

                if (reclamos.Select(x =>x.NroCaso).FirstOrDefault() == "undefined")
                {
                    isNew = true;
                }

                if (!isNew)
                {
                    var idsForm = reclamos.Select(r => r.Id).ToList();

                    var reclamosBD = await _IClientes.ObtenerDetReclamos(reclamos.Select(x => x.NroCaso).FirstOrDefault());

                    foreach (var detalle in reclamosBD.Elements.Where(x => x.Id != "undefined"))
                    {

                        if (!idsForm.Contains(detalle.Id))
                        {
                            // Eliminar de la BD porque ya no está en el formulario
                            await _IClientes.EliminarReclamoDetalle(detalle.Id);
                        }
                    }
                }

                var result = await _IClientes.GuardarReclamo(reclamos, isNew);
                if (result.Success)
                {
                    result.CodeResult = StatusCodes.Status200OK;
                    return Ok(result);
                }

                result.CodeResult = StatusCodes.Status400BadRequest;
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar reclamos: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    mensaje = "Ocurrió un error al procesar los reclamos.",
                    error = ex.Message
                });
            }
        }

        [HttpGet("getArchivoReclamo")]
        public IActionResult GetArchivo([FromQuery] string nombreArchivo)
        {
            var rutaBase = @"D:\archivosReclamos"; //Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "archivosReclamos"); 
            var rutaArchivo = Path.Combine(rutaBase, nombreArchivo);

            if (!System.IO.File.Exists(rutaArchivo))
            {
                return NotFound("Archivo no encontrado.");
            }

            var bytes = System.IO.File.ReadAllBytes(rutaArchivo);
            var contentType = "application/octet-stream"; // puedes detectar tipo según la extensión

            return File(bytes, contentType, nombreArchivo); // esta es la línea que da el archivo al cliente
        }

        [HttpPost("postObtenerReclamos")]
        public async Task<IActionResult> ObtenerReclamos([FromBody] FiltroReclamoDto filtro)
        {
            var result = await _IClientes.ObtenerReclamos(filtro);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost("postObtenerDetReclamos")]
        public async Task<IActionResult> ObtenerdDetReclamos([FromBody] FiltroReclamoDto filtro)
        {
            var result = await _IClientes.ObtenerDetReclamos(filtro.NroCaso);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpDelete("deleteReclamos/{nroCaso}")]
        public async Task<IActionResult> DeleteReclamo(string nroCaso)
        {
            var result = await _IClientes.EliminarReclamos(nroCaso);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet("getBuscarPorPartida")]
        public async Task<IActionResult> getBuscarPorPartida([FromQuery] string partida)
        {
            if (string.IsNullOrWhiteSpace(partida))
            {
                return BadRequest("Debe proporcionar 'partida'");
            }

            var result = await _IClientes.BuscarPorPartida(partida);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListaUnidadNegocio")]
        public async Task<IActionResult> getListaUnidadNegocio()
        {
            var result = await _IClientes.ListaUnidadNegocio();
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListaAreasCalidad")]
        public async Task<IActionResult> getListaAreasCalidad()
        {
            var result = await _IClientes.ListaAreasCalidad();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }


        [HttpPost]
        [Route("postAvanzaEstadoReclamo")]
        public async Task<IActionResult> postAvanzaEstadoReclamo([FromBody] int iId)
        {

            var result = await _IClientes.AvanzaEstadoReclamo(iId);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postProcesoConfirmarReclamo")]
        [ApiExplorerSettings(IgnoreApi = true)] // 👈 oculta de Swagger
        //public async Task<IActionResult> postProcesoConfirmarReclamo([FromForm] ProcesoConfirmarReclamo parameters, [FromForm][Required] IFormFile archivoCalidad)
        public async Task<IActionResult> postProcesoConfirmarReclamo()
        {
            //CAPTURAMOS LA TRAZA
            var form = Request.Form;
            var sNrCaso = form["sNroCaso"];
            var sNombreArchivoCalidad = form["sNombreArchivoCalidad"];
            var sObservacionCalidad = form["sObservacionCalidad"];
            var sCodAreaResponsableCalidad = form["sCodAreaResponsableCalidad"];
            var sCod_Usuario = form["sCod_Usuario"];
        
            //SI TIENE PROCESO POR CONFIRMAR 
            string nombreArchivo = string.Empty;
            string rutaBase = @"D:\archivosReclamos"; //Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "archivosReclamos"); 
            Directory.CreateDirectory(rutaBase); // Se asegura de que el directorio exista

            var claveArchivo = $"form['archivoCalidad']";
            var archivo = form.Files.FirstOrDefault();

            if (archivo != null && archivo.Length > 0)
                {

                nombreArchivo = $"{Guid.NewGuid()}_{archivo.FileName}";
                var rutaArchivo = Path.Combine(rutaBase, nombreArchivo);

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

            var result = await _IClientes.ProcesoConfirmarReclamo(sNrCaso!, nombreArchivo, sObservacionCalidad!, sCodAreaResponsableCalidad!, sCod_Usuario!);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListaTipoConsecuencia")]
        public async Task<IActionResult> getListaTipoConsecuencia()
        {
            var result = await _IClientes.ListaTipoConsecuencia();
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet("getListaSubTipoDevolucion")]
        public async Task<IActionResult> getListaSubTipoDevolucion([FromQuery] string sCod_Tipo_Consecuencia)
        {
            if (string.IsNullOrWhiteSpace(sCod_Tipo_Consecuencia))
            {
                return BadRequest("Debe proporcionar, tipo de consecuencia");
            }

            var result = await _IClientes.ListaSubTipoDevolucion(sCod_Tipo_Consecuencia);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postProcesoCerrarReclamo")]
        public async Task<IActionResult> postProcesoCerrarReclamo([FromBody] ProcesoCerrarReclamoParameter parameter)
        {

            var result = await _IClientes.ProcesoCerrarReclamo(parameter.NroCaso, parameter.Cod_Tipo_Consecuencia, parameter.Cod_SubTipo_Devolucion, parameter.Flg_NotaCredito, parameter.Flg_FleteAereo, parameter.Observacion_Comercial_Cierre, parameter.Cod_Usuario);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }


        [HttpGet("getObtieneUsuarioArea")]
        public async Task<IActionResult> getObtieneUsuarioArea([FromQuery] string? Cod_Trabajador)
        {
            if (string.IsNullOrWhiteSpace(Cod_Trabajador))
            {
                return BadRequest("Debe proporcionar, codigo de trabajador");
            }

            var result = await _IClientes.ObtieneUsuarioArea(Cod_Trabajador);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet("getObtieneDetalleInformeCalidad")]
        public async Task<IActionResult> getObtieneDetalleInformeCalidad([FromQuery] int Id)
        {
            var result = await _IClientes.ObtieneDetalleInformeCalidad(Id);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }


        [HttpGet("getObtieneDetalleInformeComercial")]
        public async Task<IActionResult> getObtieneDetalleInformeComercial([FromQuery] int Id)
        {
            var result = await _IClientes.ObtieneDetalleInformeComercial(Id);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet("getDescargar")]
        public IActionResult DescargarArchivo([FromQuery] string fileName)
        {
            try
            {
                // Ruta completa
                var path = Path.Combine(@"D:\archivosReclamos", fileName);

                if (!System.IO.File.Exists(path))
                    return NotFound("El archivo no existe.");

                // Detectar tipo MIME automáticamente
                var mimeType = "application/octet-stream";
                var content = System.IO.File.ReadAllBytes(path);

                return File(content, mimeType, fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al descargar archivo: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getListaEstados")]
        public async Task<IActionResult> getListaEstados()
        {
            var result = await _IClientes.ListaEstados();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost("getExportarReclamo")]
        public async Task<IActionResult> getExportarReclamo([FromBody] FiltroReclamoDto filtro)
        {
            var result = await _IClientes.ExportarReclamo(filtro);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet("getObtieneTemporada")]
        public async Task<IActionResult> getObtieneTemporada(string sCodCliente)
        {
            if (string.IsNullOrWhiteSpace(sCodCliente))
            {
                return BadRequest("Debe proporcionar, codigo de cliente");
            }

            var result = await _IClientes.ObtieneTemporada(sCodCliente);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet("getObtieneEstilo")]
        public async Task<IActionResult> getObtieneEstilo(string sCodCliente, string sTemporada)
        {
            if (string.IsNullOrWhiteSpace(sCodCliente))
            {
                return BadRequest("Debe proporcionar, codigo de cliente");
            }

            var result = await _IClientes.ObtieneEstilo(sCodCliente, sTemporada);
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

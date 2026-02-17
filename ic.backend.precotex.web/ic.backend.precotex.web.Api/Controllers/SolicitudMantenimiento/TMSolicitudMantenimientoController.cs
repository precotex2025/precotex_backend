using ic.backend.precotex.web.Api.Parameters;
using ic.backend.precotex.web.Entity.Entities.CalificacionRollosEnProceso;
using ic.backend.precotex.web.Entity.Entities.Memorandum;
using ic.backend.precotex.web.Entity.Entities.SolicitudMantenimiento;
using ic.backend.precotex.web.Service.Services.Implementacion.SolicitudMantenimiento;
using ic.backend.precotex.web.Service.Services.Implementacion.WallyChat;
using ic.backend.precotex.web.Service.Services.WallyChat;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Graph.Models;
using Org.BouncyCastle.Asn1.Crmf;
using System.IO;
using System.Net.Http.Headers;
using ZXing;
using static iTextSharp.text.pdf.AcroFields;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace ic.backend.precotex.web.Api.Controllers.SolicitudMantenimiento
{
    [Route("api/[controller]")]
    [ApiController]

    public class TMSolicitudMantenimientoController : ControllerBase
    {
        private readonly ITMSolicitudMantenimientoService _tMSolicitudMantenimientoService;
        private readonly IWaliChatService _waliChatService;
        private readonly IConfiguration _configuration;

        public TMSolicitudMantenimientoController(ITMSolicitudMantenimientoService tMSolicitudMantenimientoService,
                                                   IWaliChatService waliChatService,
                                                   IConfiguration configuration)
        {
            _tMSolicitudMantenimientoService = tMSolicitudMantenimientoService;
            _waliChatService = waliChatService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("postProcesoMntoSolicitudMantenimiento")]
        [ApiExplorerSettings(IgnoreApi = true)]
        //public async Task<IActionResult> postProcesoMntoSolicitudMantenimiento([FromBody] TmSolicitudMantenimientoParameter parameters)
        public async Task<IActionResult> postProcesoMntoSolicitudMantenimiento()
        {
            bool bExisteImagen = false;

            //string sGrupoA = _configuration["WaliChat:GrupoA"]!;
            //string sGrupoB = _configuration["WaliChat:GrupoB"]!;
            //string sGrupoC = _configuration["WaliChat:GrupoC"]!;
            //string sGrupoD = _configuration["WaliChat:GrupoD"]!;

            try
            {
                var form = Request.Form;
                var sOpcion = form["sOpcion"];
                var sCod_Solicitud = form["sCod_Solicitud"];
                var sCod_Area = form["sCod_Area"];
                var sCod_Maquina = form["sCod_Maquina"];
                var sObservacion = form["sObservacion"];
                var sPrioridad = form["sPrioridad"];
                var sParo_Maquina = form["sParo_Maquina"];
                var sHora_Inicio = form["sHora_Inicio"];
                var sUsu_Registro = form["sUsu_Registro"];
                var sRuta_Fotografia = form["sRuta_Fotografia"];
                //archivo
                var claveArchivo = $"form['itm_Foto']";
                var archivo = form.Files.FirstOrDefault();
                string nombreArchivo = string.Empty;

                if (archivo != null && archivo.Length > 0)
                {
                    bExisteImagen = true;

                    //ruta
                    string rutaBase = @"D:\htdocs\app\foto"; //Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "archivosReclamos"); 
                    Directory.CreateDirectory(rutaBase); // Se asegura de que el directorio exista

                    nombreArchivo = $"{Guid.NewGuid()}_{archivo.FileName}";
                    var rutaArchivo = Path.Combine(rutaBase, nombreArchivo);

                    if (System.IO.File.Exists(rutaArchivo))
                    {
                        System.IO.File.Delete(rutaArchivo);
                    }

                    using (var stream = new FileStream(rutaArchivo, FileMode.Create))
                    {
                        await archivo.CopyToAsync(stream);
                    }
                }

                //Preparamos la data
                TM_Solicitud_Mantenimiento _tmSolicitudMantenimiento = new TM_Solicitud_Mantenimiento
                {
                    Cod_Solicitud = sCod_Solicitud,
                    Cod_Area = sCod_Area,
                    Cod_Maquina = sCod_Maquina,
                    Observacion = sObservacion,
                    Prioridad = sPrioridad,
                    Paro_Maquina = sParo_Maquina == "1" ? true : false,
                    Ruta_Fotografia = nombreArchivo,
                    Hora_Inicio = sHora_Inicio,

                    Usu_Registro = sUsu_Registro
                };

                //Registro de Solicitud
                var result = await _tMSolicitudMantenimientoService.ProcesoMntoSolicitudMantenimiento(_tmSolicitudMantenimiento, sOpcion!);
                if (result.Success)
                {
                    if (result.CodeTransacc == 2)
                    {
                        var sNroSolicitud = result.Message[^10..];
                        string sCodigoGruposWathsApp = string.Empty;
                        string message = string.Empty;
                        string _codArea = string.Empty;

                        //Obtenemos los datos de la solicitud Generada.
                        var result2 = await _tMSolicitudMantenimientoService.ObtieneInformacionSolicitudMantenimientoByNumero(sNroSolicitud);
                        if (result2!.Success)
                        {
                            //Recorremos la información
                            foreach (var item in result2.Elements!)
                            {
                                _codArea = item.Cod_Area!;
                                var _area = item.Area;
                                var _maquina = item.Maquina;
                                var _supervisor = item.Supervisor;
                                var _prioridad = item.Prioridad;
                                message = @"🚨 *¡Solicitud de Mantenimiento!* \\n *Numero*: " + sNroSolicitud + @"\\n *Area*: " + _area + @"\\n *Maquina*: " + _maquina + @"\\n *Prioridad*: 🔴" + _prioridad + @"\\n *Supervisor*: " + _supervisor + @"\\n *Observación*: " + sObservacion;
                            }

                            sCodigoGruposWathsApp = _configuration.GetSection("WaliChat").GetValue<string>(_codArea)!;

                            //Verifica si cargo la imagen
                            if (bExisteImagen)
                            {
                                //string imageURL = "https://picsum.photos/seed/picsum/600/400";
                                string imageURL = "https://gestion.precotex.com:444/ubicaciones/api/TxRetiroRepuestos/getImagenDesdeBackEnd?imageId=" + nombreArchivo;
                                //Se envia a grupo con imagen
                                var body = await _waliChatService.EnviarMensajeImageAsync(sCodigoGruposWathsApp, message, imageURL, false);
                            }
                            else
                            {
                                //Se envia Mensaje a Wathsapp 
                                var body = await _waliChatService.EnviarMensajeAsync(sCodigoGruposWathsApp, message);
                            }
                        }
                    }

                    result.CodeResult = result.CodeTransacc == 2 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                    return Ok(result);
                }

                result.CodeResult = StatusCodes.Status400BadRequest;
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }


            /*
            TM_Solicitud_Mantenimiento _tmSolicitudMantenimiento = new TM_Solicitud_Mantenimiento
            {
                Cod_Solicitud = parameters.Cod_Solicitud,
                Cod_Area = parameters.Cod_Area,
                Cod_Maquina = parameters.Cod_Maquina,
                Observacion = parameters.Observacion,
                Prioridad = parameters.Prioridad,
                Paro_Maquina = parameters.Paro_Maquina == "1"? true: false,
                Ruta_Fotografia = parameters.Ruta_Fotografia,
                Hora_Inicio = parameters.Hora_Inicio,
                Usu_Registro = parameters.Usu_Registro
            };
            var result = await _tMSolicitudMantenimientoService.ProcesoMntoSolicitudMantenimiento(_tmSolicitudMantenimiento, parameters.Accion!);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }
            

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
            */
        }

        [HttpGet]
        [Route("getObtieneInformacionMaquinas")]
        public async Task<IActionResult> getObtieneInformacionMaquinas([FromQuery] string sCodMaquina)
        {
            var result = await _tMSolicitudMantenimientoService.ObtieneInformacionMaquinas(sCodMaquina);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtieneInformacionSolicitudMantenimiento")]
        public async Task<IActionResult> getObtieneInformacionSolicitudMantenimiento([FromQuery] DateTime FecIni, DateTime FecFin, string codUsuario)
        {
            var result = await _tMSolicitudMantenimientoService.ObtieneInformacionSolicitudMantenimiento(FecIni, FecFin, codUsuario);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtieneInformacionSolicitudesVisor")]
        public async Task<IActionResult> getObtieneInformacionSolicitudesVisor(string sCodUsuario)
        {
            var result = await _tMSolicitudMantenimientoService.ObtieneInformacionSolicitudesVisor(sCodUsuario);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getImagenDesdeBackEnd")]
        public IActionResult GetImage(string imageId)
        {
            //var path = Path.Combine(@"\\fileserverprx\imagenesretiro$\", imageId);
            var path = Path.Combine(@"D:\htdocs\app\foto\", imageId);
            if (!System.IO.File.Exists(path)) return NotFound();
            var mime = "image/jpeg";
            var bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, mime);
        }

        [HttpPost]
        [Route("postAvanzaEstadoSolicitudMantenimiento")]
        public async Task<IActionResult> postAvanzaEstadoSolicitudMantenimiento([FromBody] txSolicitudMantenimientoAvanzaParameter parameters)
        {

            var result = await _tMSolicitudMantenimientoService.AvanzaEstadoSolicitudMantenimiento(parameters.Cod_Usuario, parameters.Cod_Solicitud!, parameters.Observaciones!, parameters.sDatosLider!);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postProcesoMntoTiempoManMquina")]
        public async Task<IActionResult> postProcesoMntoTiempoManMquina([FromBody] TM_Tiempo_Mantenimiento parameters)
        {
            var result = await _tMSolicitudMantenimientoService.ProcesoMntoTiempoManMquina(parameters, parameters.Accion!);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postSendAlerta")]
        public async Task<IActionResult> postSendAlerta([FromBody] AlertaRequest parameters)
        {
            string message = string.Empty;
            var _area = parameters.Area;
            var _nombre = parameters.Nombre;
            var _fecha = parameters.FechaHora;
            var _nroDestino = parameters.NumeroDestino;
            message = @"🚨 *¡Solicitud de Alerta!* \\n *Area*: " + _area + @"\\n *Nombre*: " + _nombre + @"\\n *Fecha*: " + _fecha;
            string imageURL = "https://gestion.precotex.com:444/ubicaciones/api/TxRetiroRepuestos/getImagenDesdeBackEnd?imageId=alerta.png";
            var body = await _waliChatService.EnviarMensajeImagePhoneAsync(_nroDestino, message, imageURL);
            return Ok(1);
        }





        public class AlertaRequest 
        { 
            public string Area { get; set; } 
            public string Nombre { get; set; } 
            public string FechaHora { get; set; } 
            public string NumeroDestino { get; set; } 
        }
    }
}
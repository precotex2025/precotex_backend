using ic.backend.precotex.web.Api.Parameters;
using ic.backend.precotex.web.Entity.Entities.CalificacionRollosEnProceso;
using ic.backend.precotex.web.Entity.Entities.Memorandum;
using ic.backend.precotex.web.Entity.Entities.SolicitudMantenimiento;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.SolicitudMantenimiento;
using ic.backend.precotex.web.Service.Services.Implementacion.WallyChat;
using ic.backend.precotex.web.Service.Services.WallyChat;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Graph.Models;
using Org.BouncyCastle.Asn1.Crmf;
using SkiaSharp;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
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

        [HttpGet]
        [Route("getReporteSolicitudMantenimiento")]
        public async Task<IActionResult> getReporteSolicitudMantenimiento([FromQuery] DateTime FecIni, DateTime FecFin, string codEstado)
        {
            var result = await _tMSolicitudMantenimientoService.ReporteSolicitudMantenimiento(FecIni, FecFin, codEstado);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getNotificacionIncidenciaMantenimiento")]
        public async Task<IActionResult> getNotificacionIncidenciaMantenimiento()
        {
            var result = await _tMSolicitudMantenimientoService.NotificacionIncidenciaMantenimiento();
            if (result != null && result.Success)
            {
                string? rutaArchivo = null;

                try
                {

                    // result.Elements ya es List<TM_Notificacion_Incidencia>
                    //var mensaje = BuildTextMessage(
                    // result.Elements?.ToList() ?? new List<TM_Notificacion_Incidencia>());

                    // 1) Generar la imagen del reporte (byte[])
                    byte[] imagenPng = BuildIncidenciasImage(result.Elements?.ToList());

                    // 2) Guardarla en disco (mismo patrón que ya usas para archivos subidos)
                    string rutaBase = @"D:\htdocs\app\foto";
                    Directory.CreateDirectory(rutaBase); // Se asegura de que el directorio exista

                    string nombreArchivo = $"{Guid.NewGuid()}_reporte_incidencias.png";
                    rutaArchivo = Path.Combine(rutaBase, nombreArchivo);

                    if (System.IO.File.Exists(rutaArchivo))
                    {
                        System.IO.File.Delete(rutaArchivo);
                    }

                    await System.IO.File.WriteAllBytesAsync(rutaArchivo, imagenPng);
                    string imageURL = "https://gestion.precotex.com:444/ubicaciones/api/TxRetiroRepuestos/getImagenDesdeBackEnd?imageId=" + nombreArchivo;

                    var area = "008";
                    var grupoId = _configuration[$"WaliChat:{area}"];
                    //var body = await _waliChatService.EnviarMensajeAsync(grupoId!, mensaje);
                    await _waliChatService.EnviarMensajeImageAsync(grupoId!, "", imageURL, false);

                }
                catch (Exception ex)
                {
                    //_logger.LogError(ex, "Error generando/guardando/enviando la imagen del reporte, se envía texto como respaldo");
                    //var mensaje = WhatsAppNotificationBuilder.BuildTextMessage(incidencias);
                    //await _waliChatService.EnviarMensajeAsync(grupoId!, mensaje);
                }
                finally
                {
                    // 5) Eliminar la imagen del disco, se haya enviado bien o no, para no acumular archivos
                    if (!string.IsNullOrEmpty(rutaArchivo) && System.IO.File.Exists(rutaArchivo))
                    {
                        try
                        {
                            System.IO.File.Delete(rutaArchivo);
                        }
                        catch (Exception exDelete)
                        {
                            //_logger.LogWarning(exDelete, "No se pudo eliminar el archivo temporal {Ruta}", rutaArchivo);
                        }
                    }
                }


                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result ??= new ServiceResponseList<TM_Notificacion_Incidencia>();
            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        private const char PadChar = '\u00A0'; // non-breaking space

        private static string Pad(string text, int width)
        {
            if (string.IsNullOrEmpty(text)) text = string.Empty;
            return text.Length >= width
                ? text.Substring(0, width)
                : text + new string(PadChar, width - text.Length);
        }


        public static string BuildTextMessage(List<TM_Notificacion_Incidencia> incidencias)
        {
            if (incidencias == null || incidencias.Count == 0)
                return "✅ No se registraron incidencias el día de hoy.";

            var fecha = incidencias.First().Fecha.ToString("dd/MM/yyyy");
            var ordenadas = incidencias
                .OrderBy(i => i.Area!)
                .ThenBy(i => i.Maquina!.Trim())
                .ToList();

            var sb = new StringBuilder();

            sb.AppendLine($"*📋 Reporte de Incidencias — {fecha}*");
            sb.AppendLine();

            const int wMaquina = 10;
            const int wArea = 8;
            const int wTurno = 4;
            const int wInc = 4;

            sb.AppendLine("```");
            sb.AppendLine(Pad("MAQUINA", wMaquina) + Pad("AREA", wArea) + Pad("TUR", wTurno) + Pad("INC", wInc) + "HRS");
            sb.AppendLine(new string('-', wMaquina + wArea + wTurno + wInc + 3));

            foreach (var item in ordenadas)
            {
                var maquina = item.Maquina!.Trim();

                // Turno abreviado a 1 letra para ganar espacio (N=Noche, D=Dia)
                var turno = item.Turno!.Trim().ToUpper() switch
                {
                    "NOCHE" => "N",
                    "DIA" => "D",
                    _ => item.Turno!.Trim().Substring(0, 1)
                };

                sb.AppendLine(
                    Pad(maquina, wMaquina) +
                    Pad(item.Area ?? "", wArea) +
                    Pad(turno, wTurno) +
                    Pad(item.Numero_Incidencia.ToString(), wInc) +
                    item.Horas_Paro_Maquina);
            }
            sb.AppendLine("```");
            sb.AppendLine();

            // Resumen
            var totalMaquinas = incidencias.Count;
            var totalIncidencias = incidencias.Sum(i => i.Numero_Incidencia);
            var totalHoras = incidencias.Sum(i => i.Horas_Paro_Maquina);
            var maquinasConParo = incidencias.Count(i => i.Horas_Paro_Maquina > 0);

            sb.AppendLine($"🔧 *Máquinas reportadas:* {totalMaquinas}");
            sb.AppendLine($"⚠️ *Total incidencias:* {totalIncidencias}");
            sb.AppendLine($"⏱️ *Total horas de paro:* {totalHoras}");

            if (maquinasConParo > 0)
            {
                sb.AppendLine($"🛑 *Máquinas con paro:* {maquinasConParo}");
                sb.AppendLine();
                sb.AppendLine("*Detalle de paros:*");
                foreach (var item in ordenadas.Where(i => i.Horas_Paro_Maquina > 0))
                {
                    sb.AppendLine($"• {item.Maquina!.Trim()} ({item.Area}) — {item.Horas_Paro_Maquina}h");
                }
            }

            return sb.ToString();
        }

        // ---------------------------------------------------------
        // Generador de imagen (tabla renderizada con SkiaSharp) - Estilo gerencial
        // ---------------------------------------------------------
        // Instalar: dotnet add package SkiaSharp
        // En Linux (contenedores/servidores) además se necesita:
        //   dotnet add package SkiaSharp.NativeAssets.Linux.NoDependencies
        //   y tener instaladas las librerías del sistema: libfontconfig1
        //   (apt-get install -y libfontconfig1)

            // Paleta ejecutiva (azul marino + acentos)
            private static readonly SKColor ColorBg = new SKColor(0xF4, 0xF6, 0xF9);
            private static readonly SKColor ColorHeaderBg = new SKColor(0x0F, 0x2A, 0x4A);
            private static readonly SKColor ColorHeaderText = SKColors.White;
            private static readonly SKColor ColorHeaderSubtext = new SKColor(0xC9, 0xD6, 0xE4);
            private static readonly SKColor ColorAccent = new SKColor(0x2E, 0x75, 0xB6);
            private static readonly SKColor ColorAccentRed = new SKColor(0xC0, 0x3B, 0x2B);
            private static readonly SKColor ColorCardBg = SKColors.White;
            private static readonly SKColor ColorCardBorder = new SKColor(0xE2, 0xE6, 0xEC);
            private static readonly SKColor ColorTableHeaderBg = new SKColor(0x0F, 0x2A, 0x4A);
            private static readonly SKColor ColorTableHeaderText = SKColors.White;
            private static readonly SKColor ColorRowA = SKColors.White;
            private static readonly SKColor ColorRowB = new SKColor(0xF7, 0xF9, 0xFC);
            private static readonly SKColor ColorText = new SKColor(0x22, 0x28, 0x30);
            private static readonly SKColor ColorTextMuted = new SKColor(0x6B, 0x74, 0x80);
            private static readonly SKColor ColorBorderLight = new SKColor(0xE4, 0xE7, 0xEB);
            private static readonly SKColor ColorBadgeOkBg = new SKColor(0xE6, 0xF4, 0xEA);
            private static readonly SKColor ColorBadgeOkText = new SKColor(0x1E, 0x7B, 0x34);
            private static readonly SKColor ColorBadgeParoBg = new SKColor(0xFB, 0xE9, 0xE7);
            private static readonly SKColor ColorBadgeParoText = new SKColor(0xC0, 0x3B, 0x2B);

        /// <summary>
        /// Genera un PNG en alta resolución (escala 2x) con diseño ejecutivo: header con título,
        /// tarjetas KPI con los totales clave, y tabla con badges de estado (OK / PARO).
        /// </summary>
        public static byte[] BuildIncidenciasImage(List<TM_Notificacion_Incidencia> incidencias)
        {
            incidencias ??= new List<TM_Notificacion_Incidencia>();

            var ordenadas = incidencias
                .OrderBy(i => i.Area)
                .ThenBy(i => i.Maquina!.Trim())
                .ToList();

            var fecha = ordenadas.Count > 0
                ? ordenadas.First().Fecha.ToString("dd/MM/yyyy")
                : DateTime.Now.ToString("dd/MM/yyyy");

            // ---- Layout base (en puntos lógicos; se escala 2x al final) ----
            const int scale = 2;
            float[] colWidths = { 180, 150, 130, 100, 110, 140 }; // + columna ESTADO
            float width = colWidths.Sum();
            const int margin = 30;
            const int headerHeight = 110;
            const int kpiHeight = 110;
            const int kpiGap = 16;
            const int tableHeaderHeight = 50;
            const int rowHeight = 52;
            int rowCount = Math.Max(ordenadas.Count, 1);

            int totalHeight = margin + headerHeight + 24 + kpiHeight + 30 + tableHeaderHeight + rowHeight * rowCount + 40 + margin;
            int totalWidth = (int)width + margin * 2;

            var info = new SKImageInfo(totalWidth * scale, totalHeight * scale);
            using var surface = SKSurface.Create(info);
            var canvas = surface.Canvas;
            canvas.Scale(scale);
            canvas.Clear(ColorBg);

            // ---- Tipografías: en SkiaSharp moderno, tamaño/tipo de letra van en SKFont, no en SKPaint ----
            using var typefaceRegular = SKTypeface.FromFamilyName("Arial", SKFontStyleWeight.Normal, SKFontStyleWidth.Normal, SKFontStyleSlant.Upright);
            using var typefaceBold = SKTypeface.FromFamilyName("Arial", SKFontStyleWeight.Bold, SKFontStyleWidth.Normal, SKFontStyleSlant.Upright);

            using var fontTitle = new SKFont(typefaceBold, 30);
            using var fontSubtitle = new SKFont(typefaceRegular, 18);
            using var fontKpiNum = new SKFont(typefaceBold, 34);
            using var fontKpiLabel = new SKFont(typefaceBold, 13);
            using var fontTh = new SKFont(typefaceBold, 16);
            using var fontTd = new SKFont(typefaceRegular, 18);
            using var fontTdBold = new SKFont(typefaceBold, 18);
            using var fontBadge = new SKFont(typefaceBold, 13);
            using var fontFooter = new SKFont(typefaceRegular, 13);

            // ---- Header ----
            using (var headerBg = new SKPaint { Color = ColorHeaderBg, IsAntialias = true })
                canvas.DrawRect(new SKRect(0, 0, totalWidth, headerHeight), headerBg);
            using (var accentBar = new SKPaint { Color = ColorAccent, IsAntialias = true })
                canvas.DrawRect(new SKRect(0, headerHeight - 4, totalWidth, headerHeight), accentBar);

            using (var titlePaint = new SKPaint { Color = ColorHeaderText, IsAntialias = true })
                canvas.DrawText("REPORTE DE INCIDENCIAS DE MANTENIMIENTO", margin, 52, fontTitle, titlePaint);
            using (var subtitlePaint = new SKPaint { Color = ColorHeaderSubtext, IsAntialias = true })
                canvas.DrawText($"Fecha: {fecha}  ·  Generado automáticamente", margin, 88, fontSubtitle, subtitlePaint);

            float y = headerHeight + 24;

            // ---- Tarjetas KPI ----
            var totalMaquinas = incidencias.Count;
            var totalIncidencias = incidencias.Sum(i => i.Numero_Incidencia);
            var totalHoras = incidencias.Sum(i => i.Horas_Paro_Maquina);
            var maquinasConParo = incidencias.Count(i => i.Horas_Paro_Maquina > 0);

            var kpis = new (string Label, string Value, SKColor Accent)[]
            {
                ("MÁQUINAS REPORTADAS", totalMaquinas.ToString(), ColorAccent),
                ("TOTAL INCIDENCIAS", totalIncidencias.ToString(), ColorAccent),
                ("HORAS DE PARO", totalHoras.ToString(), totalHoras > 0 ? ColorAccentRed : ColorAccent),
                ("MÁQUINAS CON PARO", maquinasConParo.ToString(), maquinasConParo > 0 ? ColorAccentRed : ColorAccent),
            };

            float cardWidth = (width - kpiGap * 3) / 4;
            float x = margin;
            using var kpiNumPaint = new SKPaint { Color = ColorText, IsAntialias = true };
            using var kpiLabelPaint = new SKPaint { Color = ColorTextMuted, IsAntialias = true };
            using var cardBorderPaint = new SKPaint { Color = ColorCardBorder, StrokeWidth = 1, IsStroke = true, IsAntialias = true };

            foreach (var kpi in kpis)
            {
                var cardRect = new SKRect(x, y, x + cardWidth, y + kpiHeight);
                using (var cardBg = new SKPaint { Color = ColorCardBg, IsAntialias = true })
                    canvas.DrawRect(cardRect, cardBg);
                canvas.DrawRect(cardRect, cardBorderPaint);

                using (var accentBarPaint = new SKPaint { Color = kpi.Accent, IsAntialias = true })
                    canvas.DrawRect(new SKRect(x, y, x + 5, y + kpiHeight), accentBarPaint);

                canvas.DrawText(kpi.Value, x + 20, y + 48, fontKpiNum, kpiNumPaint);
                canvas.DrawText(kpi.Label, x + 20, y + kpiHeight - 20, fontKpiLabel, kpiLabelPaint);

                x += cardWidth + kpiGap;
            }

            y += kpiHeight + 30;

            // ---- Tabla ----
            string[] headers = { "MAQUINA", "AREA", "TURNO", "INC.", "HORAS", "ESTADO" };
            bool[] rightAlign = { false, false, false, true, true, false };
            float tableLeft = margin;

            using (var tableHeaderBg = new SKPaint { Color = ColorTableHeaderBg, IsAntialias = true })
                canvas.DrawRect(new SKRect(tableLeft, y, tableLeft + width, y + tableHeaderHeight), tableHeaderBg);

            using (var headerTextPaint = new SKPaint { Color = ColorTableHeaderText, IsAntialias = true })
            {
                float hx = tableLeft;
                for (int c = 0; c < headers.Length; c++)
                {
                    DrawCellText(canvas, fontTh, headerTextPaint, headers[c], hx, y, colWidths[c], tableHeaderHeight, rightAlign[c]);
                    hx += colWidths[c];
                }
            }
            y += tableHeaderHeight;

            using var textPaint = new SKPaint { Color = ColorText, IsAntialias = true };
            using var boldRedPaint = new SKPaint { Color = ColorAccentRed, IsAntialias = true };
            using var rowBorderPaint = new SKPaint { Color = ColorBorderLight, StrokeWidth = 1, IsStroke = true, IsAntialias = true };
            using var tableBorderPaint = new SKPaint { Color = ColorBorderLight, StrokeWidth = 1, IsStroke = true, IsAntialias = true };
            using var badgePaint = new SKPaint { IsAntialias = true };

            float tableTop = y - tableHeaderHeight;

            if (ordenadas.Count == 0)
            {
                using (var rowBg = new SKPaint { Color = ColorRowA, IsAntialias = true })
                    canvas.DrawRect(new SKRect(tableLeft, y, tableLeft + width, y + rowHeight), rowBg);
                DrawCellText(canvas, fontTd, textPaint, "No se registraron incidencias", tableLeft, y, width, rowHeight, false);
                y += rowHeight;
            }
            else
            {
                for (int i = 0; i < ordenadas.Count; i++)
                {
                    var item = ordenadas[i];
                    bool tieneParo = item.Horas_Paro_Maquina > 0;
                    var bg = i % 2 == 0 ? ColorRowA : ColorRowB;

                    using (var rowBg = new SKPaint { Color = bg, IsAntialias = true })
                        canvas.DrawRect(new SKRect(tableLeft, y, tableLeft + width, y + rowHeight), rowBg);
                    canvas.DrawLine(tableLeft, y + rowHeight, tableLeft + width, y + rowHeight, rowBorderPaint);

                    var turno = item.Turno?.Trim().ToUpper() switch
                    {
                        "NOCHE" => "NOCHE",
                        "DIA" => "DIA",
                        _ => item.Turno?.Trim() ?? ""
                    };

                    var values = new[]
                    {
                        item.Maquina?.Trim() ?? "",
                        item.Area ?? "",
                        turno,
                        item.Numero_Incidencia.ToString(),
                        item.Horas_Paro_Maquina.ToString()
                    };

                    float cx = tableLeft;
                    for (int c = 0; c < values.Length; c++)
                    {
                        var font = (c == 4 && tieneParo) ? fontTdBold : fontTd;
                        var paint = (c == 4 && tieneParo) ? boldRedPaint : textPaint;
                        DrawCellText(canvas, font, paint, values[c], cx, y, colWidths[c], rowHeight, rightAlign[c]);
                        cx += colWidths[c];
                    }

                    // Badge de estado (OK / PARO)
                    var badgeText = tieneParo ? "PARO" : "OK";
                    var badgeBg = tieneParo ? ColorBadgeParoBg : ColorBadgeOkBg;
                    var badgeFg = tieneParo ? ColorBadgeParoText : ColorBadgeOkText;
                    ReadOnlySpan<char> readOnlySpan = badgeText.AsSpan();
                    // Convert the ReadOnlySpan<char> to ReadOnlySpan<ushort> using SKFont.GetGlyphs
                    Span<ushort> glyphs = new ushort[badgeText.Length];
                    fontBadge.GetGlyphs(readOnlySpan, glyphs);

                    // Measure the text width using the converted glyphs
                    float badgeTextWidth = fontBadge.MeasureText(glyphs);

                    badgePaint.Color = badgeFg;
                    //float badgeTextWidth = fontBadge.MeasureText(readOnlySpan);
                    const float padX = 14, padY = 6;
                    float badgeW = badgeTextWidth + padX * 2;
                    float badgeH = 22;
                    float badgeX = cx + 16;
                    float badgeY = y + (rowHeight - badgeH) / 2f;

                    using (var badgeBgPaint = new SKPaint { Color = badgeBg, IsAntialias = true })
                        canvas.DrawRoundRect(new SKRect(badgeX, badgeY, badgeX + badgeW, badgeY + badgeH), 10, 10, badgeBgPaint);
                    canvas.DrawText(badgeText, badgeX + padX, badgeY + badgeH / 2f + 5, fontBadge, badgePaint);

                    y += rowHeight;
                }
            }

            canvas.DrawRect(new SKRect(tableLeft, tableTop, tableLeft + width, y), tableBorderPaint);

            // ---- Pie de página ----
            y += 24;
            canvas.DrawLine(tableLeft, y, tableLeft + width, y, rowBorderPaint);
            y += 14;
            using (var footerPaint = new SKPaint { Color = ColorTextMuted, IsAntialias = true })
                canvas.DrawText("Reporte generado automáticamente por el sistema de mantenimiento", tableLeft, y + 10, fontFooter, footerPaint);

            canvas.Flush();

            using var image = surface.Snapshot();
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            return data.ToArray();
        }

        // Dibuja texto centrado verticalmente dentro de una celda, alineado a la izquierda o
        // a la derecha (con un pequeño padding), truncando si no entra en el ancho de columna.
        //private static void DrawCellText(SKCanvas canvas, SKFont font, SKPaint paint, string text, float cellX, float cellY, float cellWidth, float cellHeight, bool rightAlign)
        //{
        //    const float padding = 16;
        //    float maxTextWidth = cellWidth - padding * 2;

        //    while (font.MeasureText(text) > maxTextWidth && text.Length > 1)
        //        text = text.Substring(0, text.Length - 1);

        //    float textWidth = font.MeasureText(text);
        //    float textY = cellY + cellHeight / 2f + 6;
        //    float textX = rightAlign ? cellX + cellWidth - padding - textWidth : cellX + padding;

        //    canvas.DrawText(text, textX, textY, font, paint);
        //}

        // Dibuja texto centrado verticalmente dentro de una celda, alineado a la izquierda o
        // a la derecha (con un pequeño padding), truncando si no entra en el ancho de columna.
        // Usa SKFont para medir/dibujar (API moderna de SkiaSharp: el tamaño y tipo de letra
        // viven en SKFont, no en SKPaint).
        //private static void DrawCellText(SKCanvas canvas, SKFont font, SKPaint paint, string text, float cellX, float cellY, float cellWidth, float cellHeight, bool rightAlign)
        //{
        //    const float padding = 16;
        //    float maxTextWidth = cellWidth - padding * 2;

        //    while (font.MeasureText(text) > maxTextWidth && text.Length > 1)
        //        text = text.Substring(0, text.Length - 1);

        //    float textWidth = font.MeasureText(text);
        //    float textY = cellY + cellHeight / 2f + 6;
        //    float textX = rightAlign ? cellX + cellWidth - padding - textWidth : cellX + padding;

        //    canvas.DrawText(text, textX, textY, font, paint);
        //}

        private static void DrawCellText(SKCanvas canvas, SKFont font, SKPaint paint, string text, float cellX, float cellY, float cellWidth, float cellHeight, bool rightAlign)
        {
            const float padding = 16;
            float maxTextWidth = cellWidth - padding * 2;

            while (paint.MeasureText(text) > maxTextWidth && text.Length > 1)
                text = text.Substring(0, text.Length - 1);

            float textWidth = paint.MeasureText(text);
            float textY = cellY + cellHeight / 2f + 6;
            float textX = rightAlign ? cellX + cellWidth - padding - textWidth : cellX + padding;

            canvas.DrawText(text, textX, textY, font, paint);
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
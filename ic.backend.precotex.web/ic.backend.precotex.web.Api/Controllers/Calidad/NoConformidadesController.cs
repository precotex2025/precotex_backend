using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ic.backend.precotex.web.Entity.Entities.Calidad;
using ic.backend.precotex.web.Service.Services.Implementacion.Calidad;
using ic.backend.precotex.web.Service.Services.Implementacion.WallyChat;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ic.backend.precotex.web.Api.Controllers.Calidad
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoConformidadesController : ControllerBase
    {
        private readonly INoConformidadesService _service;
        private readonly IWaliChatService _waliChatService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<NoConformidadesController> _logger;

        public NoConformidadesController(
            INoConformidadesService service,
            IWaliChatService waliChatService,
            IConfiguration configuration,
            ILogger<NoConformidadesController> logger)
        {
            _service = service;
            _waliChatService = waliChatService;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet("getInformesCabecera")]
        public async Task<IActionResult> GetInformesCabecera([FromQuery] string numInforme = "", [FromQuery] string fIni = "", [FromQuery] string fFin = "", [FromQuery] string partida = "")
        {
            try
            {
                var data = await _service.MostrarCabecera(numInforme, fIni, fFin, partida);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("getDatosInformeCalidad")]
        public async Task<IActionResult> GetDatosInformeCalidad([FromQuery] string tipo = "T", [FromQuery] string cod = "")
        {
            try
            {
                var data = await _service.ListarDatosInformeCalidad(tipo, cod);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("getPartida")]
        public async Task<IActionResult> GetPartida([FromQuery] string partida, [FromQuery] string tipo = "")
        {
            try
            {
                var data = await _service.MostrarPartida(partida, tipo);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("getInformeDetalle")]
        public async Task<IActionResult> GetInformeDetalle([FromQuery] string numInforme = "", [FromQuery] string partida = "")
        {
            try
            {
                var articulos = await _service.MostrarDetalle(numInforme, partida);
                var motivos = await _service.MostrarDetalleMotivo(numInforme, partida);

                EnriquecerMotivosConFotos(numInforme, motivos);

                return Ok(new { articulos, motivos });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        private void EnriquecerMotivosConFotos(string numInforme, List<Dictionary<string, object>> motivos)
        {
            if (string.IsNullOrWhiteSpace(numInforme) || motivos == null || motivos.Count == 0) return;

            string cleanNum = (numInforme ?? "").Trim();
            if (cleanNum.StartsWith("NC-", StringComparison.OrdinalIgnoreCase))
            {
                cleanNum = cleanNum.Substring(3).Trim();
            }
            if (int.TryParse(cleanNum, out int nVal))
            {
                cleanNum = nVal.ToString("D6");
            }
            string formattedInforme = $"NC-{cleanNum}";

            string[] targetDirectories = new string[]
            {
                @"\\\\192.168.1.36\\d$\\htdocs\\app\\noconfornidad",
                @"D:\\htdocs\\app\\noconfornidad",
                @"D:\\htdocs\\app\\foto"
            };

            var allFiles = new List<string>();
            foreach (var dir in targetDirectories)
            {
                try
                {
                    if (Directory.Exists(dir))
                    {
                        var files = Directory.GetFiles(dir, $"{formattedInforme}*")
                                             .Select(Path.GetFileName)
                                             .Where(f => !string.IsNullOrEmpty(f));
                        foreach (var f in files)
                        {
                            if (!allFiles.Contains(f!, StringComparer.OrdinalIgnoreCase))
                            {
                                allFiles.Add(f!);
                            }
                        }
                    }
                }
                catch { }
            }

            foreach (var m in motivos)
            {
                string codTela = "";
                if (m.TryGetValue("Cod_Tela", out var ct) && ct != null) codTela = ct.ToString()!.Trim();
                else if (m.TryGetValue("cod_Tela", out var ct2) && ct2 != null) codTela = ct2.ToString()!.Trim();

                string codMotivo = "";
                if (m.TryGetValue("Cod_Motivo", out var cm) && cm != null) codMotivo = cm.ToString()!.Trim();
                else if (m.TryGetValue("cod_Motivo", out var cm2) && cm2 != null) codMotivo = cm2.ToString()!.Trim();

                string targetPrefix = $"{formattedInforme}-{codTela}-{codMotivo}";
                var matched = allFiles.Where(f => f.StartsWith(targetPrefix, StringComparison.OrdinalIgnoreCase)).ToList();

                if (matched.Count == 0 && motivos.Count == 1 && allFiles.Count > 0)
                {
                    matched = allFiles;
                }

                m["Fotos"] = matched;
                m["fotos"] = matched;
            }
        }

        [HttpGet("getEvolutivo")]
        public async Task<IActionResult> GetEvolutivo()
        {
            try
            {
                var data = await _service.MostrarEvolutivo();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("getReporteNoConformidad")]
        public async Task<IActionResult> GetReporteNoConformidad([FromQuery] string fIni = "", [FromQuery] string fFin = "")
        {
            try
            {
                var data = await _service.ReporteNoConformidad(fIni, fFin);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("guardarInforme")]
        public async Task<IActionResult> GuardarInforme([FromBody] InformeGuardarRequest req)
        {
            try
            {
                var resultado = await _service.GuardarInforme(req);

                if (resultado != null && resultado.Success)
                {
                    // Disparar envío de notificación a WhatsApp en segundo plano
                    string tipoEvento = (req.Accion == "U") ? "EDICION" : "CREACION";
                    _ = Task.Run(async () =>
                    {
                        try
                        {
                            await NotificarWhatsAppNoConformidad(req, resultado.Num_Informe, tipoEvento);
                        }
                        catch (Exception exWsp)
                        {
                            _logger.LogWarning(exWsp, "Error al enviar notificación de WhatsApp para NC {Num}", resultado.Num_Informe);
                        }
                    });
                }

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("anularInforme")]
        public async Task<IActionResult> AnularInforme([FromBody] InformeAnularRequest req)
        {
            try
            {
                var resultado = await _service.AnularInforme(req);

                if (resultado != null && resultado.Success)
                {
                    // Disparar envío de notificación a WhatsApp en segundo plano
                    _ = Task.Run(async () =>
                    {
                        try
                        {
                            await NotificarWhatsAppAnulacion(req);
                        }
                        catch (Exception exWsp)
                        {
                            _logger.LogWarning(exWsp, "Error al enviar notificación de anulación de WhatsApp para NC {Num}", req.Num_Informe);
                        }
                    });
                }

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        private List<string> ObtenerGruposWhatsAppNoConformidad()
        {
            var grupos = new List<string>();
            foreach (var key in new[] { "2001", "2002", "2003" })
            {
                var g = _configuration[$"WaliChat:{key}"];
                if (!string.IsNullOrWhiteSpace(g))
                {
                    grupos.Add(g.Trim());
                }
            }
            if (!grupos.Any())
            {
                grupos.Add("120363280015488353@g.us");
            }
            return grupos;
        }

        private string? BuscarFotoInformeFullPath(string formattedNC)
        {
            string[] targetDirectories = new string[]
            {
                @"\\192.168.1.36\d$\htdocs\app\noconfornidad",
                @"D:\htdocs\app\noconfornidad",
                @"D:\htdocs\app\foto"
            };

            foreach (var dir in targetDirectories)
            {
                try
                {
                    if (Directory.Exists(dir))
                    {
                        var file = Directory.GetFiles(dir, $"{formattedNC}*").FirstOrDefault();
                        if (!string.IsNullOrEmpty(file))
                        {
                            return file;
                        }
                    }
                }
                catch { }
            }
            return null;
        }

        private async Task NotificarWhatsAppNoConformidad(InformeGuardarRequest req, string numInforme, string tipoEvento)
        {
            var grupos = ObtenerGruposWhatsAppNoConformidad();
            if (!grupos.Any()) return;

            string cleanNum = (numInforme ?? "").Trim();
            if (cleanNum.StartsWith("NC-", StringComparison.OrdinalIgnoreCase)) cleanNum = cleanNum.Substring(3).Trim();
            if (int.TryParse(cleanNum, out int nVal)) cleanNum = nVal.ToString("D6");
            string formattedNC = $"NC-{cleanNum}";

            var sbDetalle = new StringBuilder();
            if (req.Articulos != null && req.Articulos.Count > 0)
            {
                foreach (var art in req.Articulos)
                {
                    string nomTela = !string.IsNullOrWhiteSpace(art.Nom_Tela) ? art.Nom_Tela.Trim() : (!string.IsNullOrWhiteSpace(art.Cod_Tela) ? art.Cod_Tela.Trim() : "Artículo");
                    string talla = string.IsNullOrWhiteSpace(art.Talla) || art.Talla == "-" ? "-" : art.Talla.Trim();

                    if (art.Defectos != null && art.Defectos.Count > 0)
                    {
                        foreach (var def in art.Defectos)
                        {
                            string codMotivo = (def.Cod_Motivo ?? "").Trim();
                            string desMotivo = !string.IsNullOrWhiteSpace(def.Des_Motivo) ? def.Des_Motivo.Trim() : codMotivo;
                            string motivoTexto = string.IsNullOrEmpty(desMotivo) || desMotivo == codMotivo ? codMotivo : $"{codMotivo} - {desMotivo}";
                            string area = !string.IsNullOrWhiteSpace(def.Nom_Area) ? def.Nom_Area.Trim() : (!string.IsNullOrWhiteSpace(def.Cod_Area) ? def.Cod_Area.Trim() : "PRODUCCIÓN");

                            sbDetalle.AppendLine($"• {nomTela} (Talla {talla}, {art.Cant_Rollos_Rech} de {art.Rollos} rollos) — Motivo: {motivoTexto} — Área: {area}");
                        }
                    }
                    else
                    {
                        sbDetalle.AppendLine($"• {nomTela} (Talla {talla}, {art.Cant_Rollos_Rech} de {art.Rollos} rollos)");
                    }
                }
            }
            string detalleTexto = sbDetalle.ToString().TrimEnd();

            string pesoStr = req.Kg_Total.HasValue && req.Kg_Total.Value > 0 ? $"{req.Kg_Total.Value:0.00}" : "0.00";
            string usuario = !string.IsNullOrWhiteSpace(req.Nom_Usuario) ? req.Nom_Usuario.Trim() : (!string.IsNullOrWhiteSpace(req.Cod_Usuario) ? req.Cod_Usuario.Trim() : "SISTEMAS");
            string fechaHora = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            string comentarios = !string.IsNullOrWhiteSpace(req.Observacion) ? req.Observacion.Trim() : "";

            string mensajeWsp = "";
            if (tipoEvento == "CREACION")
            {
                mensajeWsp = $"🔴 *NUEVA NO CONFORMIDAD*\\n" +
                             $"{formattedNC}\\n" +
                             $"Partida: {req.Cod_OrdPro} | Cliente: {req.Nom_Cli}\\n" +
                             $"Color: {req.Color} | Peso: {pesoStr} kg\\n" +
                             $"Detalle por artículo:\\n" +
                             $"{detalleTexto}\\n" +
                             $"Registrado por: {usuario}\\n" +
                             $"Fecha: {fechaHora}\\n" +
                             (string.IsNullOrEmpty(comentarios) ? "" : $"Comentarios: {comentarios}\\n") +
                             $"Evidencia:";
            }
            else // EDICION
            {
                string motivoEdicion = !string.IsNullOrWhiteSpace(req.Motivo_Edicion) ? req.Motivo_Edicion.Trim() : "Actualización de datos";
                string cambios = !string.IsNullOrWhiteSpace(req.Detalle_Cambios) ? req.Detalle_Cambios.Trim() : "";

                mensajeWsp = $"✏️ *NC MODIFICADA*\\n" +
                             $"{formattedNC}\\n" +
                             $"Partida: {req.Cod_OrdPro} | Cliente: {req.Nom_Cli}\\n" +
                             $"Color: {req.Color} | Peso: {pesoStr} kg\\n" +
                             $"Detalle por artículo:\\n" +
                             $"{detalleTexto}\\n" +
                             $"Motivo de edición: {motivoEdicion}\\n" +
                             (string.IsNullOrEmpty(cambios) ? "" : $"Cambios realizados:\\n{cambios}\\n") +
                             $"Modificado por: {usuario}\\n" +
                             $"Fecha: {fechaHora}\\n" +
                             (string.IsNullOrEmpty(comentarios) ? "" : $"Comentarios: {comentarios}\\n") +
                             $"Evidencia:";
            }

            // Buscar si existe foto guardada para este informe o en el request
            string? fotoFullPath = BuscarFotoInformeFullPath(formattedNC);
            string? fileId = null;

            // 1. Si existe archivo físico en disco, subirlo directamente a WaliChat
            if (!string.IsNullOrEmpty(fotoFullPath) && System.IO.File.Exists(fotoFullPath))
            {
                try
                {
                    byte[] bytes = await System.IO.File.ReadAllBytesAsync(fotoFullPath);
                    string fileName = System.IO.Path.GetFileName(fotoFullPath);
                    string ext = System.IO.Path.GetExtension(fotoFullPath).ToLower();
                    string contentType = ext == ".png" ? "image/png" : "image/jpeg";

                    fileId = await _waliChatService.SubirArchivoAsync(bytes, fileName, contentType);
                }
                catch (Exception exDisk)
                {
                    _logger.LogWarning(exDisk, "Error al subir foto desde disco a WaliChat");
                }
            }

            // 2. Si no se pudo desde disco pero el request trae fotos Base64 en los defectos
            if (string.IsNullOrEmpty(fileId) && req.Articulos != null)
            {
                try
                {
                    var fotoBase64 = req.Articulos
                        .Where(a => a.Defectos != null)
                        .SelectMany(a => a.Defectos!)
                        .Where(d => d.FotosBase64 != null && d.FotosBase64.Count > 0)
                        .SelectMany(d => d.FotosBase64!)
                        .FirstOrDefault(f => !string.IsNullOrEmpty(f));

                    if (!string.IsNullOrEmpty(fotoBase64))
                    {
                        string cleanB64 = fotoBase64;
                        string contentType = "image/jpeg";
                        if (cleanB64.Contains(","))
                        {
                            var parts = cleanB64.Split(',');
                            if (parts[0].Contains("image/png")) contentType = "image/png";
                            cleanB64 = parts[1];
                        }
                        byte[] bytes = Convert.FromBase64String(cleanB64);
                        string fileName = $"{formattedNC}.jpg";

                        fileId = await _waliChatService.SubirArchivoAsync(bytes, fileName, contentType);
                    }
                }
                catch (Exception exB64)
                {
                    _logger.LogWarning(exB64, "Error al subir foto Base64 a WaliChat");
                }
            }

            // 3. Enviar a todos los grupos de Calidad / Producción / Metas configurados
            foreach (var grupoId in grupos)
            {
                try
                {
                    if (!string.IsNullOrEmpty(fileId))
                    {
                        await _waliChatService.EnviarMensajeMediaFileAsync(grupoId, mensajeWsp, fileId);
                    }
                    else
                    {
                        await _waliChatService.EnviarMensajeAsync(grupoId, mensajeWsp);
                    }
                }
                catch (Exception exSend)
                {
                    _logger.LogWarning(exSend, $"Error al enviar WhatsApp a grupo {grupoId}");
                    if (!string.IsNullOrEmpty(fileId))
                    {
                        try { await _waliChatService.EnviarMensajeAsync(grupoId, mensajeWsp); } catch { }
                    }
                }
            }
        }

        private async Task NotificarWhatsAppAnulacion(InformeAnularRequest req)
        {
            var grupos = ObtenerGruposWhatsAppNoConformidad();
            if (!grupos.Any()) return;

            string cleanNum = (req.Num_Informe ?? "").Trim();
            if (cleanNum.StartsWith("NC-", StringComparison.OrdinalIgnoreCase)) cleanNum = cleanNum.Substring(3).Trim();
            if (int.TryParse(cleanNum, out int nVal)) cleanNum = nVal.ToString("D6");
            string formattedNC = $"NC-{cleanNum}";

            string usuario = !string.IsNullOrWhiteSpace(req.Nom_Usuario) ? req.Nom_Usuario.Trim() : (!string.IsNullOrWhiteSpace(req.Cod_Usuario) ? req.Cod_Usuario.Trim() : "SISTEMAS");
            string fechaHora = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            string motivoAnula = !string.IsNullOrWhiteSpace(req.Motivo_Anula) ? req.Motivo_Anula.Trim() : "Corrección administrativa";

            string mensajeWsp = $"❌ *NC ANULADA*\\n" +
                                $"{formattedNC}\\n" +
                                $"Partida: {req.Cod_OrdTra} | Cliente: {req.Nom_Cli}\\n" +
                                $"Color: {req.Color} | Peso: {req.Peso} kg\\n" +
                                $"Detalle por artículo:\\n" +
                                $"{req.Detalle_Articulo}\\n" +
                                $"Motivo: {motivoAnula}\\n" +
                                $"Anulado por: {usuario}\\n" +
                                $"Fecha: {fechaHora}";

            string? fotoFullPath = BuscarFotoInformeFullPath(formattedNC);
            string? fileIdAnula = null;

            if (!string.IsNullOrEmpty(fotoFullPath) && System.IO.File.Exists(fotoFullPath))
            {
                try
                {
                    byte[] bytes = await System.IO.File.ReadAllBytesAsync(fotoFullPath);
                    string fileName = System.IO.Path.GetFileName(fotoFullPath);
                    string ext = System.IO.Path.GetExtension(fotoFullPath).ToLower();
                    string contentType = ext == ".png" ? "image/png" : "image/jpeg";

                    fileIdAnula = await _waliChatService.SubirArchivoAsync(bytes, fileName, contentType);
                }
                catch (Exception exDisk)
                {
                    _logger.LogWarning(exDisk, "Error al subir foto de anulación a WaliChat");
                }
            }

            foreach (var grupoId in grupos)
            {
                try
                {
                    if (!string.IsNullOrEmpty(fileIdAnula))
                    {
                        await _waliChatService.EnviarMensajeMediaFileAsync(grupoId, mensajeWsp, fileIdAnula);
                    }
                    else
                    {
                        await _waliChatService.EnviarMensajeAsync(grupoId, mensajeWsp);
                    }
                }
                catch (Exception exSend)
                {
                    _logger.LogWarning(exSend, $"Error al enviar WhatsApp de anulación a grupo {grupoId}");
                    if (!string.IsNullOrEmpty(fileIdAnula))
                    {
                        try { await _waliChatService.EnviarMensajeAsync(grupoId, mensajeWsp); } catch { }
                    }
                }
            }
        }

        private string? BuscarFotoInforme(string formattedNC)
        {
            string[] targetDirectories = new string[]
            {
                @"\\\\192.168.1.36\\d$\\htdocs\\app\\noconfornidad",
                @"D:\\htdocs\\app\\noconfornidad",
                @"D:\\htdocs\\app\\foto"
            };

            foreach (var dir in targetDirectories)
            {
                try
                {
                    if (Directory.Exists(dir))
                    {
                        var file = Directory.GetFiles(dir, $"{formattedNC}*").FirstOrDefault();
                        if (!string.IsNullOrEmpty(file))
                        {
                            return Path.GetFileName(file);
                        }
                    }
                }
                catch { }
            }
            return null;
        }

        [HttpGet("getImagen")]
        public IActionResult GetImagen([FromQuery] string imageId)
        {
            if (string.IsNullOrWhiteSpace(imageId))
            {
                return BadRequest(new { success = false, message = "El parámetro imageId es requerido." });
            }

            var fileName = Path.GetFileName(imageId.Trim());

            string[] candidatePaths = new string[]
            {
                Path.Combine(@"\\\\192.168.1.36\\d$\\htdocs\\app\\noconfornidad", fileName),
                Path.Combine(@"D:\\htdocs\\app\\noconfornidad", fileName),
                Path.Combine(@"D:\\htdocs\\app\\foto", fileName)
            };

            string foundPath = null;
            foreach (var p in candidatePaths)
            {
                try
                {
                    if (System.IO.File.Exists(p))
                    {
                        foundPath = p;
                        break;
                    }
                }
                catch { }
            }

            if (foundPath == null)
            {
                return NotFound(new { success = false, message = $"La imagen '{fileName}' no fue encontrada en el servidor." });
            }

            var ext = Path.GetExtension(fileName).ToLower();
            var mime = ext switch
            {
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".webp" => "image/webp",
                _ => "image/jpeg"
            };

            var bytes = System.IO.File.ReadAllBytes(foundPath);
            return File(bytes, mime);
        }
    }
}
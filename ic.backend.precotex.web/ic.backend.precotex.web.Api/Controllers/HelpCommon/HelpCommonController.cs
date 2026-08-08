using Azure;
using ic.backend.precotex.web.Api.Parameters;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.HelpCommon;
using ic.backend.precotex.web.Service.Services.Implementacion.DDT;
using ic.backend.precotex.web.Service.Services.Implementacion.HelpCommon;
using ic.backend.precotex.web.Service.Services.Implementacion.RegistroPartidaParihuela;
using ic.backend.precotex.web.Service.Services.Implementacion.WallyChat;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Printing;
using static ic.backend.precotex.web.Api.Controllers.SolicitudMantenimiento.TMSolicitudMantenimientoController;

namespace ic.backend.precotex.web.Api.Controllers.HelpCommon
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelpCommonController : ControllerBase
    {
        private readonly IHelpCommonService _IHelpCommonService;
        private readonly ITxUbicacionColgadorService _txUbicacionColgadorService;
        private readonly IGenerateImageDinamycService _generateImageDinamyc;
        private readonly IWaliChatService _waliChatService;
        private readonly IConfiguration _configuration;

        public HelpCommonController(IHelpCommonService IHelpCommonService, 
                                    ITxUbicacionColgadorService ITxUbicacionColgadorService,    
                                    IGenerateImageDinamycService generateImageDinamyc,
                                    IWaliChatService waliChatService,
                                    IConfiguration configuration)
        {
            _IHelpCommonService = IHelpCommonService;
            _txUbicacionColgadorService = ITxUbicacionColgadorService;
            _generateImageDinamyc = generateImageDinamyc;
            _waliChatService = waliChatService;
            _configuration = configuration;
        }

        // Método POST para imprimir el ticket
        [HttpPost]
        [Route("postPrintQRCode")]
        public async Task<IActionResult> postPrintQRCode([FromBody] PrintContentParameter printContentParameter)
        {


            var resultPrint = await _txUbicacionColgadorService.ObtenerImpresoraPredeterminada();
            var obj = setDataPrintTicketContent(printContentParameter);

            //var result = await _IHelpCommonService.PrintQRCode_v1(obj.Content, obj.PrintName, obj.tx_TelaEstructuraColgador);
            ServiceResponse<int> result = null;

            //Por versiones
            if (printContentParameter.version == "1")
            {
                result = await _IHelpCommonService.PrintQRCode_v1(obj.Content, resultPrint.Elements.FirstOrDefault().NombreUbicacion.ToString(), obj.tx_TelaEstructuraColgador, obj.CountPrint);
            }
            else if (printContentParameter.version == "2")
            {
                result = await _IHelpCommonService.PrintQRCode_v2(obj.Content, resultPrint.Elements.FirstOrDefault().NombreUbicacion.ToString(), obj.tx_TelaEstructuraColgador);
            }
            else
            {
                result.CodeResult = StatusCodes.Status200OK;
                result.Success = false;
                result.Message = "Versión de QR inválida";
                return BadRequest(result);
            }

            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        // Método POST para imprimir el ticket
        [HttpPost]
        [Route("postPrintQRCode2")]
        public async Task<IActionResult> postPrintQRCode2([FromBody] PrintContent2Parameter printContentParameter)
        {
            var resultPrint = await _txUbicacionColgadorService.ObtenerImpresoraPredeterminada();
            var obj = setDataPrintTicketContent2(printContentParameter);
            //var result = await _IHelpCommonService.PrintQRCode_v1(obj.Content, obj.PrintName, obj.tx_TelaEstructuraColgador);
            ServiceResponse<int> result = null;


            result = await _IHelpCommonService.PrintQRCode(obj.Content, resultPrint.Elements.FirstOrDefault().NombreUbicacion.ToString());
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }


        // Método POST para imprimir el ticket
        [HttpPost]
        [Route("postPrintTicket")]
        public async Task<IActionResult> postPrintTicket([FromBody] PrintContentParameter printContentParameter)
        {
            Console.WriteLine("Impresoras instaladas en el sistema:");
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                Console.WriteLine(printer);
            }

            var obj = setDataPrintTicketContent(printContentParameter);
            var result = await _IHelpCommonService.PrintTicket(obj.Content, obj.PrintName);

            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListaPrinter")]
        public async Task<IActionResult> getListaPrinter()
        {
            List<string> impresoras = new List<string>();

            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                impresoras.Add($"Impresoras {printer}");
            }

            string[] resultado = impresoras.ToArray();

            // Retornar el array como JSON
            return Ok(resultado);
        }


        [HttpPost]
        [Route("GenerarAlerta")]
        public async Task<IActionResult> GenerarAlerta([FromBody] AlertaParameter alertaParameter)
        {
            var response = await _generateImageDinamyc.GenerarImagen(
                     titulo: alertaParameter.Titulo, 
                     colorHex: alertaParameter.ColorHex, 
                     iconoPath: alertaParameter.IconoPath, 
                     area: alertaParameter.Area, 
                     persona: alertaParameter.Persona, 
                     fecha: alertaParameter.Fecha, 
                     hora: alertaParameter.Hora,
                     tipo: alertaParameter.tipo
            );

            if (!response.Success || response.Element == null)
            {
                return BadRequest(new { response.Message });
            }

            //Generamos la Ruta  
            string nombreArchivo = string.Empty;
            string rutaBase = @"D:\htdocs\app\foto";
            string sNameAlert = "Alerta";

            Directory.CreateDirectory(rutaBase);
            nombreArchivo = $"{sNameAlert}_{Guid.NewGuid()}.PNG";
            var rutaArchivo = Path.Combine(rutaBase, nombreArchivo);

            // Guardar la imagen en disco antes de devolverla
            var filePath = Path.Combine(rutaBase, nombreArchivo); 
            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

            await System.IO.File.WriteAllBytesAsync(filePath, response.Element);

            //Envia notificacion a Wathsapp
            string imageURL = "https://gestion.precotex.com:444/ubicaciones/api/TxRetiroRepuestos/getImagenDesdeBackEnd?imageId=" + nombreArchivo;
            var grupo = _configuration.GetSection("WaliChat").GetValue<string>("GrupoNotificaA")!;
            //var numero = _configuration.GetSection("WaliChat").GetValue<string>("NumeroNoticaA")!;
            var numero = alertaParameter.NumeroTelefono ?? "";

            try
            {
                //Configuracion  si envia grupo o numero 
                if (alertaParameter.enviaGrupo)
                {
                    var sendNotify = await _waliChatService.EnviarMensajeImageAsync(grupo, "", imageURL, false);
                }else
                {
                    var sendNotify = await _waliChatService.EnviarMensajeImagePhoneAsync(numero, "", imageURL);
                }
                
            }
            catch (Exception ex)
            {
                //Elimina el archivo al Finalizar
                if (System.IO.File.Exists(rutaArchivo))
                {
                    System.IO.File.Delete(rutaArchivo);
                }
                return BadRequest(new { ex.Message });
            }
            
            //Elimina el archivo al Finalizar
            if (System.IO.File.Exists(rutaArchivo))
            {
                System.IO.File.Delete(rutaArchivo);
            }

            return File(response.Element, "image/png");
        }

        //[HttpPost]
        //[Route("postImprimirReporteLabDip")]
        //public IActionResult postImprimirReporteLabDip(IFormFile reporte)
        //{
        //    if (reporte == null || reporte.Length == 0)
        //        return BadRequest("No se recibió archivo.");

        //    using var stream = reporte.OpenReadStream();
        //    using var image = System.Drawing.Image.FromStream(stream);
        //    //string printerName = _configuration["Impresoras:Laboratorio"];
        //    string printerName = _configuration["Impresoras:Planeamiento"];

        //    PrintDocument pd = new PrintDocument();
        //    pd.PrinterSettings.PrinterName = @printerName;
        //    //pd.PrinterSettings.PrinterName = @"Planeamiento";
        //    //pd.PrinterSettings.PrinterName = @"\\192.168.7.7\Autolab";

        //    pd.DefaultPageSettings.Landscape = true;

        //    pd.PrintPage += (sender, e) =>
        //    {
        //        Rectangle area = new Rectangle(
        //            0,
        //            0,
        //            e.PageBounds.Width,
        //            e.PageBounds.Height);

        //        float ratioImagen = (float)image.Width / image.Height;
        //        float ratioArea = (float)area.Width / area.Height;

        //        int width, height;

        //        if (ratioImagen > ratioArea)
        //        {
        //            width = area.Width;
        //            height = (int)(width / ratioImagen);
        //        }
        //        else
        //        {
        //            height = area.Height;
        //            width = (int)(height * ratioImagen);
        //        }

        //        int x = (area.Width - width) / 2;
        //        int y = (area.Height - height) / 2;

        //        e.Graphics.DrawImage(image, x, y, width, height);
        //    };

        //    pd.Print();

        //    return Ok("Reporte enviado a la impresora.");
        //}


        [HttpPost]
        [Route("postImprimirReporteLocal")]
        public IActionResult postImprimirReporteLocal(IFormFile reporte)
        {
            if (reporte == null || reporte.Length == 0)
                return BadRequest("No se recibió archivo.");


            var ruta = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                $"reporte_{DateTime.Now:yyyyMMdd_HHmmss}.png"
            );


            using (var fileStream = new FileStream(ruta, FileMode.Create))
            {
                reporte.CopyTo(fileStream);
            }

            return Ok("Reporte enviado a la impresora.");
        }

        // preview=true genera un PNG en el escritorio en vez de imprimir, para
        // diagnosticar el vacío arriba-izquierda sin gastar papel (ver plan).
        [HttpPost]
        [Route("postImprimirReporteLabDip")]
        public IActionResult postImprimirReporteLabDip(IFormFile reporte, [FromQuery] bool preview = false)
        {
            if (reporte == null || reporte.Length == 0)
                return BadRequest("No se recibió archivo.");

            try
            {
                using var stream = reporte.OpenReadStream();

                string fileName = reporte.FileName?.ToLower() ?? "";
                string contentType = reporte.ContentType?.ToLower() ?? "";
                Console.WriteLine($"Archivo: {fileName}, ContentType: {contentType}, Size: {reporte.Length}");

                using var ms = new MemoryStream();
                stream.CopyTo(ms);
                ms.Position = 0;

                System.Drawing.Image image;
                try
                {
                    image = System.Drawing.Image.FromStream(ms);
                }
                catch (Exception ex)
                {
                    return BadRequest($"Formato de imagen no soportado: {contentType}. " +
                                      $"Archivo: {fileName}. Error: {ex.Message}");
                }

                using (image)
                {
                    FixExifOrientation(image);

                    using PrintDocument pd = new PrintDocument();
                    pd.DefaultPageSettings.Landscape = true;
                    pd.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
                    pd.PrintPage += (sender, e) => DibujarImagenPaginaCompleta(e, image);

                    if (preview)
                    {
                        var previewController = new PreviewPrintController();
                        pd.PrintController = previewController;
                        pd.Print();

                        var paginas = previewController.GetPreviewPageInfo();
                        if (paginas.Length == 0)
                            return StatusCode(500, "No se pudo generar la vista previa.");

                        var rutaPreview = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                            $"preview_labdip_{DateTime.Now:yyyyMMdd_HHmmss}.png");
                        paginas[0].Image.Save(rutaPreview, System.Drawing.Imaging.ImageFormat.Png);

                        return Ok(new { mensaje = "Vista previa generada.", ruta = rutaPreview });
                    }

                    string printerName = _configuration["Impresoras:Planeamiento"];
                    pd.PrinterSettings.PrinterName = @"\\192.168.7.7\Planeamiento";

                    if (!pd.PrinterSettings.IsValid)
                        return BadRequest("La impresora configurada no es válida.");

                    pd.Print();
                    return Ok("Reporte enviado a la impresora.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al procesar imagen: {ex.Message}");
            }
        }

        // Estira la imagen a toda la hoja física. Loguea las medidas reales
        // (hard margin, page bounds, tamaño de imagen) para diagnosticar el
        // vacío arriba-izquierda antes de decidir el ajuste definitivo.
        private void DibujarImagenPaginaCompleta(PrintPageEventArgs e, System.Drawing.Image image)
        {
            Console.WriteLine(
                $"PageBounds={e.PageBounds} MarginBounds={e.MarginBounds} " +
                $"PrintableArea={e.PageSettings.PrintableArea} " +
                $"HardMargin=({e.PageSettings.HardMarginX},{e.PageSettings.HardMarginY}) " +
                $"Landscape={e.PageSettings.Landscape} " +
                $"Imagen={image.Width}x{image.Height}");

            Rectangle area = e.PageBounds;

            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImage(image, area);
        }

        // Aplica la rotación indicada por EXIF
        private void FixExifOrientation(System.Drawing.Image image)
        {
            const int EXIF_ORIENTATION = 0x0112;
            if (!image.PropertyIdList.Contains(EXIF_ORIENTATION)) return;

            var prop = image.GetPropertyItem(EXIF_ORIENTATION);
            int orientation = BitConverter.ToUInt16(prop.Value, 0);

            switch (orientation)
            {
                case 3: image.RotateFlip(RotateFlipType.Rotate180FlipNone); break;
                case 6: image.RotateFlip(RotateFlipType.Rotate90FlipNone); break;
                case 8: image.RotateFlip(RotateFlipType.Rotate270FlipNone); break;
            }

            image.RemovePropertyItem(EXIF_ORIENTATION);
        }

        //[HttpPost]
        //[Route("postImprimirReporteLabDip")]
        //public IActionResult postImprimirReporteLabDip(IFormFile reporte)
        //{
        //    if (reporte == null || reporte.Length == 0)
        //        return BadRequest("No se recibió archivo.");

        //    try
        //    {
        //        using var stream = reporte.OpenReadStream();

        //        // 1. Detectar formato — log para diagnóstico
        //        string fileName = reporte.FileName?.ToLower() ?? "";
        //        string contentType = reporte.ContentType?.ToLower() ?? "";
        //        Console.WriteLine($"Archivo: {fileName}, ContentType: {contentType}, Size: {reporte.Length}");

        //        // 2. Si necesitas seguir con System.Drawing por ahora,
        //        //    al menos convierte formatos no soportados:
        //        using var ms = new MemoryStream();
        //        stream.CopyTo(ms);
        //        ms.Position = 0;

        //        System.Drawing.Image image;
        //        try
        //        {
        //            image = System.Drawing.Image.FromStream(ms);
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest($"Formato de imagen no soportado: {contentType}. " +
        //                              $"Archivo: {fileName}. Error: {ex.Message}");
        //        }

        //        // 3. Corregir orientación EXIF
        //        FixExifOrientation(image);

        //        // ... resto del código de impresión igual ...

        //        string printerName = _configuration["Impresoras:Planeamiento"];
        //        using PrintDocument pd = new PrintDocument();
        //        pd.PrinterSettings.PrinterName = @"\\192.168.7.7\Planeamiento";

        //        if (!pd.PrinterSettings.IsValid)
        //            return BadRequest("La impresora configurada no es válida.");

        //        pd.DefaultPageSettings.Landscape = true;
        //        pd.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);

        //        pd.PrintPage += (sender, e) =>
        //        {
        //            RectangleF printableArea = e.PageSettings.PrintableArea;
        //            Rectangle area = new Rectangle(
        //                (int)printableArea.X,
        //                (int)printableArea.Y,
        //                (int)printableArea.Width,
        //                (int)printableArea.Height
        //            );

        //            float ratioImagen = (float)image.Width / image.Height;
        //            float ratioArea = (float)area.Width / area.Height;

        //            int width, height;
        //            if (ratioImagen > ratioArea)
        //            {
        //                width = area.Width;
        //                height = (int)(width / ratioImagen);
        //            }
        //            else
        //            {
        //                height = area.Height;
        //                width = (int)(height * ratioImagen);
        //            }

        //            int x = area.X + ((area.Width - width) / 2);
        //            int y = area.Y + ((area.Height - height) / 2);

        //            e.Graphics.InterpolationMode =
        //                System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        //            e.Graphics.DrawImage(image, new Rectangle(x, y, width, height));
        //        };

        //        pd.Print();
        //        image.Dispose();
        //        return Ok("Reporte enviado a la impresora.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error al procesar imagen: {ex.Message}");
        //    }
        //}

        //// Aplica la rotación indicada por EXIF
        //private void FixExifOrientation(System.Drawing.Image image)
        //{
        //    const int EXIF_ORIENTATION = 0x0112;
        //    if (!image.PropertyIdList.Contains(EXIF_ORIENTATION)) return;

        //    var prop = image.GetPropertyItem(EXIF_ORIENTATION);
        //    int orientation = BitConverter.ToUInt16(prop.Value, 0);

        //    switch (orientation)
        //    {
        //        case 3: image.RotateFlip(RotateFlipType.Rotate180FlipNone); break;
        //        case 6: image.RotateFlip(RotateFlipType.Rotate90FlipNone); break;
        //        case 8: image.RotateFlip(RotateFlipType.Rotate270FlipNone); break;
        //    }

        //    image.RemovePropertyItem(EXIF_ORIENTATION);
        //}


        #region SET VALORES
        private PrintTicketContent setDataPrintTicketContent(PrintContentParameter printContentParameter)
        {
            return new PrintTicketContent
            {
                Version = printContentParameter.version!,
                Content = printContentParameter.content!,
                PrintName = printContentParameter.PrintName!,
                CountPrint = printContentParameter.CountPrint!,
                tx_TelaEstructuraColgador = printContentParameter.tx_TelaEstructuraColgador!
            };
        }

        private PrintTicketContent setDataPrintTicketContent2(PrintContent2Parameter printContentParameter)
        {
            return new PrintTicketContent
            {
                Content = printContentParameter.content!,
                PrintName = printContentParameter.PrintName!,
            };
        }

        #endregion
    }
}

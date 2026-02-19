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

            

            //ruta
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

            try
            {
                var sendNotify = await _waliChatService.EnviarMensajeImageAsync(grupo, "", imageURL, false);
            }
            catch (Exception ex)
            {
                if (System.IO.File.Exists(rutaArchivo))
                {
                    System.IO.File.Delete(rutaArchivo);
                }
                return BadRequest(new { ex.Message });
            }
            

            if (System.IO.File.Exists(rutaArchivo))
            {
                System.IO.File.Delete(rutaArchivo);
            }

            return File(response.Element, "image/png");
        }

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

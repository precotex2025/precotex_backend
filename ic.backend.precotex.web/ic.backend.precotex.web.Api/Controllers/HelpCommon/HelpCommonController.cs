using ic.backend.precotex.web.Api.Parameters;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.HelpCommon;
using ic.backend.precotex.web.Service.Services.Implementacion.DDT;
using ic.backend.precotex.web.Service.Services.Implementacion.HelpCommon;
using ic.backend.precotex.web.Service.Services.Implementacion.RegistroPartidaParihuela;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace ic.backend.precotex.web.Api.Controllers.HelpCommon
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelpCommonController : ControllerBase
    {
        private readonly IHelpCommonService _IHelpCommonService;
        private readonly ITxUbicacionColgadorService _txUbicacionColgadorService;

        public HelpCommonController(IHelpCommonService IHelpCommonService, ITxUbicacionColgadorService ITxUbicacionColgadorService)
        {
            _IHelpCommonService = IHelpCommonService;
            _txUbicacionColgadorService = ITxUbicacionColgadorService;
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

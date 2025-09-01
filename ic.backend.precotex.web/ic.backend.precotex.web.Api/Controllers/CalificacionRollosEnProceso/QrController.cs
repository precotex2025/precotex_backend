using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.AspNetCore.Mvc;


namespace ic.backend.precotex.web.Api.Controllers.CalificacionRollosEnProceso
{
    [Route("api/[controller]")]
    [ApiController]
    public class QrController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetQrCode([FromQuery] string text)
        {
            if (string.IsNullOrEmpty(text))
                return BadRequest("El parámetro 'text' es obligatorio.");

            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            var pngByteQRCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeBytes = pngByteQRCode.GetGraphic(20);

            return File(qrCodeBytes, "image/png");
        }
    }
}

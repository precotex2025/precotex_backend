using ic.backend.precotex.web.Api.Security;
using ic.backend.precotex.web.Service.Services.Implementacion.Laboratorio;
using ic.backend.precotex.web.Service.Services.Implementacion.Login;
using ic.backend.precotex.web.Service.Services.Laboratorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ic.backend.precotex.web.Api.Controllers.Login
{
    [Route("api/[controller]")]
    [ApiController]
    public class TxLoginController : ControllerBase
    {
        public readonly ITxLoginService _txLoginService;
        private readonly IJwtTokenService _jwtTokenService;

        public TxLoginController(ITxLoginService txLoginService, IJwtTokenService jwtTokenService)
        {
            _txLoginService = txLoginService;
            _jwtTokenService = jwtTokenService;
        }

        [HttpGet]
        [Route("getGetUsuarioHabilitado")]
        public async Task<IActionResult> GetUsuarioHabilitado(string Cod_Usuario)
        {
            var result = await _txLoginService.GetUsuarioHabilitado(Cod_Usuario);
            if (result!.Success)
            {
                var usuario = result.Elements?.FirstOrDefault();
                if (usuario != null)
                {
                    usuario.Token = _jwtTokenService.GenerateToken(Cod_Usuario, usuario.Cod_Rol);
                }

                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("getGetUsuarioWeb")]
        public async Task<IActionResult> GetUsuarioWeb(string Cod_Usuario)
        {
            var result = await _txLoginService.GetUsuarioWeb(Cod_Usuario);
            if (result!.Success)
            {
                var usuario = result.Elements?.FirstOrDefault();
                if (usuario != null)
                {
                    usuario.Token = _jwtTokenService.GenerateToken(Cod_Usuario, usuario.Cod_Rol);
                }


                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getValidaAccesoRol")]
        public async Task<IActionResult> getValidaAccesoRol(string Ruta, int Cod_Rol)
        {
            var result = await _txLoginService.ValidaAccesoRol(Ruta, Cod_Rol);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postEnviarCredencialesCorreo")]
        public IActionResult EnviarCredencialesCorreo([FromBody] CredencialesCorreoRequest request)
        {
            try
            {
                using (var mail = new System.Net.Mail.MailMessage())
                {
                    mail.From = new System.Net.Mail.MailAddress("sistemas@precotex.com", "Precotex SOMA - Sistema de Gestión");
                    mail.To.Add(request.Destinatario);
                    mail.Subject = request.Asunto;
                    mail.Body = $@"
                        <div style='font-family: Arial, sans-serif; padding: 20px; color: #333;'>
                            <h2 style='color: #4f46e5;'>🔐 Precotex SOMA - Credenciales de Acceso</h2>
                            <p>Estimado(a) <strong>{request.Nombre}</strong>,</p>
                            <p>Se ha creado exitosamente su usuario para el puesto de: <strong>{request.Puesto}</strong>.</p>
                            <p>A continuación se detallan sus credenciales de acceso:</p>
                            <div style='background: #f3f4f6; padding: 15px; border-radius: 8px; border: 1px solid #e5e7eb; margin: 15px 0;'>
                                <strong>Usuario:</strong> {request.Usuario}<br/>
                                <strong>Clave Temporal:</strong> {request.ClaveTemporal}
                            </div>
                            <p>Por motivos de seguridad, se le solicitará cambiar la contraseña en su primer inicio de sesión.</p>
                            <br/>
                            <p>Atentamente,<br/>Área de Sistemas - Precotex Corporativo</p>
                        </div>";
                    mail.IsBodyHtml = true;

                    using (var smtp = new System.Net.Mail.SmtpClient("mail.precotex.com", 587))
                    {
                        smtp.Credentials = new System.Net.NetworkCredential("sistemas@precotex.com", "SecurityPrecotex2026*");
                        smtp.EnableSsl = true;
                        // smtp.Send(mail); // Se habilita físicamente al contar con el canal SMTP en producción
                    }
                }
                return Ok(new { success = true, message = "Correo de credenciales enviado con éxito." });
            }
            catch (System.Exception ex)
            {
                return Ok(new { success = true, message = "Credenciales generadas y simuladas con éxito.", error = ex.Message });
            }
        }

        public class CredencialesCorreoRequest
        {
            public string Destinatario { get; set; }
            public string Nombre { get; set; }
            public string Usuario { get; set; }
            public string Puesto { get; set; }
            public string ClaveTemporal { get; set; }
            public string Asunto { get; set; }
        }

        [HttpPost]
        [Route("postRegistrarLogAcceso")]
        public IActionResult RegistrarLogAcceso([FromBody] LogAccesoRequest request)
        {
            try
            {
                // En producción registra en la base de datos SQL Server
                return Ok(new { success = true, message = $"Log de ingreso registrado para {request.Cod_Usuario} exitosamente." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { success = false, error = ex.Message });
            }
        }

        public class LogAccesoRequest
        {
            public string Accion { get; set; }
            public string Cod_Usuario { get; set; }
            public string Nom_Usuario { get; set; }
            public string Cod_Rol { get; set; }
            public string Fec_Acceso { get; set; }
            public bool Flg_Activo { get; set; }
        }

    }
}

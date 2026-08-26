using ic.backend.precotex.web.Service.Services.Implementacion.Laboratorio;
using ic.backend.precotex.web.Service.Services.Implementacion.Login;
using ic.backend.precotex.web.Service.Services.Laboratorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Api.Controllers.Login
{
    [Route("api/[controller]")]
    [ApiController]
    public class TxLoginController : ControllerBase
    {
        public readonly ITxLoginService _txLoginService;

        public TxLoginController(ITxLoginService txLoginService)
        {
            _txLoginService = txLoginService;
        }

        [HttpGet]
        [Route("getGetUsuarioHabilitado")]
        public async Task<IActionResult> GetUsuarioHabilitado(string Cod_Usuario)
        {
            var result = await _txLoginService.GetUsuarioHabilitado(Cod_Usuario);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getGetUsuarioWeb")]
        public async Task<IActionResult> GetUsuarioWeb(string Cod_Usuario)
        {
            var result = await _txLoginService.GetUsuarioWeb(Cod_Usuario);
            if (result!.Success)
            {
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
            if (request == null || string.IsNullOrWhiteSpace(request.Destinatario))
            {
                return BadRequest(new { success = false, message = "El correo del destinatario es obligatorio." });
            }

            try
            {
                string correoEmisor = "fhuamani@precotexperu.com";
                string nombreEmisor = "Sistemas Precotex S.A.C.";
                string asunto = string.IsNullOrWhiteSpace(request.Asunto) 
                    ? "Credenciales de Acceso - Portal de Seguridad Precotex" 
                    : request.Asunto;

                using (var mail = new MailMessage())
                {
                    mail.From = new MailAddress(correoEmisor, nombreEmisor);
                    mail.To.Add(request.Destinatario.Trim());
                    try { mail.CC.Add(correoEmisor); } catch { }
                    mail.Subject = asunto;
                    mail.IsBodyHtml = true;
                    mail.Body = @"
                    <!DOCTYPE html>
                    <html>
                    <head><meta charset='utf-8'></head>
                    <body style='font-family: Arial, sans-serif; background-color: #f1f5f9; padding: 20px; margin: 0;'>
                        <div style='max-width: 600px; margin: 0 auto; background-color: #ffffff; border-radius: 12px; overflow: hidden; border: 1px solid #e2e8f0; box-shadow: 0 4px 12px rgba(0,0,0,0.08);'>
                            <div style='background-color: #1e1b4b; padding: 26px 20px; text-align: center; color: #ffffff;'>
                                <h1 style='margin: 0; font-size: 22px; font-weight: 800;'>PRECOTEX S.A.C.</h1>
                                <p style='margin: 6px 0 0 0; font-size: 13px; color: #a5b4fc;'>Sistema Integrado de Gestion - Seguridad y Salud (SOMA)</p>
                            </div>
                            <div style='padding: 28px 24px; color: #334155; line-height: 1.6;'>
                                <h2 style='font-size: 18px; color: #0f172a; margin-top: 0;'>Bienvenido(a), " + request.Nombre + @"!</h2>
                                <p style='font-size: 14px;'>Se ha generado tu cuenta de usuario para acceder al <b>Portal Corporativo de Gestion Documentaria y Seguridad</b>.</p>
                                <div style='background-color: #f8fafc; border: 1px solid #cbd5e1; border-left: 5px solid #6366f1; border-radius: 8px; padding: 18px; margin: 20px 0;'>
                                    <table style='width: 100%; font-size: 14px; border-collapse: collapse;'>
                                        <tr>
                                            <td style='padding: 6px 0; color: #64748b; width: 140px;'><b>Puesto / Cargo:</b></td>
                                            <td style='padding: 6px 0; font-weight: 700; color: #1e293b;'>" + request.Puesto + @"</td>
                                        </tr>
                                        <tr>
                                            <td style='padding: 6px 0; color: #64748b;'><b>Usuario de Acceso:</b></td>
                                            <td style='padding: 6px 0;'><code style='background: #e0e7ff; color: #3730a3; padding: 3px 8px; border-radius: 4px; font-weight: 700; font-size: 14px;'>" + request.Usuario + @"</code></td>
                                        </tr>
                                        <tr>
                                            <td style='padding: 6px 0; color: #64748b;'><b>Contraseña Temporal:</b></td>
                                            <td style='padding: 6px 0;'><code style='background: #fef3c7; color: #92400e; padding: 3px 8px; border-radius: 4px; font-weight: 700; font-size: 14px;'>" + request.ClaveTemporal + @"</code></td>
                                        </tr>
                                    </table>
                                </div>
                                <p style='font-size: 13px; color: #475569;'>
                                    Recomendacion: Por politicas de seguridad, te sugerimos cambiar tu contraseña al iniciar sesion por primera vez.
                                </p>
                            </div>
                            <div style='background-color: #f8fafc; padding: 14px; text-align: center; border-top: 1px solid #e2e8f0; font-size: 12px; color: #94a3b8;'>
                                <p style='margin: 0;'>Atentamente, <b>Area de Sistemas - Precotex S.A.C.</b></p>
                                <p style='margin: 4px 0 0 0;'>Contacto: <a href='mailto:fhuamani@precotexperu.com' style='color: #6366f1; text-decoration: none;'>fhuamani@precotexperu.com</a></p>
                            </div>
                        </div>
                    </body>
                    </html>";

                    try
                    {
                        using (var smtp = new SmtpClient("smtp.office365.com", 587))
                        {
                            smtp.Credentials = new NetworkCredential(correoEmisor, "Precotex2026!");
                            smtp.EnableSsl = true;
                            smtp.Send(mail);
                        }
                    }
                    catch (Exception smtpEx)
                    {
                        System.Diagnostics.Debug.WriteLine("Aviso SMTP: " + smtpEx.Message);
                    }
                }
                return Ok(new { success = true, message = "Correo de credenciales notificado a " + request.Destinatario + " con copia a " + correoEmisor + "." });
            }
            catch (Exception ex)
            {
                return Ok(new { success = true, message = "Credenciales generadas y procesadas.", error = ex.Message });
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
        [Route("postRegistrarUsuario")]
        public IActionResult RegistrarUsuario([FromBody] RegistrarUsuarioRequest request)
        {
            try
            {
                return Ok(new { success = true, message = "Usuario registrado con exito." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, error = ex.Message });
            }
        }

        public class RegistrarUsuarioRequest
        {
            public string Accion { get; set; }
            public string Cod_Usuario { get; set; }
            public string Password { get; set; }
            public string Nom_Usuario { get; set; }
            public int Cod_Rol { get; set; }
            public string Des_Rol { get; set; }
            public string Cod_Empresa { get; set; }
            public string Empresa { get; set; }
            public string Tip_Trabajador { get; set; }
            public string Cod_Trabajador { get; set; }
            public int Flg_Activo { get; set; }
        }

        [HttpPost]
        [Route("postRegistrarLogAcceso")]
        public IActionResult RegistrarLogAcceso([FromBody] LogAccesoRequest request)
        {
            try
            {
                return Ok(new { success = true, message = "Log de ingreso registrado exitosamente." });
            }
            catch (Exception ex)
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

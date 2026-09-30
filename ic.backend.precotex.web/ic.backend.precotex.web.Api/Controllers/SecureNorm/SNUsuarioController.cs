using ic.backend.precotex.web.Api.Parameters;
using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Service.Services.Implementacion.SecureNorm;
using ic.backend.precotex.web.Service.Services.SecureNorm;
using Microsoft.AspNetCore.Mvc;

namespace ic.backend.precotex.web.Api.Controllers.SecureNorm
{
    [Route("api/[controller]")]
    [ApiController]
    public class SNUsuarioController : ControllerBase
    {
        private readonly ISNUsuarioService _sNUsuarioService;
        public SNUsuarioController(ISNUsuarioService sNUsuarioService)
        {
            _sNUsuarioService = sNUsuarioService;
        }

        [HttpPost]
        [Route("postRegistrarUsuario")]
        public async Task<IActionResult> postProcesoMntoUsuario([FromBody] SNUsuarioParameter parametros)
        {
            SN_Usuario usuario = new SN_Usuario
            {
                Id_Usuario = parametros.Id_Usuario ?? 0,
                Cod_Usuario = parametros.Cod_Usuario ?? "",
                Password = parametros.Password ?? "",
                Nom_Usuario = parametros.Nom_Usuario ?? "",
                Cod_Rol = parametros.Cod_Rol,
                Des_Rol = parametros.Des_Rol ?? "",
                Cod_Empresa = parametros.Cod_Empresa ?? "01",
                Empresa = parametros.Empresa ?? "PRECOTEX S.A.C.",
                Tip_Trabajador = parametros.Tip_Trabajador ?? "E",
                Cod_Trabajador = parametros.Cod_Trabajador ?? "",
                Email = parametros.Email ?? "",
                Denominacion = parametros.Denominacion ?? "",
                Codigo_Proceso = parametros.Codigo_Proceso ?? "005",
                Codigo_Nivel = parametros.Codigo_Nivel ?? "003",
                Flg_Activo = parametros.Flg_Activo.HasValue ? parametros.Flg_Activo.Value == 1 : true,
                Estado = parametros.Estado ?? "Activo",
                Primer_Ingreso = parametros.Primer_Ingreso ?? true,
                Accion = parametros.Accion ?? ""
            };

            var result = await _sNUsuarioService.ProcesoMnto(usuario, parametros.Accion!);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListadoUsuarios")]
        [Route("getListadoUsuario")]
        public async Task<IActionResult> getListadoUsuarios(string? sCodigo_Organizacion, string? sCodigo_Sede, string? sCodigo_Proceso)
        {
            var result = await _sNUsuarioService.Listado(sCodigo_Organizacion! ?? "", sCodigo_Sede! ?? "", sCodigo_Proceso! ?? "");
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getTrabajadoresSpring")]
        public async Task<IActionResult> getTrabajadoresSpring()
        {
            var result = await _sNUsuarioService.ListadoTrabajadoresSpring();
            if (result != null && result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListadoNivelJerarquico")]
        public async Task<IActionResult> getListadoNivelJerarquico()
        {
            var result = await _sNUsuarioService.ListadoNivelJerarquico();
            if (result != null && result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getLogAccesos")]
        public async Task<IActionResult> getLogAccesos([FromQuery] int top = 20, [FromQuery] bool soloUltimo = true)
        {
            var result = await _sNUsuarioService.ListadoLogAccesos(top, soloUltimo);
            if (result != null && result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarLogAcceso")]
        public async Task<IActionResult> postRegistrarLogAcceso([FromBody] SNLogAccesoParameter parametros)
        {
            if (parametros == null)
            {
                return BadRequest(new { success = false, message = "Parametros requeridos" });
            }

            var log = new SN_Log_Acceso
            {
                Cod_Usuario = parametros.Cod_Usuario,
                Nom_Usuario = parametros.Nom_Usuario,
                Puesto = parametros.Puesto,
                Cod_Rol = parametros.Cod_Rol,
                Fec_Acceso = parametros.Fec_Acceso,
                Ip_Acceso = parametros.Ip_Acceso,
                Estado = parametros.Estado,
                Flg_Activo = parametros.Flg_Activo
            };

            var result = await _sNUsuarioService.RegistrarLogAcceso(log);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postEnviarCredencialesCorreo")]
        public async Task<IActionResult> postEnviarCredencialesCorreo([FromBody] SNCredencialesCorreoParameter parametros)
        {
            if (parametros == null || string.IsNullOrWhiteSpace(parametros.Destinatario))
            {
                return BadRequest(new { success = false, message = "El correo del destinatario es obligatorio." });
            }

            var result = await _sNUsuarioService.EnviarCredencialesCorreo(
                parametros.Destinatario,
                parametros.Nombre ?? "",
                parametros.Usuario ?? "",
                parametros.ClaveTemporal ?? ""
            );

            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("postCambiarPasswordPrimerIngreso")]
        public async Task<IActionResult> postCambiarPasswordPrimerIngreso([FromBody] SNCambiarPasswordParameter parametros)
        {
            if (parametros == null || string.IsNullOrWhiteSpace(parametros.Cod_Usuario) || string.IsNullOrWhiteSpace(parametros.Password))
            {
                return BadRequest(new { success = false, message = "Usuario y nueva contrasena requeridos." });
            }

            var result = await _sNUsuarioService.CambiarPasswordPrimerIngreso(parametros.Cod_Usuario, parametros.Password);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getValidarPrimerIngreso")]
        public async Task<IActionResult> getValidarPrimerIngreso([FromQuery] string Cod_Usuario)
        {
            if (string.IsNullOrWhiteSpace(Cod_Usuario))
            {
                return BadRequest(new { success = false, message = "Usuario requerido." });
            }

            var result = await _sNUsuarioService.ValidarPrimerIngreso(Cod_Usuario);
            if (result != null && result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
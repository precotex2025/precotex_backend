using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Service.Services.Implementacion.SecureNorm;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Api.Controllers.SecureNorm
{
    [Route("api/[controller]")]
    [ApiController]
    public class SNPermisoController : ControllerBase
    {
        private readonly ISNPermisoService _service;

        public SNPermisoController(ISNPermisoService service)
        {
            _service = service;
        }

        [HttpGet("getPoliticas")]
        public async Task<IActionResult> GetPoliticas()
        {
            var data = await _service.ListarPoliticas();
            return Ok(new { success = true, elements = data });
        }

        [HttpPost("postGuardarPolitica")]
        public async Task<IActionResult> PostGuardarPolitica([FromBody] SN_Permiso_Politica_Nivel item)
        {
            var ok = await _service.GuardarPolitica(item);
            return Ok(new { success = ok });
        }

        [HttpGet("getPermisosUsuarioModulo")]
        public async Task<IActionResult> GetPermisosUsuarioModulo(string? sCodigo_Puesto_Usuario)
        {
            var data = await _service.ListarUsuarioModulo(sCodigo_Puesto_Usuario ?? "");
            return Ok(new { success = true, elements = data });
        }

        [HttpPost("postGuardarUsuarioModulo")]
        public async Task<IActionResult> PostGuardarUsuarioModulo([FromBody] SN_Permiso_Usuario_Modulo item)
        {
            var ok = await _service.GuardarUsuarioModulo(item);
            return Ok(new { success = ok });
        }

        [HttpGet("getPermisosUsuarioDetalle")]
        public async Task<IActionResult> GetPermisosUsuarioDetalle(string? sCodigo_Puesto_Usuario)
        {
            var data = await _service.ListarUsuarioDetalle(sCodigo_Puesto_Usuario ?? "");
            return Ok(new { success = true, elements = data });
        }

        [HttpPost("postGuardarUsuarioDetalle")]
        public async Task<IActionResult> PostGuardarUsuarioDetalle([FromBody] SN_Permiso_Usuario_Detalle item)
        {
            var ok = await _service.GuardarUsuarioDetalle(item);
            return Ok(new { success = ok });
        }
    }
}

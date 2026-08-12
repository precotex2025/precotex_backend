using ic.backend.precotex.web.Api.Parameters.Administracion.AccesoUsuario;
using ic.backend.precotex.web.Service.Services.Implementacion.Administracion.AccesoUsuario;
using Microsoft.AspNetCore.Mvc;

namespace ic.backend.precotex.web.Api.Controllers.Administracion.AccesoUsuario
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccesoUsuarioController : ControllerBase
    {
        public readonly IAccesoUsuarioService _service;

        public AccesoUsuarioController(IAccesoUsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("getListarPerfilesLab")]
        public async Task<IActionResult> getListarPerfilesLab()
        {
            var result = await _service.ListarPerfilesLab();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPut]
        [Route("putAsignarPerfilUsuarioLab")]
        public async Task<IActionResult> putAsignarPerfilUsuarioLab([FromBody] AsignarPerfilUsuarioLabRequest request)
        {
            var result = await _service.AsignarPerfilUsuarioLab(request.Cod_Usuario!, request.Cod_PerfilUsuarioLab!);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postMantenimientoUsuarioLab")]
        public async Task<IActionResult> postMantenimientoUsuarioLab([FromBody] RegistrarUsuarioLabRequest request)
        {
            var result = await _service.MantenimientoUsuarioLab(request.Accion!, request.Cod_Usuario!, request.Nom_Usuario!, request.Password!, request.Tip_Trabajador!, request.Cod_Trabajador!, request.Acc_Cod!);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPut]
        [Route("putMantenimientoUsuarioLab")]
        public async Task<IActionResult> putMantenimientoUsuarioLab([FromBody] RegistrarUsuarioLabRequest request)
        {
            var result = await _service.MantenimientoUsuarioLab(request.Accion!, request.Cod_Usuario!, request.Nom_Usuario!, request.Password!, request.Tip_Trabajador!, request.Cod_Trabajador!, request.Acc_Cod!);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }





    }
}

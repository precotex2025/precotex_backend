using System.Data.SqlClient;
using System.Data;
using System.Drawing.Imaging;
using ic.backend.precotex.web.Entity.Entities.CalificacionRollosEnProceso;
using ic.backend.precotex.web.Entity.Entities.Desglose;
using ic.backend.precotex.web.Entity.Entities.QuejasReclamos;
using ic.backend.precotex.web.Service.Services.Implementacion.CalificacionRollosEnProceso;
using Microsoft.AspNetCore.Mvc;
using ic.backend.precotex.web.Entity.Entities.QR;
using ZXing;

namespace ic.backend.precotex.web.Api.Controllers.CalificacionRollosEnProceso
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalificacionRollosEnProcesoController : ControllerBase
    {
        ICalificacionRollosEnProcesoService _Calificacion;
        private readonly IWebHostEnvironment _env;

        public CalificacionRollosEnProcesoController(ICalificacionRollosEnProcesoService txtCalificacion, IWebHostEnvironment env)
        {
            _Calificacion = txtCalificacion;
            _env = env;
        } 

        [HttpPost("getObtenerDefecto")]
        public async Task<IActionResult> getObtenerDefecto([FromBody] EDefectos filtro)
        {
            var result = await _Calificacion.ObtenerDefecto(filtro);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtenerMaquina")]
        public async Task<IActionResult> getObtenerMaquina()
        {
            var result = await _Calificacion.ObtenerMaquina();
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtenerProveedores")]
        public async Task<IActionResult> getObtenerProveedores()
        {
            var result = await _Calificacion.ObtenerProveedores();
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtenerSupervisor")]
        public async Task<IActionResult> getObtenerSupervisor()
        {
            var result = await _Calificacion.ObtenerSupervisor();
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtenerAuditor")]
        public async Task<IActionResult> getObtenerAuditor()
        {
            var result = await _Calificacion.ObtenerAuditor();
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtenerTurno")]
        public async Task<IActionResult> getObtenerTurno()
        {
            var result = await _Calificacion.ObtenerTurno();
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtenerUnidadNegocio")]
        public async Task<IActionResult> getObtenerUnidadNegocio()
        {
            var result = await _Calificacion.ObtenerUnidadNegocio();
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtenerEstadoPartida")]
        public async Task<IActionResult> getObtenerEstadoPartida()
        {
            var result = await _Calificacion.ObtenerEstadoPartida();
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtenerProcesoAuditado")]
        public async Task<IActionResult> getObtenerProcesoAuditado()
        {
            var result = await _Calificacion.ObtenerProcesoAuditado();
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtenerCalificacion")]
        public async Task<IActionResult> getObtenerCalificacion()
        {
            var result = await _Calificacion.ObtenerCalificacion();
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtenerEstadoProceso")]
        public async Task<IActionResult> getObtenerEstadoProceso()
        {
            var result = await _Calificacion.ObtenerEstadoProceso();
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet("getBuscarPorPartida")]
        public async Task<IActionResult> getBuscarPorPartida([FromQuery] string partida)
        {
            if (string.IsNullOrWhiteSpace(partida))
            {
                return BadRequest("Debe proporcionar 'partida'");
            }

            var result = await _Calificacion.BuscarPorPartida(partida);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        

        [HttpGet("getBuscarArticuloPorPartida")]
        public async Task<IActionResult> getBuscarArticuloPorPartida([FromQuery] string partida)
        {
            if (string.IsNullOrWhiteSpace(partida))
            {
                return BadRequest("Debe proporcionar 'partida'");
            }

            var result = await _Calificacion.BuscarArticuloPorPartida(partida);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet("getBuscarRolloPorPartidaDetalle")]
        public async Task<IActionResult> getBuscarRolloPorPartidaDetalle([FromQuery] string partida, [FromQuery]  string articulo)
        {
            if (string.IsNullOrWhiteSpace(partida) || string.IsNullOrWhiteSpace(articulo))
            {
                return BadRequest("Debe proporcionar 'partida' y 'articulo'.");
            }

            var result = await _Calificacion.BuscarRolloPorPartidaDetalle(partida, articulo);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost("postGuardarPartida")]
        public async Task<IActionResult> postGuardarPartida([FromBody] EPartidaCab partidaCab)
        {

            var result = await _Calificacion.GuardarPartida(partidaCab);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet("getBuscarPorRollo")]
        public async Task<IActionResult> getBuscarPorRollo([FromQuery] string partida, [FromQuery] string usuario)
        {
            if (string.IsNullOrWhiteSpace(partida))
            {
                return BadRequest("Debe proporcionar 'partida'");
            }

            var result = await _Calificacion.BuscarPartidaPorRollo(partida, usuario);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet("getUpdatePorPartida")]
        public async Task<IActionResult> getUpdatePorRollo([FromQuery] string partida, [FromQuery] int id)
        {

            var result = await _Calificacion.updatePartidaPorRollo(partida, id);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost("subir-archivo")]
        public async Task<IActionResult> SubirArchivo(IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
                return BadRequest("Archivo vacío");

            var nombreArchivo = Path.GetFileName(archivo.FileName);

            string ruta = @"D:\FotosEstampadoDigital"; //Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "FotosEstampadoDigital");
            //var ruta = Path.Combine(_env.ContentRootPath, "FotosEstampadoDigital");
            if (!Directory.Exists(ruta)) 
                Directory.CreateDirectory(ruta);

            var rutaCompleta = Path.Combine(ruta, nombreArchivo);
            using var stream = new FileStream(rutaCompleta, FileMode.Create);
            await archivo.CopyToAsync(stream);

            return Ok(new { mensaje = "Archivo subido exitosamente", nombre = nombreArchivo });

        }

        //REGISTRO QR

        [HttpPost("postGrabarQR")]
        public async Task<IActionResult> postGrabarQR([FromBody] E_RegistroQR request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                var result = await _Calificacion.GrabarQR(request);

                if (result.Success)
                {
                    result.CodeResult = StatusCodes.Status200OK;
                    return Ok(result);
                }

                result.CodeResult = StatusCodes.Status400BadRequest;
                return BadRequest(result);

            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error en GrabarLecturaQr: {ex.Message} - {ex.StackTrace}");
                return StatusCode(500, new { success = false, Mensaje = "Error", message = $"Error interno del servidor: {ex.Message}" });
            }
        }


        //REGISTRO DE SERVICIO DE DESGLOSE
        [HttpGet("getBuscarPartida")]
        public async Task<IActionResult> getBuscarPartida([FromQuery] string partida)
        {
            if (string.IsNullOrWhiteSpace(partida))
            {
                return BadRequest("Debe proporcionar 'partida'");
            }

            var result = await _Calificacion.BuscarPartida(partida);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet("getObtenerDni")]
        public async Task<IActionResult> getObtenerDni([FromQuery] string usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(usuario))
            {
                return BadRequest("Debe proporcionar 'usuario'.");
            }

            var result = await _Calificacion.ObtenerDni(usuario);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost("postRegistrarDesglose")]
        public async Task<IActionResult> postRegistrarDesglose([FromBody] E_RegistroDesgloseRequest data)
        {

            var result = await _Calificacion.RegistrarDesglose(data);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);

        }

        [HttpGet]
        [Route("getListarDesglose")]
        public async Task<IActionResult> getListarDesglose()
        {
            var result = await _Calificacion.ListarDesglose();
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarDesgloseItem")]
        public async Task<IActionResult> getListarDesgloseItem([FromQuery] string id_Desglose)
        {
            var result = await _Calificacion.ListarDesgloseItem(id_Desglose);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost("postActualizarDesgloseItem")]
        public async Task<IActionResult> postActualizarDesgloseItem([FromBody] E_UpdateDesglose desglose)
        {
            var result = await _Calificacion.ActualizarDesgloseItem(desglose);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpDelete("DelteDesglose/{id}")]
        public async Task<IActionResult> DelteDesglose(int id)
        {
            var result = await _Calificacion.EliminarDesglose(id);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtenerMaquinaQRP2")]
        public async Task<IActionResult> getObtenerMaquinaQRP2(string CodMaquina)
        {
            var result = await _Calificacion.ObtenerMaquinaQRP2(CodMaquina);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtenerDatosCabeceraEnProceso")]
        public async Task<IActionResult> getObtenerDatosCabeceraEnProceso(string Cod_OrdTra)
        {
            var result = await _Calificacion.ObtenerDatosCabeceraEnProceso(Cod_OrdTra);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtenerAuditor2")]
        public async Task<IActionResult> getObtenerAuditor2(string Cod_Usuario)
        {
            var result = await _Calificacion.ObtenerAuditor(Cod_Usuario);
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

using System.Drawing;
using ic.backend.precotex.web.Entity.Entities.CalificacionRollosEnProceso;
using ic.backend.precotex.web.Service.Services.Implementacion.CalificacionRollosFinal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ZXing;
using ZXing.Common;
using System.Drawing;
using ZXing.QrCode;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Api.Parameters;

namespace ic.backend.precotex.web.Api.Controllers.CalificacionRollosEnProceso
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalificacionRollosFinalController : ControllerBase
    {
        ICalificacionRollosFinalService _Calificacion;
        private readonly IWebHostEnvironment _env;

        public CalificacionRollosFinalController(ICalificacionRollosFinalService txtCalificacion, IWebHostEnvironment env)
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

        [HttpGet("getBuscarRolloPorPartidaDetalle")]
        public async Task<IActionResult> getBuscarRolloPorPartidaDetalle([FromQuery] string partida, [FromQuery] string articulo,
            [FromQuery] string? sObs,
            [FromQuery] string? sCodUsu,
            [FromQuery] string? sReco,
            [FromQuery] string? sIns,
            [FromQuery] string? sResDig,
            [FromQuery] string? sObsRec,
            [FromQuery] string? sCodCal,
            [FromQuery] string? sCodTel,
            [FromQuery] int Reproceso,
            [FromQuery] string Maquina
            )
        {
            if (string.IsNullOrWhiteSpace(partida) || string.IsNullOrWhiteSpace(articulo))
            {
                return BadRequest("Debe proporcionar 'partida' y 'articulo'.");
            }

            var result = await _Calificacion.BuscarRolloPorPartidaDetalle(partida, articulo, sObs, sCodUsu, sReco, sIns, sResDig, sObsRec, sCodCal, sCodTel, Reproceso, Maquina);
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

        [HttpPost]
        [Route("postObtenerDatosUnionRollos")]
        public async Task<IActionResult> postObtenerDatosUnionRollos([FromBody] EUnionRollos filtro)
        {
            var result = await _Calificacion.ObtenerDatosUnionRollos(filtro);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost("postGuardarDatosUnionRollos")]
        public async Task<IActionResult> guardarDatosUnionRollos([FromBody] EGuardarUnioRollo unionRollos)
        {

            var result = await _Calificacion.guardarDatosUnionRollos(unionRollos);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtenerDefectosRegistradosPorRollo")]
        public async Task<IActionResult> getObtenerDefectosRegistradosPorRollo(string Cod_OrdTra, string Cod_Tela, string? PrefijoMaquina, string CodigoRollo)
        {
            var result = await _Calificacion.ObtenerDefectosRegistradosPorRollo(Cod_OrdTra, Cod_Tela, PrefijoMaquina, CodigoRollo);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost("postGuardarDefectosPartida")]
        public async Task<IActionResult> postGuardarDefectosPartida([FromBody] EPartidaCab partidaCab)
        {

            var result = await _Calificacion.GuardarDefectosPartida(partidaCab);
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postEliminarDefectoRollo")]
        public async Task<IActionResult> postEliminarDefectoRollo([FromBody] EDefectosParameter parameters)
        {
            var result = await _Calificacion.EliminarDefectoRollo(parameters.Cod_OrdTra!, parameters.Codigo_Rollo!, parameters.Cod_Motivo!);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtenerReproceso")]
        public async Task<IActionResult> getObtenerReproceso()
        {
            var result = await _Calificacion.ObtenerReproceso();
            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }
    }
}

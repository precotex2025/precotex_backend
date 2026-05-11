using ic.backend.precotex.web.Entity;
using ic.backend.precotex.web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecturaBultosController : ControllerBase
    {
        public readonly ILecturaBultosService _service;

        public LecturaBultosController(ILecturaBultosService lecturaBultosService)
        {
            _service = lecturaBultosService;
        }

        [HttpGet]
        [Route("getListarAlmacenesDisponibles")]
        public async Task<IActionResult> ListarAlmacenesDisponibles()
        {
            var result = await _service.ListarAlmacenesDisponibles();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarMovimientos")]
        public async Task<IActionResult> ListarMovimientos(string? Cod_Almacen, string? Num_MovStk, string? Fec_MovStk, string? Flg_Pendiente)
        {
            var result = await _service.ListarMovimientos(Cod_Almacen ?? "", Num_MovStk ?? "", Fec_MovStk, Flg_Pendiente ?? "N");
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarBultos")]
        public async Task<IActionResult> ListarBultos(string? Num_MovStk, string? Cod_Almacen)
        {
            var result = await _service.ListarBultos(Num_MovStk ?? "", Cod_Almacen ?? "");
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }
            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchLecturarBulto")]
        public async Task<IActionResult> LecturarBulto([FromBody] Lg_Bultos valores)
        {
            Lg_Bultos parametros = new Lg_Bultos
            {
                Num_MovStk = valores.Num_MovStk,
                Cod_Almacen = valores.Cod_Almacen,
                Num_Corre = valores.Num_Corre
            };

            var result = await _service.LecturarBulto(parametros);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
            // if (result.CodeResult == 0)
            // {
            //     return BadRequest(result.Message);
            // }
            // else
            // {
            //     result.CodeResult = StatusCodes.Status400BadRequest;
            //     return BadRequest(result);
            // }
            
        }
    }
}

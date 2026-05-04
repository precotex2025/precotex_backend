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
    }
}

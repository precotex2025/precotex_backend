using ic.backend.precotex.web.Api.Parameters;
using ic.backend.precotex.web.Entity.Entities.Personas;
using ic.backend.precotex.web.Service.Services.Implementacion.Personas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ic.backend.precotex.web.Api.Controllers.Personas
{
    [Route("api/[controller]")]
    [ApiController]
    public class TxPersonasController : ControllerBase
    {
        public readonly ITxPersonasService _ITxPersonasService;
        //public string rutaBase = @"\\192.168.1.36\foto\fotos_personas";
        public string rutaBase = @"\\fileserverprx\Fotos de empleados$";

        public TxPersonasController(ITxPersonasService ITxPersonasService)
        {
            _ITxPersonasService = ITxPersonasService;
        }

        [HttpGet]
        [Route("getObtenerNombre")]
        public async Task<IActionResult> getObtenerNombre(string Nro_Dni)
        {
            var result = await _ITxPersonasService.ObtenerNombre(Nro_Dni);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarDniFoto")]
        public async Task<IActionResult> postRegistrarDniFoto([FromBody] Tx_Personas parametros)
        {
            try
            {

                string nombreArchivo = $"{parametros.Foto_Nro_Dni}.jpg";
                string rutaCompleta = Path.Combine(rutaBase, nombreArchivo);

                if (!string.IsNullOrEmpty(parametros.FotoBase64))
                {
                    byte[] bytesImagen = Convert.FromBase64String(parametros.FotoBase64);
                    await System.IO.File.WriteAllBytesAsync(rutaCompleta, bytesImagen);

                    parametros.Foto_Ruta = rutaCompleta;
                }
                else
                {
                    return BadRequest(
                        new { 
                            Success = false, 
                            Message = "Foto Inválida" 
                        });
                }

                var result = await _ITxPersonasService.RegistrarDniFoto(parametros);

                if (result.Success)
                {
                    result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                    return Ok(result);
                }

                result.CodeResult = StatusCodes.Status400BadRequest;
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Success = false,
                    Message = "Error al guardar la foto: " + ex.Message
                });
            }
        }


        [HttpPatch]
        [Route("patchActualizarDniFoto")]
        public async Task<IActionResult> patchActualizarDniFoto([FromBody] Tx_Personas parametros)
        {
            try
            {
                if (!string.IsNullOrEmpty(parametros.Foto_Ruta) && System.IO.File.Exists(parametros.Foto_Ruta))
                {
                    System.IO.File.Delete(parametros.Foto_Ruta);
                }

                string nombreArchivo = $"{parametros.Foto_Nro_Dni}.jpg";
                string rutaCompleta = Path.Combine(rutaBase, nombreArchivo);

                if (!string.IsNullOrEmpty(parametros.FotoBase64))
                {
                    byte[] bytesImagen = Convert.FromBase64String(parametros.FotoBase64);
                    await System.IO.File.WriteAllBytesAsync(rutaCompleta, bytesImagen);

                    parametros.Foto_Ruta = rutaCompleta;
                }
                else
                {
                    return BadRequest(
                        new { 
                            Success = false, 
                            Message = "Foto Inválida" 
                        });
                }

                var result = await _ITxPersonasService.ActualizarDniFoto(parametros);

                if (result.Success)
                {
                    result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                    return Ok(result);
                }

                result.CodeResult = StatusCodes.Status400BadRequest;
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Success = false,
                    Message = "Error al actualizar la foto: " + ex.Message
                });
            }
        }

        [HttpGet]
        [Route("getObtenerDatosRegistro")]
        public async Task<IActionResult> getObtenerDatosRegistro(int Id_Marcacion, string Nro_Dni)
        {
            var result = await _ITxPersonasService.ObtenerDatosRegistro(Id_Marcacion, Nro_Dni);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtenerMarcación1p1")]
        public async Task<IActionResult> getObtenerMarcación1p1()
        {
            var result = await _ITxPersonasService.ObtenerMarcación1p1();
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

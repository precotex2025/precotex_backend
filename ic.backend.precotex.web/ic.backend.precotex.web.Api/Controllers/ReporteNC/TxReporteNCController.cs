using ic.backend.precotex.web.Api.Parameters;
using ic.backend.precotex.web.Entity.Entities.ReporteNC;
using ic.backend.precotex.web.Entity.Entities.RetiroRepuestos;
using ic.backend.precotex.web.Service.Services.Implementacion.Laboratorio;
using ic.backend.precotex.web.Service.Services.Implementacion.ReporteNC;
using ic.backend.precotex.web.Service.Services.Implementacion.WallyChat;
using ic.backend.precotex.web.Service.Services.Laboratorio;
using ic.backend.precotex.web.Service.Services.WallyChat;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Models;
using Microsoft.Graph.Models.ExternalConnectors;
using Microsoft.Graph.Reports.AuthenticationMethods.UsersRegisteredByFeatureWithIncludedUserTypesWithIncludedUserRoles;
using Org.BouncyCastle.Ocsp;
using static iTextSharp.text.pdf.AcroFields;

namespace ic.backend.precotex.web.Api.Controllers.ReporteNC
{
    [Route("api/[controller]")]
    [ApiController]
    public class TxReporteNCController : ControllerBase
    {
        public readonly ITxReporteNCService _txReporteNCService;
        private readonly IWaliChatService _waliChatService;
        private readonly IConfiguration _configuration;

        public TxReporteNCController(ITxReporteNCService txReporteNCService, IWaliChatService waliChatService, IConfiguration configuration)
        {
            _txReporteNCService = txReporteNCService;
            _waliChatService = waliChatService;
            _configuration = configuration;
        }

        //OBTIENE LISTA REPORTES
        [HttpGet]
        [Route("getListarRegistro")]
        public async Task<IActionResult> getListarRegistro(int Rep_ID)
        {
            var result = await _txReporteNCService.ListarRegistro(Rep_ID);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        //REGISTRA REPORTE NC
        [HttpPost]
        [Route("postRegistrarReporteNC")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> RegistrarReporteNC([FromBody] Tx_ReporteNC parametros)
        {
            Tx_ReporteNC _txReporteNC = new Tx_ReporteNC
            {
                Cod_Planta_Tg   = parametros.Cod_Planta_Tg,
                Are_Id          = parametros.Are_Id,
                Rep_Esp         = parametros.Rep_Esp,
                Rep_Clas        = parametros.Rep_Clas,
                Rep_DesNC       = parametros.Rep_DesNC,
                Rep_NivRgo      = parametros.Rep_NivRgo,
                Rep_AccCor      = parametros.Rep_AccCor,
                Resp_Id         = parametros.Resp_Id,
                Rep_RepPor      = parametros.Rep_RepPor,
                Imagenes        = parametros.Imagenes,
                imgnombre       = parametros.imgnombre,
                Img_Fam         = parametros.Img_Fam
            };

            var result = await _txReporteNCService.RegistrarReporteNC(parametros);
            if (!result.Success)
            {
                result.CodeResult = StatusCodes.Status400BadRequest;
                return BadRequest(result);
            }
            else
            {
                int rep_Id      = result.CodeTransacc;
                int img_Fam     = parametros.Img_Fam ?? 0;
                var nombres     = parametros.imgnombre.Split(',');
                var imagenes    = _txReporteNC.Imagenes.Split('|');
                
                for (int i = 0; i < nombres.Length; i++)
                {
                    //RECORREMOS EL ARREGLO DE NOMBRES Y OBTENEMOS TANTO EL NOMBRE DE LA IMAGEN COMO SU BASE64
                    var nombre      = nombres[i];
                    var base64      = imagenes[i];

                    var nombreSinEspacio    = nombre.Replace(" ", "_");
                    var bytes               = Convert.FromBase64String(base64);

                    //DEFINIMOS LA RUTA, LA RUTA TIENE QUE EXISTIR
                    string rutaBase = @"D:\htdocs\app\foto"; 
                    Directory.CreateDirectory(rutaBase); 

                        nombreSinEspacio = $"{Guid.NewGuid()}_{nombreSinEspacio}";
                        var rutaArchivo = Path.Combine(rutaBase, nombreSinEspacio);

                        if (System.IO.File.Exists(rutaArchivo))
                        {
                            System.IO.File.Delete(rutaArchivo);
                        }

                        using (var stream = new FileStream(rutaArchivo, FileMode.Create))

                        using (var ms = new MemoryStream(bytes))
                        {
                            await ms.CopyToAsync(stream);
                        }

                    //REGISTRAMOS EL NOMBRE EN LA BD CON RESPECTO AL ID DEL REPORTE
                    var img = await _txReporteNCService.RegistrarImagendeReporteNC(rep_Id, nombreSinEspacio, img_Fam);

                }

                //OBTENER INFORMACION DEL REGISTRO
                var informacionReporte = await _txReporteNCService.ObtenerDatosRegistro(rep_Id);

                string mensajeWsp = string.Empty;
                string sCodigoGruposWathsApp = string.Empty;

                if (informacionReporte!.Success)
                {
                    int?    _repId          = 0;
                    string? _planta         = string.Empty;
                    string? _area           = string.Empty;
                    string? _reportadoPor   = string.Empty;
                    string? _responsable    = string.Empty;

                    //RECORRER LISTADO Y ASIGNAR VALORES A LAS VARIABLES
                    foreach (var item in informacionReporte.Elements!)
                    {
                        _repId          = item.Rep_Id;
                        _planta         = item.Des_Planta;
                        _area           = item.Are_Des;
                        _reportadoPor   = item.Rep_RepPor;
                        _responsable    = item.Resp_Nom;
                    }

                    //GENERAR MENSAJE EN VARIABLE
                    mensajeWsp = @"🚨 *¡Se acaba de crear un registro de No Conformidad!* \\n *Numero*: " + Convert.ToString(_repId) + @"\\n *Planta*: " + _planta + @"\\n *Area*: " + _area +
                                 @"\\n *Reportado Por*: " + _reportadoPor + @"\\n *Responsable*: " + _responsable;

                    string? value = parametros.Cod_Planta_Tg == "4" ? "998" : "999";

                    //ASIGNAR CODIGO DE GRUPO 
                    sCodigoGruposWathsApp = _configuration.GetSection("WaliChat").GetValue<string>(value)!;

                    //ENVIAR MENSAJE
                    var body = await _waliChatService.EnviarMensajeAsync(sCodigoGruposWathsApp, mensajeWsp);
                }


                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }
        }


        [HttpGet]
        [Route("getListarPlantas")]
        public async Task<IActionResult> getListarPlantas()
        {
            var result = await _txReporteNCService.ListarPlantas();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarClasificaciones")]
        public async Task<IActionResult> getListarClasificaciones()
        {
            var result = await _txReporteNCService.ListarClasificaciones();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarEstado")]
        public async Task<IActionResult> patchActualizarEstado([FromBody] Tx_ReporteNCParameter parametros)
        {
            Tx_ReporteNC _txReporteNC = new Tx_ReporteNC
            {
                Rep_Id      = parametros.Rep_Id,
                Rep_Est     = parametros.Rep_Est

            };

            var result = await _txReporteNCService.ActualizarEstado(_txReporteNC);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarDatosResolvedor")]
        public async Task<IActionResult> getListarDatosResolvedor(int Rep_ID)
        {
            var result = await _txReporteNCService.ListarDatosResolvedor(Rep_ID);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarReporteNC")]
        public async Task<IActionResult> patchActualizarReporteNC([FromBody] Tx_ReporteNCParameter parametros)
        {
            Tx_ReporteNC _txReporteNC = new Tx_ReporteNC
            {
                Rep_Id                  = parametros.Rep_Id,
                Rep_Aceptado            = parametros.Rep_Aceptado,
                Rep_Resp_Levantamiento  = parametros.Rep_Resp_Levantamiento,
                Rep_AccCor_Tom          = parametros.Rep_AccCor_Tom,
                Rep_FecSub              = parametros.Rep_FecSub,
                Rep_Est                 = parametros.Rep_Est,
                Rep_DetObs              = parametros.Rep_DetObs,
                Imagenes                = parametros.Imagenes,
                imgnombre               = parametros.imgnombre,
                Img_Fam                 = parametros.Img_Fam

            };

            var result = await _txReporteNCService.ActualizarReporteNC(_txReporteNC);
            if (!result.Success)
            {
                result.CodeResult = StatusCodes.Status400BadRequest;
                return BadRequest(result);
            }
            else
            {
                int rep_Id      = parametros.Rep_Id ?? 0;
                int img_Fam     = parametros.Img_Fam ?? 0;
                var nombres     = parametros.imgnombre.Split(',');
                var imagenes    = _txReporteNC.Imagenes.Split('|');

                for (int i = 0; i < nombres.Length; i++)
                {
                    //RECORREMOS EL ARREGLO DE NOMBRES Y OBTENEMOS TANTO EL NOMBRE DE LA IMAGEN COMO SU BASE64
                    var nombre = nombres[i];
                    var base64 = imagenes[i];

                    var imagenEnBDAEliminar = await _txReporteNCService.EliminarImagenParaElPatch(nombre);

                    int index = nombre.IndexOf("_");

                    if (index != -1 && index < nombre.Length - 1)
                    {
                        nombre = nombre.Substring(index + 1);
                    }

                    var nombreSinEspacio    = nombre.Replace(" ", "_");
                    var bytes               = Convert.FromBase64String(base64);

                    //DEFINIMOS LA RUTA, LA RUTA TIENE QUE EXISTIR
                    string rutaBase = @"D:\htdocs\app\foto";
                    Directory.CreateDirectory(rutaBase);

                    nombreSinEspacio    = $"{Guid.NewGuid()}_{nombreSinEspacio}";
                    var rutaArchivo     = Path.Combine(rutaBase, nombreSinEspacio);

                    if (System.IO.File.Exists(rutaArchivo))
                    {
                        System.IO.File.Delete(rutaArchivo);
                    }

                    using (var stream = new FileStream(rutaArchivo, FileMode.Create))

                    using (var ms = new MemoryStream(bytes))
                    {
                        await ms.CopyToAsync(stream);
                    }

                    //REGISTRAMOS EL NOMBRE EN LA BD CON RESPECTO AL ID DEL REPORTE
                    //var img = await _txReporteNCService.RegistrarImagendeReporteNC(rep_Id, nombreSinEspacio, img_Fam);

                }

                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }
        }

        [HttpPatch]
        [Route("patchActualizarReporteNCCierre")]
        public async Task<IActionResult> patchActualizarReporteNCCierre([FromBody] Tx_ReporteNCParameter parametros)
        {
            Tx_ReporteNC _txReporteNC = new Tx_ReporteNC
            {
                Rep_Id      = parametros.Rep_Id,
                Rep_Est     = parametros.Rep_Est,
                Rep_DetObs  = parametros.Rep_DetObs,
                Imagenes    = parametros.Imagenes,
                imgnombre   = parametros.imgnombre,
                Img_Fam     = parametros.Img_Fam

            };

            var result = await _txReporteNCService.ActualizarReporteNCCierre(_txReporteNC);
            if (!result.Success)
            {
                result.CodeResult = StatusCodes.Status400BadRequest;
                return BadRequest(result);
            }
            else
            {
                int rep_Id      = parametros.Rep_Id ?? 0;
                int img_Fam     = parametros.Img_Fam ?? 0;
                var nombres     = parametros.imgnombre.Split(',');
                var imagenes    = _txReporteNC.Imagenes.Split('|');

                for (int i = 0; i < nombres.Length; i++)
                {
                    //RECORREMOS EL ARREGLO DE NOMBRES Y OBTENEMOS TANTO EL NOMBRE DE LA IMAGEN COMO SU BASE64
                    var nombre = nombres[i];
                    var base64 = imagenes[i];

                    var imagenEnBDAEliminar = await _txReporteNCService.EliminarImagenParaElPatch(nombre);

                    int index = nombre.IndexOf("_");

                    if (index != -1 && index < nombre.Length - 1)
                    {
                        nombre = nombre.Substring(index + 1);
                    }

                    var nombreSinEspacio    = nombre.Replace(" ", "_");
                    var bytes               = Convert.FromBase64String(base64);

                    //DEFINIMOS LA RUTA, LA RUTA TIENE QUE EXISTIR
                    string rutaBase = @"D:\htdocs\app\foto";
                    Directory.CreateDirectory(rutaBase);

                    nombreSinEspacio    = $"{Guid.NewGuid()}_{nombreSinEspacio}";
                    var rutaArchivo     = Path.Combine(rutaBase, nombreSinEspacio);

                    if (System.IO.File.Exists(rutaArchivo))
                    {
                        System.IO.File.Delete(rutaArchivo);
                    }

                    using (var stream = new FileStream(rutaArchivo, FileMode.Create))

                    using (var ms = new MemoryStream(bytes))
                    {
                        await ms.CopyToAsync(stream);
                    }

                    //REGISTRAMOS EL NOMBRE EN LA BD CON RESPECTO AL ID DEL REPORTE
                    var img = await _txReporteNCService.RegistrarImagendeReporteNC(rep_Id, nombreSinEspacio, img_Fam);

                }

                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }
        }

        [HttpGet]
        [Route("getListarEstados")]
        public async Task<IActionResult> getListarEstados()
        {
            var result = await _txReporteNCService.ListarEstados();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        //ActualizarReporteNCOriginal(Tx_ReporteNC tx_ReporteNC)
        //ACTUALIZAR REPORTE NC ORIGINAL
        [HttpPatch]
        [Route("patchActualizarReporteNCOriginal")]
        public async Task<IActionResult> patchActualizarReporteNCOriginal([FromBody] Tx_ReporteNC parametros)
        {
            Tx_ReporteNC _txReporteNC = new Tx_ReporteNC
            {
                Rep_Id              = parametros.Rep_Id,
                Cod_Planta_Tg       = parametros.Cod_Planta_Tg,
                Are_Id              = parametros.Are_Id,
                Rep_Esp             = parametros.Rep_Esp,
                Rep_Clas            = parametros.Rep_Clas,
                Rep_DesNC           = parametros.Rep_DesNC,
                Rep_NivRgo          = parametros.Rep_NivRgo,
                Rep_AccCor          = parametros.Rep_AccCor,
                Resp_Id             = parametros.Resp_Id,
                Rep_RepPor          = parametros.Rep_RepPor,
                Imagenes            = parametros.Imagenes,
                imgnombre           = parametros.imgnombre,
                Img_Fam             = parametros.Img_Fam
            };

            //var result = await _txReporteNCService.ActualizarReporteNCOriginal(parametros);
            //if (result.Success)
            //{
            //    result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
            //    return Ok(result);
            //}

            //result.CodeResult = StatusCodes.Status400BadRequest;
            //return BadRequest(result);


            var result = await _txReporteNCService.ActualizarReporteNCOriginal(_txReporteNC);
            if (!result.Success)
            {
                result.CodeResult = StatusCodes.Status400BadRequest;
                return BadRequest(result);
            }
            else
            {
                int rep_Id      = parametros.Rep_Id ?? 0;
                int img_Fam     = parametros.Img_Fam ?? 0;
                var nombres     = parametros.imgnombre.Split(',');
                var imagenes    = _txReporteNC.Imagenes.Split('|');

                for (int i = 0; i < nombres.Length; i++)
                {
                    //RECORREMOS EL ARREGLO DE NOMBRES Y OBTENEMOS TANTO EL NOMBRE DE LA IMAGEN COMO SU BASE64
                    var nombre = nombres[i];
                    var base64 = imagenes[i];

                    var imagenEnBDAEliminar = await _txReporteNCService.EliminarImagenParaElPatch(nombre);
                    
                    int index = nombre.IndexOf("_");

                    if (index != -1 && index < nombre.Length - 1)
                    {
                        nombre = nombre.Substring(index + 1);
                    }
                    //nombre = nombre.Substring(nombre.IndexOf('_', 2));

                    var nombreSinEspacio    = nombre.Replace(" ", "_");
                    var bytes               = Convert.FromBase64String(base64);

                    //DEFINIMOS LA RUTA, LA RUTA TIENE QUE EXISTIR
                    string rutaBase = @"D:\htdocs\app\foto";
                    Directory.CreateDirectory(rutaBase);

                    nombreSinEspacio        = $"{Guid.NewGuid()}_{nombreSinEspacio}";
                    var rutaArchivo         = Path.Combine(rutaBase, nombreSinEspacio);

                    if (System.IO.File.Exists(rutaArchivo))
                    {
                        System.IO.File.Delete(rutaArchivo);
                    }

                    using (var stream = new FileStream(rutaArchivo, FileMode.Create))

                    using (var ms = new MemoryStream(bytes))
                    {
                        await ms.CopyToAsync(stream);
                    }

                    //REGISTRAMOS EL NOMBRE EN LA BD CON RESPECTO AL ID DEL REPORTE
                    var img = await _txReporteNCService.RegistrarImagendeReporteNC(rep_Id, nombreSinEspacio, img_Fam);

                }

                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }
        }

        //METODO BUSCAR
        [HttpGet]
        [Route("getBuscarRegistros")]
        public async Task<IActionResult> getBuscarRegistros(int Num_Planta, int Are_Id, int Resp_Id, int Rep_Niv_Rgo, int Rep_Est)
        {
            var result = await _txReporteNCService.BuscarRegistros(Num_Planta, Are_Id, Resp_Id, Rep_Niv_Rgo, Rep_Est);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        //LISTAR RIESGOS
        [HttpGet]
        [Route("getListarRiesgos")]
        public async Task<IActionResult> getListarRiesgos()
        {
            var result = await _txReporteNCService.ListarRiesgos();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }


        /*IMAGENES*/

        [HttpGet]
        [Route("getObtenerImagenes")]
        public async Task<IActionResult> getObtenerImagenes(int Rep_Id, int Img_Fam)
        {
            var result = await _txReporteNCService.ObtenerImagenes(Rep_Id, Img_Fam);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getImagenDesdeBackEnd")]
        public IActionResult GetImage(string imageId)
        {
            //var path = Path.Combine(@"\\fileserverprx\imagenesretiro$\", imageId);
            var path        = Path.Combine(@"D:\htdocs\app\foto\", imageId);
            if (!System.IO.File.Exists(path)) return NotFound();
            var mime        = "image/jpeg";
            var bytes       = System.IO.File.ReadAllBytes(path);
            return File(bytes, mime);
        }

        [HttpDelete]
        [Route("deleteEliminarImagenes")]
        public async Task<IActionResult> deleteEliminarImagenes(int Img_Id)
        {
            var result = await _txReporteNCService.EliminarImagenes(Img_Id);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        /*AREAS*/
        [HttpGet]
        [Route("getObtenerAreas")]
        public async Task<IActionResult> getObtenerAreas(int Are_Id)
        {
            var result = await _txReporteNCService.ObtenerAreas(Are_Id);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarArea")]
        public async Task<IActionResult> postRegistrarArea([FromBody] Tx_ReportesNC_Areas parametros)
        {
            Tx_ReportesNC_Areas _txReportesNC_Areas = new Tx_ReportesNC_Areas
            {
                Are_Des     = parametros.Are_Des,
                Num_Planta  = parametros.Num_Planta
            };

            var result = await _txReporteNCService.RegistrarArea(_txReportesNC_Areas);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarArea")]
        public async Task<IActionResult> patchActualizarArea([FromBody] Tx_ReportesNC_Areas parametros)
        {
            Tx_ReportesNC_Areas _txReportesNC_Areas = new Tx_ReportesNC_Areas
            {
                Are_Id      = parametros.Are_Id,
                Are_Des     = parametros.Are_Des,
                Num_Planta  = parametros.Num_Planta
            };

            var result = await _txReporteNCService.ActualizarArea(_txReportesNC_Areas);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpDelete]
        [Route("deleteEliminarArea")]
        public async Task<IActionResult> deleteEliminarArea(int Are_Id)
        {
            var result = await _txReporteNCService.EliminarArea(Are_Id);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtenerAreaXSede")]
        public async Task<IActionResult> getObtenerAreaXSede(int Num_Planta, int Are_Id)
        {
            var result = await _txReporteNCService.ObtenerAreaXSede(Num_Planta, Are_Id);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        /*RESPONSABLES*/
        [HttpGet]
        [Route("getObtenerResponsables")]
        public async Task<IActionResult> getObtenerResponsables(int Resp_Id)
        {
            var result = await _txReporteNCService.ObtenerResponsables(Resp_Id);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarResponsable")]
        public async Task<IActionResult> postRegistrarResponsable([FromBody] Tx_ReportesNC_Responsables parametros)
        {
            Tx_ReportesNC_Responsables _txReportesNC_Responsables = new Tx_ReportesNC_Responsables
            {
                Resp_Nom        = parametros.Resp_Nom,
                Resp_Ape_Pat    = parametros.Resp_Ape_Pat,
                Resp_Ape_Mat    = parametros.Resp_Ape_Mat,
                Resp_Correo     = parametros.Resp_Correo
            };

            var result = await _txReporteNCService.RegistrarResponsable(_txReportesNC_Responsables);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarResponsable")]
        public async Task<IActionResult> patchActualizarResponsable([FromBody] Tx_ReportesNC_Responsables parametros)
        {
            Tx_ReportesNC_Responsables _txReportesNC_Areas = new Tx_ReportesNC_Responsables
            {
                Resp_Id         = parametros.Resp_Id,
                Resp_Nom        = parametros.Resp_Nom,
                Resp_Ape_Pat    = parametros.Resp_Ape_Pat,
                Resp_Ape_Mat    = parametros.Resp_Ape_Mat,
                Resp_Correo     = parametros.Resp_Correo
            };

            var result = await _txReporteNCService.ActualizarResponsable(_txReportesNC_Areas);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpDelete]
        [Route("deleteEliminarResponsable")]
        public async Task<IActionResult> deleteEliminarResponsable(int Resp_Id)
        {
            var result = await _txReporteNCService.EliminarResponsable(Resp_Id);
            if (result.Success)
            {
                result.CodeResult = result.CodeTransacc == 1 ? StatusCodes.Status200OK : StatusCodes.Status201Created;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        /*USUARIOS*/
        [HttpGet]
        [Route("getObtenerUsuarios")]
        public async Task<IActionResult> getObtenerUsuarios(string Usr_Cod)
        {
            var result = await _txReporteNCService.ObtenerUsuarios(Usr_Cod);
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

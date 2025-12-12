using ic.backend.precotex.web.Data.Repositories.Implementation.ReporteNC;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.ReporteNC;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.ReporteNC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.ReporteNC
{
    public class TxReporteNCService: ITxReporteNCService
    {
        private readonly ITxReporteNCRepository _txReporteNCRepository;

        public TxReporteNCService(ITxReporteNCRepository txReporteNCRepository)
        {
            _txReporteNCRepository = txReporteNCRepository;
        }

        //LISTAR REGISTROS
        public async Task<ServiceResponseList<Tx_ReporteNC>?> ListarRegistro(int Rep_ID)
        {
            var result = new ServiceResponseList<Tx_ReporteNC>();
            try
            {
                var resultData = await _txReporteNCRepository.ListarRegistro(Rep_ID);
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No existe información";
                }
                result.Success = true;
                result.Message = "Completado con éxito";
                result.Elements = resultData.ToList();
                result.TotalElements = resultData.ToList().Count();
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Excepción no controlada " + ex.Message;
                return result;
            }
        }
        //REGISTRAR REPORTE NC
        public async Task<ServiceResponse<int>> RegistrarReporteNC(Tx_ReporteNC tx_ReporteNC)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txReporteNCRepository.RegistrarReporteNC(tx_ReporteNC);
                if (resultData.Codigo > 0)
                {
                    result.Success = true;
                    result.Message = resultData.Mensaje;
                    result.CodeTransacc = resultData.Codigo;
                    return result;
                }
                result.Success = false;
                result.Message = resultData.Mensaje;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error inesperado " + ex.Message;
                return result;
            }
        }

        //LISTAR PLANTAS
        public async Task<ServiceResponseList<Sg_Planta>?> ListarPlantas()
        {
            var result = new ServiceResponseList<Sg_Planta>();
            try
            {
                var resultData = await _txReporteNCRepository.ListarPlantas();
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No existe información";
                }
                result.Success = true;
                result.Message = "Completado con éxito";
                result.Elements = resultData.ToList();
                result.TotalElements = resultData.ToList().Count();
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Excepción no controlada " + ex.Message;
                return result;
            }
        }

        //LISTAR CLASIFICACIONES
        public async Task<ServiceResponseList<Tx_ReportesNC_Clasificacion>?> ListarClasificaciones()
        {
            var result = new ServiceResponseList<Tx_ReportesNC_Clasificacion>();
            try
            {
                var resultData = await _txReporteNCRepository.ListarClasificaciones();
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No existe información";
                }
                result.Success = true;
                result.Message = "Completado con éxito";
                result.Elements = resultData.ToList();
                result.TotalElements = resultData.ToList().Count();
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Excepción no controlada " + ex.Message;
                return result;
            }
        }

        //ACTUALIZAR ESTADO
        public async Task<ServiceResponse<int>> ActualizarEstado(Tx_ReporteNC tx_ReporteNC)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txReporteNCRepository.ActualizarEstado(tx_ReporteNC);
                if (resultData.Codigo > 0)
                {
                    result.Success = true;
                    result.Message = resultData.Mensaje;
                    result.CodeTransacc = resultData.Codigo;
                    return result;
                }
                result.Success = false;
                result.Message = resultData.Mensaje;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error inesperado " + ex.Message;
                return result;
            }
        }

        //CARGAR DATOS RESOLVEDOR
        public async Task<ServiceResponseList<Tx_ReporteNC>?> ListarDatosResolvedor(int Rep_ID)
        {
            var result = new ServiceResponseList<Tx_ReporteNC>();
            try
            {
                var resultData = await _txReporteNCRepository.ListarDatosResolvedor(Rep_ID);
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No existe información";
                }
                result.Success = true;
                result.Message = "Completado con éxito";
                result.Elements = resultData.ToList();
                result.TotalElements = resultData.ToList().Count();
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Excepción no controlada " + ex.Message;
                return result;
            }
        }

        //ACTUALIZAR REPORTE - PERSPECTIVA RESOLVEDOR
        public async Task<ServiceResponse<int>> ActualizarReporteNC(Tx_ReporteNC tx_ReporteNC)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txReporteNCRepository.ActualizarReporteNC(tx_ReporteNC);
                if (resultData.Codigo > 0)
                {
                    result.Success = true;
                    result.Message = resultData.Mensaje;
                    result.CodeTransacc = resultData.Codigo;
                    return result;
                }
                result.Success = false;
                result.Message = resultData.Mensaje;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error inesperado " + ex.Message;
                return result;
            }
        }

        public async Task<ServiceResponse<int>> ActualizarReporteNCCierre(Tx_ReporteNC tx_ReporteNC)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txReporteNCRepository.ActualizarReporteNCCierre(tx_ReporteNC);
                if (resultData.Codigo > 0)
                {
                    result.Success = true;
                    result.Message = resultData.Mensaje;
                    result.CodeTransacc = resultData.Codigo;
                    return result;
                }
                result.Success = false;
                result.Message = resultData.Mensaje;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error inesperado " + ex.Message;
                return result;
            }
        }

        //LISTAR ESTADOS
        public async Task<ServiceResponseList<Tx_ReportesNC_Estados>?> ListarEstados()
        {
            var result = new ServiceResponseList<Tx_ReportesNC_Estados>();
            try
            {
                var resultData = await _txReporteNCRepository.ListarEstados();
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No existe información";
                }
                result.Success = true;
                result.Message = "Completado con éxito";
                result.Elements = resultData.ToList();
                result.TotalElements = resultData.ToList().Count();
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Excepción no controlada " + ex.Message;
                return result;
            }
        }

        //ACTUALIZAR REPORTE ORIGINAL
        public async Task<ServiceResponse<int>> ActualizarReporteNCOriginal(Tx_ReporteNC tx_ReporteNC)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txReporteNCRepository.ActualizarReporteNCOriginal(tx_ReporteNC);
                if (resultData.Codigo > 0)
                {
                    result.Success = true;
                    result.Message = resultData.Mensaje;
                    result.CodeTransacc = resultData.Codigo;
                    return result;
                }
                result.Success = false;
                result.Message = resultData.Mensaje;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error inesperado " + ex.Message;
                return result;
            }
        }

        //METODO BUSCAR
        public async Task<ServiceResponseList<Tx_ReporteNC>?> BuscarRegistros(int Num_Planta, int Are_Id, int Resp_Id, int Rep_Niv_Rgo, int Rep_Est)
        {
            var result = new ServiceResponseList<Tx_ReporteNC>();
            try
            {
                var resultData = await _txReporteNCRepository.BuscarRegistros(Num_Planta, Are_Id, Resp_Id, Rep_Niv_Rgo, Rep_Est);
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No existe información";
                }
                result.Success = true;
                result.Message = "Completado con éxito";
                result.Elements = resultData.ToList();
                result.TotalElements = resultData.ToList().Count();
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Excepción no controlada " + ex.Message;
                return result;
            }
        }

        //LISTAR RIESGOS
        public async Task<ServiceResponseList<Tx_ReportesNC_Riesgos>?> ListarRiesgos()
        {
            var result = new ServiceResponseList<Tx_ReportesNC_Riesgos>();
            try
            {
                var resultData = await _txReporteNCRepository.ListarRiesgos();
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No existe información";
                }
                result.Success = true;
                result.Message = "Completado con éxito";
                result.Elements = resultData.ToList();
                result.TotalElements = resultData.ToList().Count();
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Excepción no controlada " + ex.Message;
                return result;
            }
        }

        /*IMAGENES*/

        //REGISTRAR IMAGEN
        public async Task<ServiceResponse<int>> RegistrarImagendeReporteNC(int Rep_Id, string Img_Des, int Img_Fam)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txReporteNCRepository.RegistrarImagendeReporteNC(Rep_Id, Img_Des, Img_Fam);
                if (resultData.Codigo > 0)
                {
                    result.Success = true;
                    result.Message = resultData.Mensaje;
                    result.CodeTransacc = resultData.Codigo;
                    return result;
                }
                result.Success = false;
                result.Message = resultData.Mensaje;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error inesperado " + ex.Message;
                return result;
            }
        }

        //LISTAR IMAGENES
        public async Task<ServiceResponseList<Tx_ReporteNC_Img>?> ObtenerImagenes(int Rep_Id, int Img_Fam)
        {
            var result = new ServiceResponseList<Tx_ReporteNC_Img>();
            try
            {
                var resultData = await _txReporteNCRepository.ObtenerImagenes(Rep_Id, Img_Fam);
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No existe información";
                }
                result.Success = true;
                result.Message = "Completado con éxito";
                result.Elements = resultData.ToList();
                result.TotalElements = resultData.ToList().Count();
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Excepción no controlada " + ex.Message;
                return result;
            }
        }

        //ELIMINAR IMAGENES
        public async Task<ServiceResponse<int>> EliminarImagenes(int Img_Id)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txReporteNCRepository.EliminarImagenes(Img_Id);
                if (resultData.Codigo > 0)
                {
                    result.Success = true;
                    result.Message = resultData.Mensaje;
                    result.CodeTransacc = resultData.Codigo;
                    return result;
                }
                result.Success = false;
                result.Message = resultData.Mensaje;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error inesperado " + ex.Message;
                return result;
            }
        }
        
        //ELIMINAR IMAGENES PARA EL PATCH
        public async Task<ServiceResponse<int>> EliminarImagenParaElPatch(string Img_Des)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txReporteNCRepository.EliminarImagenParaElPatch(Img_Des);
                if (resultData.Codigo > 0)
                {
                    result.Success = true;
                    result.Message = resultData.Mensaje;
                    result.CodeTransacc = resultData.Codigo;
                    return result;
                }
                result.Success = false;
                result.Message = resultData.Mensaje;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error inesperado " + ex.Message;
                return result;
            }
        }
        /*AREAS*/
        //LISTAR AREAS
        public async Task<ServiceResponseList<Tx_ReportesNC_Areas>?> ObtenerAreas(int Are_Id)
        {
            var result = new ServiceResponseList<Tx_ReportesNC_Areas>();
            try
            {
                var resultData = await _txReporteNCRepository.ObtenerAreas(Are_Id);
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No existe información";
                }
                result.Success = true;
                result.Message = "Completado con éxito";
                result.Elements = resultData.ToList();
                result.TotalElements = resultData.ToList().Count();
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Excepción no controlada " + ex.Message;
                return result;
            }
        }

        //REGISTRAR AREA
        public async Task<ServiceResponse<int>> RegistrarArea(Tx_ReportesNC_Areas _txReportesNCAreas)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txReporteNCRepository.RegistrarArea(_txReportesNCAreas);
                if (resultData.Codigo > 0)
                {
                    result.Success = true;
                    result.Message = resultData.Mensaje;
                    result.CodeTransacc = resultData.Codigo;
                    return result;
                }
                result.Success = false;
                result.Message = resultData.Mensaje;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error inesperado " + ex.Message;
                return result;
            }
        }

        //ACTUALIZAR AREA
        public async Task<ServiceResponse<int>> ActualizarArea(Tx_ReportesNC_Areas _txReportesNCAreas)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txReporteNCRepository.ActualizarArea(_txReportesNCAreas);
                if (resultData.Codigo > 0)
                {
                    result.Success = true;
                    result.Message = resultData.Mensaje;
                    result.CodeTransacc = resultData.Codigo;
                    return result;
                }
                result.Success = false;
                result.Message = resultData.Mensaje;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error inesperado " + ex.Message;
                return result;
            }
        }

        //ELIMINAR AREA
        public async Task<ServiceResponse<int>> EliminarArea(int Are_Id)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txReporteNCRepository.EliminarArea(Are_Id);
                if (resultData.Codigo > 0)
                {
                    result.Success = true;
                    result.Message = resultData.Mensaje;
                    result.CodeTransacc = resultData.Codigo;
                    return result;
                }
                result.Success = false;
                result.Message = resultData.Mensaje;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error inesperado " + ex.Message;
                return result;
            }
        }

        //LISTAR AREA X SEDE
        public async Task<ServiceResponseList<Tx_ReportesNC_Areas>?> ObtenerAreaXSede(int Num_Planta, int Are_Id)
        {
            var result = new ServiceResponseList<Tx_ReportesNC_Areas>();
            try
            {
                var resultData = await _txReporteNCRepository.ObtenerAreaXSede(Num_Planta, Are_Id);
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No existe información";
                }
                result.Success = true;
                result.Message = "Completado con éxito";
                result.Elements = resultData.ToList();
                result.TotalElements = resultData.ToList().Count();
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Excepción no controlada " + ex.Message;
                return result;
            }
        }

        /*RESPONSABLES*/
        //LISTAR RESPONSABLES
        public async Task<ServiceResponseList<Tx_ReportesNC_Responsables>?> ObtenerResponsables(int Resp_Id)
        {
            var result = new ServiceResponseList<Tx_ReportesNC_Responsables>();
            try
            {
                var resultData = await _txReporteNCRepository.ObtenerResponsables(Resp_Id);
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No existe información";
                }
                result.Success = true;
                result.Message = "Completado con éxito";
                result.Elements = resultData.ToList();
                result.TotalElements = resultData.ToList().Count();
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Excepción no controlada " + ex.Message;
                return result;
            }
        }

        //REGISTRAR RESPONSABLES
        public async Task<ServiceResponse<int>> RegistrarResponsable(Tx_ReportesNC_Responsables _txReportesNCResponsables)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txReporteNCRepository.RegistrarResponsable(_txReportesNCResponsables);
                if (resultData.Codigo > 0)
                {
                    result.Success = true;
                    result.Message = resultData.Mensaje;
                    result.CodeTransacc = resultData.Codigo;
                    return result;
                }
                result.Success = false;
                result.Message = resultData.Mensaje;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error inesperado " + ex.Message;
                return result;
            }
        }

        //ACTUALIZAR RESPONSABLES
        public async Task<ServiceResponse<int>> ActualizarResponsable(Tx_ReportesNC_Responsables _txReportesNCResponsables)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txReporteNCRepository.ActualizarResponsable(_txReportesNCResponsables);
                if (resultData.Codigo > 0)
                {
                    result.Success = true;
                    result.Message = resultData.Mensaje;
                    result.CodeTransacc = resultData.Codigo;
                    return result;
                }
                result.Success = false;
                result.Message = resultData.Mensaje;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error inesperado " + ex.Message;
                return result;
            }
        }

        //ELIMINAR RESPONSABLES
        public async Task<ServiceResponse<int>> EliminarResponsable(int Resp_Id)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txReporteNCRepository.EliminarResponsable(Resp_Id);
                if (resultData.Codigo > 0)
                {
                    result.Success = true;
                    result.Message = resultData.Mensaje;
                    result.CodeTransacc = resultData.Codigo;
                    return result;
                }
                result.Success = false;
                result.Message = resultData.Mensaje;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error inesperado " + ex.Message;
                return result;
            }
        }

        /*USUARIOS*/
        //LISTAR REGISTROS
        public async Task<ServiceResponseList<Tx_ReportesNC_Usuarios>?> ObtenerUsuarios(string Usr_Cod)
        {
            var result = new ServiceResponseList<Tx_ReportesNC_Usuarios>();
            try
            {
                var resultData = await _txReporteNCRepository.ObtenerUsuarios(Usr_Cod);
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No existe información";
                }
                result.Success = true;
                result.Message = "Completado con éxito";
                result.Elements = resultData.ToList();
                result.TotalElements = resultData.ToList().Count();
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Excepción no controlada " + ex.Message;
                return result;
            }
        }

        /*MENSAJES WSP*/
        //OBTENER DATOS PARA ENVIAR MENSAJE
        public async Task<ServiceResponseList<Tx_ReporteNC>?> ObtenerDatosRegistro(int Rep_Id)
        {
            var result = new ServiceResponseList<Tx_ReporteNC>();
            try
            {
                var resultData = await _txReporteNCRepository.ObtenerDatosRegistro(Rep_Id);
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No existe información";
                }
                result.Success = true;
                result.Message = "Completado con éxito";
                result.Elements = resultData.ToList();
                result.TotalElements = resultData.ToList().Count();
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Excepción no controlada " + ex.Message;
                return result;
            }
        }

    }
}

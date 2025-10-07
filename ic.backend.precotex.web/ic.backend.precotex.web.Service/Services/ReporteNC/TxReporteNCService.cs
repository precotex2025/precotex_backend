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

    }
}

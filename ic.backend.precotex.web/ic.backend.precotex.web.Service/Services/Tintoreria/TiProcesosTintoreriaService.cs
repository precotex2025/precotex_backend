using ic.backend.precotex.web.Data.Repositories.Almacen;
using ic.backend.precotex.web.Data.Repositories.Implementation.Almacen;
using ic.backend.precotex.web.Data.Repositories.Implementation.Tintoreria;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.Tintoreria;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Tintoreria
{
    public class TiProcesosTintoreriaService : ITiProcesosTintoreriaService
    {
        private readonly ITiProcesosTintoreriaRepository _tiProcesosTintoreriaRepository;
        public TiProcesosTintoreriaService(ITiProcesosTintoreriaRepository tiProcesosTintoreriaRepository)
        {
            _tiProcesosTintoreriaRepository = tiProcesosTintoreriaRepository;
        }

        public async Task<ServiceResponseList<Tx_Muestra_Control_Proceso>?> ListaControlProcesosTintoreria(string Cod_Ordtra, DateTime? Fecha_Ini, DateTime? Fecha_Fin)
        {
            var result = new ServiceResponseList<Tx_Muestra_Control_Proceso>();
            try
            {
                var resultData = await _tiProcesosTintoreriaRepository.ListaControlProcesosTintoreria(Cod_Ordtra, Fecha_Ini, Fecha_Fin);
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No existe información";
                    return result;
                }

                result.Success = true;
                result.Elements = resultData.ToList();
                result.TotalElements = resultData.ToList().Count();
                return result;
            }
            catch (SqlException sql)
            {
                result.Message = "Error en Servidor: " + sql.Message;
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Ocurrio una excepción" + ex.Message;
                return result;
            }
        }

        public async Task<ServiceResponseList<Ti_Seguimiento_Tobera>?> ListaDetalleToberaPruebaTenido(string Cod_Ordtra, string IdOrgatexUnico)
        {
            var result = new ServiceResponseList<Ti_Seguimiento_Tobera>();
            try
            {
                var resultData = await _tiProcesosTintoreriaRepository.ListaDetalleToberaPruebaTenido(Cod_Ordtra, IdOrgatexUnico);
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No existe información";
                    return result;
                }

                result.Success = true;
                result.Elements = resultData.ToList();
                result.TotalElements = resultData.ToList().Count();
                return result;
            }
            catch (SqlException sql)
            {
                result.Message = "Error en Servidor: " + sql.Message;
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Ocurrio una excepción" + ex.Message;
                return result;
            }
        }

        public async Task<ServiceResponseList<Ti_Procesos_Tintoreria>?> ListaEstatusControlTenido(string Cod_Ordtra, DateTime? Fecha_Ini, DateTime? FechaFin, string Cod_Usuario)
        {
            var result = new ServiceResponseList<Ti_Procesos_Tintoreria>();
            try
            {
                var resultData = await _tiProcesosTintoreriaRepository.ListaEstatusControlTenido(Cod_Ordtra, Fecha_Ini, FechaFin, Cod_Usuario);
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No existe información";
                    return result;
                }

                result.Success = true;
                result.Elements = resultData.ToList();
                result.TotalElements = resultData.ToList().Count();
                return result;
            }
            catch (SqlException sql)
            {
                result.Message = "Error en Servidor: " + sql.Message;
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Ocurrio una excepción" + ex.Message;
                return result;
            }
        }
    }
}

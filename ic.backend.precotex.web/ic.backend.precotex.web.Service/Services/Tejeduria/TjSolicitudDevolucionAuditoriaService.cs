using ic.backend.precotex.web.Data.Repositories.Implementation.Tejeduria;
using ic.backend.precotex.web.Data.Repositories.Tejeduria;
using ic.backend.precotex.web.Entity.Entities.Tejeduria;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.Tejeduria;
using iTextSharp.text.pdf.codec.wmf;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Tejeduria
{
    public class TjSolicitudDevolucionAuditoriaService : ITjSolicitudDevolucionAuditoriaService
    {
        private readonly ITjSolicitudDevolucionAuditoriaRepository _tjSolicitudDevolucionAuditoriaRepository;
        public TjSolicitudDevolucionAuditoriaService(ITjSolicitudDevolucionAuditoriaRepository tjSolicitudDevolucionAuditoriaRepository)
        {
            _tjSolicitudDevolucionAuditoriaRepository = tjSolicitudDevolucionAuditoriaRepository;
        }

        public async Task<ServiceResponseList<Tj_Muestra_Solicitud_Devolucion>?> ListaSolicitudDevolucion(int NumSolicitud, string Lote, DateTime Fecha_Ini, DateTime Fecha_Fin, string Estado)
        {
            var result = new ServiceResponseList<Tj_Muestra_Solicitud_Devolucion>();
            try
            {
                var resultData = await _tjSolicitudDevolucionAuditoriaRepository.ListaSolicitudDevolucion(NumSolicitud, Lote, Fecha_Ini, Fecha_Fin, Estado);
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

        public async Task<ServiceResponseList<Tj_Muestra_Solicitud_Devolucion_Bultos>?> ListaSolicitudDevolucionBultos(int NumSolicitud, string Lote, string Semana, string Color, string Marca, string Conera)
        {
            var result = new ServiceResponseList<Tj_Muestra_Solicitud_Devolucion_Bultos>();
            try
            {
                var resultData = await _tjSolicitudDevolucionAuditoriaRepository.ListaSolicitudDevolucionBultos(NumSolicitud, Lote, Semana, Color, Marca, Conera);
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

        public async Task<ServiceResponse<int>> Proceso(Tj_Mantenimiento_Solicitud_Devolucion Man_SolicitudDevolucion, string sTipoTransac)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _tjSolicitudDevolucionAuditoriaRepository.Proceso(Man_SolicitudDevolucion, sTipoTransac);
                if (resultData.Codigo > 0)
                {
                    result.Message = resultData.Mensaje;
                    result.Success = true;
                    result.CodeTransacc = resultData.Codigo;

                    return result;
                }

                result.Message = resultData.Mensaje;
                result.Success = false;
                return result;

            }
            catch (SqlException sql)
            {
                result.Message = "Error en Servidor: " + sql.Message;
                result.Success = false;
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Ocurrio una excepción" + ex.Message;
                result.Success = false;
                return result;
            }
        }
    }
}

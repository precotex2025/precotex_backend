using ic.backend.precotex.web.Data.Repositories.Implementation.SolicitudMantenimiento;
using ic.backend.precotex.web.Entity.Entities.Memorandum;
using ic.backend.precotex.web.Entity.Entities.SolicitudMantenimiento;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.SolicitudMantenimiento;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.SolicitudMantenimiento
{
    public class TMSolicitudMantenimientoService : ITMSolicitudMantenimientoService
    {
        private readonly ITMSolicitudMantenimientoRepository _tMSolicitudMantenimientoRepository;
        public TMSolicitudMantenimientoService(ITMSolicitudMantenimientoRepository tMSolicitudMantenimientoRepository)
        {
            _tMSolicitudMantenimientoRepository = tMSolicitudMantenimientoRepository;
        }

        public async Task<ServiceResponseList<TM_Maquina>?> ObtieneInformacionMaquinas(string sCodMaquina)
        {
            var result = new ServiceResponseList<TM_Maquina>();
            try
            {
                var resultData = await _tMSolicitudMantenimientoRepository.ObtieneInformacionMaquinas(sCodMaquina);
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

        public async Task<ServiceResponseList<TM_Solicitud_Mantenimiento>?> ObtieneInformacionSolicitudMantenimiento(DateTime FecIni, DateTime FecFin, string codUsuario)
        {
            var result = new ServiceResponseList<TM_Solicitud_Mantenimiento>();
            try
            {
                var resultData = await _tMSolicitudMantenimientoRepository.ObtieneInformacionSolicitudMantenimiento(FecIni, FecFin, codUsuario);
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

        public async Task<ServiceResponse<int>> ProcesoMntoSolicitudMantenimiento(TM_Solicitud_Mantenimiento tM_Solicitud_Mantenimiento, string sTipoTransac)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _tMSolicitudMantenimientoRepository.ProcesoMntoSolicitudMantenimiento(tM_Solicitud_Mantenimiento, sTipoTransac);
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

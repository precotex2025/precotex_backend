using ic.backend.precotex.web.Data.Repositories.Implementation.SecureNorm;
using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.SecureNorm;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.SecureNorm
{
    public class SNAuditoriaService : ISNAuditoriaService
    {
        private readonly ISNAuditoriaRepository _sNAuditoriaRepository;

        public SNAuditoriaService(ISNAuditoriaRepository sNAuditoriaRepository)
        {
            _sNAuditoriaRepository = sNAuditoriaRepository;
        }

        public async Task<ServiceResponseList<SN_Auditoria>?> Listado(string sFiltro)
        {
            var result = new ServiceResponseList<SN_Auditoria>();
            try
            {
                var resultData = await _sNAuditoriaRepository.Listado(sFiltro);
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No existe información";
                    result.Elements = new System.Collections.Generic.List<SN_Auditoria>();
                    result.TotalElements = 0;
                    return result;
                }

                result.Success = true;
                result.Elements = resultData.ToList();
                result.TotalElements = resultData.Count();
                return result;
            }
            catch (SqlException sql)
            {
                result.Message = "Error en Servidor: " + sql.Message;
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Ocurrio una excepción: " + ex.Message;
                return result;
            }
        }

        public async Task<ServiceResponse<int>> ProcesoMnto(SN_Auditoria sN_Auditoria, string sTipoTransac)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _sNAuditoriaRepository.ProcesoMnto(sN_Auditoria, sTipoTransac);

                if (resultData.Codigo == 1)
                {
                    result.Success = true;
                    result.CodeTransacc = resultData.Codigo;
                    result.Message = resultData.Mensaje;
                    return result;
                }

                result.Success = false;
                result.CodeTransacc = resultData.Codigo;
                result.Message = resultData.Mensaje;
                return result;
            }
            catch (SqlException sql)
            {
                result.Message = "Error en Servidor: " + sql.Message;
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Ocurrio una excepción: " + ex.Message;
                return result;
            }
        }
    }
}

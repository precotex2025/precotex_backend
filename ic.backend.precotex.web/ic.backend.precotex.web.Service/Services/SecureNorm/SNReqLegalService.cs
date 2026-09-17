using ic.backend.precotex.web.Data.Repositories.Implementation.SecureNorm;
using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.SecureNorm;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.SecureNorm
{
    public class SNReqLegalService : ISNReqLegalService
    {
        private readonly ISNReqLegalRepository _sNReqLegalRepository;

        public SNReqLegalService(ISNReqLegalRepository sNReqLegalRepository)
        {
            _sNReqLegalRepository = sNReqLegalRepository;
        }

        public async Task<ServiceResponseList<SN_Req_Legal>?> Listado(string sFiltro)
        {
            var result = new ServiceResponseList<SN_Req_Legal>();
            try
            {
                var resultData = await _sNReqLegalRepository.Listado(sFiltro);
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.CodeResult = 200;
                    result.Message = "No existe informaciÃ³n";
                    result.Elements = new List<SN_Req_Legal>();
                    result.TotalElements = 0;
                    return result;
                }

                var list = resultData.ToList();
                result.Success = true;
                result.CodeResult = 200;
                result.Elements = list;
                result.TotalElements = list.Count;
                result.Message = "Listado de requisitos legales obtenido exitosamente.";
                return result;
            }
            catch (SqlException sql)
            {
                result.Success = false;
                result.CodeResult = 500;
                result.Message = "Error en Servidor: " + sql.Message;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.CodeResult = 500;
                result.Message = "OcurriÃ³ una excepciÃ³n: " + ex.Message;
                return result;
            }
        }

        public async Task<ServiceResponse<int>> Mnto(SN_Req_Legal sN_Req_Legal, string sTipoTransac)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _sNReqLegalRepository.Mnto(sN_Req_Legal, sTipoTransac);
                if (resultData.Codigo > 0)
                {
                    result.Message = resultData.Mensaje;
                    result.Success = true;
                    result.CodeResult = 200;
                    result.CodeTransacc = resultData.Codigo;
                    return result;
                }

                result.Message = resultData.Mensaje;
                result.Success = false;
                result.CodeResult = 400;
                return result;
            }
            catch (SqlException sql)
            {
                result.Message = "Error en Servidor: " + sql.Message;
                result.Success = false;
                result.CodeResult = 500;
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "OcurriÃ³ una excepciÃ³n: " + ex.Message;
                result.Success = false;
                result.CodeResult = 500;
                return result;
            }
        }
    }
}

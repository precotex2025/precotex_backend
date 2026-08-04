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
    public class SNMejoraService : ISNMejoraService
    {
        private readonly ISNMejoraRepository _sNMejoraRepository;

        public SNMejoraService(ISNMejoraRepository sNMejoraRepository)
        {
            _sNMejoraRepository = sNMejoraRepository;
        }

        public async Task<ServiceResponseList<SN_Mejora>?> Listado(string sFiltro)
        {
            var result = new ServiceResponseList<SN_Mejora>();
            try
            {
                var resultData = await _sNMejoraRepository.Listado(sFiltro);
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
                result.Message = "Ocurrio una excepción: " + ex.Message;
                return result;
            }
        }

        public async Task<ServiceResponse<int>> Mnto(SN_Mejora sN_Mejora, string sTipoTransac)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _sNMejoraRepository.Mnto(sN_Mejora, sTipoTransac);
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
                result.Message = "Ocurrio una excepción: " + ex.Message;
                result.Success = false;
                return result;
            }
        }
    }
}

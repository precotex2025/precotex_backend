using ic.backend.precotex.web.Data.Repositories.Implementation.Memorandum;
using ic.backend.precotex.web.Data.Repositories.Implementation.SecureNorm;
using ic.backend.precotex.web.Data.Repositories.Memorandum;
using ic.backend.precotex.web.Entity.Entities.Memorandum;
using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.SecureNorm;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.SecureNorm
{
    public class SNNormaService : ISNNormaService
    {
        private readonly ISNNormaRepository _sNNormaRepository;

        public SNNormaService(ISNNormaRepository sNNormaRepository)
        {
            _sNNormaRepository = sNNormaRepository;
        }

        public async Task<ServiceResponseList<SN_Norma>?> Listado(string sEstado)
        {
            var result = new ServiceResponseList<SN_Norma>();
            try
            {
                var resultData = await _sNNormaRepository.Listado(sEstado);
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

        public async Task<ServiceResponse<int>> ProcesoMnto(SN_Norma sN_Norma, string sTipoTransac)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _sNNormaRepository.ProcesoMnto(sN_Norma, sTipoTransac);
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

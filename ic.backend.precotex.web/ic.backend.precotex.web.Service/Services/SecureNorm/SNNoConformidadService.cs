using ic.backend.precotex.web.Data.Repositories.Implementation.SecureNorm;
using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Service.common;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.SecureNorm
{
    public class SNNoConformidadService : ISNNoConformidadService
    {
        private readonly ISNNoConformidadRepository _repository;

        public SNNoConformidadService(ISNNoConformidadRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponseList<SN_No_Conformidad>?> Listado(string sFiltro)
        {
            var result = new ServiceResponseList<SN_No_Conformidad>();
            try
            {
                var resultData = await _repository.Listado(sFiltro);
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No existe información";
                    result.Elements = new System.Collections.Generic.List<SN_No_Conformidad>();
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

        public async Task<ServiceResponse<int>> ProcesoMnto(SN_No_Conformidad sN_No_Conformidad, string sTipoTransac)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _repository.ProcesoMnto(sN_No_Conformidad, sTipoTransac);

                if (resultData.Codigo == 1)
                {
                    result.Success = true;
                    result.Message = resultData.Mensaje;
                    result.CodeTransacc = 1;
                }
                else
                {
                    result.Success = false;
                    result.Message = resultData.Mensaje;
                    result.CodeTransacc = 0;
                }
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

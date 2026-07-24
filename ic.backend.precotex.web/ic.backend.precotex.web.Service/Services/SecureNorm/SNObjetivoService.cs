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
    public class SNObjetivoService : ISNObjetivoService
    {
        private readonly ISNObjetivoRepository _sNObjetivoRepository;

        public SNObjetivoService(ISNObjetivoRepository sNObjetivoRepository)
        {
            _sNObjetivoRepository = sNObjetivoRepository;
        }

        public async Task<ServiceResponseList<SN_Objetivo>?> Listado(string sFiltro)
        {
            var result = new ServiceResponseList<SN_Objetivo>();
            try
            {
                var resultData = await _sNObjetivoRepository.Listado(sFiltro);
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No existe información";
                    result.Elements = new System.Collections.Generic.List<SN_Objetivo>();
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

        public async Task<ServiceResponse<int>> Mnto(SN_Objetivo objetivo, string sTipoTransac)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _sNObjetivoRepository.Mnto(objetivo, sTipoTransac);

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

        public async Task<ServiceResponseList<SN_Objetivo_Medicion>?> ListadoMediciones(int? idObjetivo, string sFiltro)
        {
            var result = new ServiceResponseList<SN_Objetivo_Medicion>();
            try
            {
                var resultData = await _sNObjetivoRepository.ListadoMediciones(idObjetivo, sFiltro);
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No existe información";
                    result.Elements = new System.Collections.Generic.List<SN_Objetivo_Medicion>();
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

        public async Task<ServiceResponse<int>> MntoMedicion(SN_Objetivo_Medicion medicion, string sTipoTransac)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _sNObjetivoRepository.MntoMedicion(medicion, sTipoTransac);

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

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
    public class SNIndicadorService : ISNIndicadorService
    {
        private readonly ISNIndicadorRepository _sNIndicadorRepository;

        public SNIndicadorService(ISNIndicadorRepository sNIndicadorRepository)
        {
            _sNIndicadorRepository = sNIndicadorRepository;
        }

        public async Task<ServiceResponseList<SN_Indicador>?> Listado(string sFiltro)
        {
            var result = new ServiceResponseList<SN_Indicador>();
            try
            {
                var resultData = await _sNIndicadorRepository.Listado(sFiltro);
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No existe información";
                    result.Elements = new System.Collections.Generic.List<SN_Indicador>();
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

        public async Task<ServiceResponse<int>> Mnto(SN_Indicador sN_Indicador, string sTipoTransac)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _sNIndicadorRepository.Mnto(sN_Indicador, sTipoTransac);

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

        public async Task<ServiceResponseList<SN_Indicador_Medicion>?> ListadoMediciones(int? idIndicador, string sFiltro)
        {
            var result = new ServiceResponseList<SN_Indicador_Medicion>();
            try
            {
                var resultData = await _sNIndicadorRepository.ListadoMediciones(idIndicador, sFiltro);
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No existe información";
                    result.Elements = new System.Collections.Generic.List<SN_Indicador_Medicion>();
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

        public async Task<ServiceResponse<int>> MntoMedicion(SN_Indicador_Medicion medicion, string sTipoTransac)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _sNIndicadorRepository.MntoMedicion(medicion, sTipoTransac);

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

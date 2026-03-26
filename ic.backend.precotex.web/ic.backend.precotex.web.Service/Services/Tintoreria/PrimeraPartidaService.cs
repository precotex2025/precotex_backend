using ic.backend.precotex.web.Data.Repositories.Implementation.Tintoreria;
using ic.backend.precotex.web.Entity.Entities.Memorandum;
using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Entity.Entities.Tintoreria;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.Tintoreria;
using iTextSharp.text.pdf.codec.wmf;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Tintoreria
{
    
    public class PrimeraPartidaService: IPrimeraPartidaService
    {
        private readonly IPrimeraPartidaRepository _primeraPartidaRepository;

        public PrimeraPartidaService(IPrimeraPartidaRepository primeraPartidaRepository)
        {
            _primeraPartidaRepository = primeraPartidaRepository;
        }

        public async Task<ServiceResponseList<PrimeraPartidaBandeja>?> Lista(DateTime? Fecha_Ini, DateTime? Fecha_Fin)
        {
            var result = new ServiceResponseList<PrimeraPartidaBandeja>();
            try
            {
                var resultData = await _primeraPartidaRepository.Lista(Fecha_Ini, Fecha_Fin);
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

        public async Task<ServiceResponse<int>> ProcesoMnto(AuditoriaPrimeraPartida auditoriaPrimeraPartida)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _primeraPartidaRepository.ProcesoMnto(auditoriaPrimeraPartida);
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

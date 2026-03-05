using ic.backend.precotex.web.Data.Repositories.Implementation.SecureNorm;
using ic.backend.precotex.web.Data.Repositories.SecureNorm;
using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.SecureNorm;
using iTextSharp.text.pdf.codec.wmf;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.SecureNorm
{
    public class SNPuestoService : ISNPuestoService
    {
        private readonly ISNPuestoRepository _sNPuestoRepository;

        public SNPuestoService(ISNPuestoRepository sNPuestoRepository)
        {
            _sNPuestoRepository = sNPuestoRepository;
        }

        public async Task<ServiceResponseList<SN_Puesto>?> Listado(string sCodigo_Organizacion, string sCodigo_Sede, string sCodigo_Nivel_Riesgo)
        {
            var result = new ServiceResponseList<SN_Puesto>();
            try
            {
                var resultData = await _sNPuestoRepository.Listado(sCodigo_Organizacion, sCodigo_Sede, sCodigo_Nivel_Riesgo);
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

        public async Task<ServiceResponse<int>> ProcesoMnto(SN_Puesto sN_Proceso, string sTipoTransac)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _sNPuestoRepository.ProcesoMnto(sN_Proceso, sTipoTransac);
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

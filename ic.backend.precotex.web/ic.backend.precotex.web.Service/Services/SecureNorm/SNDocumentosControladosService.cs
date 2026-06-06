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
    public class SNDocumentosControladosService : ISNDocumentosControladosService
    {
        private readonly ISNDocumentosControladosRepository _SNDocumentosControladosRepository;

        public SNDocumentosControladosService(ISNDocumentosControladosRepository sNDocumentosControladosRepository)
        {
            _SNDocumentosControladosRepository = sNDocumentosControladosRepository;
        }
        public async Task<ServiceResponseList<SN_Documentos_Controlados>?> Listado(string sCodigo_Organizacion, string sCodigo_Sede, string sCodigo_Puesto, string sCodigo_Proceso)
        {
            var result = new ServiceResponseList<SN_Documentos_Controlados>();
            try
            {
                var resultData = await _SNDocumentosControladosRepository.Listado(sCodigo_Organizacion, sCodigo_Sede, sCodigo_Puesto, sCodigo_Proceso);
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

        public async Task<ServiceResponse<int>> ProcesoCarpetaCtrolMnto(SN_Carpeta_Control sN_Carpeta_Control, string sTipoTransac)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _SNDocumentosControladosRepository.ProcesoCarpetaCtrolMnto(sN_Carpeta_Control, sTipoTransac);
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

        public async Task<ServiceResponse<int>> ProcesoMnto(SN_Documentos_Controlados sN_Documentos_Controlados, string sTipoTransac)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _SNDocumentosControladosRepository.ProcesoMnto(sN_Documentos_Controlados, sTipoTransac);
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

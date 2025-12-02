using ic.backend.precotex.web.Data.Repositories.DDT;
using ic.backend.precotex.web.Data.Repositories.Implementation.DDT;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.DDT;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.DDT
{
    public class TxDesarrolloTelaService : ITxDesarrolloTelaService
    {


        private readonly ITxDesarrolloTelaRepository _txDesarrolloTelaRepository;
        public TxDesarrolloTelaService(ITxDesarrolloTelaRepository txDesarrolloTelaRepository)
        {
            _txDesarrolloTelaRepository = txDesarrolloTelaRepository;
        }

        public async Task<ServiceResponseList<Tx_Desarrollo_Telas>?> ListadoDesarrolloTelas(string sAccion, string sCodTela, string sCodVersion, string sNomVersion, string sComentario, string sRutaArchivo, string sCodMotivoSolicitud, string sComentarioSolicitud, string sCodUsuario)
        {
            var result = new ServiceResponseList<Tx_Desarrollo_Telas>();
            try
            {
                var resultData = await _txDesarrolloTelaRepository.ListadoDesarrolloTelas(sAccion,  sCodTela,  sCodVersion,  sNomVersion,  sComentario,  sRutaArchivo,  sCodMotivoSolicitud,  sComentarioSolicitud, sCodUsuario);
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

        public async Task<ServiceResponse<int>> ProcesoDesarrolloTela(string sAccion, string sCodTela, string sCodVersion, string sNomVersion, string sComentario, string sRutaArchivo, string sCodMotivoSolicitud, string sComentarioSolicitud, string sCodUsuario)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txDesarrolloTelaRepository.ProcesoDesarrolloTela(sAccion, sCodTela, sCodVersion, sNomVersion, sComentario, sRutaArchivo,  sCodMotivoSolicitud, sComentarioSolicitud, sCodUsuario);
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

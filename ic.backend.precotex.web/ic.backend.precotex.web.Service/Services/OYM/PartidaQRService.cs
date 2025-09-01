using ic.backend.precotex.web.Data.Repositories.DDT;
using ic.backend.precotex.web.Data.Repositories.Implementation.DDT;
using ic.backend.precotex.web.Data.Repositories.Implementation.OYM;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.OYM;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.OYM
{
    public class PartidaQRService : IPartidaQRService
    {

        private readonly IPartidaQRRepository _partidaQRRepository;
        public PartidaQRService(IPartidaQRRepository partidaQRRepository)
        {
            _partidaQRRepository = partidaQRRepository;
        }

        public async Task<ServiceResponseList<Tx_Partida_IA>?> ObtieneInformacionPartidaQR(string Cod_OrdTra, int Num_Secuencia)
        {
            var result = new ServiceResponseList<Tx_Partida_IA>();
            try
            {
                var resultData = await _partidaQRRepository.ObtieneInformacionPartidaQR(Cod_OrdTra, Num_Secuencia);
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

        public async Task<ServiceResponse<int>> ProcesoInsertarPartidaQR(Tx_Partida_IA tx_Partida_IA, string sTipoTransac)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _partidaQRRepository.ProcesoInsertarPartidaQR(tx_Partida_IA, sTipoTransac);
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

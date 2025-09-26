using ic.backend.precotex.web.Data.Repositories.Implementation.Login;
using ic.backend.precotex.web.Entity.Entities.Login;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Login
{
    public class TxLoginService: ITxLoginService
    {
        private readonly ITxLoginRepository _txLoginRepository;

        public TxLoginService(ITxLoginRepository txLoginRepository)
        {
            _txLoginRepository = txLoginRepository;
        }

        public async Task<ServiceResponseList<Tx_Login>?> GetUsuarioHabilitado(string Cod_Usuario)
        {
            var result = new ServiceResponseList<Tx_Login>();
            try
            {
                var resultData = await _txLoginRepository.GetUsuarioHabilitado(Cod_Usuario);
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No existe información";
                }
                result.Success = true;
                result.Message = "Completado con éxito";
                result.Elements = resultData.ToList();
                result.TotalElements = resultData.ToList().Count();
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Excepción no controlada " + ex.Message;
                return result;
            }
        }

        public async Task<ServiceResponseList<Tx_Login>?> GetUsuarioWeb(string Cod_Usuario)
        {
            var result = new ServiceResponseList<Tx_Login>();
            try
            {
                var resultData = await _txLoginRepository.GetUsuarioWeb(Cod_Usuario);
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No existe información";
                }
                result.Success = true;
                result.Message = "Completado con éxito";
                result.Elements = resultData.ToList();
                result.TotalElements = resultData.ToList().Count();
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Excepción no controlada " + ex.Message;
                return result;
            }
        }


    }
}

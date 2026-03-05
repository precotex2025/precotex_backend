using ic.backend.precotex.web.Data.Repositories.Implementation.SecureNorm;
using ic.backend.precotex.web.Data.Repositories.SecureNorm;
using ic.backend.precotex.web.Entity.Entities;
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
    public class MaeTabService: IMaeTabService
    {
        private readonly IMaeTabRepository _maeTabRepository;
        public MaeTabService(IMaeTabRepository maeTabRepository)
        {
            _maeTabRepository = maeTabRepository;
        }

        public async Task<ServiceResponseList<ComboGral>?> Lista(string sCodigoTipo)
        {
            var result = new ServiceResponseList<ComboGral>();
            try
            {
                var resultData = await _maeTabRepository.Lista(sCodigoTipo);
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
    }
}

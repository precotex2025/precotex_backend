using ic.backend.precotex.web.Data.Repositories.Implementation.Mantto;
using ic.backend.precotex.web.Data.Repositories.Implementation.Tejeduria;
using ic.backend.precotex.web.Data.Repositories.Tejeduria;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.Mantto;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Mantto
{
    public class TxUsuarioSedeService : ITxUsuarioSedeService
    {
        private readonly ITxUsuarioSedeRepository _txUsuarioSedeRepository;
        public TxUsuarioSedeService(ITxUsuarioSedeRepository txUsuarioSedeRepository)
        {
            _txUsuarioSedeRepository = txUsuarioSedeRepository;
        }
        public async Task<ServiceResponseList<Tx_Usuario_Sede>?> ListaUsuarioSedeByUser(string? Cod_Usuario)
        {
            var result = new ServiceResponseList<Tx_Usuario_Sede>();
            try
            {
                var resultData = await _txUsuarioSedeRepository.ListaUsuarioSedeByUser(Cod_Usuario);
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

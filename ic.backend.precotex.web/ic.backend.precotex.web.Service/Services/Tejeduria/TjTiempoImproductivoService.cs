using ic.backend.precotex.web.Data.Repositories.Implementation.Tejeduria;
using ic.backend.precotex.web.Data.Repositories.Tejeduria;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.Tejeduria;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Tejeduria
{
    public class TjTiempoImproductivoService : ITjTiempoImproductivoService
    {
        public readonly ITjTiempoImproductivoRepository _tjTiempoImproductivoRepository;

        public TjTiempoImproductivoService(ITjTiempoImproductivoRepository tjTiempoImproductivoRepository)
        {
            _tjTiempoImproductivoRepository = tjTiempoImproductivoRepository;
        }

        public async Task<ServiceResponseList<Tj_Tiempo_Improductivo>?> ObtieneTiempoImproductivoPendiente(string? sCodMaquina)
        {
            var result = new ServiceResponseList<Tj_Tiempo_Improductivo>();
            try
            {
                var resultData = await _tjTiempoImproductivoRepository.ObtieneTiempoImproductivoPendiente(sCodMaquina);
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

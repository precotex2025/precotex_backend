using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ic.backend.precotex.web.Data.Repositories.Implementation.QuejasReclamosv2;
using ic.backend.precotex.web.Entity.Entities.QuejasReclamos;
using ic.backend.precotex.web.Service.common;

namespace ic.backend.precotex.web.Service.Services.QuejasReclamosv2
{
    public class SQuejasReclamosv2
    {

        private readonly IQuejasReclamosv2 _txtIQuejasReclamos;

        public async Task<ServiceResponseList<EstadoDto>?> ObtenerEstado()
        {
            var result = new ServiceResponseList<EstadoDto>();
            try
            {
                var resultData = await _txtIQuejasReclamos.ObtenerEstado();
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
                result.Message = "BD SQL: " + sql.Message;
                return result;
            }
        }

    }
}

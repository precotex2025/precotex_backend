using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.SecureNorm
{
    public interface ISNNormaService
    {
        Task<ServiceResponse<int>> ProcesoMnto(SN_Norma sN_Norma, string sTipoTransac);
        Task<ServiceResponseList<SN_Norma>?> Listado(string sEstado);
    }
}

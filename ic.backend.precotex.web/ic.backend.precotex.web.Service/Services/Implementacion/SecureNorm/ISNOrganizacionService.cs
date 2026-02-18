using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.SecureNorm
{
    public interface ISNOrganizacionService
    {
        Task<ServiceResponse<int>> ProcesoMnto(SN_Organizacion sN_Organizacion, string sTipoTransac);
        Task<ServiceResponseList<SN_Organizacion>?> Listado(string sEstado);
    }
}

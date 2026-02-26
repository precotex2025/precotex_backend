using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.SecureNorm
{
    public interface ISNProcesoService
    {
        Task<ServiceResponse<int>> ProcesoMnto(SN_Proceso sN_Sede, string sTipoTransac);
        Task<ServiceResponseList<SN_Proceso>?> Listado(string sCodigoOrganizacion, string sEstado);
    }
}

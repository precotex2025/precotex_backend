using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Service.common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.SecureNorm
{
    public interface ISNAuditoriaService
    {
        Task<ServiceResponse<int>> ProcesoMnto(SN_Auditoria sN_Auditoria, string sTipoTransac);
        Task<ServiceResponseList<SN_Auditoria>?> Listado(string sFiltro);
        Task<ServiceResponseList<SN_Auditoria_Ejecucion>?> ListadoEjecucion(string sFiltro);
        Task<ServiceResponse<int>> ProcesoMntoEjecucion(SN_Auditoria_Ejecucion ejecucion, string sTipoTransac);
    }
}

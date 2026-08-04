using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Service.common;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.SecureNorm
{
    public interface ISNMejoraService
    {
        Task<ServiceResponseList<SN_Mejora>?> Listado(string sFiltro);
        Task<ServiceResponse<int>> Mnto(SN_Mejora sN_Mejora, string sTipoTransac);
    }
}

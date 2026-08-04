using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Service.common;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.SecureNorm
{
    public interface ISNManualService
    {
        Task<ServiceResponseList<SN_Manual>?> Listado(string sFiltro);
        Task<ServiceResponse<int>> Mnto(SN_Manual sN_Manual, string sTipoTransac);
    }
}

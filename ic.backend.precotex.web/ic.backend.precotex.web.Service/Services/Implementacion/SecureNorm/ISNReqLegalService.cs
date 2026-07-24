using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Service.common;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.SecureNorm
{
    public interface ISNReqLegalService
    {
        Task<ServiceResponseList<SN_Req_Legal>?> Listado(string sFiltro);
        Task<ServiceResponse<int>> Mnto(SN_Req_Legal sN_Req_Legal, string sTipoTransac);
    }
}

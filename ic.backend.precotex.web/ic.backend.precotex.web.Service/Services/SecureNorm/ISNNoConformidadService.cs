using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Service.common;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.SecureNorm
{
    public interface ISNNoConformidadService
    {
        Task<ServiceResponseList<SN_No_Conformidad>?> Listado(string sFiltro);
        Task<ServiceResponse<int>> ProcesoMnto(SN_No_Conformidad sN_No_Conformidad, string sTipoTransac);
    }
}

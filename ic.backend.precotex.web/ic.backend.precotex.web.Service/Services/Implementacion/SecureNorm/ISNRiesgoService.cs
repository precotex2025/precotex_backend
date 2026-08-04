using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Entity.Entities.SecureNorm.Parameters;
using ic.backend.precotex.web.Service.common;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.SecureNorm
{
    public interface ISNRiesgoService
    {
        Task<ServiceResponseList<SN_Riesgo>?> GetListadoRiesgos(string sFiltro);
        Task<ServiceResponse<int>> PostProcesoMntoRiesgo(SNRiesgoParameter request);
    }
}

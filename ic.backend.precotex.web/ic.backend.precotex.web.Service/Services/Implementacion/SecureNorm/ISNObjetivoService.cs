using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Service.common;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.SecureNorm
{
    public interface ISNObjetivoService
    {
        Task<ServiceResponseList<SN_Objetivo>?> Listado(string sFiltro);
        Task<ServiceResponse<int>> Mnto(SN_Objetivo objetivo, string sTipoTransac);

        Task<ServiceResponseList<SN_Objetivo_Medicion>?> ListadoMediciones(int? idObjetivo, string sFiltro);
        Task<ServiceResponse<int>> MntoMedicion(SN_Objetivo_Medicion medicion, string sTipoTransac);
    }
}

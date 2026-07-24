using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Service.common;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.SecureNorm
{
    public interface ISNIndicadorService
    {
        Task<ServiceResponseList<SN_Indicador>?> Listado(string sFiltro);
        Task<ServiceResponse<int>> Mnto(SN_Indicador sN_Indicador, string sTipoTransac);

        Task<ServiceResponseList<SN_Indicador_Medicion>?> ListadoMediciones(int? idIndicador, string sFiltro);
        Task<ServiceResponse<int>> MntoMedicion(SN_Indicador_Medicion medicion, string sTipoTransac);
    }
}

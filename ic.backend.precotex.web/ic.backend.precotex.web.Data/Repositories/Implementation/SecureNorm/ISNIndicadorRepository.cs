using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.SecureNorm
{
    public interface ISNIndicadorRepository
    {
        Task<IEnumerable<SN_Indicador>?> Listado(string sFiltro);
        Task<(int Codigo, string Mensaje)> Mnto(SN_Indicador sN_Indicador, string sTipoTransac);

        Task<IEnumerable<SN_Indicador_Medicion>?> ListadoMediciones(int? idIndicador, string sFiltro);
        Task<(int Codigo, string Mensaje)> MntoMedicion(SN_Indicador_Medicion medicion, string sTipoTransac);
    }
}

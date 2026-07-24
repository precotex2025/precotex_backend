using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.SecureNorm
{
    public interface ISNObjetivoRepository
    {
        Task<IEnumerable<SN_Objetivo>?> Listado(string sFiltro);
        Task<(int Codigo, string Mensaje)> Mnto(SN_Objetivo objetivo, string sTipoTransac);

        Task<IEnumerable<SN_Objetivo_Medicion>?> ListadoMediciones(int? idObjetivo, string sFiltro);
        Task<(int Codigo, string Mensaje)> MntoMedicion(SN_Objetivo_Medicion medicion, string sTipoTransac);
    }
}

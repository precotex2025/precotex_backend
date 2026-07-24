using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.SecureNorm
{
    public interface ISNRiesgoRepository
    {
        Task<IEnumerable<SN_Riesgo>?> Listado(string sFiltro);
        Task<(int Codigo, string Mensaje)> Mnto(SN_Riesgo riesgo, string sTipoTransac);
    }
}

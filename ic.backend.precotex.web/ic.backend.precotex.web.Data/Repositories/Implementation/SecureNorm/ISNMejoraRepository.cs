using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.SecureNorm
{
    public interface ISNMejoraRepository
    {
        Task<IEnumerable<SN_Mejora>?> Listado(string sFiltro);
        Task<(int Codigo, string Mensaje)> Mnto(SN_Mejora sN_Mejora, string sTipoTransac);
    }
}

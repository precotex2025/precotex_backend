using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.SecureNorm
{
    public interface ISNManualRepository
    {
        Task<IEnumerable<SN_Manual>?> Listado(string sFiltro);
        Task<(int Codigo, string Mensaje)> Mnto(SN_Manual sN_Manual, string sTipoTransac);
    }
}

using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.SecureNorm
{
    public interface ISNReqLegalRepository
    {
        Task<IEnumerable<SN_Req_Legal>?> Listado(string sFiltro);
        Task<(int Codigo, string Mensaje)> Mnto(SN_Req_Legal sN_Req_Legal, string sTipoTransac);
    }
}

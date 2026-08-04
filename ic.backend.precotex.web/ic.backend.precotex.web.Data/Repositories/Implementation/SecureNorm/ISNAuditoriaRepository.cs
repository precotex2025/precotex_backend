using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.SecureNorm
{
    public interface ISNAuditoriaRepository
    {
        Task<(int Codigo, string Mensaje)> ProcesoMnto(SN_Auditoria sN_Auditoria, string sTipoTransac);
        Task<IEnumerable<SN_Auditoria>?> Listado(string sFiltro);
    }
}

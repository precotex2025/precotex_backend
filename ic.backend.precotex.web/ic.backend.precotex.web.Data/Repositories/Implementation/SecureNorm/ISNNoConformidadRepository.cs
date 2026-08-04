using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.SecureNorm
{
    public interface ISNNoConformidadRepository
    {
        Task<IEnumerable<SN_No_Conformidad>?> Listado(string sFiltro);
        Task<(int Codigo, string Mensaje)> ProcesoMnto(SN_No_Conformidad sN_No_Conformidad, string sTipoTransac);
    }
}

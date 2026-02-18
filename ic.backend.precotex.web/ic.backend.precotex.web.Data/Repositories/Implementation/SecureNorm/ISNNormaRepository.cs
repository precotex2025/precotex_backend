using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.Memorandum;
using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.SecureNorm
{
    public interface ISNNormaRepository
    {
        Task<(int Codigo, string Mensaje)> ProcesoMnto(SN_Norma sN_Norma, string sTipoTransac);
        Task<IEnumerable<SN_Norma>?> Listado(string sEstado);
    }
}

using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.SecureNorm
{
    public interface ISNSedeRepository
    {
        Task<(int Codigo, string Mensaje)> ProcesoMnto(SN_Sede sN_Sede, string sTipoTransac);
        Task<IEnumerable<SN_Sede>?> Listado();
    }
}

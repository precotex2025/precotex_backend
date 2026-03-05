using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.SecureNorm
{
    public interface IMaeTabRepository
    {
        Task<IEnumerable<ComboGral>?> Lista(string sCodigoTipo);
    }
}

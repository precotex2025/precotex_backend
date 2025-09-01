using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ic.backend.precotex.web.Entity.Entities.QuejasReclamos;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.QuejasReclamosv2
{
    public interface IQuejasReclamosv2
    {
        Task<IEnumerable<EstadoDto>?> ObtenerEstado();
    }
}

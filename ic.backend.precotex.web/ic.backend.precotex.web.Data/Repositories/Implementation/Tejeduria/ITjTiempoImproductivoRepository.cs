using ic.backend.precotex.web.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.Tejeduria
{
    public interface ITjTiempoImproductivoRepository
    {
        Task<IEnumerable<Tj_Tiempo_Improductivo>?> ObtieneTiempoImproductivoPendiente(string? sCodMaquina);
    }
}

using ic.backend.precotex.web.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.Almacen
{
    public interface ITxUbicacionRepository
    {
        Task<IEnumerable<Tx_Ubicacion>?> ListaByCodigoUbicacion(string? Cod_Ubicacion);
    }
}

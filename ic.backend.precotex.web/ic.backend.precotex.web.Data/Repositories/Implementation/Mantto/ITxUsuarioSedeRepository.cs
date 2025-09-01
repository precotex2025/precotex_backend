using ic.backend.precotex.web.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.Mantto
{
    public interface ITxUsuarioSedeRepository
    {
        Task<IEnumerable<Tx_Usuario_Sede>?> ListaUsuarioSedeByUser(string? Cod_Usuario);
    }
}

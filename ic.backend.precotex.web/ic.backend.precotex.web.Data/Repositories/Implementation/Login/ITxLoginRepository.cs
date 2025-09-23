using ic.backend.precotex.web.Data.Repositories.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ic.backend.precotex.web.Entity;
using ic.backend.precotex.web.Entity.Entities.Login;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.Login
{
    public interface ITxLoginRepository
    {
        Task<IEnumerable<Tx_Login>?> GetUsuarioHabilitado(string Cod_Usuario);
        Task<IEnumerable<Tx_Login>?> GetUsuarioWeb(string Cod_Usuario);
    }
}

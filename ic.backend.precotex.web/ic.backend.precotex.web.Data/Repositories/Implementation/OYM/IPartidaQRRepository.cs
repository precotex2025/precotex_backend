using ic.backend.precotex.web.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.OYM
{
    public interface IPartidaQRRepository
    {
        Task<(int Codigo, string Mensaje)> ProcesoInsertarPartidaQR(Tx_Partida_IA tx_Partida_IA, string sTipoTransac);
        Task<IEnumerable<Tx_Partida_IA>?> ObtieneInformacionPartidaQR(string Cod_OrdTra, int Num_Secuencia);
    }
}

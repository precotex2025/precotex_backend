using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.OYM
{
    public interface IPartidaQRService
    {
        Task<ServiceResponse<int>> ProcesoInsertarPartidaQR(Tx_Partida_IA tx_Partida_IA, string sTipoTransac);
        Task<ServiceResponseList<Tx_Partida_IA>?>ObtieneInformacionPartidaQR(string Cod_OrdTra, int Num_Secuencia);
    }
}

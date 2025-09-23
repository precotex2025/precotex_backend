using ic.backend.precotex.web.Entity.Entities.Login;
using ic.backend.precotex.web.Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.Login
{
    public interface ITxLoginService
    {
        Task<ServiceResponseList<Tx_Login>?> GetUsuarioHabilitado(string Cod_Usuario);
        Task<ServiceResponseList<Tx_Login>?> GetUsuarioWeb(string Cod_Usuario);
    }
}

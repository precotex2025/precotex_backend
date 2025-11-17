using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.WallyChat
{
    public interface IWaliChatService
    {
        Task<string> EnviarMensajeAsync(string groupId, string message);
        Task<string> EnviarMensajeImageAsync(string groupId, string message, string imageUrl, bool viewOnce);
    }
}

using ic.backend.precotex.web.Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.HelpCommon
{
    public interface IGenerateImageDinamycService
    {
        Task<ServiceResponse<byte[]>>  GenerarImagen(string titulo, string colorHex, string iconoPath, string area, string persona, string fecha, string hora, string tipo);
    }
}

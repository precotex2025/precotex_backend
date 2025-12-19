using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.Tejeduria
{
    public interface ITjTiempoImproductivoService
    {
        Task<ServiceResponseList<Tj_Tiempo_Improductivo>?> ObtieneTiempoImproductivoPendiente(string? sCodMaquina);
    }
}

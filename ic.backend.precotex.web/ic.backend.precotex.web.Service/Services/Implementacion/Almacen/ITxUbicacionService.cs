using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.Almacen
{
    public interface ITxUbicacionService
    {
        Task<ServiceResponseList<Tx_Ubicacion>?> ListaByCodigoUbicacion(string? Cod_Ubicacion);
    }
}

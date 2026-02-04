using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.Almacen
{
    public interface ITmpVisorPermanenciaTelaCrudaService
    {
        Task<ServiceResponseList<Tx_Visor_Permanencia_Tela_Cruda>?> ObtieneListaPermanenciaTelaCruda(int? anio);
        Task<ServiceResponseList<Lg_RequerimientoAlmacen>?> EstatusRequerimientoAlmacen(string? sEstado);
    }
}

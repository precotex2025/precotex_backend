using ic.backend.precotex.web.Entity.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.Almacen
{
    public interface ITmpVisorPermanenciaTelaCrudaRepository
    {
        Task<IEnumerable<Tx_Visor_Permanencia_Tela_Cruda>?> ObtieneListaPermanenciaTelaCruda(int? anio);
        Task<IEnumerable<Lg_RequerimientoAlmacen>?> EstatusRequerimientoAlmacen(string? sEstado);

    }
}

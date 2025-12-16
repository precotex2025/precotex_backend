using ic.backend.precotex.web.Entity.Entities.Cotizaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.Cotizaciones
{
    public interface ITxCotizacionesRepository
    {
        Task<IEnumerable<Tx_Cotizaciones>?> ListarProcesosExportacion(string Pro_Cen_Cos);
    }
}

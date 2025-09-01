using ic.backend.precotex.web.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.Tintoreria
{
    public interface ITiProcesosTintoreriaRepository
    {
        Task<IEnumerable<Ti_Procesos_Tintoreria>?> ListaEstatusControlTenido(string Cod_Ordtra, DateTime? Fecha_Ini, DateTime? Fecha_Fin, string Cod_Usuario);
        Task<IEnumerable<Tx_Muestra_Control_Proceso>?> ListaControlProcesosTintoreria(string Cod_Ordtra, DateTime? Fecha_Ini, DateTime? Fecha_Fin);
        Task<IEnumerable<Ti_Seguimiento_Tobera>?> ListaDetalleToberaPruebaTenido(string Cod_Ordtra, string IdOrgatexUnico);
    }
}

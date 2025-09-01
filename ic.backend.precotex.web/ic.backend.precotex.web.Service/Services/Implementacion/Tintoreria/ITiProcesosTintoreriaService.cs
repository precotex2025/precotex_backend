using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.Tintoreria
{
    public interface ITiProcesosTintoreriaService
    {
        Task<ServiceResponseList<Ti_Procesos_Tintoreria>?> ListaEstatusControlTenido(string Cod_Ordtra, DateTime? Fecha_Ini, DateTime? Fecha_Fin, string Cod_Usuario);
        Task<ServiceResponseList<Tx_Muestra_Control_Proceso>?> ListaControlProcesosTintoreria(string Cod_Ordtra, DateTime? Fecha_Ini, DateTime? Fecha_Fin);
        Task<ServiceResponseList<Ti_Seguimiento_Tobera>?> ListaDetalleToberaPruebaTenido(string Cod_Ordtra, string IdOrgatexUnico);
    }
}

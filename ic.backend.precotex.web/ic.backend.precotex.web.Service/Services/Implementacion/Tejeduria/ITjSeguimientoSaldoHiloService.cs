using ic.backend.precotex.web.Entity.Entities.Memorandum;
using ic.backend.precotex.web.Entity.Entities.Tejeduria;
using ic.backend.precotex.web.Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.Tejeduria
{
    public interface ITjSeguimientoSaldoHiloService
    {
        Task<ServiceResponseList<tj_Muestra_OT_Terminada>?> ListaOT_Terminada(DateTime Fecha, DateTime Fecha_Fin, string Flg_Pendiente);
        Task<ServiceResponseList<tj_Muestra_OT_Programada>?> ListaOT_Programada(string Cod_OrdProv, string Cod_HilTel);
        Task<ServiceResponse<int>> Proceso(tj_seguimiento_saldo_hilo_tela tj_Seguimiento_Saldo_Hilo_Tela, string sTipoTransac);
    }
}

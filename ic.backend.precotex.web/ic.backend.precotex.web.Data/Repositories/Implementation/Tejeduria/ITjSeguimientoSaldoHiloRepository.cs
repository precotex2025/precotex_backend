using ic.backend.precotex.web.Entity.Entities.Laboratorio;
using ic.backend.precotex.web.Entity.Entities.Memorandum;
using ic.backend.precotex.web.Entity.Entities.Tejeduria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.Tejeduria
{
    public interface ITjSeguimientoSaldoHiloRepository
    {
        Task<IEnumerable<tj_Muestra_OT_Terminada>?> ListaOT_Terminada(DateTime Fecha, string Flg_Pendiente);
        Task<IEnumerable<tj_Muestra_OT_Programada>?> ListaOT_Programada(string Cod_OrdProv, string Cod_HilTel);
        Task<(int Codigo, string Mensaje)> Proceso(tj_seguimiento_saldo_hilo_tela tj_Seguimiento_Saldo_Hilo_Tela, string sTipoTransac);
    }
}

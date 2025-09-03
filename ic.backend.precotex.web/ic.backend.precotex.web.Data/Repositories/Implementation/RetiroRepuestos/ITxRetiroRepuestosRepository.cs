using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.RetiroRepuestos;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.RetiroRepuestos
{
    public interface ITxRetiroRepuestosRepository
    {
        Task<IEnumerable<Tx_Retiro_Repuestos>?> ListaRetiros(DateTime FecIni, DateTime FecFin);
        Task<IEnumerable<Tx_Retiro_Repuestos>?> ListaRetirosPorNumRequerimiento(int Num_Requerimiento);
        Task<(int Codigo, string Mensaje)> RegistrarRequerimiento(Tx_Retiro_Repuestos tx_Retiro_Repuestos);
        Task<IEnumerable<Tx_Retiro_Repuestos_Detalle>?> ListaRetiroDetallePorNumRequerimiento(int Num_Requerimiento);
        Task<IEnumerable<Tx_Retiro_Repuestos_Detalle>?> ListaRetiroDetallePorNumReqySecuencia(int Num_Requerimiento, int Nro_Secuencia);
    }
}

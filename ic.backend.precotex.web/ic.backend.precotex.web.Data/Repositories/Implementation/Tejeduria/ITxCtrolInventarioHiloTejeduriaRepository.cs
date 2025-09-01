using ic.backend.precotex.web.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.Tejeduria
{
    public interface ITxCtrolInventarioHiloTejeduriaRepository
    {
        Task<(int Codigo, string Mensaje)> CrudCtrolInventarioHiloTejeduria(Tx_Ctrol_Inventario_Hilo_Tejeduria tx_Ctrol_Inventario_Hilo_Tejeduria, string sTipoTransac);
        Task<IEnumerable<Tx_Ctrol_Inventario_Hilo_Tejeduria>?> ObtenerCtrolInventarioHiloTejeduriaByLote(string? Lote); 
    }
}

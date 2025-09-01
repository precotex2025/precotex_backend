using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.Tejeduria
{
    public interface ITxCtrolInventarioHiloTejeduriaService
    {
        Task<ServiceResponse<int>> CrudCtrolInventarioHiloTejeduria(Tx_Ctrol_Inventario_Hilo_Tejeduria tx_Ctrol_Inventario_Hilo_Tejeduria, string sTipoTransac);
        Task<ServiceResponseList<Tx_Ctrol_Inventario_Hilo_Tejeduria>?> ObtenerCtrolInventarioHiloTejeduriaByLote(string? Lote);

    }
}

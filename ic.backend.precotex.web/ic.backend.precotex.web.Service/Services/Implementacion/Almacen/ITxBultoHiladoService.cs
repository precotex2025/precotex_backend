using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.Almacen
{
    public interface ITxBultoHiladoService
    {
        Task<ServiceResponseList<Tx_Bulto_Hilado>?> ListaBultosUbicacion(string sCodProveedor, string sCodOrdProv, string? sNumSemana, string? sNomConera);
        Task<ServiceResponseList<Lg_Proveedor>?> ListaProveedores();
    }
}

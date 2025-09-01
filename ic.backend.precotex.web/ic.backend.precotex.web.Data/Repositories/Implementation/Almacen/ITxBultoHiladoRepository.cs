using ic.backend.precotex.web.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.Almacen
{
    public interface ITxBultoHiladoRepository
    {
        Task<IEnumerable<Tx_Bulto_Hilado>?> ListaBultosUbicacion(string sCodProveedor, string sCodOrdProv, string? sNumSemana, string? sNomConera);
        Task<IEnumerable<Lg_Proveedor>?> ListaProveedores();
    }
}

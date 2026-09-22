using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.SecureNorm
{
    public interface ISNProveedorRepository
    {
        Task<IEnumerable<SN_Proveedor>?> Listado(string sFiltro);
        Task<(int Codigo, string Mensaje)> Mnto(SN_Proveedor sN_Proveedor, string sTipoTransac);
    }
}
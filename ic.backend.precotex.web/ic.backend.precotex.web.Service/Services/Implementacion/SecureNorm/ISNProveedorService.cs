using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Service.common;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.SecureNorm
{
    public interface ISNProveedorService
    {
        Task<ServiceResponseList<SN_Proveedor>?> Listado(string sFiltro);
        Task<ServiceResponse<int>> Mnto(SN_Proveedor sN_Proveedor, string sTipoTransac);
    }
}
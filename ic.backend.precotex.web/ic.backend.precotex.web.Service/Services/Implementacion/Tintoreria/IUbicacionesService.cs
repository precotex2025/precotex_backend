using ic.backend.precotex.web.Entity.Entities.Tintoreria;
using ic.backend.precotex.web.Service.common;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.Tintoreria
{
    public interface IUbicacionesService
    {
        Task<ServiceResponseList<Ubicaciones.ListaBultoUbicaciones>?> ListaBultoUbicaciones(string? Cod_Almacen, string? Cod_Item);
        Task<ServiceResponse<int>> InsertarBultoGrupo(Ubicaciones.InsertarBultoGrupo ubicaciones);
    }
}

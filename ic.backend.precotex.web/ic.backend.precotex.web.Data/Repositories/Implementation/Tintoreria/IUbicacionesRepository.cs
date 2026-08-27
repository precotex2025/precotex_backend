using ic.backend.precotex.web.Entity.Entities.Tintoreria;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.Tintoreria
{
    public interface IUbicacionesRepository
    {
        Task<IEnumerable<Ubicaciones.ListaBultoUbicaciones>?> ListaBultoUbicaciones(string? Cod_Almacen, string? Cod_Item);
        Task<(int Codigo, string Mensaje)> InsertarBultoGrupo(Ubicaciones.InsertarBultoGrupo ubicaciones);
    }
}

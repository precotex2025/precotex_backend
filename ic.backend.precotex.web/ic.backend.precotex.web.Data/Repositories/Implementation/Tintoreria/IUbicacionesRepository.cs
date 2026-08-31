using ic.backend.precotex.web.Entity.Entities.Tintoreria;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.Tintoreria
{
    public interface IUbicacionesRepository
    {
        Task<IEnumerable<Ubicaciones.ListaBultoUbicaciones>?> ListaBultoUbicaciones(string? Cod_Almacen, string? Cod_Item);
        Task<(int Codigo, string Mensaje, string CodigoBarraGrupo)> InsertarBultoGrupo(Ubicaciones.InsertarBultoGrupo ubicaciones);
        Task<IEnumerable<Ubicaciones.ListaAgrupamientosDelDia>?> ListaAgrupamientosDelDia(DateTime? Fec_Creacion, string? Codigo_Barra_Grupo);
        Task<IEnumerable<Ubicaciones.ListaDetalleBultosAgrupados>?> ListaDetalleBultosAgrupados(string? Cod_Almacen, int? Id_Agrupamiento);
    }
}

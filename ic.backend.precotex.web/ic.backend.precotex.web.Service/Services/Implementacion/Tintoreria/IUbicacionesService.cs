using ic.backend.precotex.web.Entity.common;
using ic.backend.precotex.web.Entity.Entities.Tintoreria;
using ic.backend.precotex.web.Service.common;
using System;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.Tintoreria
{
    public interface IUbicacionesService
    {
        Task<ServiceResponseList<Ubicaciones.ListaBultoUbicaciones>?> ListaBultoUbicaciones(string? Cod_Almacen, string? Codigo_Barra_Grupo);
        Task<ServiceResponse<Ubicaciones.GrupoCreadoResponseDto>> InsertarBultoGrupo(Ubicaciones.InsertarBultoGrupo ubicaciones);
        Task<ServiceResponse<ServiceResponseTransacSQL>> UbicarGrupoOBulto(Ubicaciones.UbicarGrupoOBulto ubicaciones);
        Task<ServiceResponseList<Ubicaciones.ListaAgrupamientosDelDia>?> ListaAgrupamientosDelDia(DateTime? Fec_Creacion, string? Codigo_Barra_Grupo);
        Task<ServiceResponseList<Ubicaciones.ListaDetalleBultosAgrupados>?> ListaDetalleBultosAgrupados(string? Cod_Almacen, int? Id_Agrupamiento, string? Codigo_Barra_Grupo);
        Task<ServiceResponse<Ubicaciones.ConsultaKardexPda>> ConsultaKardexPda(string? Cod_Almacen, string? Codigo_Escaneado);

    }
}

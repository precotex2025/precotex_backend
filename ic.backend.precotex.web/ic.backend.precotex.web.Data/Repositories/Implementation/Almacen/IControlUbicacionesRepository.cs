using ic.backend.precotex.web.Entity.Entities.Almacen;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.Almacen
{
    public interface IControlUbicacionesRepository
    {
        Task<ControlUbicacionResumen?> ObtenerResumenUbicacion(string? desUbicacion, string? idRollo, string? codUsuario);
        Task<IEnumerable<ControlUbicacionDetalle>?> ListarDetalleUbicacion(string? desUbicacion, string? idRollo, string? codUsuario);
        Task<ControlUbicacionResponse> ObtenerConsultaCompleta(string? desUbicacion, string? idRollo, string? codUsuario);

        // Métodos para Ubicación por Guía (Tmp_Tx_Muestra_Ubicacion_ID_Rollo_X_Anio_Guia y Tx_Muestra_Detalle_Ubicacion_Guia)
        Task<(int Cod, string Msg)> LimpiarTemporalGuia(string? codUsuario);
        Task<ControlUbicacionGuiaResponse> ConsultarGuiaCompleta(string numGuia, string? codUsuario);
        Task<ControlUbicacionGuiaResponse> InsertarBultoGuia(string numGuia, string idRollo, string? codUsuario);
        Task<ControlUbicacionGuiaResponse> GuardarUbicacionGuia(string numGuia, string desUbicacion, string? codUsuario);

        // Métodos para Reubicación (Tmp_Tx_Muestra_Reubicacion_ID_Rollo_X_Anio)
        Task<(int Cod, string Msg)> LimpiarTemporalReubicacion(string? codUsuario);
        Task<ControlUbicacionReubicacionResponse> ConsultarReubicacionCompleta(string? codUsuario);
        Task<ControlUbicacionReubicacionResponse> InsertarBultoReubicacion(string idRollo, string? codUsuario);
        Task<ControlUbicacionGuardarReubicacionResponse> GuardarReubicacion(string desUbicacion, string? codUsuario);
    }
}

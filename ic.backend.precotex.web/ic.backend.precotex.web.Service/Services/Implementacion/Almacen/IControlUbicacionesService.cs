using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Entity.Entities.Almacen;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.Almacen
{
    public interface IControlUbicacionesService
    {
        Task<ServiceResponse<ControlUbicacionResponse>> ObtenerDetalleUbicacion(string? desUbicacion, string? idRollo, string? codUsuario);
        Task<ServiceResponse<bool>> LimpiarTemporalGuia(string? codUsuario);
        Task<ServiceResponse<ControlUbicacionGuiaResponse>> ConsultarGuia(string numGuia, string? codUsuario);
        Task<ServiceResponse<ControlUbicacionGuiaResponse>> InsertarBultoGuia(ControlUbicacionLecturaRequest request);
        Task<ServiceResponse<ControlUbicacionGuiaResponse>> GuardarUbicacionGuia(ControlUbicacionGuardarRequest request);

        // Métodos de Reubicación
        Task<ServiceResponse<bool>> LimpiarTemporalReubicacion(string? codUsuario);
        Task<ServiceResponse<ControlUbicacionReubicacionResponse>> ConsultarReubicacion(string? codUsuario);
        Task<ServiceResponse<ControlUbicacionReubicacionResponse>> InsertarBultoReubicacion(ControlUbicacionReubicacionLecturaRequest request);
        Task<ServiceResponse<ControlUbicacionGuardarReubicacionResponse>> GuardarReubicacion(ControlUbicacionGuardarReubicacionRequest request);
    }
}

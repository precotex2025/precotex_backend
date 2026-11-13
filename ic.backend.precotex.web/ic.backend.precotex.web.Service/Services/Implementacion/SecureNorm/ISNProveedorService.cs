using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Service.common;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.SecureNorm
{
    public interface ISNProveedorService
    {
        Task<ServiceResponseList<SN_Proveedor>?> Listado(int numCiclo, int idArea, string sFiltro, int soloActivos = 1);
        Task<ServiceResponse<int>> Mnto(SN_Proveedor sN_Proveedor, string sTipoTransac);
        Task<ServiceResponseList<SN_Proveedor_HistorialCambio>?> HistorialCambios(int top = 50);

        Task<ServiceResponseList<SN_Proveedor_Evaluacion>?> ListadoEvaluaciones(int numCiclo, int idArea, int idProveedor, byte numSemestre);
        Task<ServiceResponse<SN_Proveedor_EvaluacionResultado>> GuardarEvaluacion(SN_Proveedor_Evaluacion evaluacion);

        Task<ServiceResponse<SN_Proveedor_ConsolidadoResponse>> ConsolidadoAnual(int numCiclo, int idProveedor, byte numSemestre);

        Task<ServiceResponseList<SN_Proveedor_Incidente>?> ListadoIncidentes(int idProveedor, int idArea, int numCiclo);
        Task<ServiceResponse<int>> MntoIncidente(SN_Proveedor_Incidente incidente, string sTipoTransac);

        Task<ServiceResponseList<SN_Proveedor_Documento>?> ListadoDocumentos(int idProveedor, int idTipoDoc);
        Task<ServiceResponse<int>> MntoDocumento(SN_Proveedor_Documento documento, string sTipoTransac);
        Task<ServiceResponseList<SN_Proveedor_TipoDocumento>?> ListadoTiposDocumento();

        Task<ServiceResponseList<SN_Proveedor_ConfigPeso>?> ListadoConfigPeso(bool soloActivos = false);
        Task<ServiceResponse<int>> GuardarConfigPeso(SN_Proveedor_ConfigPeso config);
    }
}

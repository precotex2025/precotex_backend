using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.SecureNorm
{
    public interface ISNProveedorRepository
    {
        // Catálogo Maestro de Proveedores
        Task<IEnumerable<SN_Proveedor>?> Listado(int numCiclo, int idArea, string sFiltro, int soloActivos = 1);
        Task<(int Codigo, string Mensaje, int IdGenerado)> Mnto(SN_Proveedor sN_Proveedor, string sTipoTransac);
        Task<IEnumerable<SN_Proveedor_HistorialCambio>?> HistorialCambios(int top = 50);

        // Evaluaciones FOR-LOG-LOG-003
        Task<IEnumerable<SN_Proveedor_Evaluacion>?> ListadoEvaluaciones(int numCiclo, int idArea, int idProveedor, byte numSemestre);
        Task<(int Codigo, string Mensaje, SN_Proveedor_EvaluacionResultado? Resultado)> GuardarEvaluacion(SN_Proveedor_Evaluacion evaluacion);

        // Consolidado Multi-Área Ponderado
        Task<SN_Proveedor_ConsolidadoResponse> ConsolidadoAnual(int numCiclo, int idProveedor, byte numSemestre);

        // Incidentes Operacionales
        Task<IEnumerable<SN_Proveedor_Incidente>?> ListadoIncidentes(int idProveedor, int idArea, int numCiclo);
        Task<(int Codigo, string Mensaje, int IdGenerado)> MntoIncidente(SN_Proveedor_Incidente incidente, string sTipoTransac);

        // Documentos SSOMA y Tipos de Documento
        Task<IEnumerable<SN_Proveedor_Documento>?> ListadoDocumentos(int idProveedor, int idTipoDoc);
        Task<(int Codigo, string Mensaje, int IdGenerado)> MntoDocumento(SN_Proveedor_Documento documento, string sTipoTransac);
        Task<IEnumerable<SN_Proveedor_TipoDocumento>?> ListadoTiposDocumento();

        // Áreas Evaluadoras (Pesos y Umbrales)
        Task<IEnumerable<SN_Proveedor_ConfigPeso>?> ListadoConfigPeso(bool soloActivos = false);
        Task<(int Codigo, string Mensaje, int IdGenerado)> GuardarConfigPeso(SN_Proveedor_ConfigPeso config);
    }
}

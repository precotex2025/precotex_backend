using ic.backend.precotex.web.Data.Repositories.Implementation.SecureNorm;
using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.SecureNorm;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.SecureNorm
{
    public class SNProveedorService : ISNProveedorService
    {
        private readonly ISNProveedorRepository _sNProveedorRepository;

        public SNProveedorService(ISNProveedorRepository sNProveedorRepository)
        {
            _sNProveedorRepository = sNProveedorRepository;
        }

        private async Task<ServiceResponseList<T>?> ExecuteListAsync<T>(Func<Task<IEnumerable<T>?>> query, string successMessage)
        {
            var result = new ServiceResponseList<T>();
            try
            {
                var data = await query();
                if (data == null || !data.Any())
                {
                    result.Success = true;
                    result.CodeResult = 200;
                    result.Message = "No existe información";
                    result.Elements = new List<T>();
                    result.TotalElements = 0;
                    return result;
                }

                var list = data.ToList();
                result.Success = true;
                result.CodeResult = 200;
                result.Elements = list;
                result.TotalElements = list.Count;
                result.Message = successMessage;
                return result;
            }
            catch (SqlException sql)
            {
                result.Success = false;
                result.CodeResult = 500;
                result.Message = "Error en Servidor: " + sql.Message;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.CodeResult = 500;
                result.Message = "Ocurrió una excepción: " + ex.Message;
                return result;
            }
        }

        private async Task<ServiceResponse<int>> ExecuteTransacAsync(Func<Task<(int Codigo, string Mensaje, int IdGenerado)>> command)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var (codigo, mensaje, idGenerado) = await command();
                if (codigo > 0)
                {
                    result.Success = true;
                    result.CodeResult = 200;
                    result.CodeTransacc = codigo;
                    result.Element = idGenerado;
                    result.Message = mensaje;
                    return result;
                }

                result.Success = false;
                result.CodeResult = 400;
                result.CodeTransacc = codigo;
                result.Message = mensaje;
                return result;
            }
            catch (SqlException sql)
            {
                result.Success = false;
                result.CodeResult = 500;
                result.Message = "Error en Servidor: " + sql.Message;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.CodeResult = 500;
                result.Message = "Ocurrió una excepción: " + ex.Message;
                return result;
            }
        }

        public Task<ServiceResponseList<SN_Proveedor>?> Listado(int numCiclo, int idArea, string sFiltro, int soloActivos = 1) =>
            ExecuteListAsync(() => _sNProveedorRepository.Listado(numCiclo, idArea, sFiltro, soloActivos), "Listado de proveedores obtenido exitosamente.");

        public Task<ServiceResponse<int>> Mnto(SN_Proveedor sN_Proveedor, string sTipoTransac) =>
            ExecuteTransacAsync(() => _sNProveedorRepository.Mnto(sN_Proveedor, sTipoTransac));

        public Task<ServiceResponseList<SN_Proveedor_HistorialCambio>?> HistorialCambios(int top = 50) =>
            ExecuteListAsync(() => _sNProveedorRepository.HistorialCambios(top), "Historial de cambios obtenido exitosamente.");

        public Task<ServiceResponseList<SN_Proveedor_Evaluacion>?> ListadoEvaluaciones(int numCiclo, int idArea, int idProveedor, byte numSemestre) =>
            ExecuteListAsync(() => _sNProveedorRepository.ListadoEvaluaciones(numCiclo, idArea, idProveedor, numSemestre), "Listado de evaluaciones obtenido exitosamente.");

        public async Task<ServiceResponse<SN_Proveedor_EvaluacionResultado>> GuardarEvaluacion(SN_Proveedor_Evaluacion evaluacion)
        {
            var result = new ServiceResponse<SN_Proveedor_EvaluacionResultado>();
            try
            {
                var (codigo, mensaje, resultado) = await _sNProveedorRepository.GuardarEvaluacion(evaluacion);
                if (codigo > 0)
                {
                    result.Success = true;
                    result.CodeResult = 200;
                    result.CodeTransacc = codigo;
                    result.Element = resultado;
                    result.Message = mensaje;
                    return result;
                }

                result.Success = false;
                result.CodeResult = 400;
                result.CodeTransacc = codigo;
                result.Message = mensaje;
                return result;
            }
            catch (SqlException sql)
            {
                result.Success = false;
                result.CodeResult = 500;
                result.Message = "Error en Servidor: " + sql.Message;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.CodeResult = 500;
                result.Message = "Ocurrió una excepción: " + ex.Message;
                return result;
            }
        }

        public async Task<ServiceResponse<SN_Proveedor_ConsolidadoResponse>> ConsolidadoAnual(int numCiclo, int idProveedor, byte numSemestre)
        {
            var result = new ServiceResponse<SN_Proveedor_ConsolidadoResponse>();
            try
            {
                var data = await _sNProveedorRepository.ConsolidadoAnual(numCiclo, idProveedor, numSemestre);
                result.Success = true;
                result.CodeResult = 200;
                result.Element = data;
                result.Message = (data.Resumen.Any() || data.Detalle.Any())
                    ? "Consolidado anual obtenido exitosamente."
                    : "No existe información";
                return result;
            }
            catch (SqlException sql)
            {
                result.Success = false;
                result.CodeResult = 500;
                result.Message = "Error en Servidor: " + sql.Message;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.CodeResult = 500;
                result.Message = "Ocurrió una excepción: " + ex.Message;
                return result;
            }
        }

        public Task<ServiceResponseList<SN_Proveedor_Incidente>?> ListadoIncidentes(int idProveedor, int idArea, int numCiclo) =>
            ExecuteListAsync(() => _sNProveedorRepository.ListadoIncidentes(idProveedor, idArea, numCiclo), "Listado de incidentes obtenido exitosamente.");

        public Task<ServiceResponse<int>> MntoIncidente(SN_Proveedor_Incidente incidente, string sTipoTransac) =>
            ExecuteTransacAsync(() => _sNProveedorRepository.MntoIncidente(incidente, sTipoTransac));

        public Task<ServiceResponseList<SN_Proveedor_Documento>?> ListadoDocumentos(int idProveedor, int idTipoDoc) =>
            ExecuteListAsync(() => _sNProveedorRepository.ListadoDocumentos(idProveedor, idTipoDoc), "Listado de documentos SSOMA obtenido exitosamente.");

        public Task<ServiceResponse<int>> MntoDocumento(SN_Proveedor_Documento documento, string sTipoTransac) =>
            ExecuteTransacAsync(() => _sNProveedorRepository.MntoDocumento(documento, sTipoTransac));

        public Task<ServiceResponseList<SN_Proveedor_TipoDocumento>?> ListadoTiposDocumento() =>
            ExecuteListAsync(() => _sNProveedorRepository.ListadoTiposDocumento(), "Tipos de documento SSOMA obtenidos exitosamente.");

        public Task<ServiceResponseList<SN_Proveedor_ConfigPeso>?> ListadoConfigPeso(bool soloActivos = false) =>
            ExecuteListAsync(() => _sNProveedorRepository.ListadoConfigPeso(soloActivos), "Áreas evaluadoras obtenidas exitosamente.");

        public Task<ServiceResponse<int>> GuardarConfigPeso(SN_Proveedor_ConfigPeso config) =>
            ExecuteTransacAsync(() => _sNProveedorRepository.GuardarConfigPeso(config));
    }
}

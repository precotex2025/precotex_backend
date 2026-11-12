using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Data.Repositories.Implementation.Almacen;
using ic.backend.precotex.web.Entity.Entities.Almacen;
using ic.backend.precotex.web.Service.Services.Implementacion.Almacen;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Almacen
{
    public class ControlUbicacionesService : IControlUbicacionesService
    {
        private readonly IControlUbicacionesRepository _controlUbicacionesRepository;

        public ControlUbicacionesService(IControlUbicacionesRepository controlUbicacionesRepository)
        {
            _controlUbicacionesRepository = controlUbicacionesRepository;
        }

        public async Task<ServiceResponse<ControlUbicacionResponse>> ObtenerDetalleUbicacion(string? desUbicacion, string? idRollo, string? codUsuario)
        {
            var result = new ServiceResponse<ControlUbicacionResponse>();
            try
            {
                var data = await _controlUbicacionesRepository.ObtenerConsultaCompleta(desUbicacion, idRollo, codUsuario);
                result.Success = true;
                result.CodeResult = 200;
                result.Message = "Búsqueda realizada con éxito";
                result.Element = data;
                return result;
            }
            catch (SqlException sql)
            {
                result.Success = false;
                result.CodeResult = 500;
                result.Message = "Error en base de datos: " + sql.Message;
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

        public async Task<ServiceResponse<bool>> LimpiarTemporalGuia(string? codUsuario)
        {
            var result = new ServiceResponse<bool>();
            try
            {
                var (cod, msg) = await _controlUbicacionesRepository.LimpiarTemporalGuia(codUsuario);
                result.Success = cod == 1;
                result.CodeResult = cod == 1 ? 200 : 400;
                result.Message = msg;
                result.Element = cod == 1;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.CodeResult = 500;
                result.Message = "Error al limpiar temporal: " + ex.Message;
                return result;
            }
        }

        public async Task<ServiceResponse<ControlUbicacionGuiaResponse>> ConsultarGuia(string numGuia, string? codUsuario)
        {
            var result = new ServiceResponse<ControlUbicacionGuiaResponse>();
            try
            {
                var data = await _controlUbicacionesRepository.ConsultarGuiaCompleta(numGuia, codUsuario);
                result.Success = true;
                result.CodeResult = 200;
                result.Message = data.Msg ?? "Consulta exitosa";
                result.Element = data;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.CodeResult = 500;
                result.Message = "Error al consultar guía: " + ex.Message;
                return result;
            }
        }

        public async Task<ServiceResponse<ControlUbicacionGuiaResponse>> InsertarBultoGuia(ControlUbicacionLecturaRequest request)
        {
            var result = new ServiceResponse<ControlUbicacionGuiaResponse>();
            try
            {
                if (string.IsNullOrWhiteSpace(request.NumGuia))
                {
                    result.Success = false;
                    result.CodeResult = 400;
                    result.Message = "Debe ingresar una Guía de Remisión válida";
                    return result;
                }

                if (string.IsNullOrWhiteSpace(request.IdRollo))
                {
                    result.Success = false;
                    result.CodeResult = 400;
                    result.Message = "Debe ingresar el código de rollo / bulto";
                    return result;
                }

                var data = await _controlUbicacionesRepository.InsertarBultoGuia(request.NumGuia, request.IdRollo, request.CodUsuario);
                result.Success = data.Cod == 1;
                result.CodeResult = data.Cod == 1 ? 200 : 400;
                result.Message = data.Msg ?? "Proceso terminado";
                result.Element = data;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.CodeResult = 500;
                result.Message = "Error al insertar bulto: " + ex.Message;
                return result;
            }
        }

        public async Task<ServiceResponse<ControlUbicacionGuiaResponse>> GuardarUbicacionGuia(ControlUbicacionGuardarRequest request)
        {
            var result = new ServiceResponse<ControlUbicacionGuiaResponse>();
            try
            {
                if (string.IsNullOrWhiteSpace(request.NumGuia))
                {
                    result.Success = false;
                    result.CodeResult = 400;
                    result.Message = "Debe ingresar una Guía de Remisión válida";
                    return result;
                }

                if (string.IsNullOrWhiteSpace(request.DesUbicacion))
                {
                    result.Success = false;
                    result.CodeResult = 400;
                    result.Message = "Debe ingresar la ubicación de destino";
                    return result;
                }

                var data = await _controlUbicacionesRepository.GuardarUbicacionGuia(request.NumGuia, request.DesUbicacion, request.CodUsuario);
                result.Success = data.Cod == 1;
                result.CodeResult = data.Cod == 1 ? 200 : 400;
                result.Message = data.Msg ?? "Proceso terminado";
                result.Element = data;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.CodeResult = 500;
                result.Message = "Error al guardar ubicación: " + ex.Message;
                return result;
            }
        }

        // ==========================================
        // MÉTODOS DE REUBICACIÓN
        // ==========================================

        public async Task<ServiceResponse<bool>> LimpiarTemporalReubicacion(string? codUsuario)
        {
            var result = new ServiceResponse<bool>();
            try
            {
                var (cod, msg) = await _controlUbicacionesRepository.LimpiarTemporalReubicacion(codUsuario);
                result.Success = cod == 1;
                result.CodeResult = cod == 1 ? 200 : 400;
                result.Message = msg;
                result.Element = cod == 1;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.CodeResult = 500;
                result.Message = "Error al limpiar temporal de reubicación: " + ex.Message;
                return result;
            }
        }

        public async Task<ServiceResponse<ControlUbicacionReubicacionResponse>> ConsultarReubicacion(string? codUsuario)
        {
            var result = new ServiceResponse<ControlUbicacionReubicacionResponse>();
            try
            {
                var data = await _controlUbicacionesRepository.ConsultarReubicacionCompleta(codUsuario);
                result.Success = true;
                result.CodeResult = 200;
                result.Message = data.Msg ?? "Consulta exitosa";
                result.Element = data;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.CodeResult = 500;
                result.Message = "Error al consultar reubicación: " + ex.Message;
                return result;
            }
        }

        public async Task<ServiceResponse<ControlUbicacionReubicacionResponse>> InsertarBultoReubicacion(ControlUbicacionReubicacionLecturaRequest request)
        {
            var result = new ServiceResponse<ControlUbicacionReubicacionResponse>();
            try
            {
                if (string.IsNullOrWhiteSpace(request.IdRollo))
                {
                    result.Success = false;
                    result.CodeResult = 400;
                    result.Message = "Debe ingresar el código del bulto / rollo";
                    return result;
                }

                var data = await _controlUbicacionesRepository.InsertarBultoReubicacion(request.IdRollo, request.CodUsuario);
                result.Success = data.Cod == 1;
                result.CodeResult = data.Cod == 1 ? 200 : 400;
                result.Message = data.Msg ?? "Proceso terminado";
                result.Element = data;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.CodeResult = 500;
                result.Message = "Error al insertar bulto para reubicar: " + ex.Message;
                return result;
            }
        }

        public async Task<ServiceResponse<ControlUbicacionGuardarReubicacionResponse>> GuardarReubicacion(ControlUbicacionGuardarReubicacionRequest request)
        {
            var response = new ServiceResponse<ControlUbicacionGuardarReubicacionResponse>();
            try
            {
                if (string.IsNullOrWhiteSpace(request?.DesUbicacion))
                {
                    response.Success = false;
                    response.Message = "Debe ingresar la nueva ubicación de destino.";
                    response.Element = new ControlUbicacionGuardarReubicacionResponse { Cod = 3, Msg = response.Message };
                    return response;
                }

                var resultado = await _controlUbicacionesRepository.GuardarReubicacion(
                    request.DesUbicacion,
                    request.CodUsuario
                );

                response.Success = resultado.Cod == 1;
                response.Message = resultado.Msg;
                response.Element = resultado;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al guardar la reubicación: " + ex.Message;
                response.Element = new ControlUbicacionGuardarReubicacionResponse { Cod = 3, Msg = response.Message };
            }
            return response;
        }
    }
}

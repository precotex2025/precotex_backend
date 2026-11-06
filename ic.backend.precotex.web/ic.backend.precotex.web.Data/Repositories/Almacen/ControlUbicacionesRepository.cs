using Dapper;
using ic.backend.precotex.web.Data.Repositories.Implementation.Almacen;
using ic.backend.precotex.web.Entity.Entities.Almacen;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Almacen
{
    public class ControlUbicacionesRepository : IControlUbicacionesRepository
    {
        private readonly string _connectionString;

        public ControlUbicacionesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnection")!;
        }

        private string NormalizarNumGuia(string? numGuia)
        {
            if (string.IsNullOrWhiteSpace(numGuia)) return string.Empty;
            string clean = numGuia.Trim().Replace(" ", "-");
            if (!clean.Contains("-") && clean.Length == 12)
            {
                clean = clean.Substring(0, 4) + "-" + clean.Substring(4);
            }
            return clean;
        }

        public async Task<ControlUbicacionResumen?> ObtenerResumenUbicacion(string? desUbicacion, string? idRollo, string? codUsuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parameters = new
                {
                    Tipo = "R",
                    Des_Ubicacion = desUbicacion ?? string.Empty,
                    ID_Rollo_X_Anio = idRollo ?? string.Empty,
                    Cod_Usuario = codUsuario ?? string.Empty
                };

                var resumen = await connection.QueryFirstOrDefaultAsync<ControlUbicacionResumen>(
                    "[dbo].[Tx_Muestra_Detalle_Ubicacion]",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return resumen;
            }
        }

        public async Task<IEnumerable<ControlUbicacionDetalle>?> ListarDetalleUbicacion(string? desUbicacion, string? idRollo, string? codUsuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parameters = new
                {
                    Tipo = "D",
                    Des_Ubicacion = desUbicacion ?? string.Empty,
                    ID_Rollo_X_Anio = idRollo ?? string.Empty,
                    Cod_Usuario = codUsuario ?? string.Empty
                };

                var detalle = await connection.QueryAsync<ControlUbicacionDetalle>(
                    "[dbo].[Tx_Muestra_Detalle_Ubicacion]",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return detalle;
            }
        }

        public async Task<ControlUbicacionResponse> ObtenerConsultaCompleta(string? desUbicacion, string? idRollo, string? codUsuario)
        {
            var response = new ControlUbicacionResponse();

            var resumenTask = ObtenerResumenUbicacion(desUbicacion, idRollo, codUsuario);
            var detalleTask = ListarDetalleUbicacion(desUbicacion, idRollo, codUsuario);

            await Task.WhenAll(resumenTask, detalleTask);

            response.Resumen = await resumenTask;
            var detalleList = await detalleTask;
            if (detalleList != null)
            {
                response.Detalle = detalleList.ToList();
            }

            return response;
        }

        public async Task<(int Cod, string Msg)> LimpiarTemporalGuia(string? codUsuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parameters = new
                {
                    Accion = "C",
                    Num_Guia = string.Empty,
                    ID_Rollo_X_Anio = string.Empty,
                    Cod_Usuario = codUsuario ?? "sistemas"
                };

                var res = await connection.QueryFirstOrDefaultAsync<dynamic>(
                    "[dbo].[Tmp_Tx_Muestra_Ubicacion_ID_Rollo_X_Anio_Guia]",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                int cod = res != null ? (int)res.Cod : 1;
                string msg = res != null ? (string)res.Msg : "Limpieza realizada";
                return (cod, msg);
            }
        }

        public async Task<ControlUbicacionGuiaResponse> ConsultarGuiaCompleta(string numGuia, string? codUsuario)
        {
            var response = new ControlUbicacionGuiaResponse();
            string guiaNormalizada = NormalizarNumGuia(numGuia);

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // 1. Obtener Tot_Enviado, Tot_Ubicado, Tot_Pendiente usando el procedimiento almacenado Tx_Muestra_Detalle_Ubicacion_Guia
                var paramGuia = new
                {
                    Num_Guia = guiaNormalizada,
                    Cod_Usuario = codUsuario ?? "sistemas"
                };

                var totalesGuia = await connection.QueryFirstOrDefaultAsync<dynamic>(
                    "[dbo].[Tx_Muestra_Detalle_Ubicacion_Guia]",
                    paramGuia,
                    commandType: CommandType.StoredProcedure
                );

                if (totalesGuia != null)
                {
                    response.Totales.Tot_Enviados = totalesGuia.Tot_Enviado != null ? (int)totalesGuia.Tot_Enviado : 0;
                    response.Totales.Tot_Ubicados = totalesGuia.Tot_Ubicado != null ? (int)totalesGuia.Tot_Ubicado : 0;
                    response.Totales.Tot_Pendientes = totalesGuia.Tot_Pendiente != null ? (int)totalesGuia.Tot_Pendiente : 0;
                }

                // 2. Obtener Totales temporales de la lectura ('T')
                var paramT = new
                {
                    Accion = "T",
                    Num_Guia = guiaNormalizada,
                    ID_Rollo_X_Anio = string.Empty,
                    Cod_Usuario = codUsuario ?? "sistemas"
                };

                using (var multiT = await connection.QueryMultipleAsync(
                    "[dbo].[Tmp_Tx_Muestra_Ubicacion_ID_Rollo_X_Anio_Guia]",
                    paramT,
                    commandType: CommandType.StoredProcedure))
                {
                    var totalesTemp = await multiT.ReadFirstOrDefaultAsync<dynamic>();
                    if (totalesTemp != null)
                    {
                        response.Totales.Tot_Lecturados = totalesTemp.Tot_Bulto != null ? (int)totalesTemp.Tot_Bulto : 0;
                        response.Totales.Tot_Peso_Neto = totalesTemp.Tot_Peso_Neto != null ? (decimal)totalesTemp.Tot_Peso_Neto : 0;
                    }
                }

                // 3. Obtener Listado de Bultos Temporales ('V')
                var paramV = new
                {
                    Accion = "V",
                    Num_Guia = guiaNormalizada,
                    ID_Rollo_X_Anio = string.Empty,
                    Cod_Usuario = codUsuario ?? "sistemas"
                };

                using (var multiV = await connection.QueryMultipleAsync(
                    "[dbo].[Tmp_Tx_Muestra_Ubicacion_ID_Rollo_X_Anio_Guia]",
                    paramV,
                    commandType: CommandType.StoredProcedure))
                {
                    var bultos = await multiV.ReadAsync<ControlUbicacionGuiaBulto>();
                    response.Bultos = bultos.ToList();
                }

                response.Cod = 1;
                response.Msg = "Proceso Realizado Correctamente!!!";
                return response;
            }
        }

        public async Task<ControlUbicacionGuiaResponse> InsertarBultoGuia(string numGuia, string idRollo, string? codUsuario)
        {
            string guiaNormalizada = NormalizarNumGuia(numGuia);
            string rolloNormalizado = (idRollo ?? "").Trim();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parameters = new
                {
                    Accion = "I",
                    Num_Guia = guiaNormalizada,
                    ID_Rollo_X_Anio = rolloNormalizado,
                    Cod_Usuario = codUsuario ?? "sistemas"
                };

                var res = await connection.QueryFirstOrDefaultAsync<dynamic>(
                    "[dbo].[Tmp_Tx_Muestra_Ubicacion_ID_Rollo_X_Anio_Guia]",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                int cod = res != null ? (int)res.Cod : 1;
                string msg = res != null ? (string)res.Msg : "Proceso realizado";

                // Consultamos el estado actualizado de la guía con los bultos registrados
                var consultaActualizada = await ConsultarGuiaCompleta(guiaNormalizada, codUsuario);
                consultaActualizada.Cod = cod;
                consultaActualizada.Msg = msg;
                return consultaActualizada;
            }
        }

        public async Task<ControlUbicacionGuiaResponse> GuardarUbicacionGuia(string numGuia, string desUbicacion, string? codUsuario)
        {
            string guiaNormalizada = NormalizarNumGuia(numGuia);
            string ubicacionNormalizada = (desUbicacion ?? "").Trim().ToUpper();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parameters = new
                {
                    Accion = "I",
                    Num_Guia = guiaNormalizada,
                    Des_Ubicacion = ubicacionNormalizada,
                    Cod_Usuario = codUsuario ?? "sistemas"
                };

                int cod = 1;
                string msg = "Proceso Realizado Correctamente!!!";

                using (var multi = await connection.QueryMultipleAsync(
                    "[dbo].[Tx_Man_Ubicacion_ID_Rollo_X_Anio_Guia]",
                    parameters,
                    commandType: CommandType.StoredProcedure))
                {
                    while (!multi.IsConsumed)
                    {
                        var row = await multi.ReadFirstOrDefaultAsync<dynamic>();
                        if (row != null && ((IDictionary<string, object>)row).ContainsKey("Cod"))
                        {
                            cod = (int)row.Cod;
                            msg = (string)row.Msg;
                            break;
                        }
                    }
                }

                // Si se guardó exitosamente, limpiamos la sesión temporal
                if (cod == 1)
                {
                    await LimpiarTemporalGuia(codUsuario);
                }

                // Consultamos el estado actualizado (ahora Tot_Ubicados se incrementará en Tx_Muestra_Detalle_Ubicacion_Guia)
                var consultaActualizada = await ConsultarGuiaCompleta(guiaNormalizada, codUsuario);
                consultaActualizada.Cod = cod;
                consultaActualizada.Msg = msg;
                return consultaActualizada;
            }
        }

        // IMPLEMENTACIÓN REUBICACIÓN

        public async Task<(int Cod, string Msg)> LimpiarTemporalReubicacion(string? codUsuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parameters = new
                {
                    Accion = "C",
                    ID_Rollo_X_Anio = string.Empty,
                    Cod_Usuario = codUsuario ?? "sistemas"
                };

                var res = await connection.QueryFirstOrDefaultAsync<dynamic>(
                    "[dbo].[Tmp_Tx_Muestra_Reubicacion_ID_Rollo_X_Anio]",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                int cod = res != null && ((IDictionary<string, object>)res).ContainsKey("Cod") ? (int)res.Cod : 1;
                string msg = res != null && ((IDictionary<string, object>)res).ContainsKey("Msg") ? (string)res.Msg : "Proceso Realizado Correctamente!!!";
                return (cod, msg);
            }
        }

        public async Task<ControlUbicacionReubicacionResponse> ConsultarReubicacionCompleta(string? codUsuario)
        {
            var response = new ControlUbicacionReubicacionResponse();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // 1. Obtener totales ('T')
                var paramT = new
                {
                    Accion = "T",
                    ID_Rollo_X_Anio = string.Empty,
                    Cod_Usuario = codUsuario ?? "sistemas"
                };

                using (var multiT = await connection.QueryMultipleAsync(
                    "[dbo].[Tmp_Tx_Muestra_Reubicacion_ID_Rollo_X_Anio]",
                    paramT,
                    commandType: CommandType.StoredProcedure))
                {
                    var totales = await multiT.ReadFirstOrDefaultAsync<dynamic>();
                    if (totales != null)
                    {
                        response.Totales.Tot_Bulto = totales.Tot_Bulto != null ? (int)totales.Tot_Bulto : 0;
                        response.Totales.Tot_Peso_Neto = totales.Tot_Peso_Neto != null ? (decimal)totales.Tot_Peso_Neto : 0;
                    }
                }

                // 2. Obtener lista de bultos ('V')
                var paramV = new
                {
                    Accion = "V",
                    ID_Rollo_X_Anio = string.Empty,
                    Cod_Usuario = codUsuario ?? "sistemas"
                };

                using (var multiV = await connection.QueryMultipleAsync(
                    "[dbo].[Tmp_Tx_Muestra_Reubicacion_ID_Rollo_X_Anio]",
                    paramV,
                    commandType: CommandType.StoredProcedure))
                {
                    var bultos = await multiV.ReadAsync<ControlUbicacionReubicacionBulto>();
                    response.Bultos = bultos.ToList();
                }

                response.Cod = 1;
                response.Msg = "Proceso Realizado Correctamente!!!";
                return response;
            }
        }

        public async Task<ControlUbicacionReubicacionResponse> InsertarBultoReubicacion(string idRollo, string? codUsuario)
        {
            string rolloNormalizado = (idRollo ?? "").Trim();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parameters = new
                {
                    Accion = "I",
                    ID_Rollo_X_Anio = rolloNormalizado,
                    Cod_Usuario = codUsuario ?? "sistemas"
                };

                int cod = 1;
                string msg = "Proceso Realizado Correctamente!!!";
                ControlUbicacionReubicacionBulto? ultimoBulto = null;

                using (var multi = await connection.QueryMultipleAsync(
                    "[dbo].[Tmp_Tx_Muestra_Reubicacion_ID_Rollo_X_Anio]",
                    parameters,
                    commandType: CommandType.StoredProcedure))
                {
                    var firstRow = await multi.ReadFirstOrDefaultAsync<dynamic>();
                    if (firstRow != null)
                    {
                        var dict = (IDictionary<string, object>)firstRow;
                        if (dict.ContainsKey("Cod"))
                        {
                            // Error: retornó Cod y Msg
                            cod = (int)firstRow.Cod;
                            msg = (string)firstRow.Msg;
                        }
                        else
                        {
                            // Éxito: primera tabla son los datos del rollo lecturado
                            ultimoBulto = new ControlUbicacionReubicacionBulto
                            {
                                ID_Rollo_X_Anio = firstRow.ID_Rollo_X_Anio,
                                Des_Ubicacion = firstRow.Des_Ubicacion,
                                Cod_Ordtra = firstRow.Cod_Ordtra,
                                Peso_Neto = firstRow.Peso_Neto != null ? (decimal)firstRow.Peso_Neto : 0
                            };

                            // Segunda tabla es el estado (Cod, Msg)
                            if (!multi.IsConsumed)
                            {
                                var statusRow = await multi.ReadFirstOrDefaultAsync<dynamic>();
                                if (statusRow != null && ((IDictionary<string, object>)statusRow).ContainsKey("Cod"))
                                {
                                    cod = (int)statusRow.Cod;
                                    msg = (string)statusRow.Msg;
                                }
                            }
                        }
                    }
                }

                // Consultamos la lista y totales actualizados
                var consultaActualizada = await ConsultarReubicacionCompleta(codUsuario);
                consultaActualizada.Cod = cod;
                consultaActualizada.Msg = msg;
                consultaActualizada.UltimoBulto = ultimoBulto;
                return consultaActualizada;
            }
        }

        public async Task<ControlUbicacionGuardarReubicacionResponse> GuardarReubicacion(string desUbicacion, string? codUsuario)
        {
            string ubicacionNormalizada = (desUbicacion ?? "").Trim().ToUpper();
            string usuario = codUsuario ?? "sistemas";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parameters = new
                {
                    Accion = "I",
                    Des_Ubicacion = ubicacionNormalizada,
                    Cod_Usuario = usuario
                };

                var row = await connection.QueryFirstOrDefaultAsync<ControlUbicacionGuardarReubicacionResponse>(
                    "Tx_Man_Reubicacion_ID_Rollo_X_Anio",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                var result = row ?? new ControlUbicacionGuardarReubicacionResponse { Cod = 1, Msg = "Proceso Realizado Correctamente!!!" };

                // Si la reubicación fue exitosa, limpiamos la tabla temporal para el usuario
                if (result.Cod == 1)
                {
                    await connection.ExecuteAsync(
                        "Tmp_Tx_Muestra_Reubicacion_ID_Rollo_X_Anio",
                        new { Accion = "C", ID_Rollo_X_Anio = string.Empty, Cod_Usuario = usuario },
                        commandType: CommandType.StoredProcedure
                    );
                }

                return result;
            }
        }
    }
}

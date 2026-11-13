using Dapper;
using ic.backend.precotex.web.Data.Repositories.Implementation.SecureNorm;
using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.SecureNorm
{
    public class SNProveedorRepository : ISNProveedorRepository
    {
        private readonly string _connectionString;

        public SNProveedorRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnectionSomma")!;
        }

        private static int ResolveCiclo(int numCiclo)
        {
            if (numCiclo > 0) return numCiclo;
            var now = DateTime.Now;
            return now.Month >= 7 ? now.Year : now.Year - 1;
        }

        private static object SafeSqlDate(DateTime? dt)
        {
            if (dt.HasValue && dt.Value >= new DateTime(1753, 1, 1) && dt.Value <= new DateTime(9999, 12, 31))
            {
                return dt.Value.Date;
            }
            return DBNull.Value;
        }

        private static (int Codigo, string Mensaje, int IdGenerado) ExtractTransacResult(dynamic? res)
        {
            if (res == null)
            {
                return (-1, "No se recibió respuesta del procedimiento almacenado.", 0);
            }

            var dict = new Dictionary<string, object?>((IDictionary<string, object?>)res, StringComparer.OrdinalIgnoreCase);
            int cod = dict.TryGetValue("COD", out var cVal) && cVal != null ? Convert.ToInt32(cVal) : -1;
            string msg = dict.TryGetValue("MSG", out var mVal) && mVal != null ? Convert.ToString(mVal) ?? string.Empty : string.Empty;
            int id = dict.TryGetValue("ID", out var iVal) && iVal != null ? Convert.ToInt32(iVal) : 0;

            return (cod, msg, id);
        }

        // =========================================================================
        // 1. CATÁLOGO DE PROVEEDORES
        // =========================================================================

        public async Task<IEnumerable<SN_Proveedor>?> Listado(int numCiclo, int idArea, string sFiltro, int soloActivos = 1)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                if (soloActivos != 1)
                {
                    try
                    {
                        var parametrosConFiltro = new
                        {
                            NUM_CICLO = ResolveCiclo(numCiclo),
                            ID_AREA = idArea,
                            SFILTRO = sFiltro ?? string.Empty,
                            SOLO_ACTIVOS = soloActivos
                        };

                        return await connection.QueryAsync<SN_Proveedor>(
                            "[dbo].[USP_SN_PROVEEDOR_LISTAR]",
                            parametrosConFiltro,
                            commandType: CommandType.StoredProcedure
                        );
                    }
                    catch (SqlException ex) when (ex.Number == 8144 || ex.Message.Contains("too many arguments") || ex.Message.Contains("demasiados argumentos"))
                    {
                        // Fallback a v2.2 si aún no se ha ejecutado el script v2.3 en BD
                    }
                }

                var parametrosBase = new
                {
                    NUM_CICLO = ResolveCiclo(numCiclo),
                    ID_AREA = idArea,
                    SFILTRO = sFiltro ?? string.Empty
                };

                return await connection.QueryAsync<SN_Proveedor>(
                    "[dbo].[USP_SN_PROVEEDOR_LISTAR]",
                    parametrosBase,
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public async Task<IEnumerable<SN_Proveedor_HistorialCambio>?> HistorialCambios(int top = 50)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                try
                {
                    var parametros = new { TOP = top <= 0 ? 50 : top };

                    return await connection.QueryAsync<SN_Proveedor_HistorialCambio>(
                        "[dbo].[USP_SN_PROVEEDOR_HISTORIAL_CAMBIOS]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );
                }
                catch (SqlException ex) when (ex.Number == 2812 || ex.Message.Contains("Could not find stored procedure") || ex.Message.Contains("no se encontró"))
                {
                    return Enumerable.Empty<SN_Proveedor_HistorialCambio>();
                }
            }
        }

        public async Task<(int Codigo, string Mensaje, int IdGenerado)> Mnto(SN_Proveedor sN_Proveedor, string sTipoTransac)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();

                parametros.Add("@OPCION", sTipoTransac ?? "I");
                parametros.Add("@ID_PROVEEDOR", sN_Proveedor.Id_Proveedor);
                parametros.Add("@RUC", sN_Proveedor.Ruc);
                parametros.Add("@RAZON_SOCIAL", sN_Proveedor.Razon_Social);
                parametros.Add("@BIEN_SERVICIO", sN_Proveedor.Bien_Servicio);
                parametros.Add("@TIPO_PROVEEDOR", sN_Proveedor.Tipo_Proveedor);
                parametros.Add("@PROCESO", sN_Proveedor.Proceso);
                parametros.Add("@IDS_AREAS", sN_Proveedor.Ids_Areas);
                parametros.Add("@CONTACTO_NOMBRE", sN_Proveedor.Contacto_Nombre);
                parametros.Add("@CONTACTO_TELEFONO", sN_Proveedor.Contacto_Telefono);
                parametros.Add("@CONTACTO_CORREO", sN_Proveedor.Contacto_Correo);
                parametros.Add("@FLG_HOMOLOGADO", sN_Proveedor.Flg_Homologado);
                parametros.Add("@COD_USUARIO", string.IsNullOrWhiteSpace(sN_Proveedor.Cod_Usuario) ? "SYSTEM" : sN_Proveedor.Cod_Usuario);

                var res = await connection.QueryFirstOrDefaultAsync<dynamic>(
                    "[dbo].[USP_SN_PROVEEDOR_MNT]",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

                return ExtractTransacResult(res);
            }
        }

        // =========================================================================
        // 2. EVALUACIONES FOR-LOG-LOG-003
        // =========================================================================

        public async Task<IEnumerable<SN_Proveedor_Evaluacion>?> ListadoEvaluaciones(int numCiclo, int idArea, int idProveedor, byte numSemestre)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    NUM_CICLO = numCiclo,
                    ID_AREA = idArea,
                    ID_PROVEEDOR = idProveedor,
                    NUM_SEMESTRE = numSemestre
                };

                return await connection.QueryAsync<SN_Proveedor_Evaluacion>(
                    "[dbo].[USP_SN_PROVEEDOR_EVALUACIONES_LISTAR]",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public async Task<(int Codigo, string Mensaje, SN_Proveedor_EvaluacionResultado? Resultado)> GuardarEvaluacion(SN_Proveedor_Evaluacion eval)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();

                parametros.Add("@ID_PROVEEDOR", eval.Id_Proveedor);
                parametros.Add("@ID_AREA", eval.Id_Area);
                parametros.Add("@COD_EVALUADOR", eval.Cod_Evaluador);
                parametros.Add("@FEC_EVALUACION", SafeSqlDate(eval.Fec_Evaluacion ?? DateTime.Today));

                parametros.Add("@NOTA_CALIDAD", eval.Flg_Na_Calidad ? (object)DBNull.Value : eval.Nota_Calidad);
                parametros.Add("@FLG_NA_CALIDAD", eval.Flg_Na_Calidad);
                parametros.Add("@OBS_CALIDAD", eval.Obs_Calidad);

                parametros.Add("@NOTA_TIEMPOS", eval.Flg_Na_Tiempos ? (object)DBNull.Value : eval.Nota_Tiempos);
                parametros.Add("@FLG_NA_TIEMPOS", eval.Flg_Na_Tiempos);
                parametros.Add("@OBS_TIEMPOS", eval.Obs_Tiempos);

                parametros.Add("@NOTA_SEGURIDAD", eval.Flg_Na_Seguridad ? (object)DBNull.Value : eval.Nota_Seguridad);
                parametros.Add("@FLG_NA_SEGURIDAD", eval.Flg_Na_Seguridad);
                parametros.Add("@OBS_SEGURIDAD", eval.Obs_Seguridad);

                parametros.Add("@NOTA_ATENCION", eval.Flg_Na_Atencion ? (object)DBNull.Value : eval.Nota_Atencion);
                parametros.Add("@FLG_NA_ATENCION", eval.Flg_Na_Atencion);
                parametros.Add("@OBS_ATENCION", eval.Obs_Atencion);

                parametros.Add("@NOTA_PRECIO", eval.Flg_Na_Precio ? (object)DBNull.Value : eval.Nota_Precio);
                parametros.Add("@FLG_NA_PRECIO", eval.Flg_Na_Precio);
                parametros.Add("@OBS_PRECIO", eval.Obs_Precio);

                parametros.Add("@DES_ACCIONES", eval.Des_Acciones);
                parametros.Add("@COD_USUARIO", string.IsNullOrWhiteSpace(eval.Cod_Usuario) ? "SYSTEM" : eval.Cod_Usuario);

                var res = await connection.QueryFirstOrDefaultAsync<dynamic>(
                    "[dbo].[USP_SN_PROVEEDOR_EVALUACION_GUARDAR]",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

                if (res == null)
                {
                    return (-1, "No se recibió respuesta al guardar evaluación.", null);
                }

                var dict = new Dictionary<string, object?>((IDictionary<string, object?>)res, StringComparer.OrdinalIgnoreCase);
                int cod = dict.TryGetValue("COD", out var cVal) && cVal != null ? Convert.ToInt32(cVal) : -1;
                string msg = dict.TryGetValue("MSG", out var mVal) && mVal != null ? Convert.ToString(mVal) ?? string.Empty : string.Empty;

                var resultado = new SN_Proveedor_EvaluacionResultado
                {
                    Id_Evaluacion = dict.TryGetValue("ID", out var idVal) && idVal != null ? Convert.ToInt32(idVal) : 0,
                    Puntaje_Normalizado = dict.TryGetValue("PUNTAJE_NORMALIZADO", out var pVal) && pVal != null ? Convert.ToDecimal(pVal) : 0m,
                    Clasificacion = dict.TryGetValue("CLASIFICACION", out var clVal) && clVal != null ? Convert.ToString(clVal) ?? string.Empty : string.Empty,
                    Aprobado = dict.TryGetValue("APROBADO", out var apVal) && apVal != null && Convert.ToBoolean(apVal)
                };

                return (cod, msg, resultado);
            }
        }

        // =========================================================================
        // 3. CONSOLIDADO ANUAL PONDERADO
        // =========================================================================

        public async Task<SN_Proveedor_ConsolidadoResponse> ConsolidadoAnual(int numCiclo, int idProveedor, byte numSemestre)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    NUM_CICLO = ResolveCiclo(numCiclo),
                    ID_PROVEEDOR = idProveedor,
                    NUM_SEMESTRE = numSemestre
                };

                using (var multi = await connection.QueryMultipleAsync(
                    "[dbo].[USP_SN_PROVEEDOR_CONSOLIDADO_ANUAL]",
                    parametros,
                    commandType: CommandType.StoredProcedure))
                {
                    return new SN_Proveedor_ConsolidadoResponse
                    {
                        Detalle = (await multi.ReadAsync<SN_Proveedor_ConsolidadoDetalle>()).ToList(),
                        Resumen = (await multi.ReadAsync<SN_Proveedor_ConsolidadoResumen>()).ToList()
                    };
                }
            }
        }

        // =========================================================================
        // 4. INCIDENTES OPERACIONALES
        // =========================================================================

        public async Task<IEnumerable<SN_Proveedor_Incidente>?> ListadoIncidentes(int idProveedor, int idArea, int numCiclo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    OPCION = "L",
                    ID_PROVEEDOR = idProveedor,
                    ID_AREA = idArea,
                    NUM_CICLO = numCiclo
                };

                return await connection.QueryAsync<SN_Proveedor_Incidente>(
                    "[dbo].[USP_SN_PROVEEDOR_INCIDENTE_MNT]",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public async Task<(int Codigo, string Mensaje, int IdGenerado)> MntoIncidente(SN_Proveedor_Incidente incidente, string sTipoTransac)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();

                parametros.Add("@OPCION", sTipoTransac ?? "I");
                parametros.Add("@ID_INCIDENTE", incidente.Id_Incidente);
                parametros.Add("@ID_PROVEEDOR", incidente.Id_Proveedor);
                parametros.Add("@ID_AREA", incidente.Id_Area);
                parametros.Add("@FEC_INCIDENTE", SafeSqlDate(incidente.Fec_Incidente ?? DateTime.Today));
                parametros.Add("@DES_PRODUCTO", incidente.Des_Producto);
                parametros.Add("@DES_INCIDENTE", incidente.Des_Incidente);
                parametros.Add("@DES_ACCIONES", incidente.Des_Acciones);
                parametros.Add("@FLG_CERRADO", incidente.Flg_Cerrado);
                parametros.Add("@FEC_CIERRE", SafeSqlDate(incidente.Fec_Cierre));
                parametros.Add("@COD_USUARIO", string.IsNullOrWhiteSpace(incidente.Cod_Usuario_Reg) ? "SYSTEM" : incidente.Cod_Usuario_Reg);

                var res = await connection.QueryFirstOrDefaultAsync<dynamic>(
                    "[dbo].[USP_SN_PROVEEDOR_INCIDENTE_MNT]",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

                return ExtractTransacResult(res);
            }
        }

        // =========================================================================
        // 5. DOCUMENTOS SSOMA Y TIPOS DE DOCUMENTO
        // =========================================================================

        public async Task<IEnumerable<SN_Proveedor_Documento>?> ListadoDocumentos(int idProveedor, int idTipoDoc)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    OPCION = "L",
                    ID_PROVEEDOR = idProveedor,
                    ID_TIPO_DOC = idTipoDoc
                };

                return await connection.QueryAsync<SN_Proveedor_Documento>(
                    "[dbo].[USP_SN_PROVEEDOR_DOCUMENTO_MNT]",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public async Task<(int Codigo, string Mensaje, int IdGenerado)> MntoDocumento(SN_Proveedor_Documento documento, string sTipoTransac)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();

                parametros.Add("@OPCION", sTipoTransac ?? "I");
                parametros.Add("@ID_DOCUMENTO", documento.Id_Documento);
                parametros.Add("@ID_PROVEEDOR", documento.Id_Proveedor);
                parametros.Add("@ID_TIPO_DOC", documento.Id_Tipo_Doc);
                parametros.Add("@REF_PERSONA", documento.Ref_Persona);
                parametros.Add("@NUM_DOCUMENTO", documento.Num_Documento);
                parametros.Add("@FEC_EMISION", SafeSqlDate(documento.Fec_Emision));
                parametros.Add("@FEC_VENCIMIENTO", SafeSqlDate(documento.Fec_Vencimiento ?? DateTime.Today));
                parametros.Add("@RUTA_ADJUNTO", documento.Ruta_Adjunto);
                parametros.Add("@OBSERVACIONES", documento.Observaciones);
                parametros.Add("@COD_USUARIO", string.IsNullOrWhiteSpace(documento.Cod_Usuario) ? "SYSTEM" : documento.Cod_Usuario);

                var res = await connection.QueryFirstOrDefaultAsync<dynamic>(
                    "[dbo].[USP_SN_PROVEEDOR_DOCUMENTO_MNT]",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

                return ExtractTransacResult(res);
            }
        }

        public async Task<IEnumerable<SN_Proveedor_TipoDocumento>?> ListadoTiposDocumento()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<SN_Proveedor_TipoDocumento>(
                    "[dbo].[USP_SN_PROVEEDOR_TIPO_DOCUMENTO_LISTAR]",
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        // =========================================================================
        // 6. ÁREAS EVALUADORAS (PESOS Y UMBRALES)
        // =========================================================================

        public async Task<IEnumerable<SN_Proveedor_ConfigPeso>?> ListadoConfigPeso(bool soloActivos = false)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                if (soloActivos)
                {
                    try
                    {
                        var parametrosConFiltro = new { OPCION = "L", SOLO_ACTIVOS = 1 };
                        return await connection.QueryAsync<SN_Proveedor_ConfigPeso>(
                            "[dbo].[USP_SN_AREA_EVALUADORA_MNT]",
                            parametrosConFiltro,
                            commandType: CommandType.StoredProcedure
                        );
                    }
                    catch (SqlException ex) when (ex.Number == 8144 || ex.Message.Contains("too many arguments") || ex.Message.Contains("demasiados argumentos"))
                    {
                        // Fallback a v2.2 si aún no se ha ejecutado el script v2.3 en BD
                    }
                }

                var parametrosBase = new { OPCION = "L" };
                return await connection.QueryAsync<SN_Proveedor_ConfigPeso>(
                    "[dbo].[USP_SN_AREA_EVALUADORA_MNT]",
                    parametrosBase,
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public async Task<(int Codigo, string Mensaje, int IdGenerado)> GuardarConfigPeso(SN_Proveedor_ConfigPeso config)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();

                parametros.Add("@OPCION", "G");
                parametros.Add("@ID_AREA", config.Id_Area);
                parametros.Add("@DES_AREA", config.Des_Area);
                parametros.Add("@NUM_PESO", config.Num_Peso);
                parametros.Add("@NUM_UMBRAL_MINIMO", config.Num_Umbral_Minimo);
                parametros.Add("@FLG_ACTIVO", config.Flg_Activo);
                parametros.Add("@COD_USUARIO", string.IsNullOrWhiteSpace(config.Cod_Usuario) ? "SYSTEM" : config.Cod_Usuario);

                var res = await connection.QueryFirstOrDefaultAsync<dynamic>(
                    "[dbo].[USP_SN_AREA_EVALUADORA_MNT]",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

                return ExtractTransacResult(res);
            }
        }
    }
}

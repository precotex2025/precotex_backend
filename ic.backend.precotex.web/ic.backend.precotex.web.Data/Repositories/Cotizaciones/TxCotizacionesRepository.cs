using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using ic.backend.precotex.web.Entity.Entities.Cotizaciones;
using ic.backend.precotex.web.Data.Repositories.Implementation.Cotizaciones;
using ic.backend.precotex.web.Entity.Entities;

namespace ic.backend.precotex.web.Data.Repositories.Cotizaciones
{
    public class TxCotizacionesRepository: ITxCotizacionesRepository
    {
        private readonly string _connectionString;

        public TxCotizacionesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnection")!;
        }

        #region ListaUnidadNegocio

        public async Task<IEnumerable<ComboGral>?> ListaUnidadNegocio()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<ComboGral>(
                    "[dbo].[sp_UnidadNegocio_Listar]"
                    , commandType: CommandType.StoredProcedure
                    );
                return result;
            }
        }

        #endregion

        #region ListaUnidadNegocioTipo

        public async Task<IEnumerable<ComboGral>?> ListaUnidadNegocioTipo(int Id_Unidad_NegocioKey)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();
                parametros.Add("@Id_Unidad_NegocioKey", Id_Unidad_NegocioKey);

                var result = await connection.QueryAsync<ComboGral>(
                    "[dbo].[sp_TipoUnidadNegocio_Listar]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                    );

                return result;
            }
        }

        #endregion

        #region ListaTelas

        public async Task<IEnumerable<Tx_Cotizaciones_Telas>?> ListaTelas(string Cod_Tela)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();
                parametros.Add("@Cod_Tela", Cod_Tela);

                var result = await connection.QueryAsync<Tx_Cotizaciones_Telas>(
                    "[dbo].[sp_Tx_Tela_Buscar]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                    );

                return result;
            }
        }

        #endregion

        #region RutaXCodTela

        public async Task<IEnumerable<Tx_Cotizaciones_Rutas>?> RutaXCodTela(string Cod_Tela)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();
                parametros.Add("@cod_tela", Cod_Tela);

                var result = await connection.QueryAsync<Tx_Cotizaciones_Rutas>(
                        "[dbo].[sp_Tx_Ruta_Tela_Cabecera_Buscar]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                    );
                return result;
            }
        }

        #endregion

        #region ValidaColorExiste

        public async Task<IEnumerable<ComboGral>?> ValidaColorExiste(string Cod_Color)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();
                parametros.Add("@Cod_Color", Cod_Color);

                var result = await connection.QueryAsync<ComboGral>(
                    "[dbo].[sp_Lb_Color_Buscar]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                    );

                return result;
            }
        }

        #endregion

        #region ListaPrecioXColor

        public async Task<IEnumerable<Tx_PreciosColor>?> ListaPrecioXColor(string Tipo_Busqueda, int Pro_Cen_Cos, string Tipo, string Cod_Cliente_Tex, string Cod_Tela, string Cod_Ruta, string? Cod_Color)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();
                parametros.Add("@Tipo_Busqueda", Tipo_Busqueda);
                parametros.Add("@Pro_Cen_Cos", Pro_Cen_Cos);
                parametros.Add("@Cod_Tipo", Tipo);
                parametros.Add("@Cod_Cliente_Tex", Cod_Cliente_Tex);
                parametros.Add("@Cod_Tela", Cod_Tela);
                parametros.Add("@Cod_Ruta", Cod_Ruta);
                parametros.Add("@Cod_Color", Cod_Color == null ? "" : Cod_Color);

                var result = await connection.QueryAsync<Tx_PreciosColor>(
                    "[dbo].[sp_ListaPrecioXColor]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                    );

                return result;
            }
        }

        #endregion

        #region ListarProcesosExportacion

        public async Task<IEnumerable<Tx_Cotizaciones>?> ListarProcesosExportacion(int Pro_Cen_Cos, string Tipo, string Cod_Cliente_Tex, string Cod_Tela, string Cod_Ruta, string? Cod_Color, decimal precio, int tiempo, int IdCotizacion_Cab)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();
                parametros.Add("@Pro_Cen_Cos", Pro_Cen_Cos);
                parametros.Add("@Tipo", Tipo);
                parametros.Add("@Cod_Cliente_Tex", Cod_Cliente_Tex);
                parametros.Add("@Cod_Tela", Cod_Tela);
                parametros.Add("@Cod_Ruta", Cod_Ruta);
                parametros.Add("@Cod_Color", Cod_Color == null ? "" : Cod_Color);
                parametros.Add("@p_Precio", precio);
                parametros.Add("@p_Tiempo", tiempo);
                parametros.Add("@IdCotizacion_Cab", IdCotizacion_Cab);

                var result = await connection.QueryAsync<Tx_Cotizaciones>(
                        "[dbo].[PA_Tx_Cotizaciones_Procesos_S0001]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        #endregion

        #region ProcesoCotizacion

        public async Task<(int Codigo, string Mensaje)> ProcesoCotizacion(Tx_Cotizaciones_Cab tx_Cotizaciones_Cab, List<Tx_Cotizaciones_Det> detalle, string sTipoTransac)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("Tx_Cotizaciones_Mant", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", sTipoTransac);
                    cmd.Parameters.AddWithValue("@IdCotizacion_Cab", tx_Cotizaciones_Cab.IdCotizacion_Cab);
                    cmd.Parameters.AddWithValue("@Pro_Id", (object?)tx_Cotizaciones_Cab.Num_Cotizacion ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Cen_Cos_Cod", tx_Cotizaciones_Cab.Id_Unidad_NegocioKey);
                    cmd.Parameters.AddWithValue("@Cod_Tipo", tx_Cotizaciones_Cab.Cod_Tipo_Orden_tinto);
                    cmd.Parameters.AddWithValue("@Cod_Cliente_Tex", tx_Cotizaciones_Cab.Cod_Cliente_Tex);
                    cmd.Parameters.AddWithValue("@Cod_Tela", tx_Cotizaciones_Cab.Cod_Tela);
                    cmd.Parameters.AddWithValue("@Cod_Ruta", tx_Cotizaciones_Cab.Cod_Ruta);
                    cmd.Parameters.AddWithValue("@Cod_Color", tx_Cotizaciones_Cab.Cod_Color);
                    cmd.Parameters.AddWithValue("@Cod_RecetaAcabado", tx_Cotizaciones_Cab.Cod_RecetaAcabado);
                    cmd.Parameters.AddWithValue("@Tiempo_Referencia", tx_Cotizaciones_Cab.Tiempo_Referencia);
                    cmd.Parameters.AddWithValue("@Precio_Referencia", tx_Cotizaciones_Cab.Precio_Referencia);
                    cmd.Parameters.AddWithValue("@SDC_Referencia", tx_Cotizaciones_Cab.SDC_Referencia);
                    cmd.Parameters.AddWithValue("@Flg_Estatus", tx_Cotizaciones_Cab.Flg_Estatus);
                    cmd.Parameters.AddWithValue("@Usu_Registro", tx_Cotizaciones_Cab.Usu_Registro);

                    // Parámetro tabla para N detalles
                    var dtDetalles = new DataTable();
                    dtDetalles.Columns.Add("Pro_Hover", typeof(string));
                    dtDetalles.Columns.Add("Pro_Factor", typeof(int));
                    dtDetalles.Columns.Add("Pro_Cos_Kg", typeof(decimal));
                    dtDetalles.Columns.Add("Pro_Tot", typeof(decimal));
                    dtDetalles.Columns.Add("Pro_Tot_Com", typeof(decimal));
                    dtDetalles.Columns.Add("Pro_Aju", typeof(decimal));
                    dtDetalles.Columns.Add("Pro_Cotizacion", typeof(decimal));
                    dtDetalles.Columns.Add("Pro_Por", typeof(decimal));
                    dtDetalles.Columns.Add("Pro_Tip", typeof(string));
                    dtDetalles.Columns.Add("Observacion", typeof(string));
                    dtDetalles.Columns.Add("Nivel", typeof(string));
                    dtDetalles.Columns.Add("cod_Subtotal", typeof(int));
                    dtDetalles.Columns.Add("parteEntera", typeof(int));
                    dtDetalles.Columns.Add("parteDecimal", typeof(int));
                    dtDetalles.Columns.Add("isParent", typeof(bool));
                    dtDetalles.Columns.Add("isChild", typeof(bool));
                    dtDetalles.Columns.Add("tieneHijos", typeof(bool));
                    dtDetalles.Columns.Add("cod_ProcesoPadre", typeof(string));
                    dtDetalles.Columns.Add("cod_Proceso_Tex", typeof(string));
                    dtDetalles.Columns.Add("Cod_SubProceso", typeof(string));

                    foreach (var det in detalle)
                    {
                        dtDetalles.Rows.Add(
                            det.Pro_Hover,
                            det.Pro_Factor,
                            det.Pro_Cos_Kg,
                            det.Pro_Tot,
                            det.Pro_Tot_Com,
                            det.Pro_Aju,
                            det.Pro_Cotizacion,
                            det.Pro_Por,
                            det.Pro_Tip,
                            det.Observacion,
                            det.Nivel,
                            det.cod_Subtotal,
                            det.parteEntera,
                            det.parteDecimal,
                            det.isParent,
                            det.isChild,
                            det.tieneHijos,
                            det.cod_ProcesoPadre,
                            det.cod_Proceso_Tex,
                            det.Cod_SubProceso
                        );
                    }

                    var paramDetalles = cmd.Parameters.AddWithValue("@Detalles", dtDetalles);
                    paramDetalles.SqlDbType = SqlDbType.Structured;
                    paramDetalles.TypeName = "CotizacionDetType";

                    // Parámetros OUTPUT
                    var paramCodigo = new SqlParameter("@Codigo", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    var paramResultado = new SqlParameter("@Resultado", SqlDbType.VarChar, 200) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(paramCodigo);
                    cmd.Parameters.Add(paramResultado);

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    int codigo = (int)paramCodigo.Value;
                    string mensaje = (string)paramResultado.Value;

                    return (codigo, mensaje);
                }
            }
            catch (Exception ex)
            {
                // Captura cualquier error y devuelve un resultado controlado
                return (-1, $"Error en ProcesoCotizacion: {ex.Message}");
            }
        }

        #endregion

        #region ObtenerNuevoCorrelativoVersion

        public async Task<Tx_Cotizaciones_Cab?> ObtenerNuevoCorrelativoVersion(int Id_Unidad_NegocioKey, string Cod_Tipo_Orden_tinto, string Cod_Cliente_Tex, string Cod_Tela, string Cod_Ruta, string? Cod_Color, string? SDC_Referencia)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();
                parametros.Add("@Id_Unidad_NegocioKey", Id_Unidad_NegocioKey);
                parametros.Add("@Cod_Tipo_Orden_tinto", Cod_Tipo_Orden_tinto);
                parametros.Add("@Cod_Cliente_Tex", Cod_Cliente_Tex);
                parametros.Add("@Cod_Tela", Cod_Tela);
                parametros.Add("@Cod_Ruta", Cod_Ruta);
                parametros.Add("@Cod_Color", Cod_Color == null ? "" : Cod_Color);
                parametros.Add("@SDC_Referencia", SDC_Referencia == null ? "" : SDC_Referencia);

                var result = await connection.QueryFirstOrDefaultAsync<Tx_Cotizaciones_Cab>(
                        "[dbo].[sp_Tx_Cotizacion_ObtenerNuevoCorrelativoVersion]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        #endregion




        #region ListaColoresXCliente

        public async Task<IEnumerable<ComboGral>?> ListaColoresXCliente(string Cod_Cliente)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();
                parametros.Add("@Cod_Cliente", Cod_Cliente);

                var result = await connection.QueryAsync<ComboGral>(
                    "[dbo].[sp_ObtieneCodigoColorXCliente]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                    );

                return result;
            }
        }

        #endregion

        #region ListaRecetasAntipilling

        public async Task<IEnumerable<ComboGral>?> ListaRecetasAntipilling()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<ComboGral>(
                    "[dbo].[sp_ListaRecetas_Antipilling]"
                    , commandType: CommandType.StoredProcedure
                    );
                return result;
            }
        }

        #endregion

        




        

        

        

        

        

        

        

        


















































        public async Task<IEnumerable<Tx_Cotizaciones>?> ListarProcesosExportacionFooter(int Pro_Cen_Cos)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                
                var parametros = new DynamicParameters();

                parametros.Add("@Pro_Cen_Cos", Pro_Cen_Cos);

                var result = await connection.QueryAsync<Tx_Cotizaciones>(
                    "[dbo].[PA_Tx_Cotizaciones_Procesos_S0002]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                );

                return result;
            }   
        }

        //LISTAR PROCESOS POR RUTA
        public async Task<IEnumerable<Tx_Cotizaciones_Rutas_Detalle>?> RutaXCodTelaDetalle(string Cod_Tela, string Cod_Ruta)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                parametros.Add("@Cod_Tela", Cod_Tela);
                parametros.Add("@Cod_Ruta", Cod_Ruta);

                var result = await connection.QueryAsync<Tx_Cotizaciones_Rutas_Detalle>(
                        "[dbo].[PA_BuscarRutaTextilDetV1_S0001]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                    );

                return result;
            }
        }

        

        //LISTAR CENTRO DE COSTOS
        public async Task<IEnumerable<Tx_Cotizaciones_Centro_Costo>?> ListaCentroCosto()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<Tx_Cotizaciones_Centro_Costo>(
                    "[dbo].[PA_Tx_Cotizaciones_Centro_Costo_S0001]"
                    , commandType: CommandType.StoredProcedure
                    );
                return result;
            }
        }

        

        

        

        public async Task<IEnumerable<ComboGral>?> ListaIntensidad(int Id_Unidad_NegocioKey)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                parametros.Add("@Id_Unidad_NegocioKey", Id_Unidad_NegocioKey);

                var result = await connection.QueryAsync<ComboGral>(
                    "[dbo].[PA_Tx_ListaIntensidad_S0001]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                    );

                return result;
            }
        }

        public async Task<IEnumerable<Tx_HilosTel>?> ListaHiladoxTela(string Cod_Tela)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                parametros.Add("@Cod_Tela", Cod_Tela);

                var result = await connection.QueryAsync<Tx_HilosTel>(
                    "[dbo].[PA_Tx_ListaHilado_S0001]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                    );

                return result;
            }
        }

        

        

        

        

        public async Task<IEnumerable<Tx_Cotizaciones_Cab>?> ValidaExistenciaHistorialxColor(int Pro_Cen_Cos, string Tipo, string Cod_Cliente_Tex, string Cod_Tela, string Cod_Ruta, string? Cod_Color, string? Cod_Receta)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                parametros.Add("@Pro_Cen_Cos", Pro_Cen_Cos);
                parametros.Add("@Cod_Tipo", Tipo);
                parametros.Add("@Cod_Cliente_Tex", Cod_Cliente_Tex);
                parametros.Add("@Cod_Tela", Cod_Tela);
                parametros.Add("@Cod_Ruta", Cod_Ruta);
                parametros.Add("@Cod_Color", Cod_Color == null ? "" : Cod_Color);
                parametros.Add("@Cod_recetaAcabado", Cod_Receta == null ? "" : Cod_Receta);

                var result = await connection.QueryAsync<Tx_Cotizaciones_Cab>(
                        "[dbo].[sp_ValidaExistenciaHistorialxColor]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        #region ListaCabecerasCotizacion

        public async Task<IEnumerable<Tx_Cotizaciones_Cab>?> ListaCabecerasCotizacion(int Pro_Cen_Cos, string Cod_Tipo, string Cod_Cliente_Tex, string Cod_Tela, string Cod_Ruta, string? Cod_Color, string? SDC_Referencia)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();
                parametros.Add("@Pro_Cen_Cos", Pro_Cen_Cos);
                parametros.Add("@Tipo", Cod_Tipo);
                parametros.Add("@Cod_Cliente_Tex", Cod_Cliente_Tex);
                parametros.Add("@Cod_Tela", Cod_Tela);
                parametros.Add("@Cod_Ruta", Cod_Ruta);
                parametros.Add("@Cod_Color", Cod_Color == null ? "" : Cod_Color);
                parametros.Add("@SDC_Referencia", SDC_Referencia == null ? "" : SDC_Referencia);

                var result = await connection.QueryAsync<Tx_Cotizaciones_Cab>(
                        "[dbo].[Tx_Cotizaciones_Cab_Listar]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        #endregion

        #region ListaDetalleCotizacionXVersion

        public async Task<IEnumerable<Tx_Cotizaciones>?> ListaDetalleCotizacionXVersion(int IdCotizacion_Cab, int Num_Version)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();
                parametros.Add("@IdCotizacion_Cab", IdCotizacion_Cab);
                parametros.Add("@Num_Version", Num_Version);

                var result = await connection.QueryAsync<Tx_Cotizaciones>(
                        "[dbo].[Tx_Cotizaciones_ListaDetalleCotizacionXVersion]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        #endregion

        #region ListaDetalleCotizacionXFiltros

        public async Task<IEnumerable<Tx_Cotizaciones>?> ListaDetalleCotizacionXFiltros(int Pro_Cen_Cos, string Cod_Tipo, string Cod_Cliente_Tex, string Cod_Tela, string Cod_Ruta, string? Cod_Color, string? SDC_Referencia, decimal precio, int tiempo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();
                parametros.Add("@Pro_Cen_Cos", Pro_Cen_Cos);
                parametros.Add("@Tipo", Cod_Tipo);
                parametros.Add("@Cod_Cliente_Tex", Cod_Cliente_Tex);
                parametros.Add("@Cod_Tela", Cod_Tela);
                parametros.Add("@Cod_Ruta", Cod_Ruta);
                parametros.Add("@Cod_Color", Cod_Color == null ? "" : Cod_Color);
                parametros.Add("@SDC_Referencia", SDC_Referencia == null ? "" : SDC_Referencia);
                parametros.Add("@p_Precio", precio);
                parametros.Add("@p_Tiempo", tiempo);

                var result = await connection.QueryAsync<Tx_Cotizaciones>(
                        "[dbo].[Tx_Cotizaciones_ListaDetalleCotizacionXFiltros]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        #endregion











    }
}

using Dapper;
using ic.backend.precotex.web.Entity.Entities.Laboratorio;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ic.backend.precotex.web.Entity.Entities.Cotizaciones;
using ic.backend.precotex.web.Data.Repositories.Implementation.Cotizaciones;
using ic.backend.precotex.web.Entity.Entities.Memorandum;
using ic.backend.precotex.web.Entity.Entities;

namespace ic.backend.precotex.web.Data.Repositories.Cotizaciones
{
    public class TxCotizacionesRepository: ITxCotizacionesRepository
    {
        private readonly string _connectionString;

        //DECLARAMOS CADENA DE CONEXION
        public TxCotizacionesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnection")!;
        }

        public async Task<IEnumerable<Tx_Cotizaciones>?> ListarProcesosExportacion(int Pro_Cen_Cos, string Tipo, string Cod_Cliente_Tex, string Cod_Tela, string Cod_Ruta, string? Cod_Color)
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

                var result = await connection.QueryAsync<Tx_Cotizaciones>(
                        "[dbo].[PA_Tx_Cotizaciones_Procesos_S0001]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

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

        //LISTAR RUTAS POR COD TELA
        public async Task<IEnumerable<Tx_Cotizaciones_Rutas>?> RutaXCodTela(string Cod_Tela)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                parametros.Add("@cod_tela", Cod_Tela);

                var result = await connection.QueryAsync<Tx_Cotizaciones_Rutas>(
                        "[dbo].[tx_sm_muestra_Tela_DatTecnicos_cabecera]"
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

        //LISTAR CODIGO Y DESCRIPCION DE TELA
        public async Task<IEnumerable<Tx_Cotizaciones_Telas>?> ListaTelas(string Cod_Tela)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                parametros.Add("@Cod_Tela", Cod_Tela);

                var result = await connection.QueryAsync<Tx_Cotizaciones_Telas>(
                    "[dbo].[PA_Tx_Tela_S0001]"
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

        public async Task<(int Codigo, string Mensaje)> ProcesoCotizacion(Tx_Cotizaciones_Cab tx_Cotizaciones_Cab, List<Tx_Cotizaciones_Det> detalle, string sTipoTransac)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("PA_Tx_CotizacionesCab_S0001", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros de acción
                    cmd.Parameters.AddWithValue("@Accion", sTipoTransac);

                    // Parámetros CAB
                    cmd.Parameters.AddWithValue("@IdCotizacion_Cab", tx_Cotizaciones_Cab.IdCotizacion_Cab);
                    cmd.Parameters.AddWithValue("@Pro_Id", (object?)tx_Cotizaciones_Cab.Pro_Id ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Cen_Cos_Cod", tx_Cotizaciones_Cab.Cen_Cos_Cod);
                    cmd.Parameters.AddWithValue("@Cod_Tipo", tx_Cotizaciones_Cab.Cod_Tipo);
                    cmd.Parameters.AddWithValue("@Cod_Cliente_Tex", tx_Cotizaciones_Cab.Cod_Cliente_Tex);
                    cmd.Parameters.AddWithValue("@Cod_Tela", tx_Cotizaciones_Cab.Cod_Tela);
                    cmd.Parameters.AddWithValue("@Cod_Ruta", tx_Cotizaciones_Cab.Cod_Ruta);
                    cmd.Parameters.AddWithValue("@Cod_Color", tx_Cotizaciones_Cab.Cod_Color);
                    cmd.Parameters.AddWithValue("@Flg_Estatus", tx_Cotizaciones_Cab.Flg_Estatus);
                    cmd.Parameters.AddWithValue("@Usu_Registro", tx_Cotizaciones_Cab.Usu_Registro);

                    // Parámetro tabla para N detalles
                    var dtDetalles = new DataTable();
                    dtDetalles.Columns.Add("Pro_Cen_Cos", typeof(int));
                    dtDetalles.Columns.Add("Pro_Hover", typeof(string));
                    dtDetalles.Columns.Add("Pro_Des", typeof(string));
                    dtDetalles.Columns.Add("Pro_Factor", typeof(int));
                    dtDetalles.Columns.Add("Pro_Cos_Kg", typeof(decimal));
                    dtDetalles.Columns.Add("Pro_Tot", typeof(decimal));
                    dtDetalles.Columns.Add("Pro_Aju", typeof(decimal));
                    dtDetalles.Columns.Add("Pro_Cotizacion", typeof(decimal));
                    dtDetalles.Columns.Add("Pro_Tip", typeof(string));
                    dtDetalles.Columns.Add("Pro_Tot_Com", typeof(decimal));
                    dtDetalles.Columns.Add("Pro_Por", typeof(decimal));
                    dtDetalles.Columns.Add("Flg_Estatus", typeof(string));
                    dtDetalles.Columns.Add("Usu_Registro", typeof(string));

                    foreach (var det in detalle)
                    {
                        dtDetalles.Rows.Add(
                            det.Pro_Cen_Cos,
                            det.Pro_Hover,
                            det.Pro_Des,
                            det.Pro_Factor,
                            det.Pro_Cos_Kg,
                            det.Pro_Tot,
                            det.Pro_Aju,
                            det.Pro_Cotizacion,
                            det.Pro_Tip,
                            det.Pro_Tot_Com,
                            det.Pro_Por,
                            det.Flg_Estatus,
                            det.Usu_Registro
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
                return (-1, $"Error en ProcesoCotizacion: { ex.Message }");
            }
        }

        public async Task<IEnumerable<ComboGral>?> ValidaColorExiste(string Cod_Color)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                parametros.Add("@Cod_Color", Cod_Color);

                var result = await connection.QueryAsync<ComboGral>(
                    "[dbo].[PA_Tx_ValidaColorExiste_S0001]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                    );

                return result;
            }
        }
    }
}

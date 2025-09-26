using Dapper;
using ic.backend.precotex.web.Data.Repositories.Implementation.Memorandum;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.Memorandum;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Memorandum
{
    public class TxProcesoMemorandumRepository : ITxProcesoMemorandumRepository
    {
        private readonly string _connectionString;

        public TxProcesoMemorandumRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnection")!;
        }

        public async Task<(int Codigo, string Mensaje)> AvanzaEstadoMemorandum(string sCodUsuario, string sNumMemo, string sObservaciones)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                // Parametros de SQL
                parametros.Add("@Cod_Usuario", sCodUsuario);
                parametros.Add("@Num_Memo", sNumMemo);
                parametros.Add("@Observaciones", sObservaciones);

                // Parámetros de salida
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                // Ejecutar el procedimiento almacenado
                try
                {
                    connection.Execute(
                        "[dbo].[TX_AVANZA_ESTADO_MEMORANDUM_PTX]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );
                }
                catch (Exception ex) { }

                //Obtener los valores de salida
                var codigo = parametros.Get<int>("@Codigo");
                var mensaje = parametros.Get<string>("@sMsj");

                return (codigo, mensaje);
            }
        }

        public async Task<(int Codigo, string Mensaje)> DevolverMemorandum(Tx_Memorandum tx_Memorandum)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                // Parametros de SQL
                parametros.Add("@Num_Memo", tx_Memorandum.Num_Memo);
                parametros.Add("@Cod_Usuario_Emisor", tx_Memorandum.Cod_Usuario_Emisor);
                parametros.Add("@Cod_Usuario_Receptor", tx_Memorandum.Cod_Usuario_Receptor);
                parametros.Add("@Num_Planta_Origen", tx_Memorandum.Num_Planta_Origen);
                parametros.Add("@Num_Planta_Destino", tx_Memorandum.Num_Planta_Destino);
                parametros.Add("@Cod_Usuario_Seguridad_Emisor", tx_Memorandum.Cod_Usuario_Seguridad_Emisor);
                parametros.Add("@Cod_Usuario_Seguridad_Receptor", tx_Memorandum.Cod_Usuario_Seguridad_Receptor);
                parametros.Add("@Cod_Tipo_Memo", tx_Memorandum.Cod_Tipo_Memo);
                parametros.Add("@Cod_Motivo_Memo", tx_Memorandum.Cod_Motivo_Memo);

                // Parámetros de salida
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                // Ejecutar el procedimiento almacenado
                try
                {
                    connection.Execute(
                        "[dbo].[TX_DEVOLVER_MEMORANDUM_PTX]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );
                }
                catch (Exception ex) { }

                //Obtener los valores de salida
                var codigo = parametros.Get<int>("@Codigo");
                var mensaje = parametros.Get<string>("@sMsj");

                return (codigo, mensaje);
            }
        }

        public async Task<IEnumerable<Tx_Memorandum_Detalle_Exportacion>?> ExportarInformacionMemorandumDetalle(DateTime FecIni, DateTime FecFin)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    FEC_INICIO = FecIni,
                    FEC_FIN = FecFin

                };

                var result = await connection.QueryAsync<Tx_Memorandum_Detalle_Exportacion>(
                     "[dbo].[TX_EXPORTAR_MEMORANDUM]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<IEnumerable<Tx_Movimiento_Memorandum>?> HistorialMovimientoMemorandum(string sNumMemo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Num_Memo = sNumMemo

                };

                var result = await connection.QueryAsync<Tx_Movimiento_Memorandum>(
                     "[dbo].[TX_HISTORIAL_MOVIMIENTOS_MEMO]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<IEnumerable<Tx_Material_Memorandum>?> Materiales()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<Tx_Material_Memorandum>(
                     "[dbo].[TX_LISTAR_MATERIAL_MEMORANDUM_PTX]"
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<IEnumerable<Tx_Motivo_Memorandum>?> Motivos()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<Tx_Motivo_Memorandum>(
                     "[dbo].[TX_LISTAR_MOTIVOS_MEMORANDUM_PTX]"
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<IEnumerable<Tx_Roles>?> ObtenerInfoUsuarioMemorandum(string sCodUsuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Cod_Usuario = sCodUsuario
                };

                var result = await connection.QueryAsync<Tx_Roles>(
                     "[dbo].[TX_INFO_USUARIO_MEMORANDUM]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<IEnumerable<Tx_Transicion_Memorandum>?> ObtenerPermisosMemorandum(string sCodUsuario, string sNumMemo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Usuario = sCodUsuario,
                    Num_Memo = sNumMemo

                };

                var result = await connection.QueryAsync<Tx_Transicion_Memorandum>(
                     "[dbo].[SP_OBTENER_PERMISOS_MEMO]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<IEnumerable<Tx_Roles>?> ObtenerRolUsuarioMemorandum(string sCodUsuario, string sNumMemo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Usuario = sCodUsuario,
                    Num_Memo = sNumMemo

                };

                var result = await connection.QueryAsync<Tx_Roles>(
                     "[dbo].[TX_OBTENER_ROL_LOGICO_MEMORANDUM]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<IEnumerable<Tx_Memorandum_Detalle>?> ObtieneDetalleMemorandumByNumMemo(string NumMemo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Num_Memo = NumMemo
                };

                var result = await connection.QueryAsync<Tx_Memorandum_Detalle>(
                     "[dbo].[TX_OBTIENE_DETALLE_MEMORANDUM_PTX]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<IEnumerable<Tx_Memorandum>?> ObtieneInformacionMemorandum(DateTime FecIni, DateTime FecFin, string NumMemo, string codUsuario, string CodPlantaGarita)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var parametros = new
                    {
                        FEC_INICIO = FecIni,
                        FEC_FIN = FecFin,
                        NUM_MEMO = NumMemo,
                        COD_USUARIO = codUsuario,
                        COD_PLANTA_GARITA = CodPlantaGarita
                    };

                    var result = await connection.QueryAsync<Tx_Memorandum>(
                         "[dbo].[TX_LISTAR_MEMORANDUM_PTX]",
                         parametros,
                         commandType: System.Data.CommandType.StoredProcedure
                    );

                    return result;
                }
            }
            catch (SqlException sqlEx)
            {
                // Errores específicos de SQL
                Console.WriteLine($"Error SQL: {sqlEx.Message}");
                throw; // puedes relanzar o manejar como quieras
            }
            catch (Exception ex)
            {
                // Errores generales
                Console.WriteLine($"Error general: {ex.Message}");
                throw;
            }
            /*
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    FEC_INICIO = FecIni,
                    FEC_FIN = FecFin,
                    NUM_MEMO = NumMemo,
                    COD_USUARIO = codUsuario,
                    COD_PLANTA_GARITA = CodPlantaGarita
                };

                var result = await connection.QueryAsync<Tx_Memorandum>(
                     "[dbo].[TX_LISTAR_MEMORANDUM_PTX]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
            */
        }

        public async Task<IEnumerable<Tx_Memorandum>?> ObtieneInformacionMemorandumDetalle(string NumMemo)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var parametros = new
                    {
                        NUM_MEMO = NumMemo
                    };

                    var result = await connection.QueryAsync<Tx_Memorandum>(
                         "[dbo].[TX_OBTENER_MEMORANDUM_DETALLADO_PTX]",
                         parametros,
                         commandType: System.Data.CommandType.StoredProcedure
                    );

                    return result;
                }
            }
            catch (SqlException sqlEx)
            {
                // Errores específicos de SQL
                Console.WriteLine($"Error SQL: {sqlEx.Message}");
                throw; // puedes relanzar o manejar como quieras
            }
            catch (Exception ex)
            {
                // Errores generales
                Console.WriteLine($"Error general: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Tx_Memorandum_Linea_Tiempo>?> ObtieneLineaTempoMemorandum(string NumMemo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Num_Memo = NumMemo
                };

                var result = await connection.QueryAsync<Tx_Memorandum_Linea_Tiempo>(
                     "[dbo].[SP_LINEA_TIEMPO_MEMORANDUM]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<IEnumerable<Sg_Planta>?> Plantas()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<Sg_Planta>(
                     "[dbo].[TX_LISTAR_PLANTA_MEMORANDUM_PTX]"
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<(int Codigo, string Mensaje)> ProcesoMntoMemorandum(Tx_Memorandum tx_Memorandum, List<Tx_Memorandum_Detalle> detalle, string sTipoTransac)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                // Parametros de SQL
                parametros.Add("@Num_Memo", tx_Memorandum.Num_Memo);
                parametros.Add("@Cod_Usuario_Emisor", tx_Memorandum.Cod_Usuario_Emisor);
                parametros.Add("@Cod_Usuario_Receptor", tx_Memorandum.Cod_Usuario_Receptor);
                parametros.Add("@Num_Planta_Origen", tx_Memorandum.Num_Planta_Origen);
                parametros.Add("@Num_Planta_Destino", tx_Memorandum.Num_Planta_Destino);
                parametros.Add("@Cod_Usuario_Seguridad_Emisor", tx_Memorandum.Cod_Usuario_Seguridad_Emisor);
                parametros.Add("@Cod_Usuario_Seguridad_Receptor", tx_Memorandum.Cod_Usuario_Seguridad_Receptor);
                parametros.Add("@Cod_Tipo_Memo", tx_Memorandum.Cod_Tipo_Memo);
                parametros.Add("@Cod_Motivo_Memo", tx_Memorandum.Cod_Motivo_Memo);

                //Nuevos Campos
                parametros.Add("@Cod_Tipo_Movimiento", tx_Memorandum.Cod_Tipo_Movimiento);
                parametros.Add("@Datos_Externo", tx_Memorandum.Datos_Externo);
                parametros.Add("@Direccion_Externo", tx_Memorandum.Direccion_Externo);

                // Parámetro tipo tabla (TVP)
                var tvp = CrearDataTable(detalle);
                parametros.Add("@Detalle", tvp.AsTableValuedParameter("dbo.TVP_MEMORANDUM_DETALLE"));
                parametros.Add("@Accion", sTipoTransac);

                // Parámetros de salida
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                // Ejecutar el procedimiento almacenado
                try
                {
                    connection.Execute(
                        "[dbo].[TX_PROCESO_MNTO_MEMORANDUM_PTX]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );
                }
                catch (Exception ex) { }

                //Obtener los valores de salida
                var codigo = parametros.Get<int>("@Codigo");
                var mensaje = parametros.Get<string>("@sMsj");

                return (codigo, mensaje);
            }
        }

        public async Task<(int Codigo, string Mensaje)> RevertirEstadoMemorandum(string sCodUsuario, string sNumMemo, string sObservaciones)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                // Parametros de SQL
                parametros.Add("@Cod_Usuario", sCodUsuario);
                parametros.Add("@Num_Memo", sNumMemo);
                parametros.Add("@Observaciones", sObservaciones);

                // Parámetros de salida
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                // Ejecutar el procedimiento almacenado
                try
                {
                    connection.Execute(
                        "[dbo].[TX_REVERTIR_ESTADO_MEMORANDUM_PTX]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );
                }
                catch (Exception ex) { }

                //Obtener los valores de salida
                var codigo = parametros.Get<int>("@Codigo");
                var mensaje = parametros.Get<string>("@sMsj");

                return (codigo, mensaje);
            }
        }

        public async Task<IEnumerable<Tx_Tipo_Memorandum>?> TipoMemorandum()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<Tx_Tipo_Memorandum>(
                     "[dbo].[TX_LISTAR_TIPO_MEMORANDUM_PTX]"
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<IEnumerable<SEG_Usuarios>?> Usuario(string Cod_Trabajador, string Tip_Trabajador)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Cod_Trabajador = Cod_Trabajador,
                    Tip_Trabajador = Tip_Trabajador
                };

                var result = await connection.QueryAsync<SEG_Usuarios>(
                     "[dbo].[TX_OBTENER_INFORMACION_USUARIO]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        private DataTable CrearDataTable(List<Tx_Memorandum_Detalle> detalles)
        {
            var table = new DataTable();
            table.Columns.Add("Num_Memo_Detalle", typeof(string));
            table.Columns.Add("Num_Memo", typeof(string));
            //table.Columns.Add("Cod_Material_Memo", typeof(string));
            table.Columns.Add("Glosa", typeof(string));
            table.Columns.Add("Cantidad", typeof(string));
            table.Columns.Add("Flg_Estatus", typeof(string));
            table.Columns.Add("Usu_Registro", typeof(string));
            

            foreach (var d in detalles)
            {
                table.Rows.Add(
                    d.Num_Memo_Detalle,
                    d.Num_Memo,
                    //d.Cod_Material_Memo,
                    d.Glosa,
                    d.Cantidad,
                    d.Flg_Estatus,
                    d.Usu_Registro
                    );
            }

            return table;

        }
    }
}

using Dapper;
using ic.backend.precotex.web.Data.Repositories.Implementation.RetiroRepuestos;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.RetiroRepuestos;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph.Drives.Item.Items.Item.Workbook.Names.Item.RangeNamespace.ColumnsBeforeWithCount;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ZXing;

namespace ic.backend.precotex.web.Data.Repositories.RetiroRepuestos
{
    public class TxRetiroRepuestosRepository: ITxRetiroRepuestosRepository
    {
        private readonly string _connectionString;

        //DECLARAMOS CADENA DE CONEXION
        public TxRetiroRepuestosRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnection")!;
        }
        /******************************CABECERA**********************************/
        //OBTENER LISTA DE REPUESTOS ENTRE FECHAS
        public async Task<IEnumerable<Tx_Retiro_Repuestos>?> ListaRetiros(DateTime FecIni, DateTime FecFin)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Fec_Ini = FecIni,
                    Fec_Fin = FecFin
                };
                var result = await connection.QueryAsync<Tx_Retiro_Repuestos>(
                        "[dbo].[PA_Lg_RequerimientoAlmacen_WB_S0001]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                );
                return result;
            }            
        }
        //OBTENER LISTA DE REPUESTOS POR NUM_REQUERIMIENTO
        public async Task<IEnumerable<Tx_Retiro_Repuestos>?> ListaRetirosPorNumRequerimiento(int Num_Requerimiento)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Num_Requerimiento = Num_Requerimiento
                };
                var result = await connection.QueryAsync<Tx_Retiro_Repuestos>(
                    "[dbo].[PA_Lg_RequerimientoAlmacen_WB_S0002]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        //INSERTAR DATOS CABECERA
        public async Task<(int Codigo, string Mensaje)> RegistrarRequerimiento(Tx_Retiro_Repuestos tx_Retiro_Repuestos)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();
                //PARAMETROS ENTRADA
                parametros.Add("@Num_Requerimiento", tx_Retiro_Repuestos.Num_Requerimiento);
                //parametros.Add("@Fec_Requerimiento", tx_Retiro_Repuestos.Fec_Requerimiento);
                parametros.Add("@Cod_Seguridad", tx_Retiro_Repuestos.Cod_Seguridad);
                parametros.Add("@Cod_Mantenimiento", tx_Retiro_Repuestos.Cod_Mantenimiento);
                parametros.Add("@Nro_Precinto_Apertura", tx_Retiro_Repuestos.Nro_Precinto_Apertura);
                parametros.Add("@Nro_Precinto_Cierre", "");

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Lg_RequerimientoAlmacen_WB_I0001]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                    );
                }
                catch(Exception ex)
                {
                    
                }

                var Codigo = parametros.Get<int>("@Codigo");
                var mensaje = parametros.Get<string>("@sMsj");
                return (Codigo, mensaje);
            }
        }

        //ACTUALIZA DATOS CABECERA
        public async Task<(int Codigo, string Mensaje)> ActualizarRequerimiento(Tx_Retiro_Repuestos tx_Retiro_Repuestos)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Num_Requerimiento", tx_Retiro_Repuestos.Num_Requerimiento);
                parametros.Add("@Cod_Seguridad", tx_Retiro_Repuestos.Cod_Seguridad);
                parametros.Add("@Cod_Mantenimiento", tx_Retiro_Repuestos.Cod_Mantenimiento);
                parametros.Add("@Nro_Precinto_Apertura", tx_Retiro_Repuestos.Nro_Precinto_Apertura);    
                parametros.Add("@Codigo", 0);    
                parametros.Add("@sMsj", "");    

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Lg_RequerimientoAlmacen_WB_U0001]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                    );
                }
                catch (SqlException ex)
                {
                    ;
                }

                var Codigo = parametros.Get<int>("@Codigo");
                var mensaje = parametros.Get<string>("@sMsj");
                return (Codigo, mensaje);
            }
        }

        //ACTUALIZA DATOS CABECERA -> ACTUALIZA PRECINTO CIERRE
        public async Task<(int Codigo, string Mensaje)> ActualizarRequerimientoPrecintoCierre(Tx_Retiro_Repuestos tx_Retiro_Repuestos)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Num_Requerimiento", tx_Retiro_Repuestos.Num_Requerimiento);
                parametros.Add("@Nro_Precinto_Cierre", tx_Retiro_Repuestos.Nro_Precinto_Cierre);

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Lg_RequerimientoAlmacen_WB_U0002]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                    );
                }
                catch (SqlException ex)
                {
                    ;
                }

                var Codigo = parametros.Get<int>("@Codigo");
                var mensaje = parametros.Get<string>("@sMsj");
                return (Codigo, mensaje);
            }
        }

        /************************************************************************/




        /******************************DETALLE***********************************/
        //OBTENER LISTA DE DETALLE DE RETIRO
        public async Task<IEnumerable<Tx_Retiro_Repuestos_Detalle>?> ListaRetiroDetallePorNumRequerimiento(int Num_Requerimiento)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Num_Requerimiento = Num_Requerimiento
                };

                var result = await connection.QueryAsync<Tx_Retiro_Repuestos_Detalle>(
                    "[dbo].[PA_Lg_RequerimientoAlmacenDetalle_WB_S0001]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                    );
                return result;
            }
        }

        //OBTENER DATOS DEL DETALLE DE UN RETIRO
        public async Task<IEnumerable<Tx_Retiro_Repuestos_Detalle>?> ListaRetiroDetallePorNumReqySecuencia(int Num_Requerimiento, int Nro_Secuencia)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Num_Requerimiento = Num_Requerimiento,
                    Nro_Secuencia = Nro_Secuencia
                };

                var result = await connection.QueryAsync<Tx_Retiro_Repuestos_Detalle>(
                    "[dbo].[PA_Lg_RequerimientoAlmacenDetalle_WB_S0002]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                );
                return result;
            };
        }
        
        //REGISTRAR DETALLE A UN RETIRO
        //public async Task<(int Codigo, string Mensaje)> RegistrarRequerimientoDetalle(Tx_Retiro_Repuestos_Detalle tx_Retiro_Repuestos_Detalle)
        //{

        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        await connection.OpenAsync();
        //        var parametros = new DynamicParameters();
        //        //PARAMETROS DE ENTRADA
        //        parametros.Add("@Num_Requerimiento", tx_Retiro_Repuestos_Detalle.Num_Requerimiento);
        //        parametros.Add("@Cod_Item", tx_Retiro_Repuestos_Detalle.Cod_Item);
        //        //parametros.Add("@Itm_Descripcion", tx_Retiro_Repuestos_Detalle.Itm_Descripcion);
        //        parametros.Add("@Can_Requerida", tx_Retiro_Repuestos_Detalle.Can_Requerida);
        //        //parametros.Add("@Itm_Unidad_Medida", tx_Retiro_Repuestos_Detalle.Itm_Unidad_Medida);
        //        parametros.Add("@Rpt_Cambio", Convert.ToInt32(tx_Retiro_Repuestos_Detalle.Rpt_Cambio));
        //        parametros.Add("@Itm_Foto", tx_Retiro_Repuestos_Detalle.Itm_Foto);

        //        //PARAMETROS DE SALIDA
        //        parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
        //        parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

        //        try
        //        {
        //            //EJECUTAR EL STORED PROCEDURE
        //            connection.Execute(
        //                "[dbo].[PA_Lg_RequerimientoAlmacenDetalle_WB_I0001]"
        //                , parametros
        //                , commandType: CommandType.StoredProcedure
        //                );
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //        var Codigo = parametros.Get<int>("@Codigo");
        //        var mensaje = parametros.Get<string>("@sMsj");
        //        return (Codigo, mensaje);
        //    }
        //}

        public async Task<(int Codigo, string Mensaje)> RegistrarRequerimientoDetalle(string nNum_Requerimiento, string sCod_Item, string nCan_Requerida, string sRpt_Cambio, string nombreArchivo)
        {

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();
                //PARAMETROS DE ENTRADA
                parametros.Add("@Num_Requerimiento", nNum_Requerimiento);
                parametros.Add("@Cod_Item", sCod_Item);
                parametros.Add("@Can_Requerida", nCan_Requerida);
                parametros.Add("@Rpt_Cambio", Convert.ToInt32(sRpt_Cambio));
                parametros.Add("@Itm_Foto", nombreArchivo);

                //PARAMETROS DE SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Lg_RequerimientoAlmacenDetalle_WB_I0001]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                        );
                }
                catch (Exception ex)
                {

                }
                var Codigo = parametros.Get<int>("@Codigo");
                var mensaje = parametros.Get<string>("@sMsj");
                return (Codigo, mensaje);
            }
        }

        //ACTUALIZAR DETALLE DE UN RETIRO

        public async Task<(int Codigo, string Mensaje)> ActualizarRequerimientoDetalle(string nNum_Requerimiento, string nNum_Secuencia ,string nCan_Requerida, string sRpt_Cambio, string sNombreArchivo)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();
                
                //PARAMETROS DE ENTRADA
                parametros.Add("@Num_Requerimiento", nNum_Requerimiento);
                parametros.Add("@Nro_Secuencia", nNum_Secuencia);
                //parametros.Add("@Cod_Item", tx_Retiro_Repuestos_Detalle.Cod_Item);
                //parametros.Add("@Itm_Descripcion", tx_Retiro_Repuestos_Detalle.Itm_Descripcion);
                parametros.Add("@Can_Requerida", nCan_Requerida);
                //parametros.Add("@Itm_Unidad_Medida", tx_Retiro_Repuestos_Detalle.Itm_Unidad_Medida);
                parametros.Add("@Rpt_Cambio", Convert.ToInt32(sRpt_Cambio));
                parametros.Add("@Itm_Foto", sNombreArchivo);

                //PARAMETROS DE SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Lg_RequerimientoAlmacenDetalle_WB_U0001]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                        );
                }
                catch (Exception ex)
                {

                }

                var Codigo = parametros.Get<int>("@Codigo");
                var mensaje = parametros.Get<string>("@sMsj");
                return (Codigo, mensaje);
            }
        }


        /************************************************************************/



        /********************************COMPLEMENTARIOS****************************************/
        //OBTENER DATOS DE ITEM       
        public async Task<IEnumerable<Lg_Item>?> ListaItems(string Cod_Item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();
                parametros.Add("@Cod_Item", Cod_Item);

                var result = await connection.QueryAsync<Lg_Item>(
                    "[dbo].[PA_Lg_Item_S0001]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }
        
        public async Task<IEnumerable<Lg_Item>?> ListaItemsCompletos()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
               
                var result = await connection.QueryAsync<Lg_Item>(
                    "[dbo].[PA_Lg_Item_S0002]"
                    , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        public async Task<IEnumerable<Lg_Retiro_Repuesto_Usuario>?> ListaRetiroRepuestoUsuario(int Id_Usuario)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();
                parametros.Add("@Id_Usuario", Id_Usuario);

                var result = await connection.QueryAsync<Lg_Retiro_Repuesto_Usuario>(
                    "[dbo].[PA_Lg_Retiro_Repuesto_Usuario_S0001]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                );

                return result;
            }
        }

        public async Task<IEnumerable<Lg_Retiro_Repuesto_Usuario>?> ListaRetiroRepuestoUsuarioPorTipo (int Tip_Usuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();
                parametros.Add("@Tip_Usuario", Tip_Usuario);

                var result = await connection.QueryAsync<Lg_Retiro_Repuesto_Usuario>(
                    "[dbo].[PA_Lg_Retiro_Repuesto_Usuario_S0002]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                    );
                return result;
            }
        }

        public async Task<IEnumerable<Lg_Retiro_Repuesto_Usuario>?> ListaRetiroRepuestoUsuarioSeguridadNombres()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();
                parametros.Add("@Tip_Usuario", 1);
                var result = await connection.QueryAsync<Lg_Retiro_Repuesto_Usuario>(
                    "[dbo].[PA_Lg_Retiro_Repuesto_Usuario_S0003]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                    );
                return result;
            }
        }

        public async Task<IEnumerable<Lg_Retiro_Repuesto_Usuario>?> ListaRetiroRepuestoUsuarioMantenimientoNombres()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();
                parametros.Add("@Tip_Usuario", 2);
                var result = await connection.QueryAsync<Lg_Retiro_Repuesto_Usuario>(
                    "[dbo].[PA_Lg_Retiro_Repuesto_Usuario_S0003]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                    );
                return result;
            }
        }

        public async Task<IEnumerable<Lg_Item>?> ListaDatosItemsPorNumReqySecuencia(int Num_Requerimiento, int Nro_Secuencia)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();
                parametros.Add("@Num_Requerimiento", Num_Requerimiento);
                parametros.Add("@Nro_Secuencia", Nro_Secuencia);
                var result = await connection.QueryAsync<Lg_Item>(
                    "[dbo].[PA_Lg_Item_S0003]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                    );
                return result;
            }
        }

        public async Task<IEnumerable<Tx_Retiro_Repuestos_Reporte>?> ListaDatosReporte(DateTime FecIni, DateTime FecFin)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                parametros.Add("@Fec_Ini", FecIni);
                parametros.Add("@Fec_Fin", FecFin);

                var result = await connection.QueryAsync<Tx_Retiro_Repuestos_Reporte>(
                    "[dbo].[PA_Lg_RequerimientoAlmacen_WB_S0003]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                    );
                return result;
            }
        }

        public async Task<IEnumerable<Tx_Retiro_Repuestos_Reporte>?> ListaRetiroRepuestosPorIdRequerimientoMAX()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<Tx_Retiro_Repuestos_Reporte>(
                    "[dbo].[PA_Lg_RequerimientoAlmacen_WB_S0004]"
                    , commandType: CommandType.StoredProcedure
                    );
                return result;
            }
        }
        public async Task<(int Codigo, string Mensaje)> EnviarCorreo()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Codigo", 0);
                parametros.Add("@sMsj", "");

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Lg_RequerimientoAlmacen_CORREO_WB]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                    );
                }
                catch (SqlException ex)
                {
                    ;
                }

                var Codigo = parametros.Get<int>("@Codigo");
                var mensaje = parametros.Get<string>("@sMsj");
                return (Codigo, mensaje);
            }
        }


    }
}

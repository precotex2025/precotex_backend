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
                parametros.Add("@Fec_Requerimiento", tx_Retiro_Repuestos.Fec_Requerimiento);
                parametros.Add("@Nom_Seguridad", tx_Retiro_Repuestos.Cod_Seguridad);
                parametros.Add("@Nom_Mantenimiento", tx_Retiro_Repuestos.Cod_Mantenimiento);
                parametros.Add("@Nro_Precinto_Apertura", tx_Retiro_Repuestos.Nro_Precinto_Apertura);
                parametros.Add("@Nro_Precinto_Cierre", "");

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                try
                {
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

        /************************************************************************/

    }
}

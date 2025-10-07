using ic.backend.precotex.web.Data.Repositories.Implementation.ReporteNC;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ic.backend.precotex.web.Entity.Entities.RetiroRepuestos;
using ic.backend.precotex.web.Entity.Entities.ReporteNC;
using ic.backend.precotex.web.Entity.Entities;

namespace ic.backend.precotex.web.Data.Repositories.ReporteNC
{
    public class TxReporteNCRepository: ITxReporteNCRepository
    {
        private readonly string _connectionString;

        //DECLARAMOS CADENA DE CONEXION
        public TxReporteNCRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnection")!;
        }
        //LISTAR REGISTROS
        public async Task<IEnumerable<Tx_ReporteNC>?> ListarRegistro(int Rep_ID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                parametros.Add("@Rep_ID", Rep_ID);

                var result = await connection.QueryAsync<Tx_ReporteNC>(
                        "[dbo].[PA_Tx_ReportesNC_S0001]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        //REGISTRAR REPORTE NC
        public async Task<(int Codigo, string Mensaje)> RegistrarReporteNC(Tx_ReporteNC tx_ReporteNC)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                //parametros.Add("@Rep_FecObs", tx_ReporteNC.Rep_FecObs);
                //parametros.Add("@Rep_HorObs", tx_ReporteNC.Rep_HorObs);
                parametros.Add("@Cod_Planta_Tg", tx_ReporteNC.Cod_Planta_Tg);
                parametros.Add("@Are_Id", tx_ReporteNC.Are_Id);
                parametros.Add("@Rep_Esp", tx_ReporteNC.Rep_Esp);
                parametros.Add("@Rep_Clas", tx_ReporteNC.Rep_Clas);
                parametros.Add("@Rep_DesNC", tx_ReporteNC.Rep_DesNC);
                parametros.Add("@Rep_NivRgo", tx_ReporteNC.Rep_NivRgo);
                parametros.Add("@Rep_AccCor", tx_ReporteNC.Rep_AccCor);
                parametros.Add("@Resp_Id", tx_ReporteNC.Resp_Id);
                parametros.Add("@Rep_RepPor", tx_ReporteNC.Rep_RepPor);


                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Tx_ReportesNC_I0001]"
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

        public async Task<IEnumerable<Sg_Planta>?> ListarPlantas()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                var result = await connection.QueryAsync<Sg_Planta>(
                        "[dbo].[PA_Sg_Planta_S0001]"
                        , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }
        
        //LISTAR CLASIFICACIONES
        public async Task<IEnumerable<Tx_ReportesNC_Clasificacion>?> ListarClasificaciones()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                var result = await connection.QueryAsync<Tx_ReportesNC_Clasificacion>(
                        "[dbo].[PA_Tx_ReportesNC_Clasificacion_S0001]"
                        , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        //ACTUALIZAR ESTADO
        public async Task<(int Codigo, string Mensaje)> ActualizarEstado(Tx_ReporteNC tx_ReporteNC)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Rep_Id", tx_ReporteNC.Rep_Id);
                parametros.Add("@Rep_Est", tx_ReporteNC.Rep_Est);


                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Tx_ReportesNC_U0001]"
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

        //CARGAR DATOS PARA RESOLVEDOR
        public async Task<IEnumerable<Tx_ReporteNC>?> ListarDatosResolvedor(int Rep_ID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                parametros.Add("@Rep_ID", Rep_ID);

                var result = await connection.QueryAsync<Tx_ReporteNC>(
                        "[dbo].[PA_Tx_ReportesNC_S0002]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        //ACTUALIZAR REPORTE - PERSPECTIVA RESOLVEDOR
        public async Task<(int Codigo, string Mensaje)> ActualizarReporteNC(Tx_ReporteNC tx_ReporteNC)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Rep_Est", tx_ReporteNC.Rep_Est);
                parametros.Add("@Rep_Aceptado", tx_ReporteNC.Rep_Aceptado);
                parametros.Add("@Rep_AccCor_Tom", tx_ReporteNC.Rep_AccCor_Tom);
                parametros.Add("@Rep_DetObs", tx_ReporteNC.Rep_DetObs);
                parametros.Add("@Rep_Resp_Levantamiento", tx_ReporteNC.Rep_Resp_Levantamiento);
               
                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Tx_ReportesNC_U0002]"
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

        //LISTAR ESTADOS
        public async Task<IEnumerable<Tx_ReportesNC_Estados>?> ListarEstados()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<Tx_ReportesNC_Estados>(
                        "[dbo].[PA_Tx_ReportesNC_Est_S0001]"
                        , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        //ACTUALIZAR REPORTE ORIGINAL
        public async Task<(int Codigo, string Mensaje)> ActualizarReporteNCOriginal(Tx_ReporteNC tx_ReporteNC)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Rep_Id", tx_ReporteNC.Rep_Id);
                parametros.Add("@Cod_Planta_Tg", tx_ReporteNC.Cod_Planta_Tg);
                parametros.Add("@Are_Id", tx_ReporteNC.Are_Id);
                parametros.Add("@Rep_Esp", tx_ReporteNC.Rep_Esp);
                parametros.Add("@Rep_Clas", tx_ReporteNC.Rep_Clas);
                parametros.Add("@Rep_DesNC", tx_ReporteNC.Rep_DesNC);
                parametros.Add("@Rep_NivRgo", tx_ReporteNC.Rep_NivRgo);
                parametros.Add("@Rep_AccCor", tx_ReporteNC.Rep_AccCor);
                parametros.Add("@Resp_Id", tx_ReporteNC.Resp_Id);
                parametros.Add("@Rep_RepPor", tx_ReporteNC.Rep_RepPor);

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Tx_ReportesNC_U0003]"
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

    }
}

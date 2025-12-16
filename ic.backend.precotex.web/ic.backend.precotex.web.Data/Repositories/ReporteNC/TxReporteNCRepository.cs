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
                parametros.Add("@Rep_Id", tx_ReporteNC.Rep_Id);
                parametros.Add("@Rep_Aceptado", tx_ReporteNC.Rep_Aceptado);
                parametros.Add("@Rep_Resp_Levantamiento", tx_ReporteNC.Rep_Resp_Levantamiento);
                parametros.Add("@Rep_AccCor_Tom", tx_ReporteNC.Rep_AccCor_Tom);
                parametros.Add("@Rep_Est", tx_ReporteNC.Rep_Est);
                parametros.Add("@Rep_DetObs", tx_ReporteNC.Rep_DetObs);
                parametros.Add("@Rep_FecSub", tx_ReporteNC.Rep_FecSub);
                parametros.Add("@Codigo", 0);
                parametros.Add("@sMsj", "");

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                Console.Write(parametros);
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

        public async Task<(int Codigo, string Mensaje)> ActualizarReporteNCCierre(Tx_ReporteNC tx_ReporteNC)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Rep_Id", tx_ReporteNC.Rep_Id);
                parametros.Add("@Rep_Est", tx_ReporteNC.Rep_Est);
                parametros.Add("@Rep_DetObs", tx_ReporteNC.Rep_DetObs);
                parametros.Add("@Codigo", 0);
                parametros.Add("@sMsj", "");

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                Console.Write(parametros);
                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Tx_ReportesNC_U0004]"
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

        //METODO BUSCAR
        public async Task<IEnumerable<Tx_ReporteNC>?> BuscarRegistros(int Num_Planta, int Are_Id, int Resp_Id, int Rep_Niv_Rgo, int Rep_Est)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                parametros.Add("@Num_Planta", Num_Planta);
                parametros.Add("@Are_Id", Are_Id);
                parametros.Add("@Resp_Id", Resp_Id);
                parametros.Add("@Rep_Niv_Rgo", Rep_Niv_Rgo);
                parametros.Add("@Rep_Est", Rep_Est);

                var result = await connection.QueryAsync<Tx_ReporteNC>(
                        "[dbo].[PA_Tx_ReportesNC_S0003]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        //LISTAR RIESGOS
        public async Task<IEnumerable<Tx_ReportesNC_Riesgos>?> ListarRiesgos()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<Tx_ReportesNC_Riesgos>(
                        "[dbo].[PA_Tx_ReportesNC_Niv_Riesgo_S0001]"
                        , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        /*IMAGENES*/

        //GUARDAR IMAGENES
        public async Task<(int Codigo, string Mensaje)> RegistrarImagendeReporteNC(int Rep_Id, string Img_Des, int Img_Fam)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Rep_Id", Rep_Id);
                parametros.Add("@Img_Des", Img_Des);
                parametros.Add("@Img_Fam", Img_Fam);

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Tx_ReportesNC_Imagenes_I0001]"
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

        //OBTENEMOS NOS NOMBRES DE LAS IMAGENES DE LA BASE DE DATOS
        public async Task<IEnumerable<Tx_ReporteNC_Img>?> ObtenerImagenes(int Rep_Id, int Img_Fam)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                parametros.Add("@Rep_Id", Rep_Id);
                parametros.Add("@Img_Fam", Img_Fam);
                var result = await connection.QueryAsync<Tx_ReporteNC_Img>(
                        "[dbo].[PA_Tx_ReportesNC_Imagenes_S0001]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        //ELIMINAR IMAGENES
        public async Task<(int Codigo, string Mensaje)> EliminarImagenes(int Img_Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Img_Id", Img_Id);

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Tx_ReportesNC_Imagenes_D0001]"
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

        public async Task<(int Codigo, string Mensaje)> EliminarImagenParaElPatch(string Img_Des)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Img_Des", Img_Des);

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Tx_ReportesNC_Imagenes_D0002]"
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

        /*AREAS*/

        //SELECCIONAR AREAS
        public async Task<IEnumerable<Tx_ReportesNC_Areas>?> ObtenerAreas(int Are_Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                parametros.Add("@Are_Id", Are_Id);

                var result = await connection.QueryAsync<Tx_ReportesNC_Areas>(
                        "[dbo].[PA_Tx_ReportesNC_Areas_S0001]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        //REGISTRAR AREAS
        public async Task<(int Codigo, string Mensaje)> RegistrarArea(Tx_ReportesNC_Areas _txReportesNCAreas)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Are_Des", _txReportesNCAreas.Are_Des);
                parametros.Add("@Num_Planta", _txReportesNCAreas.Num_Planta);

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Tx_ReportesNC_Areas_I0001]"
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
        //ACTUALIZAR ÁREAS
        public async Task<(int Codigo, string Mensaje)> ActualizarArea(Tx_ReportesNC_Areas _txReportesNCAreas)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Are_Id", _txReportesNCAreas.Are_Id);
                parametros.Add("@Are_Des", _txReportesNCAreas.Are_Des);
                parametros.Add("@Num_Planta", _txReportesNCAreas.Num_Planta);

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Tx_ReportesNC_Areas_U0001]"
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

        //ELIMINAR ÁREAS
        public async Task<(int Codigo, string Mensaje)> EliminarArea(int Are_Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Are_Id", Are_Id);

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Tx_ReportesNC_Areas_D0001]"
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

        //SELECCIONAR AREAS X SEDE
        public async Task<IEnumerable<Tx_ReportesNC_Areas>?> ObtenerAreaXSede(int Num_Planta, int Are_Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                parametros.Add("@Num_Planta", Num_Planta);
                parametros.Add("@Are_Id", Are_Id);

                var result = await connection.QueryAsync<Tx_ReportesNC_Areas>(
                        "[dbo].[PA_Tx_ReportesNC_Areas_S0002]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }


        /*RESPONSABLES*/

        //SELECCIONAR RESPONSABLES
        public async Task<IEnumerable<Tx_ReportesNC_Responsables>?> ObtenerResponsables(int Resp_Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                parametros.Add("@Resp_Id", Resp_Id);

                var result = await connection.QueryAsync<Tx_ReportesNC_Responsables>(
                        "[dbo].[PA_Tx_ReportesNC_Responsables_S0001]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        //REGISTRAR RESPONSABLES
        public async Task<(int Codigo, string Mensaje)> RegistrarResponsable(Tx_ReportesNC_Responsables _txReporteNCResponsable)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Resp_Nom", _txReporteNCResponsable.Resp_Nom);
                parametros.Add("@Resp_Ape_Pat", _txReporteNCResponsable.Resp_Ape_Pat);
                parametros.Add("@Resp_Ape_Mat", _txReporteNCResponsable.Resp_Ape_Mat);
                parametros.Add("@Resp_Correo", _txReporteNCResponsable.Resp_Correo);

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Tx_ReportesNC_Responsables_I0001]"
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
        //ACTUALIZAR RESPONSABLES
        public async Task<(int Codigo, string Mensaje)> ActualizarResponsable(Tx_ReportesNC_Responsables _txReporteNCResponsable)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Resp_Id", _txReporteNCResponsable.Resp_Id);
                parametros.Add("@Resp_Nom", _txReporteNCResponsable.Resp_Nom);
                parametros.Add("@Resp_Ape_Pat", _txReporteNCResponsable.Resp_Ape_Pat);
                parametros.Add("@Resp_Ape_Mat", _txReporteNCResponsable.Resp_Ape_Mat);
                parametros.Add("@Resp_Correo", _txReporteNCResponsable.Resp_Correo);

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Tx_ReportesNC_Responsables_U0001]"
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

        //ELIMINAR RESPONSABLES
        public async Task<(int Codigo, string Mensaje)> EliminarResponsable(int Resp_Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Resp_Id", Resp_Id);

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Tx_ReportesNC_Responsables_D0001]"
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



        /*USUARIOS*/
        //OBTENER FUNCION DE USUARIOS QUE MÓSTRARÁ LA GRILLA
        public async Task<IEnumerable<Tx_ReportesNC_Usuarios>?> ObtenerUsuarios(string Usr_Cod)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                parametros.Add("@Usr_Cod", Usr_Cod);

                var result = await connection.QueryAsync<Tx_ReportesNC_Usuarios>(
                        "[dbo].[PA_Tx_ReportesNC_Usuarios_S0001]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        /*MENSAJES WSP*/
        //OBTENER DATOS PARA ENVIAR MENSAJE
        public async Task<IEnumerable<Tx_ReporteNC>?> ObtenerDatosRegistro(int Rep_Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                parametros.Add("@Rep_Id", Rep_Id);

                var result = await connection.QueryAsync<Tx_ReporteNC>(
                        "[dbo].[PA_Tx_ReportesNC_S0004]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }
    }
}

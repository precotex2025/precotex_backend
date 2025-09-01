using Dapper;
using ic.backend.precotex.web.Data.Repositories.Implementation.Tejeduria;
using ic.backend.precotex.web.Entity.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ic.backend.precotex.web.Data.Repositories.Tejeduria
{
    public  class TxTelaEstructuraTejidoItemsRepository: ITxTelaEstructuraTejidoItemsRepository
    {
            
        private readonly string _connectionString;

        public TxTelaEstructuraTejidoItemsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnection")!;
        }

        public async Task<IEnumerable<Tx_Ots_Hojas_Arranque_Versiones>?> GeneraVersionHojasArranque(string? Cod_Ordtra, string? Num_Secuencia, string? Cod_Talla)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Cod_Ordtra = Cod_Ordtra,
                    Num_Secuencia = Num_Secuencia,
                    Cod_Talla  = Cod_Talla
                };

                var result = await connection.QueryAsync<Tx_Ots_Hojas_Arranque_Versiones>(
                     "[dbo].[TI_GENERAR_OTS_HOJAS_ARRANQUE_VERSIONES_WS]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<(int Codigo, string Mensaje)> InsertarCargaEstructuraTejido(string NombreVersion, string Cod_Tela, string Servicio, string Observacion, string Elaborado, string Revisado, string Cod_Usuario, string XMLData)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();
                parametros.Add("@NombreVersion", NombreVersion);
                parametros.Add("@Cod_Tela", Cod_Tela);
                parametros.Add("@Servicio", Servicio);
                parametros.Add("@Observacion", Observacion);
                parametros.Add("@Elaborado", Elaborado);
                parametros.Add("@Revisado", Revisado);
                parametros.Add("@XmlData", XMLData);
                parametros.Add("@Cod_Usuario", Cod_Usuario);

                // Parámetros de salida
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                // Ejecutar el procedimiento almacenado
                connection.Execute(
                    "[dbo].[Tx_Carga_Tela_Estructura_Tejido]",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

                //Obtener los valores de salida
                var codigo = parametros.Get<int>("@Codigo");
                var mensaje = parametros.Get<string>("@sMsj");

                return (codigo, mensaje);
            }
        }

        public async Task<(int Codigo, string Mensaje)> InsertarEstructuraTejidoItem(string Cod_Ordtra, int Num_Secuencia, string Cod_Comb, string Cod_Talla, string Cod_Usuario, string XMLData)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();
                parametros.Add("@Cod_Ordtra", Cod_Ordtra);
                parametros.Add("@Num_Secuencia", Num_Secuencia);
                parametros.Add("@Cod_Comb", Cod_Comb);
                parametros.Add("@Cod_Talla", Cod_Talla);
                parametros.Add("@Cod_Usuario", Cod_Usuario);
                parametros.Add("@XmlData", XMLData);

                // Parámetros de salida
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                // Ejecutar el procedimiento almacenado
                connection.Execute(
                    "[dbo].[Tx_Registrar_Hojas_Arranque_Det]",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

                //Obtener los valores de salida
                var codigo = parametros.Get<int>("@Codigo");
                var mensaje = parametros.Get<string>("@sMsj");

                return (codigo, mensaje);
            }
        }

        public async Task<(int Codigo, string Mensaje)> InsertarTelaMedida(string Cod_Ordtra, int Num_Secuencia, string Cod_Tela, string Cod_Comb, string Cod_Talla, string Cod_Usuario, string XMLData)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();
                parametros.Add("@Cod_Ordtra", Cod_Ordtra);
                parametros.Add("@Num_Secuencia", Num_Secuencia);
                parametros.Add("@Cod_Tela", Cod_Tela);
                parametros.Add("@Cod_Comb", Cod_Comb);
                parametros.Add("@Cod_Talla", Cod_Talla);
                //parametros.Add("@Rec_Largo_Real", Rec_Largo_Real);
                //parametros.Add("@Rec_Alto_Real", Rec_Alto_Real);
                parametros.Add("@Cod_Usuario", Cod_Usuario);
                parametros.Add("@XmlData", XMLData);

                // Parámetros de salida
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                // Ejecutar el procedimiento almacenado
                connection.Execute(
                    "[dbo].[Tx_Registrar_Hojas_Arranque_Medida]",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

                //Obtener los valores de salida
                var codigo = parametros.Get<int>("@Codigo");
                var mensaje = parametros.Get<string>("@sMsj");

                return (codigo, mensaje);
            }
        }

        public async Task<IEnumerable<Tx_Ots_Hojas_Arranque_Versiones>?> ObtenerVersionHojasArranque(string? Cod_Ordtra, string? Num_Secuencia)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Cod_Ordtra = Cod_Ordtra,
                    Num_Secuencia = Num_Secuencia
                };

                var result = await connection.QueryAsync<Tx_Ots_Hojas_Arranque_Versiones>(
                     "[dbo].[TI_OBTENER_OTS_HOJAS_ARRANQUE_VERSIONES_WS]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        //public async Task<(int Codigo, string Mensaje)> ModificarMedida(string Cod_Ordtra, int Num_Secuencia)
        //{
        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        await connection.OpenAsync();

        //        var parametros = new DynamicParameters();
        //        parametros.Add("@Cod_Ordtra", Cod_Ordtra);
        //        parametros.Add("@Num_Secuencia", Num_Secuencia);

        //        // Parámetros de salida
        //        parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
        //        parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

        //        // Ejecutar el procedimiento almacenado
        //        connection.Execute(
        //            "[dbo].[Tx_Actualizar_Largo_Alto_Real]",
        //            parametros,
        //            commandType: CommandType.StoredProcedure
        //        );

        //        //Obtener los valores de salida
        //        var codigo = parametros.Get<int>("@Codigo");
        //        var mensaje = parametros.Get<string>("@sMsj");

        //        return (codigo, mensaje);
        //    }
        //}

        public async Task<IEnumerable<Tx_TelaEstructuraTejidoItems>?> ObtieneEstructuraTejidoItem(string? codTela, string? Cod_Ordtra, string? Num_Secuencia)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Cod_Tela = codTela,
                    Cod_Ordtra = Cod_Ordtra,
                    Num_Secuencia = Num_Secuencia
                };

                var result = await connection.QueryAsync<Tx_TelaEstructuraTejidoItems>(
                     "[dbo].[Tx_Obtiene_Estructura_Tejido_Item]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<IEnumerable<Tx_TelaMed>?> ObtieneTelaMedida(string? codTela, string? Cod_Talla)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Cod_Tela = codTela,
                    Cod_Talla = Cod_Talla
                };

                var result = await connection.QueryAsync<Tx_TelaMed>(
                     "[dbo].[Tx_Obtiene_Tela_Medida]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<IEnumerable<Tx_TelaMed>?> ObtieneTelaMedidaHist(string? codTela, string? Cod_Ordtra, string? Num_Secuencia, string? Cod_Comb, string? Cod_Talla)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Cod_Tela = codTela,
                    Cod_Ordtra = Cod_Ordtra,
                    Num_Secuencia = Num_Secuencia,
                    Cod_Comb = Cod_Comb == null ? "" : Cod_Comb,
                    Cod_Talla = Cod_Talla
                };

                var result = await connection.QueryAsync<Tx_TelaMed>(
                     "[dbo].[Tx_Obtiene_Tela_Medida_Hist]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<(int Codigo, string Mensaje)> ValidaVersionHojasArranque(string Cod_Ordtra, int Num_Secuencia, int Version, string Flg_Rectilineo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();
                parametros.Add("@Cod_Ordtra", Cod_Ordtra);
                parametros.Add("@Num_Secuencia", Num_Secuencia);
                parametros.Add("@Version", Version);
                parametros.Add("@Flg_Rectilineo", Flg_Rectilineo);

                // Parámetros de salida
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                // Ejecutar el procedimiento almacenado
                connection.Execute(
                    "[dbo].[TI_VALIDA_OTS_HOJAS_ARRANQUE_VERSIONES_WS]",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

                //Obtener los valores de salida
                var codigo = parametros.Get<int>("@Codigo");
                var mensaje = parametros.Get<string>("@sMsj");

                return (codigo, mensaje);
            }
        }

        public async Task<IEnumerable<Tx_Ots_Hojas_Arranque_Versiones_Listado>?> ListadoVersionHojasArranqueHist(DateTime FecIni, DateTime FecFin, string Cod_Ordtra)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    FechaInicio = FecIni,
                    FechaFin = FecFin, 
                    Cod_Ordtra = Cod_Ordtra  
                };

                var result = await connection.QueryAsync<Tx_Ots_Hojas_Arranque_Versiones_Listado>(
                     "[dbo].[TI_LISTADO_ARRANQUE_MOVIMIENTOS_VERSION_HIST]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<IEnumerable<Tx_Maquinas_Revisadoras>?> ListaMaquinaRevisadora()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
               
                var result = await connection.QueryAsync<Tx_Maquinas_Revisadoras>(
                     "[dbo].[Tx_Listar_Maquinas_Revisadoras]"
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<(int Codigo, string Mensaje)> CrudArranqueCtrol(Tx_Ots_Hojas_Arranque_Ctrol tx_Ots_Hojas_Arranque_Ctrol, string sTipoTransac)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();
                parametros.Add("@Accion", sTipoTransac);
                parametros.Add("@Cod_OrdTra", tx_Ots_Hojas_Arranque_Ctrol.Cod_OrdTra);
                parametros.Add("@Num_Secuencia", tx_Ots_Hojas_Arranque_Ctrol.Num_Secuencia);
                parametros.Add("@Version", tx_Ots_Hojas_Arranque_Ctrol.Version);
                parametros.Add("@Usu_Registro", tx_Ots_Hojas_Arranque_Ctrol.Usu_Registro);

                // Parámetros de salida
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                // Ejecutar el procedimiento almacenado
                connection.Execute(
                    "[dbo].[TJ_PROCESO_CTROL_INICIO]",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

                //Obtener los valores de salida
                var codigo = parametros.Get<int>("@Codigo");
                var mensaje = parametros.Get<string>("@sMsj");

                return (codigo, mensaje);

            }
        }

        public async Task<IEnumerable<Tx_Ots_Hojas_Arranque_Ctrol>?> ObtenerArranqueCtrol(string Cod_OrdTra, int Num_Secuencia, int Version)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Cod_Ordtra = Cod_OrdTra,
                    Num_Secuencia = Num_Secuencia,
                    Version = Version
                };

                var result = await connection.QueryAsync<Tx_Ots_Hojas_Arranque_Ctrol>(
                     "[dbo].[TJ_OBTENER_PROCESO_CTROL]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<IEnumerable<Tx_Ots_Hojas_Arranque_Ctrol>?> ObtenerArranqueCtrolSinVersion(string Cod_OrdTra, int Num_Secuencia)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Cod_Ordtra = Cod_OrdTra,
                    Num_Secuencia = Num_Secuencia
                };

                var result = await connection.QueryAsync<Tx_Ots_Hojas_Arranque_Ctrol>(
                     "[dbo].[TI_OBTENER_CTROL_INICIO]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<IEnumerable<Tx_Ots_Hojas_Arranque_Versiones_Listado>?> ListadoVersionHojasArranqueHistDetail(DateTime FecIni, DateTime FecFin, string Cod_Ordtra)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    FechaInicio = FecIni,
                    FechaFin = FecFin,
                    Cod_Ordtra = Cod_Ordtra
                };

                var result = await connection.QueryAsync<Tx_Ots_Hojas_Arranque_Versiones_Listado>(
                     "[dbo].[TI_LISTADO_ARRANQUE_MOVIMIENTOS_VERSION_HIST_DETAIL]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }
    }
}

using Dapper;
using ic.backend.precotex.web.Data.Repositories.Implementation.DDT;
using ic.backend.precotex.web.Entity.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.DDT
{
    public class TxProcesoColgadorRegistroRepository : ITxProcesoColgadorRegistroRepository
    {

        private readonly string _connectionString;

        public TxProcesoColgadorRegistroRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnection")!;
        }

        public async Task<IEnumerable<Tx_Colgador_Registro_Cab>?> ListadoColgadoresBandeja(DateTime FecIni, DateTime FecFin, string Cod_Tela)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    FechaInicio = FecIni,
                    FechaFin = FecFin,
                    Cod_Tela = Cod_Tela,
                };

                var result = await connection.QueryAsync<Tx_Colgador_Registro_Cab>(
                     "[dbo].[Tx_Listar_Colgadores_Bandeja]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<IEnumerable<Tx_Cliente>?> ObtieneInformacionClienteColgador()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<Tx_Cliente>(
                     "[dbo].[Tx_Obtiene_Informacion_Clientes_Colgador]"
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<IEnumerable<Tx_TelaEstructuraRuta>?> ObtieneInformacionRutaColgador(string Cod_Tela)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Cod_Tela = Cod_Tela,
                };

                var result = await connection.QueryAsync<Tx_TelaEstructuraRuta>(
                     "[dbo].[Tx_Obtiene_Informacion_Rutas_Colgador]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<IEnumerable<Tx_TelaEstructuraColgador>?> ObtieneInformacionTelaColgador(string Cod_Tela)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Cod_Tela = Cod_Tela,
                };

                var result = await connection.QueryAsync<Tx_TelaEstructuraColgador>(
                     "[dbo].[Tx_Obtiene_Informacion_Tela_Colgador]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<IEnumerable<Tx_Colgador_Registro_Det>?> ObtieneInformacionTelaColgadorDet(int Id_Tx_Colgador_Registro_Cab)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Id_Tx_Colgador_Registro_Cab = Id_Tx_Colgador_Registro_Cab,
                };

                var result = await connection.QueryAsync<Tx_Colgador_Registro_Det>(
                     "[dbo].[Tx_Obtener_Informacion_Colgador_Detalle]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<(int Codigo, string Mensaje)> ProcesoEliminarColgador(int Id_Tx_Colgador_Registro_Cab, string Usu_Registro)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                // Parametros de SQL
                parametros.Add("@Id_Tx_Colgador_Registro_Cab", Id_Tx_Colgador_Registro_Cab);
                parametros.Add("@Usu_Registro", Usu_Registro);

                // Parámetros de salida
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                // Ejecutar el procedimiento almacenado
                try
                {
                    connection.Execute(
                        "[dbo].[Tx_Proceso_Anular_Colgador]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );
                }
                catch (Exception ex){}

                //Obtener los valores de salida
                var codigo = parametros.Get<int>("@Codigo");
                var mensaje = parametros.Get<string>("@sMsj");

                return (codigo, mensaje);
            }
        }

        public async Task<(int Codigo, string Mensaje)> ProcesoMntoColgador(Tx_Colgador_Registro_Cab tx_Colgador_Registro_Cab, List<Tx_Colgador_Registro_Det> detalle, string sTipoTransac)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                // Parametros de SQL
                parametros.Add("@Cod_Tela", tx_Colgador_Registro_Cab.Cod_Tela);
                parametros.Add("@Cod_OrdTra", tx_Colgador_Registro_Cab.Cod_OrdTra);
                parametros.Add("@Cod_Ruta", tx_Colgador_Registro_Cab.Cod_Ruta);
                parametros.Add("@Cod_Cliente_Tex", tx_Colgador_Registro_Cab.Cod_Cliente_Tex);
                parametros.Add("@Fabric", tx_Colgador_Registro_Cab.Fabric);
                parametros.Add("@Yarn", tx_Colgador_Registro_Cab.Yarn);
                parametros.Add("@Composicion", tx_Colgador_Registro_Cab.Composicion);
                parametros.Add("@Flg_Estatus_Cab", tx_Colgador_Registro_Cab.Flg_Estatus);
                parametros.Add("@Usu_Registro", tx_Colgador_Registro_Cab.Usu_Registro);
                // Parámetro tipo tabla (TVP)
                var tvp = CrearDataTable(detalle);
                parametros.Add("@Detalle", tvp.AsTableValuedParameter("dbo.TVP_ColgadorDetalle"));
                parametros.Add("@Accion", sTipoTransac);

                // Parámetros de salida
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                // Ejecutar el procedimiento almacenado
                try
                {
                    connection.Execute(
                        "[dbo].[Tx_Proceso_Mnto_Colgador]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );
                }
                catch (Exception ex)
                {


                }


                //Obtener los valores de salida
                var codigo = parametros.Get<int>("@Codigo");
                var mensaje = parametros.Get<string>("@sMsj");

                return (codigo, mensaje);
            }
        }

        private DataTable CrearDataTable(List<Tx_Colgador_Registro_Det> detalles)
        {
            var table = new DataTable();
            //table.Columns.Add("Id_Tx_Colgador_Registro_Cab", typeof(int));
            table.Columns.Add("Encog_Largo", typeof(decimal));
            table.Columns.Add("Encog_Ancho", typeof(decimal));
            table.Columns.Add("Ancho_Acabado", typeof(decimal));
            table.Columns.Add("Ancho_Lavado", typeof(decimal));
            table.Columns.Add("Gramaje_Acab", typeof(decimal));
            table.Columns.Add("Gramaje_Comercial", typeof(decimal));
            table.Columns.Add("Rendimiento", typeof(decimal));
            table.Columns.Add("Diametro", typeof(int));
            table.Columns.Add("Des_Galga", typeof(string));
            table.Columns.Add("Des_Color", typeof(string));
            table.Columns.Add("Des_Fabric_Finish", typeof(string));
            table.Columns.Add("Des_Fabric_Wash", typeof(string));
            table.Columns.Add("Glosa", typeof(string));
            table.Columns.Add("Flg_Estatus", typeof(string));

            foreach (var d in detalles)
            {
                table.Rows.Add(

                        //d.Id_Tx_Colgador_Registro_Cab, 
                        d.Encog_Largo, 
                        d.Encog_Ancho,
                        d.Ancho_Acabado,
                        d.Ancho_Lavado,
                        d.Gramaje_Acab,
                        d.Gramaje_Comercial,
                        d.Rendimiento,
                        d.Diametro,
                        d.Des_Galga,
                        d.Des_Color,
                        d.Des_Fabric_Finish,
                        d.Des_Fabric_Wash,
                        d.Glosa,
                        d.Flg_Estatus);
            }

            return table;
        }

    }
}

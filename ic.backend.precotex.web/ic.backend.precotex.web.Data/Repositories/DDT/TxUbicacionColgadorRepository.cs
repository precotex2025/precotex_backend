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
    public class TxUbicacionColgadorRepository : ITxUbicacionColgadorRepository
    {
        private readonly string _connectionString;

        public TxUbicacionColgadorRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnection")!;
        }

        public async Task<(int Codigo, string Mensaje)> CrudUbicacionColgador(Tx_Ubicacion_Colgador tx_Ubicacion_Colgador, string sTipoTransac)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();
                parametros.Add("@Accion", sTipoTransac);
                parametros.Add("@Id_Tx_Ubicacion_Colgador", tx_Ubicacion_Colgador.Id_Tx_Ubicacion_Colgador);
                parametros.Add("@Id_Tipo_Ubicacion_Colgador", tx_Ubicacion_Colgador.Id_Tipo_Ubicacion_Colgador);
                parametros.Add("@Id_Tipo_Ubicacion_Colgador_Padre", tx_Ubicacion_Colgador.Id_Tipo_Ubicacion_Colgador_Padre);
                parametros.Add("@CodigoBarra", tx_Ubicacion_Colgador.CodigoBarra);
                parametros.Add("@Cod_FamTela", tx_Ubicacion_Colgador.Cod_FamTela);
                parametros.Add("@CorrelativoCtrol", tx_Ubicacion_Colgador.Correlativo);
                parametros.Add("@Cod_Usuario", tx_Ubicacion_Colgador.Cod_Usuario);

                // Parámetros de salida
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                // Ejecutar el procedimiento almacenado
                connection.Execute(
                    "[dbo].[Tx_Insertar_Ubicacion_Colgador]",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

                //Obtener los valores de salida
                var codigo = parametros.Get<int>("@Codigo");
                var mensaje = parametros.Get<string>("@sMsj");

                return (codigo, mensaje);

            }
        }

        public async Task<IEnumerable<Tx_Ubicacion_Colgador>?> ListadoUbicacionColgador(DateTime FecIni, DateTime FecFin, int Id_Tipo_Ubicacion_Colgador)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    FechaInicio = FecIni,
                    FechaFin = FecFin,
                    Id_Tipo_Ubicacion_Colgador = Id_Tipo_Ubicacion_Colgador,
                };

                var ubicacionColgador = await connection.QueryAsync<Tx_Ubicacion_Colgador>(
                     "[dbo].[Tx_Listar_Ubicacion_Colgador]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return ubicacionColgador;
            }
        }

        public async Task<IEnumerable<Tx_FamTela>?> ListadoTipoFamTela()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var ubicacionColgador = await connection.QueryAsync<Tx_FamTela>(
                     "[dbo].[Tx_Listar_Tipo_FamTela]",
                     commandType: System.Data.CommandType.StoredProcedure
                 );

                return ubicacionColgador;
            }
        }

        public async Task<IEnumerable<Tx_Tipo_Ubicacion_Colgador>?> ListadoTipoUbicacionColgador()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var ubicacionColgador = await connection.QueryAsync<Tx_Tipo_Ubicacion_Colgador>(
                     "[dbo].[Tx_Listar_Tipo_Ubicaciones]",
                     commandType: System.Data.CommandType.StoredProcedure
                 );

                return ubicacionColgador;
            }
        }

        public async Task<IEnumerable<Tx_TipoUbicacionControl>?> ObtenerCorrelativo(int Id_Tipo_Ubicacion_Colgador, string Cod_FamTela)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Id_Tipo_Ubicacion_Colgador = Id_Tipo_Ubicacion_Colgador,
                    Cod_FamTela = Cod_FamTela
                };

                var result = await connection.QueryAsync<Tx_TipoUbicacionControl>(
                     "[dbo].[Tx_Obtener_Correlativo_Tipo_Ubicacion]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<(int Codigo, string Mensaje)> CrudUbicacionColgadorItems(Tx_Ubicacion_Colgador_Items tx_Ubicacion_Colgador_Items, string CodigoBarra, string sTipoTransac)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();
                parametros.Add("@Accion", sTipoTransac);
                parametros.Add("@Id_Tx_Ubicacion_Colgador", tx_Ubicacion_Colgador_Items.Id_Tx_Ubicacion_Colgador);
                parametros.Add("@CodigoBarra", CodigoBarra);
                parametros.Add("@Id_Tx_Ubicacion_Fisica", tx_Ubicacion_Colgador_Items.Id_Tx_Ubicacion_Fisica);
                parametros.Add("@Cod_Usuario", tx_Ubicacion_Colgador_Items.Usu_Registro);

                // Parámetros de salida
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                // Ejecutar el procedimiento almacenado
                connection.Execute(
                    "[dbo].[Tx_Ubicar_Reubicar_Colgador]",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

                //Obtener los valores de salida
                var codigo = parametros.Get<int>("@Codigo");
                var mensaje = parametros.Get<string>("@sMsj");

                return (codigo, mensaje);

            }
        }

        public async Task<IEnumerable<Tx_Ubicacion_Colgador>?> ObtenerUbicacionColgadorQR(string CodigoBarra)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    CodigoBarra = CodigoBarra
                };

                var result = await connection.QueryAsync<Tx_Ubicacion_Colgador>(
                     "[dbo].[Tx_Obtener_Ubicacion_Colgador_Codigo_QR]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<IEnumerable<Tx_Ubicacion_Colgador>?> ObtenerUbicacionColgadorById(int Id_Tx_Ubicacion_Colgador)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Id_Tx_Ubicacion_Colgador = Id_Tx_Ubicacion_Colgador
                };

                var result = await connection.QueryAsync<Tx_Ubicacion_Colgador>(
                     "[dbo].[Tx_Obtener_Ubicacion_Colgador_ById]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<IEnumerable<Tx_Ubicacion_Colgador>?> ListadoTotalColgadoresxTipoUbicaciones(DateTime? FecCrea)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Fch_Crea = FecCrea
                };

                var result = await connection.QueryAsync<Tx_Ubicacion_Colgador>(
                     "[dbo].[Tx_Obtener_Total_Colgadores_x_Tipo_Ubicacion]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<IEnumerable<Tx_Colgador_Registro_Cab>?> ListadoColgadoresxUbicacion(int Id_Tx_Ubicacion_Colgador)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Id_Tx_Ubicacion_Colgador = Id_Tx_Ubicacion_Colgador
                };

                var result = await connection.QueryAsync<Tx_Colgador_Registro_Cab>(
                     "[dbo].[Tx_Listar_Colgadores_x_Tipo_Ubicacion]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<IEnumerable<Tx_Ubicacion_Colgador>?> ListadoTotalColgadoresxCodigoBarra(string CodigoBarra)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    CodigoBarra = CodigoBarra
                };

                var result = await connection.QueryAsync<Tx_Ubicacion_Colgador>(
                     "[dbo].[Tx_Obtener_Total_Colgadores_x_CodigoBarra]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<IEnumerable<Tx_Ubicacion_Fisica>?> ListadoUbicacioFisica()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var ubicacionColgador = await connection.QueryAsync<Tx_Ubicacion_Fisica>(
                     "[dbo].[Tx_Listar_Ubicacion_Fisica]",
                     commandType: System.Data.CommandType.StoredProcedure
                 );

                return ubicacionColgador;
            }
        }

        public async Task<IEnumerable<Tx_Ubicacion_Colgador>?> ObtenerInformacionTotalCajasxUbicacion(int Id_Tx_Ubicacion_Fisica)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Id_Tx_Ubicacion_Fisica = Id_Tx_Ubicacion_Fisica
                };

                var result = await connection.QueryAsync<Tx_Ubicacion_Colgador>(
                     "[dbo].[Tx_Obtiene_Informacion_Total_Cajas_x_Ubicacion]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<IEnumerable<Tx_Ubicacion_Colgador>?> ObtenerInformacionCajasxUbicacion(int Id_Tx_Ubicacion_Fisica)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Id_Tx_Ubicacion_Fisica = Id_Tx_Ubicacion_Fisica
                };

                var result = await connection.QueryAsync<Tx_Ubicacion_Colgador>(
                     "[dbo].[Tx_Obtiene_Informacion_Cajas_x_Ubicacion]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<IEnumerable<Tx_Ubicacion_Impresora_Activa>?> ObtenerImpresoraPredeterminada()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.QueryAsync<Tx_Ubicacion_Impresora_Activa>(
                     "[dbo].[Tx_Obtener_Impresora_Predeterminada]",
                     commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<IEnumerable<Tx_Cliente>?> ObtieneAbreviaturaCliente(string Cod_Tela, string Cod_Ruta, string Cod_OrdTra)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Cod_Tela = Cod_Tela,
                    Cod_Ruta = Cod_Ruta,
                    Cod_OrdTra = Cod_OrdTra
                };

                var result = await connection.QueryAsync<Tx_Cliente>(
                     "[dbo].[Tx_Obtiene_Abreviatura_Cliente]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<IEnumerable<Tx_Colgador_Reporte_Gral>?> ReporteColgadoresGralDetallado()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.QueryAsync<Tx_Colgador_Reporte_Gral>(
                     "[dbo].[Tx_Reporte_Ctrol_Colgadores_Detalle]",
                     commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }
    }
}

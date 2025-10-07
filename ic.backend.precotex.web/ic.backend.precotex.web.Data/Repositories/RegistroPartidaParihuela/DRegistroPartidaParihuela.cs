using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ic.backend.precotex.web.Data.Repositories.Implementation.RegistroPartidaParihuela;
using ic.backend.precotex.web.Entity.Entities;
using Microsoft.Extensions.Configuration;

namespace ic.backend.precotex.web.Data.Repositories.RegistroPartidaParihuela
{
    public class DRegistroPartidaParihuela: IRegistroPartidaParihuela
    {
        private readonly string _connectionString;

        public DRegistroPartidaParihuela(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnection")!;
        }

        public async Task<IEnumerable<E_RegistroPartidaParihuela>?> ObtenerDetPartida(string pCod_Partida, string pOpcion)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var corteEncogimiento = await connection.QueryAsync<E_RegistroPartidaParihuela>(
                        "[dbo].[SP_Buscar_Partida_Parihuela]",
                        new { pCod_Partida = pCod_Partida, pOpcion = pOpcion
                        },
                        commandType: System.Data.CommandType.StoredProcedure
                    );

                    return corteEncogimiento;
                }
            }
            catch (SqlException sqlEx) // Captura errores de SQL Server
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw; // Relanza la excepción si es necesario
            }

        }

        public async Task<IEnumerable<E_RegistroPartidaParihuela>?> UpdateDetPartida(List<E_RegistroPartidaParihuela> pData, string pCod_Usuario, string pEstadoParihuela)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    // Convertir List<E_RegistroPartidaParihuela> a DataTable
                    var dataTable = new DataTable();
                    dataTable.Columns.Add("CodigoPartida", typeof(string));
                    dataTable.Columns.Add("CodigoParihuela", typeof(string));
                    dataTable.Columns.Add("PesoParihuela", typeof(decimal));
                    dataTable.Columns.Add("PesoBruto", typeof(decimal));
                    dataTable.Columns.Add("Complemento", typeof(string));
                    dataTable.Columns.Add("PesoNeto", typeof(decimal));
                    dataTable.Columns.Add("PesoComplemento", typeof(decimal));

                    foreach (var item in pData)
                    {
                        dataTable.Rows.Add(
                            item.CodigoPartida,
                            item.CodigoParihuela,
                            item.PesoParihuela,
                            item.PesoBruto,
                            item.Complemento,
                            item.PesoNeto,
                            item.PesoComplemento
                        );
                    }

                    // Parámetros para el procedimiento almacenado
                    var parameters = new DynamicParameters();
                    parameters.Add("@pData", dataTable.AsTableValuedParameter("dbo.TipoRegistroPartidaParihuela")); // Tipo de tabla en SQL
                    parameters.Add("@pCod_Usuario", pCod_Usuario, DbType.String);
                    parameters.Add("@pEstadoParihuela", pEstadoParihuela, DbType.String);

                    // Ejecutar SP con Dapper
                    var result = await connection.QueryAsync<E_RegistroPartidaParihuela>(
                        "[dbo].[SP_Update_Detalle_Partida]",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    return result.ToList();
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw new Exception("Error al actualizar la partida. Consulte con el administrador.", sqlEx);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                throw new Exception("Se produjo un error inesperado.", ex);
            }

        }

        public async Task<IEnumerable<E_Complemento>?> ObtenerCategoriasPorId(string pIdPartida)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var corteEncogimiento = await connection.QueryAsync<E_Complemento>(
                         "[dbo].[SP_Obtener_Complemento]",
                        new { pIdPartida = pIdPartida },
                        commandType: System.Data.CommandType.StoredProcedure
                     );

                    return corteEncogimiento;
                }
            }
            catch (SqlException sqlEx) // Captura errores de SQL Server
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw; // Relanza la excepción si es necesario
            }
        }

        public async Task<IEnumerable<String>?> validarMerma(string pIdPartida)
        {
            try
            {

                using (var connection = new SqlConnection(_connectionString))
                {
                    var EstadoMerma = await connection.QueryAsync<String>(
                        "SP_S_CALCULO_MERMA_WEB",
                        new
                        {
                            @pCod_Partida = pIdPartida
                        },
                        commandType: System.Data.CommandType.StoredProcedure
                    );

                    return EstadoMerma;
                }

                //using (var connection = new SqlConnection(_connectionString))
                //{
                //    var corteEncogimiento = await connection.QueryAsync<RolloDetalle>(
                //        "TI_MUESTRA_DETALLE_POR_ROLLO_POR_PARTIDA_CALIFICACION",
                //        new
                //        {
                //            @COD_ORDTRA = pIdPartida,
                //            @OPCION = "1"
                //        },
                //        commandType: System.Data.CommandType.StoredProcedure
                //    );

                //    // Filtrar los registros donde Calidad es 1 y sumar los KGS_CRUDO
                //    decimal totalKgsCrudo = corteEncogimiento
                //        .Where(r => r.Calidad == "1")
                //        .Sum(r => r.KGS_CRUDO);

                //    return new List<decimal> { totalKgsCrudo };
                //}
            }
            catch (SqlException sqlEx) // Captura errores de SQL Server
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw; // Relanza la excepción si es necesario
            }
        }


        public async Task<IEnumerable<String>?> UpdateEstadoMermaAsync(string pIdPartida)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = @"
                                    UPDATE PartidaPeso
                                    SET EstadoParihuela = @NuevoEstado
                                    WHERE CodigoPartida = @IdPartida
                                ";

                    var parameters = new
                    {
                        NuevoEstado = "1", // o lo que necesites asignar
                        IdPartida = pIdPartida
                    };

                    int filasAfectadas = await connection.ExecuteAsync(query, parameters);
                    return null;
                   // Console.WriteLine($"Filas actualizadas: {filasAfectadas}");
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<string>?> EnviarDespacho(string pCod_Partida)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rusltado = await connection.QueryAsync<string>(
                        "[dbo].[SP_S_Enviar_Despacho]",
                        new
                        {
                            pCod_Partida = pCod_Partida
                        },
                        commandType: System.Data.CommandType.StoredProcedure
                    );

                    return rusltado;
                }
            }
            catch (SqlException sqlEx)
            {
                // Lanza una excepción personalizada o estándar para que el controlador lo maneje
                throw; // new ApplicationException($"Error en SQL Server: {sqlEx.Message}", sqlEx);
            }

        }


        //INSERTA CABECERA CUANDO SE DA CLICK AL BOTON ENVIAR DESPACHO PARA LUEGO EJECUTAR EL MÉTODO EnviarDespacho
        public async Task<(int Codigo, string Mensaje)> EnviarCabecera(string pCod_Partida)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();

                parametros.Add("@pCod_Partida", pCod_Partida);

                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                try
                {
                    connection.Execute(
                    "[dbo].[PA_PRE_TX_MOVISTK_I0001]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                    );
                }
                catch
                {

                }

                var Codigo = parametros.Get<int>("@Codigo");
                var mensaje = parametros.Get<string>("@sMsj");
                return (Codigo, mensaje);
            }
        }

    }
}

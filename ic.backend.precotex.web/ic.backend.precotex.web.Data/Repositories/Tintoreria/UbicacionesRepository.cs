using Dapper;
using ic.backend.precotex.web.Data.Repositories.Implementation.Tintoreria;
using ic.backend.precotex.web.Entity.Entities.Tintoreria;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Tintoreria
{
    public class UbicacionesRepository : IUbicacionesRepository
    {
        private readonly string _connectionString;

        public UbicacionesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnection")!;
        }

        public async Task<IEnumerable<Ubicaciones.ListaBultoUbicaciones>?> ListaBultoUbicaciones(string? Cod_Almacen, string? Codigo_Barra_Grupo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Cod_Almacen = Cod_Almacen,
                    Codigo_Barra_Grupo = Codigo_Barra_Grupo
                };

                var result = await connection.QueryAsync<Ubicaciones.ListaBultoUbicaciones>(
                     "[dbo].[Tx_Listar_Bultos_Ubicados_Multialmacen]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<(int Codigo, string Mensaje, string CodigoBarraGrupo)> InsertarBultoGrupo(Ubicaciones.InsertarBultoGrupo ubicaciones)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();
                parametros.Add("@Accion", ubicaciones.Accion);
                parametros.Add("@Id_Bulto_Hilado_Grupo", ubicaciones.Id_Bulto_Hilado_Grupo);
                parametros.Add("@Num_Corre", ubicaciones.Num_Corre);
                parametros.Add("@Cod_Usuario", ubicaciones.Cod_Usuario);

                // Parámetros de salida
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                string codigoBarraGrupo = null;

                // ACCIÓN 'C' (Creación): Devuelve un ResultSet (SELECT), usamos QueryFirstOrDefaultAsync
                if (ubicaciones.Accion == "C")
                {
                    var resultado = await connection.QueryFirstOrDefaultAsync<dynamic>(
                        "[dbo].[Tx_Insertar_Bulto_Grupo_Multialmacen]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                    if (resultado != null)
                    {
                        // Mapeamos la columna 'Codigo_Barra_Grupo' que devuelve el SELECT del procedimiento
                        codigoBarraGrupo = resultado.Codigo_Barra_Grupo;
                    }
                }
                // ACCIONES 'I' o 'D' (Vincular/Desvincular): No devuelven ResultSet, usamos ExecuteAsync
                else
                {
                    await connection.ExecuteAsync(
                        "[dbo].[Tx_Insertar_Bulto_Grupo_Multialmacen]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );
                }

                // Recuperamos los parámetros de salida al finalizar la ejecución
                var codigo = parametros.Get<int>("@Codigo");
                var mensaje = parametros.Get<string>("@sMsj");

                return (codigo!, mensaje!, codigoBarraGrupo!);
            }
        }

        public async Task<(int Codigo, string Mensaje)> UbicarGrupoOBulto(Ubicaciones.UbicarGrupoOBulto ubicaciones)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();
                parametros.Add("@Accion", ubicaciones.Accion);
                parametros.Add("@Id_Agrupamiento", ubicaciones.Id_Agrupamiento);
                parametros.Add("@Num_Corre", ubicaciones.Num_Corre);
                parametros.Add("@Codigo_Ubicacion_Dest", ubicaciones.Codigo_Ubicacion_Dest);
                parametros.Add("@Cod_Usuario", ubicaciones.Cod_Usuario);

                // Parámetros de salida
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                await connection.ExecuteAsync(
                    "[dbo].[Tx_Ubicar_Grupo_O_Bulto_Multialmacen]",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

                var codigo = parametros.Get<int>("@Codigo");
                var mensaje = parametros.Get<string>("@sMsj");

                return (codigo!, mensaje!);
            }
        }

        public async Task<IEnumerable<Ubicaciones.ListaAgrupamientosDelDia>?> ListaAgrupamientosDelDia(DateTime? Fec_Creacion, string? Codigo_Barra_Grupo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Fec_Creacion = Fec_Creacion,
                    Codigo_Barra_Grupo = Codigo_Barra_Grupo
                };

                var result = await connection.QueryAsync<Ubicaciones.ListaAgrupamientosDelDia>(
                     "[dbo].[Tx_Listar_Agrupamientos_Del_Dia_Multialmacen]"
                     , parametros
                     , commandType: CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<IEnumerable<Ubicaciones.ListaDetalleBultosAgrupados>?> ListaDetalleBultosAgrupados(string? Cod_Almacen, int? Id_Agrupamiento, string? Codigo_Barra_Grupo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Cod_Almacen = Cod_Almacen,
                    Id_Agrupamiento = Id_Agrupamiento,
                    Codigo_Barra_Grupo = Codigo_Barra_Grupo
                };

                var result = await connection.QueryAsync<Ubicaciones.ListaDetalleBultosAgrupados>(
                     "[dbo].[Tx_Obtener_Detalle_Bultos_Agrupados]"
                     , parametros
                     , commandType: CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<Ubicaciones.ConsultaKardexPda?> ConsultaKardexPda(string? Cod_Almacen, string? Codigo_Escaneado)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    var parametros = new
                    {
                        Cod_Almacen = Cod_Almacen,
                        Codigo_Escaneado = Codigo_Escaneado
                    };

                    using (var multi = await connection.QueryMultipleAsync(
                        "[dbo].[Tx_Consulta_Kardex_PDA]",
                        parametros,
                        commandType: CommandType.StoredProcedure))
                    {
                        var cabecera = await multi.ReadFirstOrDefaultAsync<Ubicaciones.CabeceraKardexPda>();
                        var movimientos = (await multi.ReadAsync<Ubicaciones.MovimientoKardexPda>()).ToList();

                        return new Ubicaciones.ConsultaKardexPda
                        {
                            Cabecera = cabecera,
                            Movimientos = movimientos
                        };
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                    throw;
                }
            }
        }
    }
}

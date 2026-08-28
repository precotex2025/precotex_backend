using Dapper;
using ic.backend.precotex.web.Data.Repositories.Implementation.Tintoreria;
using ic.backend.precotex.web.Entity.Entities.Tintoreria;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        public async Task<IEnumerable<Ubicaciones.ListaBultoUbicaciones>?> ListaBultoUbicaciones(string? Cod_Almacen, string? Cod_Item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Cod_Almacen = Cod_Almacen,
                    Cod_Item = Cod_Item
                };

                var result = await connection.QueryAsync<Ubicaciones.ListaBultoUbicaciones>(
                     "[dbo].[Tx_Listar_Bultos_Ubicados_Multialmacen]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<(int Codigo, string Mensaje)> InsertarBultoGrupo(Ubicaciones.InsertarBultoGrupo ubicaciones)
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

                await connection.ExecuteAsync(
                    "[dbo].[Tx_Insertar_Bulto_Grupo_Multialmacen]",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

                var codigo = parametros.Get<int>("@Codigo");
                var mensaje = parametros.Get<string>("@sMsj");

                return (codigo, mensaje);
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

        public async Task<IEnumerable<Ubicaciones.ListaDetalleBultosAgrupados>?> ListaDetalleBultosAgrupados(string? Cod_Almacen, int? Id_Agrupamiento)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Cod_Almacen = Cod_Almacen,
                    Id_Agrupamiento = Id_Agrupamiento
                };

                var result = await connection.QueryAsync<Ubicaciones.ListaDetalleBultosAgrupados>(
                     "[dbo].[Tx_Obtener_Detalle_Bultos_Agrupados]"
                     , parametros
                     , commandType: CommandType.StoredProcedure
                 );

                return result;
            }
        }
    }
}

using Dapper;
using ic.backend.precotex.web.Data.Repositories.Implementation.Almacen;
using ic.backend.precotex.web.Entity.common;
using ic.backend.precotex.web.Entity.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Almacen
{
    public class TxBultoHiladoGrupoRepository : ITxBultoHiladoGrupoRepository
    {
        private readonly string _connectionString;
        public TxBultoHiladoGrupoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnection")!;
        }

        public async Task<(int Codigo, string Mensaje)> Insertar(Tx_Bulto_Hilado_Grupo tx_Bulto_Hilado_Grupo)
        {
            //int codigo = 0;
            //string mensaje = string.Empty;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();
                parametros.Add("@Accion", tx_Bulto_Hilado_Grupo.Accion);
                parametros.Add("@Id_Bulto_Hilado_Grupo", tx_Bulto_Hilado_Grupo.Id_Bulto_Hilado_Grupo);
                parametros.Add("@Num_Corre", tx_Bulto_Hilado_Grupo.Num_Corre);
                parametros.Add("@Cod_Usuario", tx_Bulto_Hilado_Grupo.Usu_Registro);

                // Parámetros de salida
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                // Ejecutar el procedimiento almacenado
                connection.Execute(
                    "[dbo].[Tx_Insertar_Bulto_Hilado_Grupo]",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

                //Obtener los valores de salida
                var codigo = parametros.Get<int>("@Codigo");
                var mensaje = parametros.Get<string>("@sMsj");

                return (codigo, mensaje);
            }
        }

        public async Task<IEnumerable<Tx_Bulto_Hilado_Grupo>?> Lista(DateTime? FecCrea, string? Grupo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var grupo = await connection.QueryAsync<Tx_Bulto_Hilado_Grupo>(
                     "[dbo].[Tx_Listar_Bulto_Hilado_Grupo_Cab]",
                     new { Fch_Crea = FecCrea, Grupo = Grupo == null ? "" : Grupo },
                     commandType: System.Data.CommandType.StoredProcedure
                 );

                return grupo;
            }
        }

        public async Task<IEnumerable<Tx_Bulto_Hilado_Grupo>?> ListaByIdUbicacion(string? CodUbicacion)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var grupoUbi = await connection.QueryAsync<Tx_Bulto_Hilado_Grupo>(
                     "[dbo].[Tx_Listar_Bulto_Hilado_Grupo_By_Ubicacion]",
                     new { Cod_Ubicacion = CodUbicacion },
                     commandType: System.Data.CommandType.StoredProcedure
                 );

                return grupoUbi;
            }
        }

        public async Task<IEnumerable<Tx_Bulto_Hilado_Grupo>?> ListaDet(string? Grupo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new
                {
                    Grupo = Grupo ?? (object)DBNull.Value,  // Si el parámetro es null, lo asignamos como DBNull
                };

                var grupo = await connection.QueryAsync<Tx_Bulto_Hilado_Grupo>(
                     "[dbo].[Tx_Listar_Bulto_Hilado_Grupo_Det]",
                     new { Grupo = Grupo },
                     commandType: System.Data.CommandType.StoredProcedure
                 );

                return grupo;
            }
        }

        public async Task<IEnumerable<Tx_Bulto_Hilado_Grupo>?> ListaDetById(int? IdBultoHiladoGrupo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var grupo = await connection.QueryAsync<Tx_Bulto_Hilado_Grupo>(
                     "[dbo].[Tx_Listar_Bulto_Hilado_Grupo_Det_By_Id]",
                     new { Id_Bulto_Hilado_Grupo = IdBultoHiladoGrupo },
                     commandType: System.Data.CommandType.StoredProcedure
                 );

                return grupo;
            }
        }

        public async Task<IEnumerable<Tx_Bulto_Hilado_Grupo>?> Obtener(int? IdBultoHiladoGrupo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var grupo = await connection.QueryAsync<Tx_Bulto_Hilado_Grupo>(
                     "[dbo].[Tx_Obtener_Bulto_Hilado_Grupo]",
                     new { Id_Bulto_Hilado_Grupo = IdBultoHiladoGrupo },
                     commandType: System.Data.CommandType.StoredProcedure
                 );

                return grupo;
            }
        }

        public async Task<IEnumerable<Tx_Bulto_Hilado_Grupo>?> ObtenerByCode(string? Grupo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var grupo = await connection.QueryAsync<Tx_Bulto_Hilado_Grupo>(
                     "[dbo].[Tx_Obtener_Bulto_Hilado_Grupo_By_Codigo]",
                     new { Grupo = Grupo },
                     commandType: System.Data.CommandType.StoredProcedure
                 );

                return grupo;
            }
        }

        public async Task<(int Codigo, string Mensaje)> UbicarReubicar(Tx_Bulto_Hilado_Grupo tx_Bulto_Hilado_Grupo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();
                parametros.Add("@Accion", tx_Bulto_Hilado_Grupo.Accion);
                parametros.Add("@Grupo", tx_Bulto_Hilado_Grupo.Grupo);
                parametros.Add("@Cod_Ubicacion", tx_Bulto_Hilado_Grupo.Cod_Ubicacion);
                parametros.Add("@Cod_Usuario", tx_Bulto_Hilado_Grupo.Usu_Registro);

                // Parámetros de salida
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                // Ejecutar el procedimiento almacenado
                connection.Execute(
                    "[dbo].[Tx_Ubicar_Reubicar_Bulto_Hilado_Grupo]",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

                //Obtener los valores de salida
                var codigo = parametros.Get<int>("@Codigo");
                var mensaje = parametros.Get<string>("@sMsj");

                return (codigo, mensaje);
            }
        }

        public async Task<(int Codigo, string Mensaje)> Validar(string? Grupo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();
                parametros.Add("@Grupo", Grupo);

                // Parámetros de salida
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                // Ejecutar el procedimiento almacenado
                connection.Execute(
                    "[dbo].[Tx_Valida_Bulto_Hilado_Grupo]",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

                //Obtener los valores de salida
                var codigo = parametros.Get<int>("@Codigo");
                var mensaje = parametros.Get<string>("@sMsj");

                return (codigo, mensaje);
            }
        }
    }
}

using System.Data;
using System.Data.SqlClient;
using Dapper;
using ic.backend.precotex.web.Entity;
using Microsoft.Extensions.Configuration;

namespace ic.backend.precotex.web.Data;

public class LecturaBultosRepository: ILecturaBultosRepository
{
    private readonly string _connectionString;

    public LecturaBultosRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("TextilConnection")!;
    }

    //LISTAR ALMACENES
    public async Task<IEnumerable<Lg_LecturaBultos_Almacenes>?> ListarAlmacenesDisponibles()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var result = await connection.QueryAsync<Lg_LecturaBultos_Almacenes>(
                "[dbo].[PA_Lg_Almacen_S0001]"
                , commandType: System.Data.CommandType.StoredProcedure
            );

            return result;
        }
    }

    //LISTAR NOVIMIENTOS
    public async Task<IEnumerable<Lg_LecturaBultos>?> ListarMovimientos(string? Num_MovStk, string? Cod_Almacen, DateTime? Fec_MovStk)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            
            var parametros = new DynamicParameters();
            parametros.Add("@Num_MovStk", Num_MovStk);
            parametros.Add("@Cod_Almacen", Cod_Almacen);
            parametros.Add("@Fec_MovStk", Fec_MovStk);

            var result = await connection.QueryAsync<Lg_LecturaBultos>(
                "[dbo].[PA_Lg_MoviStk_S0001]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }

    //LISTAR BULTOS
    public async Task<IEnumerable<Lg_Bultos>?> ListarBultos(string? Num_MovStk, string? Cod_Almacen)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            
            var parametros = new DynamicParameters();
            parametros.Add("@Num_MovStk", Num_MovStk);
            parametros.Add("@Cod_Almacen", Cod_Almacen);

            var result = await connection.QueryAsync<Lg_Bultos>(
                "[dbo].[PA_Lg_MoviStk_Bultos_S0001]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }

    public async Task<(int Codigo, string Mensaje)> LecturarBulto(Lg_Bultos valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            
            var parametros = new DynamicParameters();
            parametros.Add("@Num_MovStk", valores.Num_MovStk);  
            parametros.Add("@Cod_Almacen", valores.Cod_Almacen);
            parametros.Add("@Num_Corre", valores.Num_Corre);
            parametros.Add("@Codigo", 0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_MoviStk_Bultos_U0001]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                );
            }catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            var Codigo = parametros.Get<int>("@Codigo");
            var Mensaje = parametros.Get<string>("@sMsj");

            return (Codigo, Mensaje);
        }
    }

}

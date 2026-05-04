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

}

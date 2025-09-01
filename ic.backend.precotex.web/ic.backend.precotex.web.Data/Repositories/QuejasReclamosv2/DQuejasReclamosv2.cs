using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ic.backend.precotex.web.Entity.Entities.QuejasReclamos;
using Microsoft.Extensions.Configuration;

namespace ic.backend.precotex.web.Data.Repositories.QuejasReclamosv2
{
    public class DQuejasReclamosv2
    {
        private readonly string _connectionString;
        private readonly string _connectionSeguridadString;

        public DQuejasReclamosv2(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnection")!;
            _connectionSeguridadString = configuration.GetConnectionString("TextilConnectionSeguridad")!;
        }

        public async Task<IEnumerable<EstadoDto>?> ObtenerEstado()
        {
            try

            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = @"
                                    SELECT IdEstado, Acronimo, Estado FROM EstadoQuejas WHERE FlagEstado = 1
                                ";

                    var estado = await connection.QueryAsync<EstadoDto>(query);
                    return estado;
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

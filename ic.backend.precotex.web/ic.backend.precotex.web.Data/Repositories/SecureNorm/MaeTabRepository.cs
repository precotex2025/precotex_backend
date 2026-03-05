using Dapper;
using ic.backend.precotex.web.Data.Repositories.Implementation.SecureNorm;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.SecureNorm
{
    public class MaeTabRepository : IMaeTabRepository
    {
        private readonly string _connectionString;

        public MaeTabRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnectionSomma")!;
        }

        public async Task<IEnumerable<ComboGral>?> Lista(string sCodigoTipo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    CodigoTipo = sCodigoTipo
                };

                var result = await connection.QueryAsync<ComboGral>(
                     "[dbo].[SN_MAE_TAB]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }
    }
}

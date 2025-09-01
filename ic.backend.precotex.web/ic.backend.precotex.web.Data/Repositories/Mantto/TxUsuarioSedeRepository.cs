using Dapper;
using ic.backend.precotex.web.Data.Repositories.Implementation.Mantto;
using ic.backend.precotex.web.Entity.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Mantto
{
    public class TxUsuarioSedeRepository : ITxUsuarioSedeRepository
    {

        private readonly string _connectionString;

        public TxUsuarioSedeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnection")!;
        }

        public async Task<IEnumerable<Tx_Usuario_Sede>?> ListaUsuarioSedeByUser(string? Cod_Usuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Cod_Usuario = Cod_Usuario,
                };

                var result = await connection.QueryAsync<Tx_Usuario_Sede>(
                     "[dbo].[Tx_Lista_Sedes_Usuario]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }
    }
}

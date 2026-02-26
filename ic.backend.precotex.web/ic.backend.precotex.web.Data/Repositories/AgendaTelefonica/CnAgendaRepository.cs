using Dapper;
using ic.backend.precotex.web.Data.Repositories.Implementation.AgendaTelefonica;
using ic.backend.precotex.web.Entity.Entities.AgendaTelefonica;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ic.backend.precotex.web.Data.Repositories.AgendaTelefonica
{
    public class CnAgendaRepository: ICnAgendaRepository
    {
        private readonly string _connectionString;

        public CnAgendaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnection")!;
        }

        public async Task<IEnumerable<Cn_Agenda>?> ObtenerNumeros()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<Cn_Agenda>(
                    "[dbo].[PA_CN_AGENDA_TELEFONO_S0001]"
                    , commandType: CommandType.StoredProcedure
                    );

                return result;
            }
        }
    }
}

using Dapper;
using ic.backend.precotex.web.Data.Repositories.Implementation.Almacen;
using ic.backend.precotex.web.Entity.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Almacen
{
    public class TmpVisorPermanenciaTelaCrudaRepository : ITmpVisorPermanenciaTelaCrudaRepository
    {

        private readonly string _connectionString;
        public TmpVisorPermanenciaTelaCrudaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnection")!;
        }

        public async Task<IEnumerable<Tx_Visor_Permanencia_Tela_Cruda>?> ObtieneListaPermanenciaTelaCruda(int? anio)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var grupo = await connection.QueryAsync<Tx_Visor_Permanencia_Tela_Cruda>(
                "[dbo].[Tx_Visor_Permanencia_Tela_Cruda]",
                new { ANIO = anio },
                     commandType: System.Data.CommandType.StoredProcedure
                 );

                return grupo;
            }
        }
    }
}

using Dapper;
using ic.backend.precotex.web.Data.Repositories.Implementation.Tejeduria;
using ic.backend.precotex.web.Entity.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Tejeduria
{
    public class TjTiempoImproductivoRepository : ITjTiempoImproductivoRepository
    {
        private readonly string _connectionString;

        public TjTiempoImproductivoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnection")!;
        }


        public async Task<IEnumerable<Tj_Tiempo_Improductivo>?> ObtieneTiempoImproductivoPendiente(string? sCodMaquina)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    cod_maquina = sCodMaquina,
                };

                var result = await connection.QueryAsync<Tj_Tiempo_Improductivo>(
                     "[dbo].[Tx_Obtiene_Tiempo_Improductivo_Pendiente]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }
    }
}

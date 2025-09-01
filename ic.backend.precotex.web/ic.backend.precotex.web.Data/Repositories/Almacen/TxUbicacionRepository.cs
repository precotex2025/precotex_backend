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
    public class TxUbicacionRepository: ITxUbicacionRepository
    {
        private readonly string _connectionString;
        public TxUbicacionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnection")!;
        }

        public async Task<IEnumerable<Tx_Ubicacion>?> ListaByCodigoUbicacion(string? Cod_Ubicacion)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var ubicacion = await connection.QueryAsync<Tx_Ubicacion>(
                     "[dbo].[Tx_Obtiene_Ubicacion]",
                     new { Cod_Ubicacion = Cod_Ubicacion },
                     commandType: System.Data.CommandType.StoredProcedure
                 );

                return ubicacion;
            }
        }
    }
}

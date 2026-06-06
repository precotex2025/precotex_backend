using Dapper;
using ic.backend.precotex.web.Data.Repositories.Implementation.Tejeduria;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.Tejeduria;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Tejeduria
{
    public class TjSeguimientoSaldoHiloRepository: ITjSeguimientoSaldoHiloRepository
    {
        private readonly string _connectionString;

        public TjSeguimientoSaldoHiloRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnection")!;
        }

        public async Task<IEnumerable<tj_Muestra_OT_Programada>?> ListaOT_Programada(string Cod_OrdProv, string Cod_HilTel)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Cod_OrdProv = Cod_OrdProv,
                    Cod_HilTel = Cod_HilTel
                };

                var result = await connection.QueryAsync<tj_Muestra_OT_Programada>(
                     "[dbo].[Tj_Muestra_OT_Programadas]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<IEnumerable<tj_Muestra_OT_Terminada>?> ListaOT_Terminada(DateTime Fecha, string Flg_Pendiente)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Fecha = Fecha,
                    Flg_Pendiente = Fecha
                };

                var result = await connection.QueryAsync<tj_Muestra_OT_Terminada>(
                     "[dbo].[Tj_Muestra_OT_Terminadas]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }
    }
}

using Dapper;
using ic.backend.precotex.web.Entity.Entities.Laboratorio;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ic.backend.precotex.web.Entity.Entities.Cotizaciones;
using ic.backend.precotex.web.Data.Repositories.Implementation.Cotizaciones;

namespace ic.backend.precotex.web.Data.Repositories.Cotizaciones
{
    public class TxCotizacionesRepository: ITxCotizacionesRepository
    {
        private readonly string _connectionString;

        //DECLARAMOS CADENA DE CONEXION
        public TxCotizacionesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnection")!;
        }

        public async Task<IEnumerable<Tx_Cotizaciones>?> ListarProcesosExportacion(string Pro_Cen_Cos)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                parametros.Add("@Pro_Cen_Cos", Pro_Cen_Cos);

                var result = await connection.QueryAsync<Tx_Cotizaciones>(
                        "[dbo].[PA_Tx_Cotizaciones_Procesos_S0001]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

    }
}

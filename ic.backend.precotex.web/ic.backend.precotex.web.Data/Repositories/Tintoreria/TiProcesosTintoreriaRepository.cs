using Dapper;
using ic.backend.precotex.web.Data.Repositories.Implementation.Tintoreria;
using ic.backend.precotex.web.Entity.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Tintoreria
{
    public class TiProcesosTintoreriaRepository : ITiProcesosTintoreriaRepository
    {
        private readonly string _connectionString;

        public TiProcesosTintoreriaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnection")!;
        }

        public async Task<IEnumerable<Tx_Muestra_Control_Proceso>?> ListaControlProcesosTintoreria(string Cod_Ordtra, DateTime? Fecha_Ini, DateTime? Fecha_Fin)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sPartida = Cod_Ordtra ?? "";
                var parametros = new
                {
                    CLIENTE     = "",
                    MACHINE     = "",
                    PARTIDA     = Cod_Ordtra,
                    FECREGINI   = Fecha_Ini,
                    FECREGFIN   = Fecha_Fin,
                    COD_USUARIO = "",
                };

                var estatusProcesoTinto = await connection.QueryAsync<Tx_Muestra_Control_Proceso>(
                     "[dbo].[TI_MUESTRA_CONTROL_PROCESO]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return estatusProcesoTinto;
            }
        }

        public async Task<IEnumerable<Ti_Seguimiento_Tobera>?> ListaDetalleToberaPruebaTenido(string Cod_Ordtra, string IdOrgatexUnico)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Partida = Cod_Ordtra,
                    IdOrgatexUnico = IdOrgatexUnico
                };

                var result = await connection.QueryAsync<Ti_Seguimiento_Tobera>(
                     "[dbo].[Tx_Obtiene_Detalle_Tobera_Prueba_Tenido]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<IEnumerable<Ti_Procesos_Tintoreria>?> ListaEstatusControlTenido(string? Cod_Ordtra, DateTime? Fecha_Ini, DateTime? Fecha_Fin, string Cod_Usuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sPartida = Cod_Ordtra ?? "";
                var parametros = new
                {
                    Cod_Ordtra = sPartida,
                    Fecha_Ini = Fecha_Ini,
                    Fecha_Fin = Fecha_Fin,
                    Cod_Usuario = Cod_Usuario
                };

                var estatusProcesoTinto = await connection.QueryAsync<Ti_Procesos_Tintoreria>(
                     "[dbo].[Tx_Obtiene_Informacion_Estatus_Control_Tenido]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return estatusProcesoTinto;
            }
        }


    }
}

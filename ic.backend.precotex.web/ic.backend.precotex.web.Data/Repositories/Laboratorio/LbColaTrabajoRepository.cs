using ic.backend.precotex.web.Entity.Entities.Laboratorio;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ic.backend.precotex.web.Data.Repositories.Implementation.Laboratorio;

namespace ic.backend.precotex.web.Data.Repositories.Laboratorio
{
    public class LbColaTrabajoRepository: ILbColaTrabajoRepository
    {
        private readonly string _connectionString;

        //DECLARAMOS CADENA DE CONEXION
        public LbColaTrabajoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnection")!;
        }

        //OBTENER DATOS CABECERA
        public async Task<IEnumerable<Lb_ColTra_Cab>?> ListaSDCPorEstado()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<Lb_ColTra_Cab>(
                        "[dbo].[PA_LB_CARTACOL_DG_S0001]"
                        , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        //LISTAR COLORES DETALLE SDC
        public async Task<IEnumerable<Lb_ColTra_Det>?> ListaColoresSDC(int Corr_Carta)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();
                parametros.Add("@Corr_Carta", Corr_Carta);
                var result = await connection.QueryAsync<Lb_ColTra_Det>(
                    "[dbo].[PA_Lb_CartaCol_Detalle_S0001]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }
    }
}

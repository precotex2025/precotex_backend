using Dapper;
using ic.backend.precotex.web.Data.Repositories.Implementation.DDT;
using ic.backend.precotex.web.Data.Repositories.Implementation.OYM;
using ic.backend.precotex.web.Entity.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.OYM
{
    public class PartidaQRRepository : IPartidaQRRepository
    {
        private readonly string _connectionString;

        public PartidaQRRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnection")!;
        }

        public async Task<IEnumerable<Tx_Partida_IA>?> ObtieneInformacionPartidaQR(string Cod_OrdTra, int Num_Secuencia)
        {

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Cod_OrdTra = Cod_OrdTra,
                    Num_Secuencia = Num_Secuencia
                };

                var result = await connection.QueryAsync<dynamic>(
                     "[dbo].[TX_LISTADO_PARTIDA_IA]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );


                //Obtener el valor en caso sea
                var first = result.FirstOrDefault();
                if (first != null && first.Estado == "NO_DATA")
                {
                    throw new Exception(first.Mensaje); // o devuelve nulo, como prefieras
                }

                //Mapear las partidas
                var partidas = result.Select(r => new Tx_Partida_IA
                {
                    Cod_OrdTra = r.COD_ORDTRA,
                    Num_Secuencia = r.NUM_SECUENCIA,
                    Cod_Tela = r.COD_TELA,
                    Cod_Talla = r.COD_TALLA,
                    Largo = r.LARGO,
                    Ancho = r.ANCHO
                });

                return partidas;
            }
        }

        public async Task<(int Codigo, string Mensaje)> ProcesoInsertarPartidaQR(Tx_Partida_IA tx_Partida_IA, string sTipoTransac)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();
                parametros.Add("@Accion", sTipoTransac);
                parametros.Add("@Cod_OrdTra", tx_Partida_IA.Cod_OrdTra);
                parametros.Add("@Num_Secuencia", tx_Partida_IA.Num_Secuencia);
                parametros.Add("@Largo", tx_Partida_IA.Largo);
                parametros.Add("@Ancho", tx_Partida_IA.Ancho);
                parametros.Add("@Cod_Usuario", tx_Partida_IA.Usu_Registro);

                // Parámetros de salida
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                // Ejecutar el procedimiento almacenado
                connection.Execute(
                    "[dbo].[TX_REGISTRA_PARTIDA_IA]",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

                //Obtener los valores de salida
                var codigo = parametros.Get<int>("@Codigo");
                var mensaje = parametros.Get<string>("@sMsj");

                return (codigo, mensaje);

            }
        }
    }
}

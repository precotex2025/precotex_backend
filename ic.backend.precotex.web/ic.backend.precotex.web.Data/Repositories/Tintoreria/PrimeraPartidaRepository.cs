using Dapper;
using ic.backend.precotex.web.Data.Repositories.Implementation.Tintoreria;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Entity.Entities.Tintoreria;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Tintoreria
{
    public class PrimeraPartidaRepository : IPrimeraPartidaRepository
    {
        private readonly string _connectionString;
        public PrimeraPartidaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnection")!;
        }

        public async Task<IEnumerable<PrimeraPartidaBandeja>?> Lista(DateTime? Fecha_Ini, DateTime? Fecha_Fin)
        {
            try
            {

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var parametros = new
                    {
                        Fec_Inicio = Fecha_Ini,
                        Fec_Fin = Fecha_Fin,
                    };

                    var result = await connection.QueryAsync<PrimeraPartidaBandeja>(
                         "[dbo].[Tx_Visor_Primeras_Partidas]"
                         , parametros
                         , commandType: System.Data.CommandType.StoredProcedure
                     );

                    return result;
                }

            }
            catch (SqlException sqlEx)
            {
                // Errores específicos de SQL
                Console.WriteLine($"Error SQL: {sqlEx.Message}");
                throw; // puedes relanzar o manejar como quieras
            }
            catch (Exception ex)
            {
                // Errores generales
                Console.WriteLine($"Error general: {ex.Message}");
                throw;
            }

        }

        public async Task<(int Codigo, string Mensaje)> ProcesoMnto(AuditoriaPrimeraPartida auditoriaPrimeraPartida)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                // Parametros de SQL
                parametros.Add("@CodCliente", auditoriaPrimeraPartida.Cod_Cliente_Tex);
                parametros.Add("@SerOrdComp", auditoriaPrimeraPartida.Ser_OrdComp);
                parametros.Add("@CodOrdComp", auditoriaPrimeraPartida.Cod_OrdComp);
                parametros.Add("@SecOrdComp", auditoriaPrimeraPartida.Sec_OrdComp);

                parametros.Add("@Cod_Ordtra", auditoriaPrimeraPartida.Cod_Ordtra);
                parametros.Add("@Cod_Tela", auditoriaPrimeraPartida.Cod_Tela);
                parametros.Add("@Num_Secuencia", auditoriaPrimeraPartida.Num_Secuencia);
                parametros.Add("@Kgs_Tenidos", auditoriaPrimeraPartida.Kgs_Tenidos);
                parametros.Add("@Comentario", auditoriaPrimeraPartida.Comentario);
                parametros.Add("@Cod_Ordtra_Nueva", auditoriaPrimeraPartida.Cod_Ordtra_Nueva);
                parametros.Add("@Flg_Status", auditoriaPrimeraPartida.Flg_Status);
                parametros.Add("@Usu_Registro", auditoriaPrimeraPartida.Usu_Registro);

                // Parámetros de salida
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                // Ejecutar el procedimiento almacenado
                try
                {
                    connection.Execute(
                        "[dbo].[USP_Insertar_Auditoria_1raPartida]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );
                }
                catch (Exception ex) { }

                //Obtener los valores de salida
                var codigo = parametros.Get<int>("@Codigo");
                var mensaje = parametros.Get<string>("@sMsj");

                return (codigo, mensaje);
            }
        }
    }
}

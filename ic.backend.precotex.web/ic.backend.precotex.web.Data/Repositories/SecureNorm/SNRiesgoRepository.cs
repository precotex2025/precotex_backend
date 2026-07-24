using Dapper;
using ic.backend.precotex.web.Data.Repositories.Implementation.SecureNorm;
using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.SecureNorm
{
    public class SNRiesgoRepository : ISNRiesgoRepository
    {
        private readonly string _connectionString;

        public SNRiesgoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnectionSomma")!;
        }

        public async Task<IEnumerable<SN_Riesgo>?> Listado(string sFiltro)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    p_Filtro = sFiltro ?? ""
                };

                var result = await connection.QueryAsync<SN_Riesgo>(
                    "[dbo].[SP_SN_RIESGO_LISTAR]",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

                return result;
            }
        }

        public async Task<(int Codigo, string Mensaje)> Mnto(SN_Riesgo riesgo, string sTipoTransac)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();

                parametros.Add("@p_Accion", sTipoTransac);
                parametros.Add("@p_Codigo", riesgo.Codigo);
                parametros.Add("@p_Tipo", riesgo.Tipo);
                parametros.Add("@p_Descripcion_Breve", riesgo.Descripcion_Breve);
                parametros.Add("@p_Proceso", riesgo.Proceso);
                parametros.Add("@p_Nivel", riesgo.Nivel);
                parametros.Add("@p_Estado", riesgo.Estado);
                parametros.Add("@p_Responsable", riesgo.Responsable);
                parametros.Add("@p_Fecha_Revision", riesgo.Fecha_Revision);
                parametros.Add("@p_Medida_Control", riesgo.Medida_Control);
                parametros.Add("@p_Usuario", riesgo.Usuario_Registro);

                try
                {
                    var result = await connection.QueryFirstOrDefaultAsync<dynamic>(
                        "[dbo].[SP_SN_RIESGO_MANTENIMIENTO]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                    if (result != null)
                    {
                        return (Convert.ToInt32(result.success), result.message);
                    }
                    return (0, "Error desconocido al ejecutar mantenimiento de riesgo");
                }
                catch (Exception ex)
                {
                    return (0, ex.Message);
                }
            }
        }
    }
}

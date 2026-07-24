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
    public class SNReqLegalRepository : ISNReqLegalRepository
    {
        private readonly string _connectionString;

        public SNReqLegalRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnectionSomma")!;
        }

        public async Task<IEnumerable<SN_Req_Legal>?> Listado(string sFiltro)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    p_Filtro = sFiltro ?? ""
                };

                var result = await connection.QueryAsync<SN_Req_Legal>(
                     "[dbo].[SP_SN_REQ_LEGAL_LISTAR]",
                     parametros,
                     commandType: CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<(int Codigo, string Mensaje)> Mnto(SN_Req_Legal sN_Req_Legal, string sTipoTransac)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();

                parametros.Add("@p_Accion", sTipoTransac);
                parametros.Add("@p_Codigo", sN_Req_Legal.Codigo);
                parametros.Add("@p_Requisito", sN_Req_Legal.Requisito);
                parametros.Add("@p_Ambito", sN_Req_Legal.Ambito);
                parametros.Add("@p_Tipo", sN_Req_Legal.Tipo);
                parametros.Add("@p_Norma", sN_Req_Legal.Norma);
                parametros.Add("@p_Entidad", sN_Req_Legal.Entidad);
                parametros.Add("@p_Obligacion", sN_Req_Legal.Obligacion);
                parametros.Add("@p_Estado", sN_Req_Legal.Estado);
                parametros.Add("@p_Responsable", sN_Req_Legal.Responsable);
                parametros.Add("@p_Evaluacion", sN_Req_Legal.Evaluacion);
                parametros.Add("@p_Proxeval", sN_Req_Legal.Proxeval);
                parametros.Add("@p_Vencimiento", sN_Req_Legal.Vencimiento);
                parametros.Add("@p_Evidencia", sN_Req_Legal.Evidencia);
                parametros.Add("@p_Usuario", sN_Req_Legal.Usuario_Registro);

                try
                {
                    var result = await connection.QueryFirstOrDefaultAsync<dynamic>(
                        "[dbo].[SP_SN_REQ_LEGAL_MANTENIMIENTO]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                    if (result != null)
                    {
                        return (Convert.ToInt32(result.success), result.message);
                    }
                    return (0, "Error desconocido al ejecutar mantenimiento de requisito legal");
                }
                catch (Exception ex)
                {
                    return (0, ex.Message);
                }
            }
        }
    }
}

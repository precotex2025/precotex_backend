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
    public class SNAuditoriaRepository : ISNAuditoriaRepository
    {
        private readonly string _connectionString;

        public SNAuditoriaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnectionSomma")!;
        }

        public async Task<IEnumerable<SN_Auditoria>?> Listado(string sFiltro)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    p_Filtro = sFiltro ?? ""
                };

                var result = await connection.QueryAsync<SN_Auditoria>(
                    "[dbo].[SP_SN_AUDITORIA_LISTAR]",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

                return result;
            }
        }

        public async Task<(int Codigo, string Mensaje)> ProcesoMnto(SN_Auditoria sN_Auditoria, string sTipoTransac)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();
                parametros.Add("@p_Accion", sTipoTransac);
                parametros.Add("@p_Codigo_Auditoria", sN_Auditoria.Codigo_Auditoria);
                parametros.Add("@p_Tipo", sN_Auditoria.Tipo);
                parametros.Add("@p_Norma", sN_Auditoria.Norma);
                parametros.Add("@p_Responsable", sN_Auditoria.Responsable);
                parametros.Add("@p_Areas", sN_Auditoria.Areas);
                parametros.Add("@p_Fecha_Inicio", sN_Auditoria.Fecha_Inicio);
                parametros.Add("@p_Fecha_Fin", sN_Auditoria.Fecha_Fin);
                parametros.Add("@p_Frecuencia", sN_Auditoria.Frecuencia);
                parametros.Add("@p_Alcance", sN_Auditoria.Alcance);
                parametros.Add("@p_Estado", sN_Auditoria.Estado);
                parametros.Add("@p_Usuario", sN_Auditoria.Usuario_Registro);

                var res = await connection.QueryFirstOrDefaultAsync<dynamic>(
                    "[dbo].[SP_SN_AUDITORIA_MANTENIMIENTO]",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

                int codigo = 1;
                string mensaje = "Operación completada con éxito.";

                if (res != null)
                {
                    IDictionary<string, object> dictionary = (IDictionary<string, object>)res;
                    if (dictionary.ContainsKey("success"))
                    {
                        codigo = Convert.ToInt32(dictionary["success"]);
                    }
                    if (dictionary.ContainsKey("message"))
                    {
                        mensaje = Convert.ToString(dictionary["message"])!;
                    }
                }

                return (codigo, mensaje);
            }
        }
    }
}

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
    public class SNMejoraRepository : ISNMejoraRepository
    {
        private readonly string _connectionString;

        public SNMejoraRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnectionSomma")!;
        }

        public async Task<IEnumerable<SN_Mejora>?> Listado(string sFiltro)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    p_Filtro = sFiltro ?? ""
                };

                var result = await connection.QueryAsync<SN_Mejora>(
                     "[dbo].[SP_SN_MEJORA_LISTAR]",
                     parametros,
                     commandType: CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<(int Codigo, string Mensaje)> Mnto(SN_Mejora sN_Mejora, string sTipoTransac)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();

                parametros.Add("@p_Accion", sTipoTransac);
                parametros.Add("@p_Codigo", sN_Mejora.Codigo);
                parametros.Add("@p_Fuente", sN_Mejora.Fuente);
                parametros.Add("@p_Codigo_Proceso", sN_Mejora.Codigo_Proceso);
                parametros.Add("@p_Descripcion", sN_Mejora.Descripcion);
                parametros.Add("@p_Responsable", sN_Mejora.Responsable);
                parametros.Add("@p_Fecha_Inicio", sN_Mejora.Fecha_Inicio);
                parametros.Add("@p_Fecha_Fin_Estimada", sN_Mejora.Fecha_Fin_Estimada);
                parametros.Add("@p_Fecha_Fin", sN_Mejora.Fecha_Fin);
                parametros.Add("@p_Estado", sN_Mejora.Estado);
                parametros.Add("@p_Sede", sN_Mejora.Sede);
                parametros.Add("@p_Herramienta", sN_Mejora.Herramienta);
                parametros.Add("@p_Proveniente", sN_Mejora.Proveniente);
                parametros.Add("@p_Cumplimiento", sN_Mejora.Cumplimiento);
                parametros.Add("@p_Archivo", sN_Mejora.Archivo);
                parametros.Add("@p_Usuario", sN_Mejora.Usuario_Registro);

                try
                {
                    var result = await connection.QueryFirstOrDefaultAsync<dynamic>(
                        "[dbo].[SP_SN_MEJORA_MANTENIMIENTO]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                    if (result != null)
                    {
                        return (Convert.ToInt32(result.success), result.message);
                    }
                    return (0, "Error desconocido al ejecutar mantenimiento de mejora");
                }
                catch (Exception ex)
                {
                    return (0, ex.Message);
                }
            }
        }
    }
}

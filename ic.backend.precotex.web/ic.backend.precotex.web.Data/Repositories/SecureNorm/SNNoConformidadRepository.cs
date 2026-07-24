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
    public class SNNoConformidadRepository : ISNNoConformidadRepository
    {
        private readonly string _connectionString;

        public SNNoConformidadRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnectionSomma")!;
        }

        public async Task<IEnumerable<SN_No_Conformidad>?> Listado(string sFiltro)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    p_Filtro = sFiltro ?? ""
                };

                var result = await connection.QueryAsync<SN_No_Conformidad>(
                    "[dbo].[SP_SN_NO_CONFORMIDAD_LISTAR]",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

                return result;
            }
        }

        public async Task<(int Codigo, string Mensaje)> ProcesoMnto(SN_No_Conformidad sN_No_Conformidad, string sTipoTransac)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();
                parametros.Add("@p_Accion", sTipoTransac);
                parametros.Add("@p_NC", sN_No_Conformidad.NC);
                parametros.Add("@p_Tipo", sN_No_Conformidad.Tipo);
                parametros.Add("@p_Accion_Desc", sN_No_Conformidad.Accion);
                parametros.Add("@p_Proceso", sN_No_Conformidad.Proceso);
                parametros.Add("@p_Responsable", sN_No_Conformidad.Responsable);
                parametros.Add("@p_Fecha_Inicio", sN_No_Conformidad.Fecha_Inicio);
                parametros.Add("@p_Fecha_Limite", sN_No_Conformidad.Fecha_Limite);
                parametros.Add("@p_Estado", sN_No_Conformidad.Estado);
                parametros.Add("@p_Descripcion", sN_No_Conformidad.Descripcion);
                parametros.Add("@p_Codigo_Auditoria", sN_No_Conformidad.Codigo_Auditoria);
                parametros.Add("@p_Usuario", sN_No_Conformidad.Usuario_Registro);

                var result = await connection.QueryFirstOrDefaultAsync<dynamic>(
                    "[dbo].[SP_SN_NO_CONFORMIDAD_MANTENIMIENTO]",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

                if (result != null)
                {
                    int success = Convert.ToInt32(result.success);
                    string message = Convert.ToString(result.message);
                    return (success, message);
                }

                return (0, "Sin respuesta de la BD");
            }
        }
    }
}

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
    public class SNManualRepository : ISNManualRepository
    {
        private readonly string _connectionString;

        public SNManualRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnectionSomma")!;
        }

        public async Task<IEnumerable<SN_Manual>?> Listado(string sFiltro)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    p_Filtro = sFiltro ?? ""
                };

                var result = await connection.QueryAsync<SN_Manual>(
                     "[dbo].[SP_SN_MANUAL_LISTAR]",
                     parametros,
                     commandType: CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<(int Codigo, string Mensaje)> Mnto(SN_Manual sN_Manual, string sTipoTransac)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();

                parametros.Add("@p_Accion", sTipoTransac);
                parametros.Add("@p_Id_Manual", sN_Manual.Id_Manual);
                parametros.Add("@p_Codigo", sN_Manual.Codigo);
                parametros.Add("@p_Titulo", sN_Manual.Titulo);
                parametros.Add("@p_Subtitulo", sN_Manual.Subtitulo);
                parametros.Add("@p_Descripcion", sN_Manual.Descripcion);
                parametros.Add("@p_Autor", sN_Manual.Autor);
                parametros.Add("@p_Fecha_Publicacion", sN_Manual.Fecha_Publicacion);
                parametros.Add("@p_Version", sN_Manual.Version);
                parametros.Add("@p_Color", sN_Manual.Color);
                parametros.Add("@p_Icono", sN_Manual.Icono);
                parametros.Add("@p_Archivo", sN_Manual.Archivo);
                parametros.Add("@p_Usuario", sN_Manual.Usuario_Registro);

                try
                {
                    var result = await connection.QueryFirstOrDefaultAsync<dynamic>(
                        "[dbo].[SP_SN_MANUAL_MANTENIMIENTO]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                    if (result != null)
                    {
                        return (Convert.ToInt32(result.success), result.message);
                    }
                    return (0, "Error desconocido al ejecutar mantenimiento de manual");
                }
                catch (Exception ex)
                {
                    return (0, ex.Message);
                }
            }
        }
    }
}

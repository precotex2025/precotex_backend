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
    public class SNIndicadorRepository : ISNIndicadorRepository
    {
        private readonly string _connectionString;

        public SNIndicadorRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnectionSomma")!;
        }

        public async Task<IEnumerable<SN_Indicador>?> Listado(string sFiltro)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    p_Filtro = sFiltro ?? ""
                };

                var result = await connection.QueryAsync<SN_Indicador>(
                     "[dbo].[SP_SN_INDICADOR_LISTAR]"
                     , parametros
                     , commandType: CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<(int Codigo, string Mensaje)> Mnto(SN_Indicador sN_Indicador, string sTipoTransac)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();

                parametros.Add("@p_Accion", sTipoTransac);
                parametros.Add("@p_Codigo", sN_Indicador.Codigo);
                parametros.Add("@p_Nombre", sN_Indicador.Nombre);
                parametros.Add("@p_Codigo_Proceso", sN_Indicador.Codigo_Proceso);
                parametros.Add("@p_Unidad_Medida", sN_Indicador.Unidad_Medida);
                parametros.Add("@p_Meta", sN_Indicador.Meta);
                parametros.Add("@p_Frecuencia", sN_Indicador.Frecuencia);
                parametros.Add("@p_Usuario", sN_Indicador.Usuario_Registro);

                try
                {
                    var result = await connection.QueryFirstOrDefaultAsync<dynamic>(
                        "[dbo].[SP_SN_INDICADOR_MANTENIMIENTO]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                    if (result != null)
                    {
                        return (Convert.ToInt32(result.success), result.message);
                    }
                    return (0, "Error desconocido al ejecutar mantenimiento de indicador");
                }
                catch (Exception ex)
                {
                    return (0, ex.Message);
                }
            }
        }

        public async Task<IEnumerable<SN_Indicador_Medicion>?> ListadoMediciones(int? idIndicador, string sFiltro)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    p_Id_Indicador = idIndicador,
                    p_Filtro = sFiltro ?? ""
                };

                var result = await connection.QueryAsync<SN_Indicador_Medicion>(
                    "[dbo].[SP_SN_INDICADOR_MEDICION_LISTAR]",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

                return result;
            }
        }

        public async Task<(int Codigo, string Mensaje)> MntoMedicion(SN_Indicador_Medicion medicion, string sTipoTransac)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();

                parametros.Add("@p_Accion", sTipoTransac);
                parametros.Add("@p_Id_Medicion", medicion.Id_Medicion > 0 ? medicion.Id_Medicion : (int?)null);
                parametros.Add("@p_Codigo_Indicador", medicion.Codigo_Indicador);
                parametros.Add("@p_Id_Indicador", medicion.Id_Indicador > 0 ? medicion.Id_Indicador : (int?)null);
                parametros.Add("@p_Periodo", medicion.Periodo);
                parametros.Add("@p_Valor_Obtenido", medicion.Valor_Obtenido);
                parametros.Add("@p_Comentario", medicion.Comentario);
                parametros.Add("@p_Usuario", medicion.Usuario_Registro);

                try
                {
                    var result = await connection.QueryFirstOrDefaultAsync<dynamic>(
                        "[dbo].[SP_SN_INDICADOR_MEDICION_MANTENIMIENTO]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                    if (result != null)
                    {
                        return (Convert.ToInt32(result.success), result.message);
                    }
                    return (0, "Error al ejecutar mantenimiento de medición de indicador");
                }
                catch (Exception ex)
                {
                    return (0, ex.Message);
                }
            }
        }
    }
}

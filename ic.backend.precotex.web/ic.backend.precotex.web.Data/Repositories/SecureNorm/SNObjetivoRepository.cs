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
    public class SNObjetivoRepository : ISNObjetivoRepository
    {
        private readonly string _connectionString;

        public SNObjetivoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnectionSomma")!;
        }

        public async Task<IEnumerable<SN_Objetivo>?> Listado(string sFiltro)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    p_Filtro = sFiltro ?? ""
                };

                var result = await connection.QueryAsync<SN_Objetivo>(
                    "[dbo].[SP_SN_OBJETIVO_LISTAR]",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

                return result;
            }
        }

        public async Task<(int Codigo, string Mensaje)> Mnto(SN_Objetivo objetivo, string sTipoTransac)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();

                parametros.Add("@p_Accion", sTipoTransac);
                parametros.Add("@p_Codigo", objetivo.Codigo);
                parametros.Add("@p_Nombre", objetivo.Nombre);
                parametros.Add("@p_Proceso", objetivo.Proceso);
                parametros.Add("@p_Meta", objetivo.Meta);
                parametros.Add("@p_Usuario", objetivo.Usuario_Registro);

                try
                {
                    var result = await connection.QueryFirstOrDefaultAsync<dynamic>(
                        "[dbo].[SP_SN_OBJETIVO_MANTENIMIENTO]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                    if (result != null)
                    {
                        return (Convert.ToInt32(result.success), result.message);
                    }
                    return (0, "Error desconocido al ejecutar mantenimiento de objetivo");
                }
                catch (Exception ex)
                {
                    return (0, ex.Message);
                }
            }
        }

        public async Task<IEnumerable<SN_Objetivo_Medicion>?> ListadoMediciones(int? idObjetivo, string sFiltro)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    p_Id_Objetivo = idObjetivo,
                    p_Filtro = sFiltro ?? ""
                };

                var result = await connection.QueryAsync<SN_Objetivo_Medicion>(
                    "[dbo].[SP_SN_OBJETIVO_MEDICION_LISTAR]",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

                return result;
            }
        }

        public async Task<(int Codigo, string Mensaje)> MntoMedicion(SN_Objetivo_Medicion medicion, string sTipoTransac)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();

                parametros.Add("@p_Accion", sTipoTransac);
                parametros.Add("@p_Id_Obj_Medicion", medicion.Id_Obj_Medicion > 0 ? medicion.Id_Obj_Medicion : (int?)null);
                parametros.Add("@p_Codigo_Objetivo", medicion.Codigo_Objetivo);
                parametros.Add("@p_Id_Objetivo", medicion.Id_Objetivo > 0 ? medicion.Id_Objetivo : (int?)null);
                parametros.Add("@p_Periodo", medicion.Periodo);
                parametros.Add("@p_Valor", medicion.Valor);
                parametros.Add("@p_Usuario", medicion.Usuario_Registro);

                try
                {
                    var result = await connection.QueryFirstOrDefaultAsync<dynamic>(
                        "[dbo].[SP_SN_OBJETIVO_MEDICION_MANTENIMIENTO]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                    if (result != null)
                    {
                        return (Convert.ToInt32(result.success), result.message);
                    }
                    return (0, "Error al ejecutar mantenimiento de medición de objetivo");
                }
                catch (Exception ex)
                {
                    return (0, ex.Message);
                }
            }
        }
    }
}

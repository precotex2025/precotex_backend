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
                parametros.Add("@p_Tipo", sN_Indicador.Tipo);
                parametros.Add("@p_Sede", sN_Indicador.Sede);
                parametros.Add("@p_Norma", sN_Indicador.Norma);
                parametros.Add("@p_Frecuencia", sN_Indicador.Frecuencia);
                parametros.Add("@p_Meta", sN_Indicador.Meta);
                parametros.Add("@p_Unidad_Medida", sN_Indicador.Unidad_Medida);
                parametros.Add("@p_Tipo_Meta", sN_Indicador.Tipo_Meta);
                parametros.Add("@p_Sentido", sN_Indicador.Sentido);
                parametros.Add("@p_Linea_Base", sN_Indicador.Linea_Base);
                parametros.Add("@p_Formula", sN_Indicador.Formula);
                parametros.Add("@p_Codigo_Proceso", sN_Indicador.Codigo_Proceso);
                parametros.Add("@p_Nombre_Proceso", sN_Indicador.Nombre_Proceso);
                parametros.Add("@p_Responsable", sN_Indicador.Responsable);
                parametros.Add("@p_Resp_Medicion", sN_Indicador.Resp_Medicion);
                parametros.Add("@p_Fuente_Datos", sN_Indicador.Fuente_Datos);
                parametros.Add("@p_Fecha_Inicio", sN_Indicador.Fecha_Inicio ?? sN_Indicador.Fec_Inicio);
                parametros.Add("@p_Fecha_Fin", sN_Indicador.Fecha_Fin ?? sN_Indicador.Fec_Fin);
                parametros.Add("@p_Areas_Acceso", sN_Indicador.Areas_Acceso);
                parametros.Add("@p_Estado", sN_Indicador.Estado);
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
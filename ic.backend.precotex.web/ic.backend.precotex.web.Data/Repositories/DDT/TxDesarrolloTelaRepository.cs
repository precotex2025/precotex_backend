using Dapper;
using ic.backend.precotex.web.Data.Repositories.Implementation.DDT;
using ic.backend.precotex.web.Entity.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.DDT
{
    public class TxDesarrolloTelaRepository : ITxDesarrolloTelaRepository
    {


        private readonly string _connectionString;

        public TxDesarrolloTelaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnection")!;
        }

        public async Task<IEnumerable<Tx_Desarrollo_Telas>?> ListadoDesarrolloTelas(string sAccion, string sCodTela, string sCodVersion, string sNomVersion, string sComentario, string sRutaArchivo, string sCodMotivoSolicitud, string sComentarioSolicitud, string sCodUsuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Accion                 = sAccion,
                    Cod_Tela               = sCodTela,
                    Cod_Version            = sCodVersion,
                    Nom_Version            = sNomVersion,
                    Comentario             = sComentario,
                    Ruta_Archivo           = sRutaArchivo,
                    Cod_Motivo_Solicitud   = sCodMotivoSolicitud,
                    Comentario_Solicitud   = sComentarioSolicitud,
                    Cod_Usuario            = sCodUsuario
                };                         

                var result = await connection.QueryAsync<Tx_Desarrollo_Telas>(
                     "[dbo].[Tx_Man_TelaFichaTecnica]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<(int Codigo, string Mensaje)> ProcesoDesarrolloTela(
            string sAccion,
            string sCodTela,
            string sCodVersion,
            string sNomVersion,
            string sComentario,
            string sRutaArchivo,
            string sCodMotivoSolicitud,
            string sComentarioSolicitud,
            string sCodUsuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                parametros.Add("@Accion", sAccion);
                parametros.Add("@Cod_Tela", sCodTela);
                parametros.Add("@Cod_Version", sCodVersion);
                parametros.Add("@Nom_Version", sNomVersion);
                parametros.Add("@Comentario", sComentario);
                parametros.Add("@Ruta_Archivo", sRutaArchivo);
                parametros.Add("@Cod_Motivo_Solicitud", sCodMotivoSolicitud);
                parametros.Add("@Comentario_Solicitud", sComentarioSolicitud);
                parametros.Add("@Cod_Usuario", sCodUsuario);

                try
                {
                    await connection.ExecuteAsync(
                        "[dbo].[Tx_Man_TelaFichaTecnica]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                    return (1, "Proceso ejecutado correctamente.");
                }
                catch (SqlException ex)
                {
                    // Captura mensajes del RAISERROR del procedure
                    return (-1, ex.Message);
                }
                catch (Exception ex)
                {
                    return (-1, $"Error inesperado: {ex.Message}");
                }
            }
        }

    }
}

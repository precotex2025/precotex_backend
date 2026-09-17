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
                    sFiltro = sFiltro ?? ""
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

                int idReq = sN_Req_Legal.Id > 0 ? sN_Req_Legal.Id : sN_Req_Legal.Id_Req;

                parametros.Add("@cAccion", sTipoTransac);
                parametros.Add("@nid_req_legal", idReq);
                parametros.Add("@citem", sN_Req_Legal.Item);
                parametros.Add("@vrequisito", sN_Req_Legal.Requisito);
                parametros.Add("@vtema", sN_Req_Legal.Tema);
                parametros.Add("@vambito", sN_Req_Legal.Ambito);
                parametros.Add("@vtipo", sN_Req_Legal.Tipo);
                parametros.Add("@vnorma", sN_Req_Legal.Norma);
                parametros.Add("@varticulo", sN_Req_Legal.Articulo);
                parametros.Add("@ventidad", sN_Req_Legal.Entidad);
                parametros.Add("@vextracto_obligacion", sN_Req_Legal.Obligacion);
                parametros.Add("@vevidencia_cumplimiento", sN_Req_Legal.Evidenciadoc);
                parametros.Add("@vestado", sN_Req_Legal.Estado ?? "En proceso");
                parametros.Add("@vresponsable", sN_Req_Legal.Responsable);
                parametros.Add("@vfrecuencia", sN_Req_Legal.Frecuencia);

                DateTime dtTemp;
                parametros.Add("@devaluacion", (!string.IsNullOrWhiteSpace(sN_Req_Legal.Evaluacion) && DateTime.TryParse(sN_Req_Legal.Evaluacion, out dtTemp)) ? (object)dtTemp : DBNull.Value);
                parametros.Add("@dproxeval", (!string.IsNullOrWhiteSpace(sN_Req_Legal.Proxeval) && DateTime.TryParse(sN_Req_Legal.Proxeval, out dtTemp)) ? (object)dtTemp : DBNull.Value);
                parametros.Add("@dvencimiento", (!string.IsNullOrWhiteSpace(sN_Req_Legal.Vencimiento) && DateTime.TryParse(sN_Req_Legal.Vencimiento, out dtTemp)) ? (object)dtTemp : DBNull.Value);

                parametros.Add("@vobservaciones", sN_Req_Legal.Observaciones);
                parametros.Add("@vevidencia_archivo", sN_Req_Legal.Evidencia);
                parametros.Add("@cusu_usuario", sN_Req_Legal.Usuario_Registro ?? "SISTEMAS");

                try
                {
                    var result = await connection.QueryFirstOrDefaultAsync<dynamic>(
                        "[dbo].[SP_SN_REQ_LEGAL_MNTO]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                    if (result != null)
                    {
                        int exito = Convert.ToInt32(result.bExito);
                        string mensaje = result.vMensaje?.ToString() ?? "Operación completada exitosamente.";
                        return (exito, mensaje);
                    }
                    return (0, "Error desconocido al ejecutar mantenimiento de requisito legal.");
                }
                catch (Exception ex)
                {
                    return (0, ex.Message);
                }
            }
        }
    }
}

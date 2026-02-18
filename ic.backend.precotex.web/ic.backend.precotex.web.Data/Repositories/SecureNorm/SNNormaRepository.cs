using Dapper;
using ic.backend.precotex.web.Data.Repositories.Implementation.SecureNorm;
using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ic.backend.precotex.web.Entity.Entities.Memorandum;

namespace ic.backend.precotex.web.Data.Repositories.SecureNorm
{
    public class SNNormaRepository : ISNNormaRepository
    {
        private readonly string _connectionString;

        public SNNormaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnectionSomma")!;
        }

        public async  Task<IEnumerable<SN_Norma>?> Listado(string sEstado)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    SoloActivos = sEstado
                };

                var result = await connection.QueryAsync<SN_Norma>(
                     "[dbo].[SN_Norma_Listado]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<(int Codigo, string Mensaje)> ProcesoMnto(SN_Norma sN_Norma, string sTipoTransac)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                // Parametros de SQL
                parametros.Add("@Accion", sTipoTransac);
                parametros.Add("@CodigoReg", sN_Norma.Codigo_Norma);
                parametros.Add("@Norma", sN_Norma.Norma);
                parametros.Add("@Descripcion", sN_Norma.Descripcion);
                parametros.Add("@Flg_Activo", sN_Norma.Flg_Activo);
                parametros.Add("@Cod_Usuario", sN_Norma.Cod_Usuario);

                // Parámetros de salida
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                // Ejecutar el procedimiento almacenado
                try
                {
                    connection.Execute(
                        "[dbo].[SN_Norma_Mnto_Proceso]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );
                }
                catch (Exception ex) { }

                //Obtener los valores de salida
                var codigo = parametros.Get<int>("@Codigo");
                var mensaje = parametros.Get<string>("@sMsj");

                return (codigo, mensaje);
            }
        }
    }
}

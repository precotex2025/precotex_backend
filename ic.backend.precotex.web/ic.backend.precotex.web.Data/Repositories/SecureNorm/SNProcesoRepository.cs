using Dapper;
using ic.backend.precotex.web.Data.Repositories.Implementation.SecureNorm;
using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.SecureNorm
{
    public class SNProcesoRepository : ISNProcesoRepository
    {
        private readonly string _connectionString;

        public SNProcesoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnectionSomma")!;
        }

        public async Task<IEnumerable<SN_Proceso>?> Listado(string sCodigoOrganizacion, string sEstado)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    SoloActivos = sEstado,
                    Codigo_Organizacion = sCodigoOrganizacion
                };

                var result = await connection.QueryAsync<SN_Proceso>(
                     "[dbo].[SN_Procesos_Listado]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<(int Codigo, string Mensaje)> ProcesoMnto(SN_Proceso sN_Proceso, string sTipoTransac)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                // Parametros de SQL
                parametros.Add("@Accion", sTipoTransac);
                parametros.Add("@Codigo_Proceso", sN_Proceso.Codigo_Proceso);
                parametros.Add("@Codigo_Sede", sN_Proceso.Codigo_Sede);
                parametros.Add("@Proceso", sN_Proceso.Proceso);
                parametros.Add("@Codigo_Tipo_Proceso", sN_Proceso.Codigo_Tipo_Proceso);
                parametros.Add("@Descripcion", sN_Proceso.Descripcion);
                parametros.Add("@Nombre_Adjunto", sN_Proceso.Nombre_Adjunto);
                parametros.Add("@Ruta_Adjunto", sN_Proceso.Ruta_Adjunto);

                parametros.Add("@Flg_Activo", sN_Proceso.Flg_Activo);
                parametros.Add("@Cod_Usuario", sN_Proceso.Cod_Usuario);

                // Parámetros de salida
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                // Ejecutar el procedimiento almacenado
                try
                {
                    connection.Execute(
                        "[dbo].[SN_Sedes_Mnto_Proceso]",
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

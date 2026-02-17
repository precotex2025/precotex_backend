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
    public class SNOrganizacionRepository : ISNOrganizacionRepository
    {
        private readonly string _connectionString;

        public SNOrganizacionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnectionSomma")!;
        }

        public async Task<IEnumerable<SN_Organizacion>?> Listado(string sEstado)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    SoloActivos = sEstado
                };

                var result = await connection.QueryAsync<SN_Organizacion>(
                     "[dbo].[SN_Organizacion_Listado]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<(int Codigo, string Mensaje)> ProcesoMnto(SN_Organizacion sN_Organizacion, string sTipoTransac)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                // Parametros de SQL
                parametros.Add("@Accion", sTipoTransac);
                parametros.Add("@Codigo_Organizacion", sN_Organizacion.Codigo_Organizacion);
                parametros.Add("@Denominacion", sN_Organizacion.Denominacion);
                parametros.Add("@Direccion", sN_Organizacion.Direccion);
                parametros.Add("@Localidad", sN_Organizacion.Localidad);
                parametros.Add("@Provincia", sN_Organizacion.Provincia);
                parametros.Add("@Pais", sN_Organizacion.Pais);
                //parametros.Add("@Denominacion_Sede_Principal", sN_Organizacion.Denominacion_Sede_Principal);
                //parametros.Add("@Acronimo", sN_Organizacion.Acronimo);
                //parametros.Add("@Sede_Direccion", sN_Organizacion.Sede_Direccion);
                //parametros.Add("@Sede_Localidad", sN_Organizacion.Sede_Localidad);
                //parametros.Add("@Sede_Provincia", sN_Organizacion.Sede_Provincia);
                //parametros.Add("@Sede_Pais", sN_Organizacion.Sede_Pais);
                parametros.Add("@Flg_Activo", sN_Organizacion.Flg_Activo);
                parametros.Add("@Cod_Usuario", sN_Organizacion.Cod_Usuario);

                // Parámetros de salida
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                // Ejecutar el procedimiento almacenado
                try
                {
                    connection.Execute(
                        "[dbo].[SN_Organizacion_Mnto_Proceso]",
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

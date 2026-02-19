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
    public class SNSedeRepository : ISNSedeRepository
    {
        private readonly string _connectionString;

        public SNSedeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnectionSomma")!;
        }

        public async Task<IEnumerable<SN_Sede>?> Listado(string sEstado)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    SoloActivos = sEstado
                };

                var result = await connection.QueryAsync<SN_Sede>(
                     "[dbo].[SN_Sede_Listado]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<(int Codigo, string Mensaje)> ProcesoMnto(SN_Sede sN_Sede, string sTipoTransac)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                // Parametros de SQL
                parametros.Add("@Accion", sTipoTransac);
                parametros.Add("@Codigo_Sede", sN_Sede.Codigo_Sede);
                parametros.Add("@Codigo_Organizacion", sN_Sede.Codigo_Organizacion);
                parametros.Add("@Denominacion", sN_Sede.Denominacion);
                parametros.Add("@Acronimo", sN_Sede.Acronimo);
                parametros.Add("@Direccion", sN_Sede.Direccion);
                parametros.Add("@Localidad", sN_Sede.Localidad);
                parametros.Add("@Provincia", sN_Sede.Provincia);
                parametros.Add("@Pais", sN_Sede.Pais);

                parametros.Add("@Flg_Activo", sN_Sede.Flg_Activo);
                parametros.Add("@Cod_Usuario", sN_Sede.Cod_Usuario);

                // Parámetros de salida
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                // Ejecutar el procedimiento almacenado
                try
                {
                    connection.Execute(
                        "[dbo].[SN_Procesos_Mnto_Proceso]",
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

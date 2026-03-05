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
    public class SNPuestoRepository : ISNPuestoRepository
    {
        private readonly string _connectionString;

        public SNPuestoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnectionSomma")!;
        }



        public async Task<IEnumerable<SN_Puesto>?> Listado(string sCodigo_Organizacion, string sCodigo_Sede, string sCodigo_Nivel_Riesgo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Codigo_Organizacion = sCodigo_Organizacion,
                    Codigo_Sede = sCodigo_Sede,
                    Codigo_Nivel_Riesgo = sCodigo_Nivel_Riesgo
                };

                var result = await connection.QueryAsync<SN_Puesto>(
                     "[dbo].[SN_Puestos_Listar]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<(int Codigo, string Mensaje)> ProcesoMnto(SN_Puesto sN_Puesto, string sTipoTransac)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                // Parametros de SQL
                parametros.Add("@Accion", sTipoTransac);
                parametros.Add("@Codigo_Puesto", sN_Puesto.Codigo_Puesto);
                parametros.Add("@Codigo_Organizacion", sN_Puesto.Codigo_Organizacion);
                parametros.Add("@Codigo_Sede", sN_Puesto.Codigo_Sede);
                parametros.Add("@Denominacion", sN_Puesto.Denominacion);
                parametros.Add("@Codigo_Nivel_Riesgo", sN_Puesto.Codigo_Nivel_Riesgo);
                parametros.Add("@Validacion_Periodica", sN_Puesto.Validacion_Periodica);

                parametros.Add("@Puesto_Descripcion", sN_Puesto.Puesto_Descripcion);
                parametros.Add("@Puesto_Funciones", sN_Puesto.Puesto_Funciones);
                parametros.Add("@Puesto_Requisitos", sN_Puesto.Puesto_Requisitos);
                parametros.Add("@Puesto_Caracteristicas", sN_Puesto.Puesto_Caracteristicas);
                parametros.Add("@Caracteristicas_Visible", sN_Puesto.Caracteristicas_Visible);

                parametros.Add("@Flg_Activo", sN_Puesto.Flg_Activo);
                parametros.Add("@Cod_Usuario", sN_Puesto.Cod_Usuario);

                // Parámetros de salida
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                // Ejecutar el procedimiento almacenado
                try
                {
                    connection.Execute(
                        "[dbo].[SN_Puestos_Mnto_Proceso]",
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

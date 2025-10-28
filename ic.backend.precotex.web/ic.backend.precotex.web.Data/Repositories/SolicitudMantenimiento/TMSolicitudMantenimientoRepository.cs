using Dapper;
using ic.backend.precotex.web.Data.Repositories.Implementation.SolicitudMantenimiento;
using ic.backend.precotex.web.Entity.Entities.Memorandum;
using ic.backend.precotex.web.Entity.Entities.SolicitudMantenimiento;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.SolicitudMantenimiento
{
    public class TMSolicitudMantenimientoRepository : ITMSolicitudMantenimientoRepository
    {
        private readonly string _connectionString;

        public TMSolicitudMantenimientoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnection")!;
        }

        public async Task<IEnumerable<TM_Maquina>?> ObtieneInformacionMaquinas(string sCodMaquina)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Cod_Maquina = sCodMaquina
                };

                var result = await connection.QueryAsync<TM_Maquina>(
                     "[dbo].[sp_Obtener_Descripcion_Maquinas_Mnto]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<IEnumerable<TM_Solicitud_Mantenimiento>?> ObtieneInformacionSolicitudMantenimiento(DateTime FecIni, DateTime FecFin, string codUsuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Fec_Inicio = FecIni,
                    Fec_Fin = FecFin,
                    CodUsuario = codUsuario
                };

                var result = await connection.QueryAsync<TM_Solicitud_Mantenimiento>(
                     "[dbo].[SP_Listar_Solicitudes_Mantenimiento]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<(int Codigo, string Mensaje)> ProcesoMntoSolicitudMantenimiento(TM_Solicitud_Mantenimiento tM_Solicitud_Mantenimiento, string sTipoTransac)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                // Parametros de SQL
                parametros.Add("@Cod_Solicitud", tM_Solicitud_Mantenimiento.Cod_Solicitud);
                parametros.Add("@Cod_Area", tM_Solicitud_Mantenimiento.Cod_Area);
                parametros.Add("@Cod_Maquina", tM_Solicitud_Mantenimiento.Cod_Maquina);
                parametros.Add("@Observacion", tM_Solicitud_Mantenimiento.Observacion);
                parametros.Add("@Prioridad", tM_Solicitud_Mantenimiento.Prioridad);
                parametros.Add("@Paro_Maquina", tM_Solicitud_Mantenimiento.Paro_Maquina);
                parametros.Add("@Ruta_Fotografia", tM_Solicitud_Mantenimiento.Ruta_Fotografia);
                parametros.Add("@Hora_Inicio", tM_Solicitud_Mantenimiento.Hora_Inicio);
                //parametros.Add("@Hora_Fin", tM_Solicitud_Mantenimiento.Hora_Fin);
                parametros.Add("@Usu_Registro", tM_Solicitud_Mantenimiento.Usu_Registro);
                parametros.Add("@Accion", sTipoTransac);

                // Parámetros de salida
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                // Ejecutar el procedimiento almacenado
                try
                {
                    connection.Execute(
                        "[dbo].[sp_Registrar_Solicitud_Mantenimiento]",
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

        public async Task<IEnumerable<TM_Solicitud_Mantenimiento>?> ObtieneInformacionSolicitudesVisor()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<TM_Solicitud_Mantenimiento>(
                     "[dbo].[SP_Visor_Solicitudes_Mantenimiento]"
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }
    }
}

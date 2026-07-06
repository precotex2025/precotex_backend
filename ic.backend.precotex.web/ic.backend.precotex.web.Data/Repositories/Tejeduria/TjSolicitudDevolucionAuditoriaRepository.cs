using Dapper;   
using ic.backend.precotex.web.Data.Repositories.Implementation.Tejeduria;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.Tejeduria;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Tejeduria
{
    public class TjSolicitudDevolucionAuditoriaRepository : ITjSolicitudDevolucionAuditoriaRepository
    {
        private readonly string _connectionString;

        public TjSolicitudDevolucionAuditoriaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnection")!;
        }

        public async Task<IEnumerable<Tj_Muestra_Solicitud_Devolucion>?> ListaSolicitudDevolucion(int NumSolicitud, string? Lote, DateTime Fecha_Ini, DateTime Fecha_Fin, string? Estado)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if (Lote == null)
                {
                    Lote = "";
                }

                if (Estado == null)
                {
                    Estado = "";
                }

                await connection.OpenAsync();
                var parametros = new
                {
                    Num_Solicitud = NumSolicitud,
                    Cod_OrdProv = Lote,
                    FecInicio = Fecha_Ini,
                    FecFin = Fecha_Fin,
                    Estado = Estado
                };

                var result = await connection.QueryAsync<Tj_Muestra_Solicitud_Devolucion>(
                     "[dbo].[Tj_Muestra_Solicitud_Devolucion_Auditoria]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<IEnumerable<Tj_Muestra_Solicitud_Devolucion_Bultos>?> ListaSolicitudDevolucionBultos(int NumSolicitud, string? Lote, string? Semana, string? Color, string? Marca, string? Conera)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if (Lote == null)
                {
                    Lote = "";
                }

                if (Semana == null)
                {
                    Semana = "";
                }

                if (Color == null)
                {
                    Color = "";
                }

                if (Marca == null)
                {
                    Marca = "";
                }

                if (Conera == null)
                {
                    Conera = "";
                }

                await connection.OpenAsync();
                var parametros = new
                {
                    Num_Solicitud = NumSolicitud,
                    Lote = Lote,
                    Semana = Semana,
                    Color = Color,
                    Marca = Marca,
                    Conera = Conera
                };

                var result = await connection.QueryAsync<Tj_Muestra_Solicitud_Devolucion_Bultos>(
                     "[dbo].[Tj_Muestra_Solicitud_Devolucion_Auditoria_Bultos]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<(int Codigo, string Mensaje)> Proceso(Tj_Mantenimiento_Solicitud_Devolucion Tj_Man_Solicitud_Devolucion, string sTipoTransac)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                // Parametros de SQL
                parametros.Add("@Accion", sTipoTransac);
                parametros.Add("@Num_Solicitud", Tj_Man_Solicitud_Devolucion.Num_Solicitud);
                parametros.Add("@Lote", Tj_Man_Solicitud_Devolucion.Lote);
                parametros.Add("@Semana", Tj_Man_Solicitud_Devolucion.Semana);
                parametros.Add("@Color", Tj_Man_Solicitud_Devolucion.Color);
                parametros.Add("@Marca", Tj_Man_Solicitud_Devolucion.Marca);
                parametros.Add("@Conera", Tj_Man_Solicitud_Devolucion.Conera);
                parametros.Add("@Estado", Tj_Man_Solicitud_Devolucion.Estado);
                parametros.Add("@Tipo", Tj_Man_Solicitud_Devolucion.Tipo);
                parametros.Add("@Cod_Usuario", Tj_Man_Solicitud_Devolucion.Cod_Usuario);

                // Parámetros de salida
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                // Ejecutar el procedimiento almacenado
                try
                {
                    connection.Execute(
                        "[dbo].[Tj_Man_Solicitud_Devolucion_Auditoria]",
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

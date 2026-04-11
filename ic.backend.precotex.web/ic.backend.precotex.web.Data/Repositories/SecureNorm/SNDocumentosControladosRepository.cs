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
    public class SNDocumentosControladosRepository: ISNDocumentosControladosRepository
    {
        private readonly string _connectionString;

        public SNDocumentosControladosRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnectionSomma")!;
        }

        public async Task<IEnumerable<SN_Documentos_Controlados>?> Listado(string sCodigo_Organizacion, string sCodigo_Sede, string sCodigo_Puesto, string sCodigo_Proceso)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Codigo_Organizacion = sCodigo_Organizacion,
                    Codigo_Sede = sCodigo_Sede,
                    Codigo_Puesto = sCodigo_Puesto,
                    Codigo_Proceso = sCodigo_Proceso
                };

                var result = await connection.QueryAsync<SN_Documentos_Controlados>(
                     "[dbo].[SN_Documentos_Controlados_Listado]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<(int Codigo, string Mensaje)> ProcesoCarpetaCtrolMnto(SN_Carpeta_Control sN_Carpeta_Control, string sTipoTransac)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                // Parametros de SQL
                parametros.Add("@Accion", sTipoTransac);
                parametros.Add("@Codigo_Carpeta_Control", sN_Carpeta_Control.CodigoCarpetaControl);
                parametros.Add("@Codigo_Sede", sN_Carpeta_Control.CodigoSede);
                parametros.Add("@Denominacion", sN_Carpeta_Control.Denominacion);
                parametros.Add("@Tipo_Carpeta", sN_Carpeta_Control.TipoCarpeta);

                parametros.Add("@Flg_Activo", sN_Carpeta_Control.FlgActivo);
                parametros.Add("@Cod_Usuario", sN_Carpeta_Control.CodUsuario);

                // Parámetros de salida
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                // Ejecutar el procedimiento almacenado
                try
                {
                    connection.Execute(
                        "[dbo].[SN_Carpeta_Mnto_Proceso]",
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

        public async Task<(int Codigo, string Mensaje)> ProcesoMnto(SN_Documentos_Controlados sN_Documentos_Controlados, string sTipoTransac)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();


                // Parametros de SQL
                parametros.Add("@Accion", sTipoTransac);
                parametros.Add("@Codigo_Documentos_Controlados", sN_Documentos_Controlados.CodigoDocumentosControlados);
                parametros.Add("@Codigo_Carpeta_Control", sN_Documentos_Controlados.CodigoCarpetaControl);
                parametros.Add("@Codigo_Norma", sN_Documentos_Controlados.CodigoNorma);
                parametros.Add("@Codigo_Tiempo_Conservacion", sN_Documentos_Controlados.CodigoTiempoConservacion);
                parametros.Add("@Codigo_Tipo_Descarga", sN_Documentos_Controlados.CodigoTipoDescarga);
                parametros.Add("@Denominacion", sN_Documentos_Controlados.Denominacion);

                parametros.Add("@Codigo_Documento", sN_Documentos_Controlados.CodigoDocumento);
                parametros.Add("@Version_Documento", sN_Documentos_Controlados.VersionDocumento);
                parametros.Add("@Ruta_Adjunto", sN_Documentos_Controlados.RutaAdjunto);
                parametros.Add("@Descripcion", sN_Documentos_Controlados.Descripcion);
                parametros.Add("@bRegistroAsociado", sN_Documentos_Controlados.bRegistroAsociado, DbType.Int32);
                parametros.Add("@bRequiereRevision", sN_Documentos_Controlados.bRequiereRevision, DbType.Int32);
                parametros.Add("@Flg_Estado", sN_Documentos_Controlados.FlgEstado);

                parametros.Add("@Flg_Activo", sN_Documentos_Controlados.FlgActivo);
                parametros.Add("@Cod_Usuario", sN_Documentos_Controlados.CodUsuario);

                // Parámetros de salida
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                // Ejecutar el procedimiento almacenado
                try
                {
                    connection.Execute(
                        "[dbo].[SN_Documentos_Controlados_Mnto_Proceso]",
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

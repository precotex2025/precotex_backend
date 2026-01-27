using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
using ic.backend.precotex.web.Data.Repositories.Implementation.QuejasReclamos;
using ic.backend.precotex.web.Entity.common;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.CalificacionRollosEnProceso;
using ic.backend.precotex.web.Entity.Entities.QuejasReclamos;
using Microsoft.Extensions.Configuration;
using static ic.backend.precotex.web.Entity.Entities.QuejasReclamos.Clientes;

namespace ic.backend.precotex.web.Data.Repositories.QuejasReclamos
{
    public class DQuejasReclamos: IQuejasReclamos
    {
        private readonly string _connectionString;
        private readonly string _connectionSeguridadString;

        public DQuejasReclamos(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnection")!;
            _connectionSeguridadString = configuration.GetConnectionString("TextilConnectionSeguridad")!;
        }

        public async Task<IEnumerable<Cliente>?> ObtenerClintes()
        {
            try
            
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = @"
                SELECT Cod_Cliente_Tex, Nom_Cliente, Abr_Cliente
                FROM TX_CLIENTE
            ";

                    var clientes = await connection.QueryAsync<Cliente>(query);
                    return clientes;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<EstadoDto>?> ObtenerEstado()
        {
            try

            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = @"
                                    SELECT IdEstado, Acronimo, Estado, IdArea FROM EstadoQuejas WHERE FlagEstado = 1
                                ";

                    var estado = await connection.QueryAsync<EstadoDto>(query);
                    return estado;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<MotivoDto>?> ObtenerMotivo()
        {
            try

            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = @"
                                   select Cod_Motivo, UPPER(Descripcion) AS Descripcion from CC_CONFEC_MOTIVOS where Flg_Estado = 'A' and Descripcion <> '*' ORDER BY 2 ASC
                                ";

                    var motivo = await connection.QueryAsync<MotivoDto>(query);
                    return motivo;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<UnidadNegocioDto>?> ObtenerUnidadNegocio()
        {
            try

            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = @"
                                    SELECT Cod_Unidad_Negocio, Descripcion  FROM TG_UNIDAD_NEGOCIO;
                                ";

                    var unidadNegocio = await connection.QueryAsync<UnidadNegocioDto>(query);
                    return unidadNegocio;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<ResponsableDto>?> ObtenerResponsable()
        {
            try

            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = @"
                                    SELECT IdArea, NombreArea FROM TX_AREA WHERE Estado = 1 ORDER BY NombreArea ASC;
                                ";

                    var responsable = await connection.QueryAsync<ResponsableDto>(query);
                    return responsable;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<ReclamoClienteDto>?> GuardarReclamo(List<ReclamoClienteDto> reclamo, bool isNew)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    if (isNew == true)
                    {
                        var detalles = new DataTable();
                        detalles.Columns.Add("Cliente");
                        detalles.Columns.Add("TipoRegistro");
                        detalles.Columns.Add("UnidadNegocio");
                        detalles.Columns.Add("Responsable");
                        detalles.Columns.Add("MotivoRegistro");
                        detalles.Columns.Add("EstadoSolicitud");
                        detalles.Columns.Add("Observacion");
                        detalles.Columns.Add("RutaArchivo");
                        //NUEVOS CAMPOS
                        detalles.Columns.Add("Cod_Cliente_Tex");
                        detalles.Columns.Add("Cod_Ordtra");
                        detalles.Columns.Add("Cod_Tela");
                        detalles.Columns.Add("Cod_Color");
                        detalles.Columns.Add("Id_Unidad_NegocioKey");
                        detalles.Columns.Add("Cod_Motivo");
                        //Nuevos Campos v2
                        detalles.Columns.Add("IdArea");
                        detalles.Columns.Add("IdResponsable");
                        //Nuevos Campos v3
                        detalles.Columns.Add("Cod_TemCli");
                        detalles.Columns.Add("Cod_EstCli");

                        foreach (var item in reclamo)
                        {
                            detalles.Rows.Add(
                                item.Cliente,
                                item.TipoRegistro,
                                item.UnidadNegocio,
                                item.Responsable,
                                item.MotivoRegistro,
                                item.EstadoSolicitud,
                                item.Observacion,
                                item.archivoAdjunto, // o nombre del archivo si guardaste la ruta
                                //NUEVOS CAMPOS
                                item.Cod_Cliente_Tex,
                                item.Cod_Ordtra,
                                item.Cod_Tela,
                                item.Cod_Color,
                                item.Id_Unidad_NegocioKey,
                                item.Cod_Motivo,
                                //Nuevos Campos v2
                                item.IdArea,
                                item.IdResponsable,
                                //Nuevos Campos v3
                                item.Cod_TemCli,
                                item.Cod_EstCli
                            );
                        }

                        var parameters = new DynamicParameters();
                        parameters.Add("@EstadoSolicitud", reclamo[0].EstadoSolicitud);
                        parameters.Add("@UsuarioRegistro", reclamo[0].UsuarioRegistro);
                        parameters.Add("@Detalles", detalles.AsTableValuedParameter("ReclamoDetalleType"));

                        await connection.ExecuteAsync("USP_I_ReclamoCabeceraDetalle", parameters, commandType: CommandType.StoredProcedure);

                    }

                    else
                    {
                        // Actualizar detalles
                        var Ncaso = reclamo.Select(x => x.NroCaso).FirstOrDefault();

                        //Nuevo Recorrido debe de actualizar tambien 
                        foreach (var item in reclamo)
                        {
                            //INSERTA CUANDO ES NUEVO
                            if (item.Id == "undefined")
                            {
                                var pDetalle = new DynamicParameters();
                                pDetalle.Add("@Opcion", "I");
                                pDetalle.Add("@NroCaso", Ncaso);
                                pDetalle.Add("@Cliente", item.Cliente);
                                pDetalle.Add("@TipoRegistro", item.TipoRegistro);
                                pDetalle.Add("@UnidadNegocio", item.UnidadNegocio);
                                pDetalle.Add("@Responsable", item.Responsable);
                                pDetalle.Add("@MotivoRegistro", item.MotivoRegistro);
                                pDetalle.Add("@EstadoSolicitud", item.EstadoSolicitud);
                                pDetalle.Add("@Observacion", item.Observacion);
                                pDetalle.Add("@NombreArchivo", item.archivoAdjunto);
                                //NUEVOS CAMPOS
                                pDetalle.Add("@Cod_Cliente_Tex", item.Cod_Cliente_Tex);
                                pDetalle.Add("@Cod_Ordtra", item.Cod_Ordtra);
                                pDetalle.Add("@Cod_Tela", item.Cod_Tela);
                                pDetalle.Add("@Cod_Color", item.Cod_Color);
                                pDetalle.Add("@Id_Unidad_NegocioKey", item.Id_Unidad_NegocioKey);
                                pDetalle.Add("@Cod_Motivo", item.Cod_Motivo);
                                pDetalle.Add("@IdReclamoClienteDetalle", 0);
                                pDetalle.Add("@IdArea", item.IdArea);
                                pDetalle.Add("@IdResponsable", item.IdResponsable);
                                pDetalle.Add("@Cod_TemCli", item.Cod_TemCli);
                                pDetalle.Add("@Cod_EstCli", item.Cod_EstCli);

                                await connection.ExecuteAsync("sp_UpdateReclamoDetalle", pDetalle, commandType: CommandType.StoredProcedure);
                            }
                            //MODIFICA SI HAY ALGUN CAMBIO
                            else
                            {
                                var pDetalle = new DynamicParameters();
                                pDetalle.Add("@Opcion", "E");
                                pDetalle.Add("@NroCaso", Ncaso);
                                pDetalle.Add("@Cliente", item.Cliente);
                                pDetalle.Add("@TipoRegistro", item.TipoRegistro);
                                pDetalle.Add("@UnidadNegocio", item.UnidadNegocio);
                                pDetalle.Add("@Responsable", item.Responsable);
                                pDetalle.Add("@MotivoRegistro", item.MotivoRegistro);
                                pDetalle.Add("@EstadoSolicitud", item.EstadoSolicitud);
                                pDetalle.Add("@Observacion", item.Observacion);
                                pDetalle.Add("@NombreArchivo", item.archivoAdjunto);
                                //NUEVOS CAMPOS
                                pDetalle.Add("@Cod_Cliente_Tex", item.Cod_Cliente_Tex);
                                pDetalle.Add("@Cod_Ordtra", item.Cod_Ordtra);
                                pDetalle.Add("@Cod_Tela", item.Cod_Tela);
                                pDetalle.Add("@Cod_Color", item.Cod_Color);
                                pDetalle.Add("@Id_Unidad_NegocioKey", item.Id_Unidad_NegocioKey);
                                pDetalle.Add("@Cod_Motivo", item.Cod_Motivo);
                                pDetalle.Add("@IdReclamoClienteDetalle", item.Id);
                                pDetalle.Add("@IdArea", item.IdArea);
                                pDetalle.Add("@IdResponsable", item.IdResponsable);
                                pDetalle.Add("@Cod_TemCli", item.Cod_TemCli);
                                pDetalle.Add("@Cod_EstCli", item.Cod_EstCli);

                                await connection.ExecuteAsync("sp_UpdateReclamoDetalle", pDetalle, commandType: CommandType.StoredProcedure);
                            }
                        }

                        /* NO VA 
                        foreach (var detalle in reclamo.Where(x => x.Id == "undefined"))
                        {
                            var pDetalle = new DynamicParameters();
                            pDetalle.Add("@NroCaso", Ncaso);
                            pDetalle.Add("@Cliente", detalle.Cliente);
                            pDetalle.Add("@TipoRegistro", detalle.TipoRegistro);
                            pDetalle.Add("@UnidadNegocio", detalle.UnidadNegocio);
                            pDetalle.Add("@Responsable", detalle.Responsable);
                            pDetalle.Add("@MotivoRegistro", detalle.MotivoRegistro);
                            pDetalle.Add("@EstadoSolicitud", detalle.EstadoSolicitud);
                            pDetalle.Add("@Observacion", detalle.Observacion);
                            pDetalle.Add("@NombreArchivo", detalle.archivoAdjunto);

                            await connection.ExecuteAsync("sp_UpdateReclamoDetalle", pDetalle, commandType: CommandType.StoredProcedure);
                        }
                        */
                    }
   
                }
                return null;

            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<FiltroReclamoDto>?> ObtenerReclamos(FiltroReclamoDto filtro)
        {
            try

            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@NroCaso", filtro.NroCaso);
                    parameters.Add("@Cliente", filtro.Cliente);
                    parameters.Add("@TipoRegistro", filtro.TipoRegistro);
                    parameters.Add("@EstadoSolicitud", filtro.EstadoSolicitud);
                    parameters.Add("@Responsable", filtro.Responsable);
                    parameters.Add("@FechaInicio", filtro.FechaInicio);
                    parameters.Add("@FechaFin", filtro.FechaFin);
                    parameters.Add("@Cod_Ordtra", filtro.cod_Ordtra);
                    parameters.Add("@Cod_Unidad_Negocio", filtro.cod_Unidad_Negocio);

                    var result = await connection.QueryAsync<FiltroReclamoDto>(
                        "usp_ObtenerReclamosCliente",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    return result;
                }

            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<ReclamoClienteDto>?> ObtenerDetReclamos(string nroCaso)
        {
            try

            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@NroCaso", nroCaso);

                    var result = await connection.QueryAsync<ReclamoClienteDto>(
                        "usp_ObtenerReclamosDetCliente",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    return result;
                }

            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<string>?> EliminarReclamoDetalle(string id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = @"
                            delete from ReclamoClienteDetalle where id = @Id
                                ";

                    var filasAfectadas = await connection.ExecuteAsync(query, new { Id = id });

                    return new List<string> { "0" };
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error SQL: {sqlEx.Message}");
                return new List<string> { "1" };
            }
        }

        public async Task<IEnumerable<bool>?> EliminarReclamos(string nroCaso)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = @"
                UPDATE ReclamoClienteCabecera
                SET FlagEstado = 1
                WHERE NROCASO = @NroCaso
            ";

                    var filasAfectadas = await connection.ExecuteAsync(query, new { NroCaso = nroCaso });

                    return new List<bool> { filasAfectadas > 0 };
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error SQL: {sqlEx.Message}");
                return new List<bool> { false };
            }
        }

        public async Task<IEnumerable<ArticuloDto>?> BuscarPorPartida(string partida)
        {
            try

            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@COD_ORDTRA", partida);

                    var result = await connection.QueryAsync<ArticuloDto>(
                        "ACA_MUESTRA_TELAS_REVISADORA_WS_QR",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    return result;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw new Exception("Se produjo un error inesperado.", sqlEx);
            }
        }

        public async Task<IEnumerable<UnidadNegocioDto>?> ListaUnidadNegocio()
        {
            try

            {
                using (var connection = new SqlConnection(_connectionString))
                {

                    var result = await connection.QueryAsync<UnidadNegocioDto>(
                        "SP_LISTA_UNIDAD_MEDIDA_OC_QR",
                        commandType: CommandType.StoredProcedure
                    );

                    return result;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw new Exception("Se produjo un error inesperado.", sqlEx);
            }
        }

        public async Task<IEnumerable<AreasDto>?> ListaAreasCalidad()
        {
            try

            {
                using (var connection = new SqlConnection(_connectionString))
                {

                    var result = await connection.QueryAsync<AreasDto>(
                        "SP_LISTAR_AREAS_CALIDAD_PTX",
                        commandType: CommandType.StoredProcedure
                    );

                    return result;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw new Exception("Se produjo un error inesperado.", sqlEx);
            }
        }

        public async Task<(int Codigo, string Mensaje)> AvanzaEstadoReclamo(int iId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                // Parametros de SQL
                parametros.Add("@Id", iId);
                // Parámetros de salida
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                // Ejecutar el procedimiento almacenado
                try
                {
                    connection.Execute(
                        "[dbo].[SP_Avanzar_Estado_Reclamo]",
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

        public async Task<(int Codigo, string Mensaje)> ProcesoConfirmarReclamo(string sNroCaso, string sNombreArchivoCalidad, string sObservacionCalidad, string sCodAreaResponsableCalidad, string sCod_Usuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                // Parametros de SQL
                parametros.Add("@NroCaso", sNroCaso);
                parametros.Add("@NombreArchivo_Calidad", sNombreArchivoCalidad);
                parametros.Add("@Observacion_Calidad", sObservacionCalidad);
                parametros.Add("@Cod_Area_Responsable_Calidad", sCodAreaResponsableCalidad);
                parametros.Add("@Cod_Usuario", sCod_Usuario);
                // Parámetros de salida
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                // Ejecutar el procedimiento almacenado
                try
                {
                    connection.Execute(
                        "[dbo].[SP_Proceso_Confirmar_Reclamo]",
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

        public async Task<IEnumerable<ReclamoTipoConsecuenciaDto>?> ListaTipoConsecuencia()
        {
            try

            {
                using (var connection = new SqlConnection(_connectionString))
                {

                    var result = await connection.QueryAsync<ReclamoTipoConsecuenciaDto>(
                        "SP_LISTAR_CONSECUENCIA_PRINCIPAL_PTX",
                        commandType: CommandType.StoredProcedure
                    );

                    return result;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw new Exception("Se produjo un error inesperado.", sqlEx);
            }
        }

        public async Task<IEnumerable<ReclamoSubTipoDevolucion>?> ListaSubTipoDevolucion(string sCod_Tipo_Consecuencia)
        {
            try

            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Cod_Tipo_Consecuencia", sCod_Tipo_Consecuencia);

                    var result = await connection.QueryAsync<ReclamoSubTipoDevolucion>(
                        "SP_LISTAR_SUBTIPO_DEVOLUCION_PTX",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    return result;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw new Exception("Se produjo un error inesperado.", sqlEx);
            }
        }

        public async Task<(int Codigo, string Mensaje)> ProcesoCerrarReclamo(string sNroCaso, string sCod_Tipo_Consecuencia, string sCod_SubTipo_Devolucion, string sFlg_NotaCredito, string sFlg_FleteAereo, string sObservacion_Comercial_Cierre, string sCod_Usuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                // Parametros de SQL
                parametros.Add("@NroCaso", sNroCaso);
                parametros.Add("@Cod_Tipo_Consecuencia", sCod_Tipo_Consecuencia);
                parametros.Add("@Cod_SubTipo_Devolucion", sCod_SubTipo_Devolucion);
                parametros.Add("@Flg_NotaCredito", sFlg_NotaCredito);
                parametros.Add("@Flg_FleteAereo", sFlg_FleteAereo);
                parametros.Add("@Observacion_Comercial_Cierre", sObservacion_Comercial_Cierre);
                parametros.Add("@Cod_Usuario", sCod_Usuario);
                // Parámetros de salida
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                // Ejecutar el procedimiento almacenado
                try
                {
                    connection.Execute(
                        "[dbo].[SP_Proceso_Cierre_Reclamo]",
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

        public async Task<IEnumerable<ReclamoUsuarioAreaDto>?> ObtieneUsuarioArea(string Cod_Trabajador)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Cod_Trabajador", Cod_Trabajador);

                    var result = await connection.QueryAsync<ReclamoUsuarioAreaDto>(
                        "SP_OBTIENE_AREA_USUARIO_PTX",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    return result;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw new Exception("Se produjo un error inesperado.", sqlEx);
            }
        }

        public async Task<IEnumerable<InformeCalidadDto>?> ObtieneDetalleInformeCalidad(int Id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Id", Id);

                    var result = await connection.QueryAsync<InformeCalidadDto>(
                        "SP_OBTIENE_DETALLE_INFORME_CALIDAD_PTX",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    return result;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw new Exception("Se produjo un error inesperado.", sqlEx);
            }
        }

        public async Task<IEnumerable<InformeComercialDto>?> ObtieneDetalleInformeComercial(int Id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Id", Id);

                    var result = await connection.QueryAsync<InformeComercialDto>(
                        "SP_OBTIENE_DETALLE_INFORME_COMERCIAL_PTX",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    return result;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw new Exception("Se produjo un error inesperado.", sqlEx);
            }
        }

        public async Task<IEnumerable<ReclamoClienteEstadoDto>?> ListaEstados()
        {
            try

            {
                using (var connection = new SqlConnection(_connectionString))
                {

                    var result = await connection.QueryAsync<ReclamoClienteEstadoDto>(
                        "SP_LISTA_ESTADOS_QUEJAS",
                        commandType: CommandType.StoredProcedure
                    );

                    return result;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw new Exception("Se produjo un error inesperado.", sqlEx);
            }
        }

        public async Task<IEnumerable<ReclamoExportarDto>?> ExportarReclamo(FiltroReclamoDto filtro)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@NroCaso", filtro.NroCaso);
                    parameters.Add("@Cliente", filtro.Cliente);
                    parameters.Add("@TipoRegistro", filtro.TipoRegistro);
                    parameters.Add("@EstadoSolicitud", filtro.EstadoSolicitud);
                    parameters.Add("@Responsable", filtro.Responsable);
                    parameters.Add("@FechaInicio", filtro.FechaInicio);
                    parameters.Add("@FechaFin", filtro.FechaFin);
                    parameters.Add("@Cod_Ordtra", filtro.cod_Ordtra);

                    var result = await connection.QueryAsync<ReclamoExportarDto>(
                        "usp_ExportarReclamosCliente",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    return result;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<dtoGeneral>?> ObtieneTemporada(string sCod_Cliente)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Cod_Cliente", sCod_Cliente);

                    var result = await connection.QueryAsync<dtoGeneral>(
                        "sp_Obtener_Temporadas_x_cliente",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    return result;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw new Exception("Se produjo un error inesperado.", sqlEx);
            }
        }

        public async Task<IEnumerable<dtoGeneral>?> ObtieneEstilo(string sCodCliente, string sTemporada)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Cliente", sCodCliente);
                    parameters.Add("@Temporada", sTemporada);

                    var result = await connection.QueryAsync<dtoGeneral>(
                        "sp_Obtener_Estilos_x_Temporadas",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    return result;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw new Exception("Se produjo un error inesperado.", sqlEx);
            }
        }

        public async Task<(int Codigo, string Mensaje)> ProcesoReenviaReclamo(int iId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                // Parametros de SQL
                parametros.Add("@Id", iId);
                // Parámetros de salida
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                // Ejecutar el procedimiento almacenado
                try
                {
                    connection.Execute(
                        "[dbo].[SP_Proceso_Reenvia_Reclamo]",
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

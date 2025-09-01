using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
using ic.backend.precotex.web.Data.Repositories.Implementation.QuejasReclamos;
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
                                    SELECT IdEstado, Acronimo, Estado FROM EstadoQuejas WHERE FlagEstado = 1
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

        public async Task<IEnumerable<UnidadNegocioDto>?> ObtenerMotivo()
        {
            try

            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = @"
                                   select Cod_Motivo as Cod_Unidad_Negocio, Descripcion from CC_CONFEC_MOTIVOS where Flg_Estado = 'A' and Descripcion <> '*'
                                ";

                    var motivo = await connection.QueryAsync<UnidadNegocioDto>(query);
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
                                    SELECT IdArea, NombreArea FROM TX_AREA WHERE Estado = 1;
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
                                item.archivoAdjunto // o nombre del archivo si guardaste la ruta
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
    }
}

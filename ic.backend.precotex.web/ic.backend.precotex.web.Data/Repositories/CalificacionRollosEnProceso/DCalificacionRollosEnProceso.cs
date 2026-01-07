using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Dapper;
using ic.backend.precotex.web.Data.Repositories.Implementation.CalificacionRollosEnProceso;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.CalificacionRollosEnProceso;
using ic.backend.precotex.web.Entity.Entities.Desglose;
using ic.backend.precotex.web.Entity.Entities.QR;
using ic.backend.precotex.web.Entity.Entities.QuejasReclamos;
using Microsoft.Extensions.Configuration; 

namespace ic.backend.precotex.web.Data.Repositories.CalificacionRollosEnProceso
{
    public class DCalificacionRollosEnProceso: ICalificacionRollosEnProceso
    {
        private readonly string _connectionString;

        public DCalificacionRollosEnProceso(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnection")!;
        }

        public async Task<IEnumerable<EDefectos>?> ObtenerDefecto(EDefectos filtro)
        {
            try

            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();

                    //filtro.COD_MOTIVO = "";
                    filtro.PAGINA = 1;
                    parameters = new DynamicParameters();
                    parameters.Add("@COD_AREA_CC", filtro.COD_MOTIVO);
                    parameters.Add("@PAGINA", filtro.PAGINA);

                    var result = await connection.QueryAsync<EDefectos>(
                        "CC_MUESTRA_DEFECTO_AREA_PRE",
                        parameters,
                        commandType: CommandType.StoredProcedure
                        
                    );

                    if (result.Count() == 0)
                    {
                        filtro.COD_MOTIVO = "ACA";
                        filtro.PAGINA = 1;
                        parameters = new DynamicParameters();
                        parameters.Add("@COD_AREA_CC", filtro.COD_MOTIVO);
                        parameters.Add("@PAGINA", filtro.PAGINA);

                        result = await connection.QueryAsync<EDefectos>(
                            "CC_MUESTRA_DEFECTO_AREA_PRE",
                            parameters,
                            commandType: CommandType.StoredProcedure
                        );
                    }

                    return result;

                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<EMaquina>?> ObtenerMaquina()
        {
            try

            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = @"
                
                        	SELECT  Descrip acronimo, CodPartidaEstado idMaquina  FROM EstadoPartidaDefecto WHERE CodDescripcion = 'RA'

                        ";

                    var maquina = await connection.QueryAsync<EMaquina>(query);
                    return maquina;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<EMaquina>?> ObtenerProveedores()
        {
            try

            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = @"
                
                        	select distinct p.Des_Proveedor acronimo, p.Cod_Proveedor from Lg_Proveedor p inner join Lg_OrdComp d
on p.Cod_Proveedor = d.Cod_Proveedor 
where Cod_ClaOrdComp = 'SR'


                        ";

                    var maquina = await connection.QueryAsync<EMaquina>(query);
                    return maquina;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<EAuditor>?> ObtenerSupervisor()
        {
            try

            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = @"
                    
SELECT a.Nom_Auditor+ '  '+a.Tip_Auditor + a.cod_Auditor  As acronimo, cod_Auditor idAuditor FROM ti_cc_auditor a INNER join cc_areas b ON b.Cod_Area_CC = a.Area_Auditor  WHERE A.F_Tintoreria = 'A' AND A.Activo = 'S' ORDER BY a.tip_auditor, a.cod_auditor


";

                    var supervisor = await connection.QueryAsync<EAuditor>(query);
                    return supervisor;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<EAuditor>?> ObtenerAuditor()
        {
            try

            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = @"
                
SELECT a.Nom_Auditor+ '  '+a.Tip_Auditor + a.cod_Auditor  As acronimo, cod_Auditor idAuditor FROM ti_cc_auditor a INNER join cc_areas b ON b.Cod_Area_CC = a.Area_Auditor  WHERE A.F_Tintoreria = 'A' AND A.Activo = 'S' ORDER BY a.tip_auditor, a.cod_auditor


            ";

                    var auditor = await connection.QueryAsync<EAuditor>(query);
                    return auditor;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<ETurno>?> ObtenerTurno()
        {
            try

            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = @"
               SELECT Descripcion as acronimo, Cod_Turno as idTurno  FROM caTurno

            ";

                    var turno = await connection.QueryAsync<ETurno>(query);
                    return turno;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<EUnidadNegocio>?> ObtenerUnidadNegocio()
        {
            try

            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = @"
                SELECT Descrip acronimo, CodPartidaEstado idUnidadNegocio  FROM EstadoPartidaDefecto where CodDescripcion = 'UN'

            ";

                    var unidadNegocio = await connection.QueryAsync<EUnidadNegocio>(query);
                    return unidadNegocio;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<ECalificacion>?> ObtenerCalificacion()
        {
            try

            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = @"
                 SELECT  Descrip acronimo, CodSitAc idCalificacion  FROM casitactual where CodSitAc in ('07','08')

            ";

                    var calificacion = await connection.QueryAsync<ECalificacion>(query);
                    return calificacion;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<ECalificacion>?> ObtenerEstadoProceso()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = @"
               SELECT  Descrip acronimo, Codigo idCalificacion  FROM EstadoProcesoInspeccionPre where Codigo = 1

            ";

                    var estadoProceso = await connection.QueryAsync<ECalificacion>(query);
                    return estadoProceso;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<EEstadoPartida>?> ObtenerEstadoPartida()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = @"
                      SELECT  Descrip acronimo, CodPartidaEstado idEstadoPartida  FROM EstadoPartidaDefecto WHERE CodDescripcion = 'EP1'
                    ";

                    var estadoPartida = await connection.QueryAsync<EEstadoPartida>(query);
                    return estadoPartida;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<EEstadoPartida>?> ObtenerProcesoAuditado()
        {
            try

            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = @"
                    SELECT  Descrip acronimo, CodPartidaEstado idEstadoPartida  FROM EstadoPartidaDefecto WHERE CodDescripcion = 'PA'

            ";

                    var estadoPartida = await connection.QueryAsync<EEstadoPartida>(query);
                    return estadoPartida;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<ERrollosPorPartida>?> BuscarPorPartida(string partida)
        {
            try

            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@COD_ORDTRA", partida);

                    var result = await connection.QueryAsync<ERrollosPorPartida>(
                        "ACA_MUESTRA_TELAS_REVISADORA",
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

        public async Task<IEnumerable<ERrollosPorPartida>?> BuscarArticuloPorPartida(string partida)
        {
            try

            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@COD_ORDTRA", partida);

                    var result = await connection.QueryAsync<ERrollosPorPartida>(
                        "ACA_MUESTRA_TELAS_REVISADORA_XPARTIDA",
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

        public async Task<IEnumerable<ERrollosPorPartida>?> BuscarRolloPorPartidaDetalle(string partida, string articulo)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@COD_ORDTRA", partida);
                    parameters.Add("@NUM_SECUENCIA", articulo);
                    parameters.Add("@OPCION", 1);

                    var result = await connection.QueryAsync<ERrollosPorPartida>(
                        "TI_MUESTRA_DETALLE_POR_ROLLO_POR_PARTIDA_CALIFICACION_REV_PRE",
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

        public async Task<IEnumerable<EPartidaCab>?> GuardarPartida(EPartidaCab partida)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {

                    var parameters = new DynamicParameters();
                    parameters.Add("@usuarioRegistro", partida.usuario ?? (object)DBNull.Value);
                    parameters.Add("@acronimoSup", partida.supervisor ?? (object)DBNull.Value);
                    parameters.Add("@acronimoAud", partida.auditor ?? (object)DBNull.Value);
                    parameters.Add("@acronimoMaqu", partida.maquina ?? (object)DBNull.Value);
                    parameters.Add("@acronimoTur", partida.turno ?? (object)DBNull.Value);
                    parameters.Add("@acronimoUniNe", partida.unidadNegocio ?? (object)DBNull.Value);
                    parameters.Add("@acronimoEstPartida", partida.estadoPartida ?? (object)DBNull.Value);
                    parameters.Add("@acronimoEstProceso", partida.estadoProceso ?? (object)DBNull.Value);
                    parameters.Add("@acronimoCal", partida.calificacion ?? (object)DBNull.Value);
                    parameters.Add("@observaciones", partida.observaciones ?? (object)DBNull.Value);
                    parameters.Add("@cod_OrdTra", partida.datosPartida ?? (object)DBNull.Value);
                    parameters.Add("@tela", partida.datosTela ?? (object)DBNull.Value);
                    parameters.Add("@cliente", partida.datosCliente ?? (object)DBNull.Value);
                    parameters.Add("@acronimoProcAuditado", partida.procesoAuditado ?? (object)DBNull.Value);

                    // Serialize detPartida and detDefecto to JSON
                    string detPartidaJson = JsonSerializer.Serialize(partida.detPartida);
                    string detDefectoJson = JsonSerializer.Serialize(partida.detDefecto);

                    parameters.Add("@detPartidaJson", detPartidaJson ?? (object)DBNull.Value);
                    parameters.Add("@detDefectoJson", detDefectoJson ?? (object)DBNull.Value);

                    var result = await connection.QueryAsync<EPartidaCab>(
                        "TI_INSERTA_PARTIDA_CAB_PRE",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    return result;

                }
            }
            catch (Exception sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw;
            }

        }

        public async Task<IEnumerable<EPartidaPorRollo>?> BuscarPartidaPorRollo(string partida, string usuario)
        {
            try

            {
                string[] partes = partida.Split('|');

                string codPartida = partes.Length > 0 ? partes[0] : string.Empty;
                string rollo = partes.Length > 1 ? partes[1] : string.Empty;

                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@COD_ORDTRA", codPartida);
                    parameters.Add("@Codigo_Rollo", rollo);
                    parameters.Add("@UsuarioProceso", usuario);

                    var result = await connection.QueryAsync<EPartidaPorRollo>(
                        "SP_C_BUSCAR_PARTIDA_POR_ROLLO",
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

        public async Task<IEnumerable<EPartidaPorRollo>?> updatePartidaPorRollo(string partida, int id)
        {
            try

            {

                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@COD_ORDTRA", partida);
                    parameters.Add("@id", id);

                    var result = await connection.QueryAsync<EPartidaPorRollo>(
                        "SP_U_DESACTIVAR_ROLLO_POR_ID",
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

        //REGISTRAR QR

        public async Task<IEnumerable<E_RegistroQR>?> GrabarQR(E_RegistroQR request)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();

                    parameters.Add("@CodMotQr", request.CodMotivo ?? (object)DBNull.Value);
                    parameters.Add("@CodMaquina", request.CodMaquina ?? (object)DBNull.Value);
                    parameters.Add("@CodUsuario", request.CodUsuario ?? (object)DBNull.Value);
                    parameters.Add("@Comentario", request.Observacion ?? (object)DBNull.Value);


                    var result = await connection.QueryAsync<E_RegistroQR>(
                        "QR_MAN_REGISTRO_MAQUINAS_LECTURA_QR",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    return result;
                }
            }
            catch (Exception sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw;
            }

        }

        //REGISTRO DE SERVICIO DE DESGLOSE

        public async Task<IEnumerable<string>?> ObtenerDni(string usuario)
        {
            string nroDocIde = string.Empty;
            try

            {
               
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // El comando para ejecutar la función.
                    // Es importante usar 'SELECT [dbo].[NombreDeTuFuncion](@parametro)' para ejecutarla y obtener el valor.
                    string query = "SELECT [dbo].[Sg_Buscar_Nro_DocIde_Cod_Usuario](@Cod_Usuario)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Añadir el parámetro de la función
                        command.Parameters.AddWithValue("@Cod_Usuario", usuario);

                        try
                        {
                            connection.Open();

                            // ExecuteScalar() se usa cuando esperas un solo valor (escalar) como resultado.
                            object result = command.ExecuteScalar();

                            // Convertir el resultado a string (puede ser DBNull si la función devuelve NULL)
                            if (result != null && result != DBNull.Value)
                            {
                                nroDocIde = result.ToString();
                            }

                            // Si se encontró el DNI, devuelve una lista con ese único elemento
                            if (!string.IsNullOrEmpty(nroDocIde))
                            {
                                return new List<string> { nroDocIde };
                            }
                            else
                            {
                                // Si no se encontró, devuelve una colección vacía (o null si prefieres, pero empty es más estándar)
                                return Enumerable.Empty<string>();
                            }
                        }


                        catch (SqlException ex)
                        {
                            // Manejo de errores específicos de SQL
                            Console.WriteLine($"Error de SQL al buscar Nro_DocIde: {ex.Message}");
                            // Puedes lanzar una excepción personalizada o loguear el error
                            throw new Exception("Error al obtener el número de documento.", ex);
                        }
                        catch (Exception ex)
                        {
                            // Manejo de otros errores
                            Console.WriteLine($"Error general al buscar Nro_DocIde: {ex.Message}");
                            throw new Exception("Error inesperado al obtener el número de documento.", ex);
                        }
                    }
                }

                //return nroDocIde;
            
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<ERrollosPorPartida>?> BuscarPartida(string partida)
        {
            try

            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Cod_Ordtra", partida);

                    var result = await connection.QueryAsync<ERrollosPorPartida>(
                        "sp_ObtenerTiposRectilineoPorOrden",
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

        public async Task<IEnumerable<E_RegistroDesgloseRequest>?> RegistrarDesglose(E_RegistroDesgloseRequest model)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();

                    parameters.Add("@Proveedor", model.Proveedor ?? (object)DBNull.Value);
                    parameters.Add("@Partida", model.Partida ?? (object)DBNull.Value);
                    parameters.Add("@FechaInicio", model.FechaInicio);
                    parameters.Add("@FechaFin", model.FechaFin);
                    parameters.Add("@Auditor", model.Auditor ?? (object)DBNull.Value);
                    parameters.Add("@Total", model.Total);
                    parameters.Add("@Colitas", model.Colitas);
                    parameters.Add("@UsuarioCrea", model.UsuarioCrea ?? (object)DBNull.Value);

                    // Armar TVP para los ítems
                    var table = new DataTable();
                    table.Columns.Add("Tipo", typeof(string));
                    table.Columns.Add("Cantidad", typeof(decimal));

                    foreach (var item in model.Items)
                    {
                         table.Rows.Add(item.Descripcion, item.Metros);
                        if (item.Descripcion == "CUELLOS")
                        {
                            parameters.Add("@Cuello", item.Metros);
                        }
                        if (item.Descripcion == "PUÑOS")
                        {
                            parameters.Add("@Punio", item.Metros);
                        }
                        if (item.Descripcion == "PECHERA")
                        {
                            parameters.Add("@Pechera", item.Metros);
                        }

                        if (item.Descripcion == "TIRA")
                        {
                            parameters.Add("@Tira", item.Metros);
                        }

                    }

                    parameters.Add("@Items", table.AsTableValuedParameter("TVP_DesgloseItems"));

                    var result = await connection.QueryAsync<E_RegistroDesgloseRequest>(
                        "sp_Registrar_Desglose",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    return result;
                }
            }
            catch (Exception sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw;
            }

        }

        public async Task<IEnumerable<ListaDesgloseDetalle>?> ListarDesglose()
        {

            List<ListaDesgloseDetalle> desgloses = new List<ListaDesgloseDetalle>();
            string storedProcName = "[dbo].[sp_GetLatestRegistroDesgloseByArticulo]";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(storedProcName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {

                            while (await reader.ReadAsync())
                            {
                                ListaDesgloseDetalle detalle = new ListaDesgloseDetalle
                                {
                                    Id_Desglose = reader.GetInt32(reader.GetOrdinal("Id_Desglose")),
                                    Auditor = reader.GetString(reader.GetOrdinal("Auditor")),
                                    Cod_Ordtra = reader.GetString(reader.GetOrdinal("Cod_Ordtra")),
                                    Cod_Color = reader.GetString(reader.GetOrdinal("Cod_Color")),
                                    Proveedor = reader.GetString(reader.GetOrdinal("Proveedor")),
                                    FechaInicio = reader.GetDateTime(reader.GetOrdinal("FechaInicio")),
                                    FechaFin = reader.IsDBNull(reader.GetOrdinal("FechaFin")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("FechaFin")),
                                    Total = reader.GetDecimal(reader.GetOrdinal("Total")),
                                    Punio = reader.GetDecimal(reader.GetOrdinal("Punio")),
                                    Cuello = reader.GetDecimal(reader.GetOrdinal("Cuello")),
                                    Pechera = reader.GetDecimal(reader.GetOrdinal("Pechera")),
                                    Tira = reader.GetDecimal(reader.GetOrdinal("Tira")),
                                    Pretina = reader.GetDecimal(reader.GetOrdinal("Pretina")),
                                    Colitas = reader.GetDecimal(reader.GetOrdinal("Colitas"))
                                };
                                desgloses.Add(detalle);
                            }
                        }
                    }
                }
                return desgloses;

            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<E_DesgloseItem>?> ListarDesgloseItem(string id_Desglose)
        {
            try

            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = @"
                    SELECT
                        Tipo Descripcion,
                        Cantidad Metros,
                        Id_Desglose -- Include Id_Desglose if you need it in the returned object
                    FROM
                        Registro_Desglose_Items
                    WHERE
                        Id_Desglose = @IdDesglose;
                ";

                    // Open the connection (Dapper can often do this implicitly, but explicit is safer)
                    await connection.OpenAsync();

                    // Execute the query using Dapper, passing parameters as an anonymous object
                    var desgloseItems = await connection.QueryAsync<E_DesgloseItem>(
                        query,
                        new { IdDesglose = id_Desglose } // Pass the parameter with a matching name
                    );

                    return desgloseItems;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<E_UpdateDesglose>?> ActualizarDesgloseItem(E_UpdateDesglose data)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parametros = new DynamicParameters();

                    parametros.Add("@id_Desglose", data.Id_Desglose);
                    parametros.Add("@fechaInicio", data.FechaInicio);
                    parametros.Add("@fechaFin", data.FechaFin);
                    parametros.Add("@total", data.Total);
                    parametros.Add("@colitas", data.Colitas);

                    // Serializa la lista de ítems en JSON
                    var itemsJson = JsonSerializer.Serialize(data.Items);


                    foreach (var item in data.Items)
                    {
                        if (item.Descripcion == "CUELLOS")
                        {
                            parametros.Add("@Cuello", item.Metros);
                        }
                        if (item.Descripcion == "PUÑOS")
                        {
                            parametros.Add("@Punio", item.Metros);
                        }
                        if (item.Descripcion == "PECHERA")
                        {
                            parametros.Add("@Pechera", item.Metros);
                        }

                        if (item.Descripcion == "PRETINA")
                        {
                            parametros.Add("@Pretina", item.Metros);
                        }
                        if (item.Descripcion == "TIRA")
                        {
                            parametros.Add("@Tira", item.Metros);
                        }

                    }


                    parametros.Add("@itemsJson", itemsJson);

                    var result = await connection.QueryAsync<E_UpdateDesglose>(
                        "TI_ACTUALIZA_DESGLOSE_Y_ITEMS",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                    return result;
                }
            }
            catch (Exception sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<E_RegistroDesgloseRequest>?> EliminarDesglose(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parametros = new DynamicParameters();

                    parametros.Add("@id_Desglose", id);

                    var result = await connection.QueryAsync<E_RegistroDesgloseRequest>(
                        "SP_EliminarDesglose",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );
                    return result;
                }         
            }
            catch (Exception sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw;
            }

        }

        public async Task<IEnumerable<Tx_Maquinas_Gral_QR_P2>?> ObtenerMaquinaQRP2(string CodMaquina)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Cod_Maquina = CodMaquina
                };

                var result = await connection.QueryAsync<Tx_Maquinas_Gral_QR_P2>(
                     "[dbo].[Tx_Obtener_Maquinas_Gral_QR_P2]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<IEnumerable<EPartidaCab>?>ObtenerDatosCabeceraEnProceso(string Cod_OrdTra)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                
                var parametros = new DynamicParameters();
                parametros.Add("@Cod_OrdTra", Cod_OrdTra);

                var result = await connection.QueryAsync<EPartidaCab>(
                    "[dbo].[PA_CC_AUDITORIA_TINTORERIA_CABECERA_PRE_S0001]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                    );

                return result;
            }
        }

        public async Task<IEnumerable<EAuditor>?> ObtenerAuditor(string Cod_Usuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();
                parametros.Add("@Cod_Usuario", Cod_Usuario);

                var result = await connection.QueryAsync<EAuditor>(
                    "[dbo].[PA_ti_cc_auditor_S0001]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                    );

                return result;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Dapper;
using ic.backend.precotex.web.Data.Repositories.Implementation.CalificacionRollosEnProceso;
using ic.backend.precotex.web.Entity.Entities.CalificacionRollosEnProceso;
using Microsoft.Extensions.Configuration;
using ic.backend.precotex.web.Data.Repositories.Implementation.CalificacionRollosFinal;
using ic.backend.precotex.web.Entity.Entities.Memorandum;

namespace ic.backend.precotex.web.Data.Repositories.CalificacionRollosFinal
{
    public class DCalificacionRolloFinal: ICalificacionRolloFinal
    {
        private readonly string _connectionString;

        public DCalificacionRolloFinal(IConfiguration configuration)
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
                throw new Exception("Se produjo un error inesperado.", sqlEx);
            }
        }

        public async Task<IEnumerable<EMaquina>?> ObtenerMaquina()
        {
            try

            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = @"
                
                        	SELECT  caNombre_Maquina acronimo,  caCod_MaquinaRev idMaquina FROM ca_Maq_Revisadora

                        ";

                    var maquina = await connection.QueryAsync<EMaquina>(query);
                    return maquina;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw new Exception("Se produjo un error inesperado.", sqlEx);
            }
        }

        public async Task<IEnumerable<EAuditor>?> ObtenerSupervisor()
        {
            try

            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = @"
SELECT a.Tip_Auditor + +a.cod_Auditor + '  ' + a.Nom_Auditor  As acronimo, cod_Auditor idAuditor FROM 
ti_cc_auditor a INNER join cc_areas b ON b.Cod_Area_CC = a.Area_Auditor  
WHERE A.F_Tintoreria = 'A' AND A.Activo = 'S' ORDER BY a.tip_auditor, a.cod_auditor
";

                    var supervisor = await connection.QueryAsync<EAuditor>(query);
                    return supervisor;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw new Exception("Se produjo un error inesperado.", sqlEx);
            }
        }

        public async Task<IEnumerable<EAuditor>?> ObtenerAuditor()
        {
            try

            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = @"
                
SELECT a.Tip_Auditor + +a.cod_Auditor + '  ' + a.Nom_Auditor  As acronimo, cod_Auditor idAuditor FROM 
ti_cc_auditor a INNER join cc_areas b ON b.Cod_Area_CC = a.Area_Auditor  
WHERE A.F_Tintoreria = 'A' AND A.Activo = 'S' ORDER BY a.tip_auditor, a.cod_auditor
            ";

                    var auditor = await connection.QueryAsync<EAuditor>(query);
                    return auditor;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw new Exception("Se produjo un error inesperado.", sqlEx);
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
                throw new Exception("Se produjo un error inesperado.", sqlEx);
            }
        }

        public async Task<IEnumerable<EUnidadNegocio>?> ObtenerUnidadNegocio()
        {
            try

            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = @"
                SELECT Descripcion acronimo, CodUnidNeg idUnidadNegocio  FROM caunidnegocio

            ";

                    var unidadNegocio = await connection.QueryAsync<EUnidadNegocio>(query);
                    return unidadNegocio;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw new Exception("Se produjo un error inesperado.", sqlEx);
            }
        }

        public async Task<IEnumerable<ECalificacion>?> ObtenerCalificacion()
        {
            try

            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = @"
               SELECT  Descrip acronimo, CodSitAc idCalificacion  FROM casitactual

            ";

                    var calificacion = await connection.QueryAsync<ECalificacion>(query);
                    return calificacion;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw new Exception("Se produjo un error inesperado.", sqlEx);
            }
        }

        public async Task<IEnumerable<ECalificacion>?> ObtenerEstadoProceso()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = @"
               SELECT  Descrip acronimo, Codigo idCalificacion  FROM EstadoProcesoInspeccionPre

            ";

                    var estadoProceso = await connection.QueryAsync<ECalificacion>(query);
                    return estadoProceso;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw new Exception("Se produjo un error inesperado.", sqlEx);
            }
        }

        public async Task<IEnumerable<EEstadoPartida>?> ObtenerEstadoPartida()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = @"
                      SELECT  Descrip acronimo, CodPartidaEstado idEstadoPartida  FROM EstadoPartidaDefecto WHERE CodDescripcion = 'EP'
                    ";

                    var estadoPartida = await connection.QueryAsync<EEstadoPartida>(query);
                    return estadoPartida;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw new Exception("Se produjo un error inesperado.", sqlEx);
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
                throw new Exception("Se produjo un error inesperado.", sqlEx);
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
                throw new Exception("Se produjo un error inesperado.", sqlEx);
            }
        }

        public async Task<IEnumerable<ERrollosPorPartida>?> BuscarRolloPorPartidaDetalle(string partida, string articulo, string sObs, string sCodUsu, string sReco, string sIns, string sResDig, string sObsRec, string sCodCal, string sCodTel)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@COD_ORDTRA", partida);
                    parameters.Add("@NUM_SECUENCIA", articulo);

                    parameters.Add("@Observacion", sObs);
                    parameters.Add("@COD_USUARIO", sCodUsu);
                    parameters.Add("@Recomendacion", sReco);
                    parameters.Add("@Inspector", sIns);
                    parameters.Add("@responsable_digitado", sResDig);
                    parameters.Add("@Observacion_Rectilineo", sObsRec);
                    parameters.Add("@Cod_Calidad", sCodCal);
                    parameters.Add("@Cod_Telas", sCodTel);


                    parameters.Add("@OPCION", 1);

                    var result = await connection.QueryAsync<ERrollosPorPartida>(
                        "TI_MUESTRA_DETALLE_POR_ROLLO_POR_PARTIDA_CALIFICACION_REV_PRE_1_HM",
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

        public async Task<IEnumerable<EPartidaCab>?> GuardarPartida(EPartidaCab partida)
        {
            string _telaComb = partida.datosTela.Substring(0, 8);
            decimal _calidadAuditada = 00;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    IEnumerable<EPartidaCab>? result = null;

                    // Si hay partida.detPartida se ejecuta todo lo relacionado
                    //if (partida.detPartida != null && partida.detPartida.Count > 0)
                    //{
                        var parameters = new DynamicParameters();
                        parameters.Add("@Num_Id", 0);
                        parameters.Add("@cod_OrdTra", partida.datosPartida ?? (object)DBNull.Value);
                        parameters.Add("@Observacion", partida.observaciones ?? (object)DBNull.Value);
                        parameters.Add("@COD_USUARIO", partida.usuario ?? (object)DBNull.Value);
                        parameters.Add("@Recomendacion", "");
                        parameters.Add("@Inspector", partida.auditor ?? (object)DBNull.Value);
                        parameters.Add("@responsable_digitado", partida.supervisor ?? (object)DBNull.Value);
                        parameters.Add("@Observacion_Rectilineo", "");


                        // Serialize detPartida and detDefecto to JSON
                        string detPartidaJson = JsonSerializer.Serialize(partida.detPartida);
                    /*
                    using (JsonDocument doc = JsonDocument.Parse(detPartidaJson))
                    {
                        var root = doc.RootElement;

                        // Si es un array, accede al primer objeto
                        JsonElement firstItem = root.ValueKind == JsonValueKind.Array
                            ? root[0]
                            : root;

                        if (firstItem.TryGetProperty("tela_Comb", out JsonElement telaCombElement))
                        {
                            _telaComb = telaCombElement.GetString();
                        }
                        if (firstItem.TryGetProperty("calidadAuditada", out JsonElement calidadAuditadaElement))
                        {
                            _calidadAuditada = calidadAuditadaElement.GetDecimal();
                        }


                        parameters.Add("@Cod_Calidad", _calidadAuditada);
                        parameters.Add("@Cod_Tela", _telaComb);
                    }
                    */
                    parameters.Add("@Cod_Calidad", "");
                    parameters.Add("@Cod_Tela", _telaComb);

                    // 👉 Solo se ejecuta si hay detPartida
                    result = await connection.QueryAsync<EPartidaCab>(
                            "cc_mant_auditoria_tintoreria_cabecera_tela_WS",
                            parameters,
                            commandType: CommandType.StoredProcedure
                        );
                    //}

                    //Guarda Defectos
                    
                    if (partida.detDefecto.Count > 0)
                    {
                        var resultDefecto = GuardarDefectosPartida(partida);
                    }
                    

                    //Guarda Rectilineos
                    if (partida.detRectilineo.Count > 0)
                    {
                        var resultRectilineo = GuardarRectilineos(partida);
                    }

                    //Actualizar o sincroniza Calidad y Metros en todos los rollos segun los cambios
                    //if (partida.detPartida.Count > 0)
                    //{
                        var resultReproceso = reprocesarPartidaRollos(partida);
                    //}

                    return result;

                }
            }
            catch (Exception sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw new Exception("Se produjo un error inesperado.", sqlEx);
            }

        }


        public async Task<IEnumerable<EPartidaCab>?> GuardarDefectosPartida(EPartidaCab partida)
        {
            string _rollo = string.Empty;
            string _cod_rollo = string.Empty;
            string _prefijo_maquina = string.Empty;
            int _calidadAuditada = 0;
            string _mtrsAuditados = string.Empty;
            string _largo = string.Empty;
            string _alto = string.Empty;
            string _medida_real = string.Empty;
            string _unidad_real = string.Empty;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Verifica que la cadena tenga al menos 2 caracteres antes de hacer Substring
                    string tipoArticulo = partida.datosTela.Length >= 2 ? partida.datosTela.Substring(0, 2) : string.Empty;

                    if (tipoArticulo == "RE")
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@TipoArticulo", tipoArticulo);
                        parameters.Add("@Accion", "I");
                        parameters.Add("@cod_OrdTra", partida.datosPartida ?? (object)DBNull.Value);
                        parameters.Add("@Flg_Contar", "S");

                        // Serialize detPartida and detDefecto to JSON
                        string detPartidaJson = JsonSerializer.Serialize(partida.detPartida);
                        string detDefectoJson = JsonSerializer.Serialize(partida.detDefecto);

                        /*NUEVO*/
                        JsonDocument doc = JsonDocument.Parse(detPartidaJson);
                        JsonElement root = doc.RootElement;

                        // Si es un string (caso "[{...}]")
                        if (root.ValueKind == JsonValueKind.String)
                        {
                            string innerJson = root.GetString();
                            doc = JsonDocument.Parse(innerJson); // reparsea
                            root = doc.RootElement;
                        }
                        /*FIN*/

                        foreach (var item in root.EnumerateArray())
                        {

                            try
                            {

                                if (item.TryGetProperty("rollo", out JsonElement rolloElement))
                                {
                                    _rollo = rolloElement.GetString();

                                    if (_rollo.Length > 19)
                                    {
                                        _prefijo_maquina = "";
                                        _cod_rollo = _rollo;
                                    }
                                    else
                                    {
                                        string[] parts = _rollo.Split('-');
                                        if (parts.Length == 2)
                                        {
                                            _prefijo_maquina = parts[0];
                                            _cod_rollo = parts[1];
                                        }
                                        else
                                        {
                                            _prefijo_maquina = "";
                                            _cod_rollo = _rollo;
                                        }
                                    }
                                }

                                if (item.TryGetProperty("calidadAuditada", out JsonElement calidadAuditadaElement))
                                {
                                    _calidadAuditada = calidadAuditadaElement.GetInt32();
                                }

                                if (item.TryGetProperty("und_Real", out JsonElement unidadRealElement))
                                {
                                    _unidad_real = unidadRealElement.GetString();
                                }

                                if (item.TryGetProperty("largo", out JsonElement largoElement))
                                {
                                    _largo = largoElement.GetString();
                                }

                                if (item.TryGetProperty("alto", out JsonElement altoElement))
                                {
                                    _alto = altoElement.GetString();
                                }

                                parameters.Add("@Prefijo_Maquina", _prefijo_maquina);
                                parameters.Add("@Codigo_Rollo", _cod_rollo);
                                parameters.Add("@Calidad", _calidadAuditada);
                                parameters.Add("@MtrsAuditados", _mtrsAuditados);
                                parameters.Add("@Ancho", partida.ancho);
                                parameters.Add("@Calf_Auto", "N");
                                parameters.Add("@Inspector", partida.auditor ?? (object)DBNull.Value);
                                parameters.Add("@Factor_Conversion", 0);
                                parameters.Add("@Maquina", partida.maquina);
                                parameters.Add("@MedidaReal", _largo + "X" + _alto);
                                parameters.Add("@UnidadReal", _unidad_real);
                                parameters.Add("@detDefectoJson", detDefectoJson ?? (object)DBNull.Value); // Solo el item actual


                                var result = await connection.QueryAsync<EPartidaCab>(
                                    "cc_Insert_Defecto_Auditoria_tintoreria_detalle_WS",
                                    parameters,
                                    commandType: CommandType.StoredProcedure
                                );

                            }
                            catch (Exception ex)
                            {
                                // 👇 Manejo de error para que no se rompa el bucle
                                Console.WriteLine($"Error procesando item: {ex.Message}");
                                // aquí puedes registrar el error en logs o DB si lo deseas
                                continue; // sigue con el siguiente item
                            }
                        }



                            /*
                            using (JsonDocument doc = JsonDocument.Parse(detPartidaJson))
                            {
                                var root = doc.RootElement;

                                if (root.ValueKind == JsonValueKind.Array)
                                {
                                    foreach (var item in root.EnumerateArray())
                                    {

                                        if (item.TryGetProperty("rollo", out JsonElement rolloElement))
                                        {
                                            _rollo = rolloElement.GetString();
                                            string[] parts = _rollo.Split('-');

                                            if (parts.Length == 2)
                                            {
                                                _prefijo_maquina = parts[0];
                                                _cod_rollo = parts[1];

                                                if (_prefijo_maquina.Length > 2)
                                                {
                                                    _prefijo_maquina = "00";
                                                    _cod_rollo = _rollo;

                                                }
                                            }
                                        }

                                        if (item.TryGetProperty("calidadAuditada", out JsonElement calidadAuditadaElement))
                                        {
                                            _calidadAuditada = calidadAuditadaElement.GetInt32();
                                        }

                                        if (item.TryGetProperty("und_Real", out JsonElement unidadRealElement))
                                        {
                                            _unidad_real = unidadRealElement.GetString();
                                        }

                                        if (item.TryGetProperty("largo", out JsonElement largoElement))
                                        {
                                            _largo = largoElement.GetString();
                                        }

                                        if (item.TryGetProperty("alto", out JsonElement altoElement))
                                        {
                                            _alto = altoElement.GetString();
                                        }

                                        parameters.Add("@Prefijo_Maquina", _prefijo_maquina);
                                        parameters.Add("@Codigo_Rollo", _cod_rollo);
                                        parameters.Add("@Calidad", _calidadAuditada);
                                        parameters.Add("@MtrsAuditados", _mtrsAuditados);
                                        parameters.Add("@Ancho", partida.ancho);
                                        parameters.Add("@Calf_Auto", "N");
                                        parameters.Add("@Inspector", partida.auditor ?? (object)DBNull.Value);
                                        parameters.Add("@Factor_Conversion", 0);
                                        parameters.Add("@Maquina", partida.maquina);
                                        parameters.Add("@MedidaReal", _largo + "X" + _alto);
                                        parameters.Add("@UnidadReal", _unidad_real);
                                        parameters.Add("@detDefectoJson", detDefectoJson ?? (object)DBNull.Value); // Solo el item actual

                                        var result = await connection.QueryAsync<EPartidaCab>(
                                            "cc_Insert_Defecto_Auditoria_tintoreria_detalle_WS",
                                            parameters,
                                            commandType: CommandType.StoredProcedure
                                        );
                                    }
                                }
                                else
                                {
                                    // Manejo si el JSON no es un array
                                    throw new Exception("El JSON proporcionado no es un arreglo.");
                                }
                            }*/

                            //Actualizar o sincroniza Calidad y Metros en todos los rollos
                            if (partida.detPartida.Count > 0)
                        {
                            var resultReproceso = reprocesarPartidaRollos(partida);
                        }

                        return null;
                    }

                    else
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@TipoArticulo", tipoArticulo);
                        parameters.Add("@Accion", "I");
                        parameters.Add("@cod_OrdTra", partida.datosPartida ?? (object)DBNull.Value);
                        parameters.Add("@Flg_Contar", "S");

                        // Serialize detPartida and detDefecto to JSON
                        string detPartidaJson = JsonSerializer.Serialize(partida.detPartida);
                        string detDefectoJson = JsonSerializer.Serialize(partida.detDefecto);

                        /*NUEVO*/
                        JsonDocument doc = JsonDocument.Parse(detPartidaJson);
                        JsonElement root = doc.RootElement;

                        // Si es un string (caso "[{...}]")
                        if (root.ValueKind == JsonValueKind.String)
                        {
                            string innerJson = root.GetString();
                            doc = JsonDocument.Parse(innerJson); // reparsea
                            root = doc.RootElement;
                        }
                        /*FIN*/

                        foreach (var item in root.EnumerateArray())
                        {

                            try
                            {

                                if (item.TryGetProperty("rollo", out JsonElement rolloElement))
                                {
                                    _rollo = rolloElement.GetString();

                                    if (_rollo.Length > 19)
                                    {
                                        _prefijo_maquina = "";
                                        _cod_rollo = _rollo;
                                    }
                                    else
                                    {
                                        string[] parts = _rollo.Split('-');
                                        if (parts.Length == 2)
                                        {
                                            _prefijo_maquina = parts[0];
                                            _cod_rollo = parts[1];
                                        }
                                        else
                                        {
                                            _prefijo_maquina = "";
                                            _cod_rollo = _rollo;
                                        }
                                    }
                                }

                                if (item.TryGetProperty("calidadAuditada", out JsonElement calidadAuditadaElement))
                                {
                                    _calidadAuditada = calidadAuditadaElement.GetInt32();
                                }

                                if (item.TryGetProperty("mtrsAuditados", out JsonElement mtrsAuditadosElement))
                                {
                                    _mtrsAuditados = mtrsAuditadosElement.GetString();
                                }

                                parameters.Add("@Prefijo_Maquina", _prefijo_maquina);
                                parameters.Add("@Codigo_Rollo", _cod_rollo);
                                parameters.Add("@Calidad", _calidadAuditada);
                                parameters.Add("@MtrsAuditados", _mtrsAuditados);
                                parameters.Add("@Ancho", partida.ancho == "" ? 0 : partida.ancho);
                                parameters.Add("@Calf_Auto", "N");
                                parameters.Add("@Inspector", partida.auditor ?? (object)DBNull.Value);
                                parameters.Add("@Factor_Conversion", 0m, DbType.Decimal);
                                parameters.Add("@Maquina", partida.maquina);
                                parameters.Add("@detDefectoJson", detDefectoJson ?? (object)DBNull.Value);

                                var result = await connection.QueryAsync<EPartidaCab>(
                                    "cc_Insert_Defecto_Auditoria_tintoreria_detalle_WS",
                                    parameters,
                                    commandType: CommandType.StoredProcedure
                                );

                            }
                            catch (Exception ex)
                            {
                                // 👇 Manejo de error para que no se rompa el bucle
                                Console.WriteLine($"Error procesando item: {ex.Message}");
                                // aquí puedes registrar el error en logs o DB si lo deseas
                                continue; // sigue con el siguiente item
                            }

                        }




                        /*
                        using (JsonDocument doc = JsonDocument.Parse(detPartidaJson))
                        {
                            var root = doc.RootElement;

                            if (root.ValueKind == JsonValueKind.Array)
                            {
                                
                                foreach (var item in root.EnumerateArray())
                                {

                                    if (item.TryGetProperty("rollo", out JsonElement rolloElement))
                                    {
                                        _rollo = rolloElement.GetString();

                                        if (_rollo.Length > 19)
                                        {
                                            _prefijo_maquina = "";
                                            _cod_rollo = _rollo;
                                        }
                                        else
                                        {
                                            string[] parts = _rollo.Split('-');

                                            if (parts.Length == 2)
                                            {
                                                _prefijo_maquina = parts[0];
                                                _cod_rollo = parts[1];
                                            }
                                            else
                                            {
                                                _prefijo_maquina = "";
                                                _cod_rollo = _rollo;
                                            }
                                        }

                                    }

                                    if (item.TryGetProperty("calidadAuditada", out JsonElement calidadAuditadaElement))
                                    {
                                        _calidadAuditada = calidadAuditadaElement.GetInt32();
                                    }

                                    if (item.TryGetProperty("mtrsAuditados", out JsonElement mtrsAuditadosElement))
                                    {
                                        _mtrsAuditados = mtrsAuditadosElement.GetString();
                                    }

                                    parameters.Add("@Prefijo_Maquina", _prefijo_maquina);
                                    parameters.Add("@Codigo_Rollo", _cod_rollo);
                                    parameters.Add("@Calidad", _calidadAuditada);
                                    parameters.Add("@MtrsAuditados", _mtrsAuditados);
                                    parameters.Add("@Ancho", partida.ancho == "" ? 0 : partida.ancho);
                                    parameters.Add("@Calf_Auto", "N");
                                    parameters.Add("@Inspector", partida.auditor ?? (object)DBNull.Value);
                                    parameters.Add("@Factor_Conversion", 0m, DbType.Decimal);
                                    parameters.Add("@Maquina", partida.maquina);
                                    parameters.Add("@detDefectoJson", detDefectoJson ?? (object)DBNull.Value); // Solo el item actual

                                    var result = await connection.QueryAsync<EPartidaCab>(
                                        "cc_Insert_Defecto_Auditoria_tintoreria_detalle_WS",
                                        parameters,
                                        commandType: CommandType.StoredProcedure
                                    );
                                }
                            }
                            else
                            {
                                // Manejo si el JSON no es un array
                                throw new Exception("El JSON proporcionado no es un arreglo.");
                            }
                        }
                        */

                        //Actualizar o sincroniza Calidad y Metros en todos los rollos
                        if (partida.detPartida.Count > 0)
                        {
                            var resultReproceso = reprocesarPartidaRollos(partida);
                        }

                        return null;

                    }


                }
            }
            catch (Exception sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw new Exception("Se produjo un error inesperado.", sqlEx);
            }

        }


        public async Task<IEnumerable<EPartidaCab>?> GuardarRectilineos(EPartidaCab partida)
        {
            string _rollo = string.Empty;
            string _cod_rollo = string.Empty;
            string _prefijo_maquina = string.Empty;
            int _calidadAuditada = 0;
            string _mtrsAuditados = string.Empty;
            string _largo = string.Empty;
            string _alto = string.Empty;
            string _medida_real = string.Empty;
            string _unidad_real = string.Empty;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Verifica que la cadena tenga al menos 2 caracteres antes de hacer Substring
                    string tipoArticulo = partida.datosTela.Length >= 2 ? partida.datosTela.Substring(0, 2) : string.Empty;

                    if (tipoArticulo == "RE")
                    {
                        var parameters = new DynamicParameters();
                        //parameters.Add("@TipoArticulo", tipoArticulo);
                        parameters.Add("@Accion", "I");
                        parameters.Add("@cod_OrdTra", partida.datosPartida ?? (object)DBNull.Value);
                        //parameters.Add("@Flg_Contar", "S");

                        // Serialize detPartida and detDefecto to JSON
                        string detPartidaJson = JsonSerializer.Serialize(partida.detPartida);
                        string detRectilineoJson = JsonSerializer.Serialize(partida.detRectilineo);

                        using (JsonDocument doc = JsonDocument.Parse(detPartidaJson))
                        {
                            var root = doc.RootElement;

                            if (root.ValueKind == JsonValueKind.Array)
                            {
                                foreach (var item in root.EnumerateArray())
                                {

                                    if (item.TryGetProperty("rollo", out JsonElement rolloElement))
                                    {
                                        _rollo = rolloElement.GetString();
                                        string[] parts = _rollo.Split('-');

                                        if (parts.Length == 2)
                                        {
                                            _prefijo_maquina = parts[0];
                                            _cod_rollo = parts[1];

                                            if (_prefijo_maquina.Length > 2)
                                            {
                                                _prefijo_maquina = "00";
                                                _cod_rollo = _rollo;

                                            }
                                        }
                                    }

                                    if (item.TryGetProperty("calidadAuditada", out JsonElement calidadAuditadaElement))
                                    {
                                        _calidadAuditada = calidadAuditadaElement.GetInt32();
                                    }

                                    if (item.TryGetProperty("und_Real", out JsonElement unidadRealElement))
                                    {
                                        _unidad_real = unidadRealElement.GetString();
                                    }

                                    if (item.TryGetProperty("largo", out JsonElement largoElement))
                                    {
                                        _largo = largoElement.GetString();
                                    }

                                    if (item.TryGetProperty("alto", out JsonElement altoElement))
                                    {
                                        _alto = altoElement.GetString();
                                    }

                                    int indexGuion = partida.datosTela.IndexOf('-');

                                    parameters.Add("@Prefijo_Maquina", _prefijo_maquina);
                                    parameters.Add("@Codigo_Rollo", _cod_rollo);
                                    parameters.Add("@COD_TELA", indexGuion > 0 ? partida.datosTela.Substring(0, indexGuion).Trim() : partida.datosTela);
                                    parameters.Add("@COD_TALLA", "0");
                                    parameters.Add("@Cantidad", "0");
                                    parameters.Add("@Calidad", _calidadAuditada);
                                    parameters.Add("@COD_USUARIO", partida.usuario);
                                    //parameters.Add("@Ancho", partida.ancho);
                                    //parameters.Add("@Calf_Auto", "N");
                                    //parameters.Add("@Inspector", partida.auditor ?? (object)DBNull.Value);
                                    //parameters.Add("@Factor_Conversion", 0);
                                    //parameters.Add("@Maquina", partida.maquina);
                                    //parameters.Add("@MedidaReal", _largo + "X" + _alto);
                                    //parameters.Add("@UnidadReal", _unidad_real);
                                    parameters.Add("@detRectilineoJson", detRectilineoJson ?? (object)DBNull.Value); // Solo el item actual

                                    var result = await connection.QueryAsync<EPartidaCab>(
                                        "CC_INSERT_AUDITORIA_TINTORERIA_DETALLE_RECTILINEOS_WS",
                                        parameters,
                                        commandType: CommandType.StoredProcedure
                                    );
                                }
                            }
                            else
                            {
                                // Manejo si el JSON no es un array
                                throw new Exception("El JSON proporcionado no es un arreglo.");
                            }
                        }

                        
                    }
                    return null;

                }
            }
            catch (Exception sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw new Exception("Se produjo un error inesperado.", sqlEx);
            }

        }


        //ELIMINAR
        public async Task<IEnumerable<EPartidaCab>?> GuardarPartidac(EPartidaCab partida)
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
                throw new Exception("Se produjo un error inesperado.", sqlEx);
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
                throw new Exception("Se produjo un error inesperado.", sqlEx);
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
                throw new Exception("Se produjo un error inesperado.", sqlEx);
            }
        }

        public async Task<IEnumerable<EUnionRollos>?> ObtenerDatosUnionRollos(EUnionRollos filtro)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    filtro.Opcion = Convert.ToString(filtro.Calidad);
                    var codigo_rollo = filtro.Rollo;
                    var codigo_Tela = filtro.Tela_comb;
                    var resultado = await connection.QueryAsync<EUnionRollos>(
                        "TI_MUESTRA_DETALLE_POR_ROLLO_POR_PARTIDA_DETALLE_NUEVO_WS",
                        new
                        {
                            // Asegúrate de que estos nombres coincidan con los parámetros del procedimiento almacenado
                            filtro.Cod_Ordtra,
                            filtro.Opcion,
                            codigo_rollo, // Reemplaza con los nombres reales
                            codigo_Tela
                        },
                        commandType: CommandType.StoredProcedure
                    );

                    return resultado;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw new Exception("Se produjo un error inesperado al obtener la unión de rollos.", sqlEx);
            }
        }

        public async Task<IEnumerable<EGuardarUnioRollo>?> guardarDatosUnionRollos(EGuardarUnioRollo unionRollos)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    if (unionRollos.Ot_Tejeduria != "")
                    {

                        var parameters = new DynamicParameters();
                        parameters.Add("@Cod_Usuario_Union", unionRollos.usuario ?? (object)DBNull.Value);
                        parameters.Add("@Cod_Ordtra_Tejeduria", unionRollos.Ot_Tejeduria ?? (object)DBNull.Value);
                        parameters.Add("@Codigo_Rollo_Crudo1", unionRollos.Rollo1 ?? (object)DBNull.Value);
                        parameters.Add("@Codigo_Rollo_Crudo2", unionRollos.Rollo2 ?? (object)DBNull.Value);

                        var result = await connection.QueryAsync<EPartidaCab>(
                            "Usp_Union_Rollo_Tejeduria_WS",
                            parameters,
                            commandType: CommandType.StoredProcedure
                        );
                    }

                    else
                    {

                        var parameters = new DynamicParameters();
                        parameters.Add("@Cod_Usuario_Union", unionRollos.usuario ?? (object)DBNull.Value);
                        parameters.Add("@Cod_Ordtra_Tejeduria", unionRollos.Ot_Tejeduria ?? (object)DBNull.Value);
                        parameters.Add("@Codigo_Rollo_Crudo1", unionRollos.Rollo1 ?? (object)DBNull.Value);
                        parameters.Add("@Codigo_Rollo_Crudo2", unionRollos.Rollo2 ?? (object)DBNull.Value);

                        var result = await connection.QueryAsync<EPartidaCab>(
                            "Usp_Union_Rollo_Servicios_WS",
                            parameters,
                            commandType: CommandType.StoredProcedure
                        );

                    }


                    return null;

                }
            }
            catch (Exception sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw new Exception("Se produjo un error inesperado.", sqlEx);
            }

        }

        public async Task<IEnumerable<EPartidaCab>?> reprocesarPartidaRollos(EPartidaCab partida)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {

                    var parameters = new DynamicParameters();
                    parameters.Add("@cod_OrdTra", partida.datosPartida ?? (object)DBNull.Value);
                    parameters.Add("@Prefijo_Maquina", partida.maquina ?? (object)DBNull.Value);
                    //parameters.Add("@Ancho", partida.ancho ?? "0");
                    parameters.Add("@Ancho", string.IsNullOrEmpty(partida.ancho) ? "0" : partida.ancho);

                    // Serialize detPartida and detDefecto to JSON
                    string detPartidaJson = JsonSerializer.Serialize(partida.detPartida);
                    parameters.Add("@detPartidasJson", detPartidaJson ?? (object)DBNull.Value);

                    // Serializa detRollosTotal to JSON
                    string detRollosTotalJson = JsonSerializer.Serialize(partida.detRollosTotal);
                    parameters.Add("@detRollosTotal", detRollosTotalJson ?? (object)DBNull.Value);

                    var result = await connection.QueryAsync<EPartidaCab>(
                        "CC_Reproceso_Rollos_Auditoria_tintoreria_detalle_WS",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    return result;

                }
            }
            catch (Exception sqlEx)
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw new Exception("Se produjo un error inesperado.", sqlEx);
            }
        }

        public async Task<IEnumerable<EDefectosRegistrados>?> ObtenerDefectosRegistradosPorRollo(string Cod_OrdTra, string Cod_Tela, string PrefijoMaquina, string CodigoRollo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Cod_OrdTra = Cod_OrdTra,
                    Cod_Tela = Cod_Tela,
                    Prefijo_Maquina = PrefijoMaquina == null ? "" : PrefijoMaquina,
                    Codigo_Rollo = CodigoRollo,
                };

                var result = await connection.QueryAsync<EDefectosRegistrados>(
                     "[dbo].[CC_Listado_Defectos_Rollos_Auditoria_tintoreria_WS]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<(int Codigo, string Mensaje)> EliminarDefectoRollo(string CodOrdTra, string CodigoRollo, string CodMotivo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                // Parametros de SQL
                parametros.Add("@Cod_OrdTra", CodOrdTra);
                parametros.Add("@Codigo_Rollo", CodigoRollo);
                parametros.Add("@Cod_Motivo", CodMotivo);
                // Parámetros de salida
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                // Ejecutar el procedimiento almacenado
                try
                {
                    connection.Execute(
                        "[dbo].[CC_Eliminar_Defectos_Rollos_Auditoria_tintoreria_WS]",
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

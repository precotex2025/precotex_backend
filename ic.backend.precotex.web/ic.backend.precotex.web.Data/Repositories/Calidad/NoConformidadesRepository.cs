using System;

using System.Collections.Generic;

using System.Data;

using System.IO;

using System.Threading.Tasks;

using ic.backend.precotex.web.Data.Repositories.Implementation.Calidad;

using ic.backend.precotex.web.Entity.Entities.Calidad;

using System.Data.SqlClient;

using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.Logging;

namespace ic.backend.precotex.web.Data.Repositories.Calidad

{

    public class NoConformidadesRepository : INoConformidadesRepository

    {

        private readonly string _cn;

        private readonly ILogger<NoConformidadesRepository> _logger;

        public NoConformidadesRepository(IConfiguration configuration, ILogger<NoConformidadesRepository> logger)

        {

            _cn = configuration.GetConnectionString("TextilConnection")!;

            _logger = logger;

        }

        public async Task<List<Dictionary<string, object>>> ListarDatosInformeCalidad(string tipo, string cod = "")

        {

            return await ExecuteReaderToDictListAsync("UP_CC_Listar_Datos_Informe_Calidad", cmd =>

            {

                cmd.Parameters.AddWithValue("@Tipo", string.IsNullOrWhiteSpace(tipo) ? "T" : tipo.Trim());

                cmd.Parameters.AddWithValue("@Cod_Motivo", cod ?? "");

            });

        }

        public async Task<List<NoConformidades>> MostrarCabecera(string? numInforme, string? fIni, string? fFin, string? partida)

        {

            DateTime dtIni = DateTime.Today.AddYears(-2);

            DateTime dtFin = DateTime.Today.AddDays(1);

            if (!string.IsNullOrWhiteSpace(fIni) && DateTime.TryParse(fIni, out var parsedIni)) dtIni = parsedIni;

            if (!string.IsNullOrWhiteSpace(fFin) && DateTime.TryParse(fFin, out var parsedFin)) dtFin = parsedFin;

            var result = new List<NoConformidades>();

            using (var con = new SqlConnection(_cn))

            using (var cmd = new SqlCommand("UP_CC_Muestra_Informe_Calidad_Cabecera", con))

            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Cod_OrdTra", partida ?? "");

                cmd.Parameters.AddWithValue("@Fec_Inicio", dtIni);

                cmd.Parameters.AddWithValue("@Fec_Fin", dtFin);

                cmd.Parameters.AddWithValue("@Cod_Usuario", "");

                await con.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())

                {

                    while (await reader.ReadAsync())

                    {

                        result.Add(new NoConformidades

                        {

                            Numero = reader["Numero"] != DBNull.Value ? reader["Numero"].ToString() : null,

                            NumeroNC = reader["NumeroNC"] != DBNull.Value ? reader["NumeroNC"].ToString() : null,

                            Partida = reader["Partida"] != DBNull.Value ? reader["Partida"].ToString() : null,

                            Fecha = reader["Fecha"] != DBNull.Value ? reader["Fecha"].ToString() : null,

                            CodCliente = reader["CodCliente"] != DBNull.Value ? reader["CodCliente"].ToString() : null,

                            Cliente = reader["Cliente"] != DBNull.Value ? reader["Cliente"].ToString() : null,

                            CodColor = reader["CodColor"] != DBNull.Value ? reader["CodColor"].ToString() : null,

                            Color = reader["Color"] != DBNull.Value ? reader["Color"].ToString() : null,

                            Area = reader["Area"] != DBNull.Value ? reader["Area"].ToString() : null,

                            Usuario = reader["Usuario"] != DBNull.Value ? reader["Usuario"].ToString() : null,

                            Responsable = reader["Responsable"] != DBNull.Value ? reader["Responsable"].ToString() : null

                        });

                    }

                }

            }

            return result;

        }

        public async Task<List<Dictionary<string, object>>> MostrarPartida(string partida, string tipo = "")

        {

            return await ExecuteReaderToDictListAsync("UP_CC_Mostrar_Partida", cmd =>

            {

                cmd.Parameters.AddWithValue("@COD_ORDTRA", (partida ?? "").Trim());

                cmd.Parameters.AddWithValue("@Tipo", string.IsNullOrWhiteSpace(tipo) ? "T" : tipo.Trim());

            });

        }

        public async Task<List<Dictionary<string, object>>> MostrarDetalle(string numInforme = "", string partida = "")

        {

            return await ExecuteReaderToDictListAsync("UP_CC_Muestra_Informe_Calidad_Detalle", cmd =>

            {

                cmd.Parameters.AddWithValue("@Num_Informe", (numInforme ?? "").Trim());

                cmd.Parameters.AddWithValue("@COD_ORDTRA", (partida ?? "").Trim());

            });

        }

        public async Task<List<Dictionary<string, object>>> MostrarDetalleMotivo(string numInforme, string partida = "")

        {

            return await ExecuteReaderToDictListAsync("UP_CC_Muestra_Informe_Calidad_Detalle_Motivo", cmd =>

            {

                cmd.Parameters.AddWithValue("@Num_Informe", (numInforme ?? "").Trim());

                cmd.Parameters.AddWithValue("@Cod_OrdTra", (partida ?? "").Trim());

            });

        }

        public async Task<List<Dictionary<string, object>>> ReporteNoConformidad(string fIni = "", string fFin = "")

        {

            DateTime dtIni = DateTime.Today.AddMonths(-1);

            DateTime dtFin = DateTime.Today;

            if (!string.IsNullOrWhiteSpace(fIni) && DateTime.TryParse(fIni, out var parsedIni)) dtIni = parsedIni;

            if (!string.IsNullOrWhiteSpace(fFin) && DateTime.TryParse(fFin, out var parsedFin)) dtFin = parsedFin;

            return await ExecuteReaderToDictListAsync("UP_CC_Reporte_Informe_No_Conformidad", cmd =>

            {

                cmd.Parameters.AddWithValue("@Fec_Ini", dtIni.ToString("dd/MM/yyyy"));

                cmd.Parameters.AddWithValue("@Fec_Fin", dtFin.ToString("dd/MM/yyyy"));

            });

        }

        public async Task<List<Dictionary<string, object>>> MostrarHistorial(string numInforme, string partida = "")
        {
            string cleanNum = (numInforme ?? "").Trim();
            if (cleanNum.StartsWith("NC-", StringComparison.OrdinalIgnoreCase)) cleanNum = cleanNum.Substring(3).Trim();
            if (int.TryParse(cleanNum, out int nVal)) cleanNum = nVal.ToString("D6");

            return await ExecuteReaderToDictListAsync("UP_CC_Muestra_Informe_Calidad_Historial", cmd =>
            {
                cmd.Parameters.AddWithValue("@Num_Informe", cleanNum);
                cmd.Parameters.AddWithValue("@Cod_OrdTra", (partida ?? "").Trim());
            });
        }

        public async Task<ResponseResultado> AnularInforme(InformeAnularRequest req)

        {

            if (req == null) throw new ArgumentNullException(nameof(req));

            using (var con = new SqlConnection(_cn))

            {

                await con.OpenAsync();

                using (var cmdCab = new SqlCommand("UP_Man_Informe_No_Conformidad_Cabecera", con))

                {

                    cmdCab.CommandType = CommandType.StoredProcedure;

                    cmdCab.Parameters.AddWithValue("@Accion", "D");

                    cmdCab.Parameters.AddWithValue("@Num_Informe", (req.Num_Informe ?? "").Trim());

                    cmdCab.Parameters.AddWithValue("@Cod_OrdTra", (req.Cod_OrdTra ?? "").Trim());

                    cmdCab.Parameters.AddWithValue("@CC_Usu_Crea", string.IsNullOrWhiteSpace(req.Cod_Usuario) ? "SISTEMAS" : req.Cod_Usuario.Trim());

                    cmdCab.Parameters.AddWithValue("@Motivo_Anula", req.Motivo_Anula ?? "");

                    var pCod = new SqlParameter("@Codigo", SqlDbType.Int) { Direction = ParameterDirection.Output };

                    var pMsj = new SqlParameter("@sMsj", SqlDbType.VarChar, 255) { Direction = ParameterDirection.Output };

                    cmdCab.Parameters.Add(pCod);

                    cmdCab.Parameters.Add(pMsj);

                    await cmdCab.ExecuteNonQueryAsync();

                    int codResult = pCod.Value != null && pCod.Value != DBNull.Value ? Convert.ToInt32(pCod.Value) : 0;

                    string msjResult = pMsj.Value != null && pMsj.Value != DBNull.Value ? pMsj.Value.ToString()! : "";

                    if (codResult == 0)

                    {

                        throw new Exception($"Error al anular informe: {msjResult}");

                    }

                    return new ResponseResultado

                    {

                        Success = true,

                        Message = string.IsNullOrWhiteSpace(msjResult) ? "Informe anulado exitosamente." : msjResult,

                        Num_Informe = req.Num_Informe

                    };

                }

            }

        }

        public async Task<List<Dictionary<string, object>>> MostrarEvolutivo()

        {

            return await ExecuteReaderToDictListAsync("UP_CC_Muestra_Informe_Calidad_Evolutivo", cmd => { });

        }

        public async Task<ResponseResultado> GuardarTransaccionCompleta(InformeGuardarRequest req)
        {
            if (req == null) throw new ArgumentNullException(nameof(req));

            string codOrdTra = (req.Cod_OrdPro ?? "").Trim();
            string codUsuario = string.IsNullOrWhiteSpace(req.Cod_Usuario) ? "SISTEMAS" : req.Cod_Usuario.Trim();

            using (var con = new SqlConnection(_cn))
            {
                await con.OpenAsync();
                try
                {
                    string numInformeGenerado = (req.Num_Informe ?? "").Trim();
                    bool esNuevo = string.IsNullOrWhiteSpace(numInformeGenerado) || req.Accion == "I";

                    // =========================================================
                    // 1. CABECERA (Solo en creación; en edición la cabecera no se modifica)
                    // =========================================================
                    if (esNuevo)
                    {
                        // MODO CREACIÓN: UP_Man_Informe_No_Conformidad_Cabecera con @Accion = 'I'
                        using (var cmdCab = new SqlCommand("UP_Man_Informe_No_Conformidad_Cabecera", con))
                        {
                            cmdCab.CommandType = CommandType.StoredProcedure;
                            cmdCab.Parameters.AddWithValue("@Accion", "I");
                            var pNumInf = new SqlParameter("@Num_Informe", SqlDbType.Char, 10)
                            {
                                Direction = ParameterDirection.InputOutput,
                                Value = ""
                            };
                            cmdCab.Parameters.Add(pNumInf);
                            cmdCab.Parameters.AddWithValue("@Cod_OrdTra", codOrdTra);
                            cmdCab.Parameters.AddWithValue("@CC_Usu_Crea", codUsuario);
                            cmdCab.Parameters.AddWithValue("@Motivo_Anula", "");
                            var pCod = new SqlParameter("@Codigo", SqlDbType.Int) { Direction = ParameterDirection.Output };
                            var pMsj = new SqlParameter("@sMsj", SqlDbType.VarChar, 255) { Direction = ParameterDirection.Output };
                            cmdCab.Parameters.Add(pCod);
                            cmdCab.Parameters.Add(pMsj);

                            await cmdCab.ExecuteNonQueryAsync();

                            int codResult = pCod.Value != null && pCod.Value != DBNull.Value ? Convert.ToInt32(pCod.Value) : 0;
                            string msjResult = pMsj.Value != null && pMsj.Value != DBNull.Value ? pMsj.Value.ToString()! : "";

                            numInformeGenerado = pNumInf.Value != null && pNumInf.Value != DBNull.Value
                                ? pNumInf.Value.ToString()!.Trim()
                                : "";

                            if (codResult == 0)
                            {
                                throw new Exception($"Error al crear informe: {msjResult}");
                            }
                        }
                    }

                    // ===========================================================
                    // 2. ACTUALIZAR ROLLOS RECHAZADOS por artículo
                    //    UP_Man_Informe_No_Conformidad_Detalle con @ACCION = 'U'
                    // ===========================================================
                    if (req.Articulos != null && req.Articulos.Count > 0)
                    {
                        foreach (var art in req.Articulos)
                        {
                            int numSecuencia = 0;
                            int.TryParse(art.Item, out numSecuencia);

                            // La secuencia proviene directamente del frontend (art.Item / a.itemSec)
                            if (numSecuencia <= 0 || art.Cant_Rollos_Rech <= 0)
                            {
                                continue;
                            }

                            using (var cmdDet = new SqlCommand("UP_Man_Informe_No_Conformidad_Detalle", con))
                            {
                                cmdDet.CommandType = CommandType.StoredProcedure;
                                cmdDet.Parameters.AddWithValue("@ACCION", "U");
                                cmdDet.Parameters.AddWithValue("@Num_Informe", numInformeGenerado);
                                cmdDet.Parameters.AddWithValue("@Cod_OrdTra", codOrdTra);
                                cmdDet.Parameters.AddWithValue("@Num_Secuencia", numSecuencia);
                                cmdDet.Parameters.AddWithValue("@Rollos_Rechazados", art.Cant_Rollos_Rech);
                                var pCodDet = new SqlParameter("@Codigo", SqlDbType.Int) { Direction = ParameterDirection.Output };
                                var pMsjDet = new SqlParameter("@sMsj", SqlDbType.VarChar, 255) { Direction = ParameterDirection.Output };
                                cmdDet.Parameters.Add(pCodDet);
                                cmdDet.Parameters.Add(pMsjDet);

                                await cmdDet.ExecuteNonQueryAsync();
                            }

                            // ========================================================
                            // 3. ACTUALIZAR / REEMPLAZAR MOTIVOS/DEFECTOS del artículo
                            // ========================================================
                            // A) Obtener los motivos actualmente registrados en BD para esta secuencia
                            var motivosEnBD = new List<string>();
                            using (var cmdGetMot = new SqlCommand(
                                "SELECT RTRIM(Cod_Motivo) AS Cod_Motivo FROM CC_Informe_Control_Calidad_Det " +
                                "WHERE CC_Numero_Informe = @Num AND Cod_OrdTra = @Ot AND Num_Secuencia = @Sec", con))
                            {
                                cmdGetMot.Parameters.AddWithValue("@Num", numInformeGenerado);
                                cmdGetMot.Parameters.AddWithValue("@Ot", codOrdTra);
                                cmdGetMot.Parameters.AddWithValue("@Sec", numSecuencia);
                                using (var rdr = await cmdGetMot.ExecuteReaderAsync())
                                {
                                    while (await rdr.ReadAsync())
                                    {
                                        string m = rdr["Cod_Motivo"]?.ToString()?.Trim() ?? "";
                                        if (!string.IsNullOrEmpty(m)) motivosEnBD.Add(m);
                                    }
                                }
                            }

                            // B) Identificar los nuevos motivos válidos que vienen en la edición
                            var nuevosMotivos = (art.Defectos ?? new List<DefectoGuardarDto>())
                                .Where(d => (d.Accion ?? "").Trim().ToUpper() != "D" && !string.IsNullOrWhiteSpace(d.Cod_Motivo))
                                .Select(d => d.Cod_Motivo!.Trim().Length > 6 ? d.Cod_Motivo.Trim().Substring(0, 6) : d.Cod_Motivo.Trim())
                                .Distinct(StringComparer.OrdinalIgnoreCase)
                                .ToList();

                            // C) Eliminar de BD los motivos anteriores que ya NO están en la lista (reemplazados o eliminados)
                            var motivosAEliminar = motivosEnBD
                                .Where(m => !nuevosMotivos.Contains(m, StringComparer.OrdinalIgnoreCase))
                                .ToList();

                            foreach (var mDel in motivosAEliminar)
                            {
                                using (var cmdDel = new SqlCommand("UP_Man_Informe_No_Conformidad_Motivo", con))
                                {
                                    cmdDel.CommandType = CommandType.StoredProcedure;
                                    cmdDel.Parameters.AddWithValue("@ACCION", "D");
                                    cmdDel.Parameters.AddWithValue("@Num_Informe", numInformeGenerado);
                                    cmdDel.Parameters.AddWithValue("@Cod_OrdTra", codOrdTra);
                                    cmdDel.Parameters.AddWithValue("@Num_Secuencia", numSecuencia);
                                    cmdDel.Parameters.AddWithValue("@Cod_Area_CC", "TIN");
                                    cmdDel.Parameters.AddWithValue("@Cod_Motivo", mDel);
                                    cmdDel.Parameters.AddWithValue("@Cod_Motivo_Ant", "");
                                    cmdDel.Parameters.AddWithValue("@CC_Observacion", "Reemplazado en edición");
                                    cmdDel.Parameters.AddWithValue("@Usuario", codUsuario);

                                    var pCod = new SqlParameter("@Codigo", SqlDbType.Int) { Direction = ParameterDirection.Output };
                                    var pMsj = new SqlParameter("@sMsj", SqlDbType.VarChar, 255) { Direction = ParameterDirection.Output };
                                    cmdDel.Parameters.Add(pCod);
                                    cmdDel.Parameters.Add(pMsj);

                                    await cmdDel.ExecuteNonQueryAsync();
                                }
                            }

                            // D) Guardar / actualizar los motivos actuales
                            if (art.Defectos != null && art.Defectos.Count > 0)
                            {
                                foreach (var def in art.Defectos)
                                {
                                    string codMotivo = (def.Cod_Motivo ?? "").Trim();
                                    if (string.IsNullOrEmpty(codMotivo)) continue;

                                    string codArea = (def.Cod_Area ?? "").Trim();
                                    if (string.IsNullOrEmpty(codArea)) codArea = "TIN";
                                    if (codArea.Length > 5) codArea = codArea.Substring(0, 5);
                                    if (codMotivo.Length > 6) codMotivo = codMotivo.Substring(0, 6);

                                    string accionDeseada = (def.Accion ?? "").Trim().ToUpper();
                                    if (accionDeseada == "D") continue; // Ya fue eliminado en el paso C

                                    if (motivosEnBD.Contains(codMotivo, StringComparer.OrdinalIgnoreCase))
                                    {
                                        // Ya existía en BD -> Actualizar con @ACCION = 'U'
                                        using (var cmdUpMot = new SqlCommand("UP_Man_Informe_No_Conformidad_Motivo", con))
                                        {
                                            cmdUpMot.CommandType = CommandType.StoredProcedure;
                                            cmdUpMot.Parameters.AddWithValue("@ACCION", "U");
                                            cmdUpMot.Parameters.AddWithValue("@Num_Informe", numInformeGenerado);
                                            cmdUpMot.Parameters.AddWithValue("@Cod_OrdTra", codOrdTra);
                                            cmdUpMot.Parameters.AddWithValue("@Num_Secuencia", numSecuencia);
                                            cmdUpMot.Parameters.AddWithValue("@Cod_Area_CC", codArea);
                                            cmdUpMot.Parameters.AddWithValue("@Cod_Motivo", codMotivo);
                                            cmdUpMot.Parameters.AddWithValue("@Cod_Motivo_Ant", codMotivo);
                                            cmdUpMot.Parameters.AddWithValue("@CC_Observacion", def.Observacion ?? "");
                                            cmdUpMot.Parameters.AddWithValue("@Usuario", codUsuario);

                                            var pCodUp = new SqlParameter("@Codigo", SqlDbType.Int) { Direction = ParameterDirection.Output };
                                            var pMsjUp = new SqlParameter("@sMsj", SqlDbType.VarChar, 255) { Direction = ParameterDirection.Output };
                                            cmdUpMot.Parameters.Add(pCodUp);
                                            cmdUpMot.Parameters.Add(pMsjUp);

                                            await cmdUpMot.ExecuteNonQueryAsync();
                                        }
                                    }
                                    else
                                    {
                                        // Es nuevo o reemplazo -> Insertar con @ACCION = 'I'
                                        int codMotRes = 0;
                                        using (var cmdMot = new SqlCommand("UP_Man_Informe_No_Conformidad_Motivo", con))
                                        {
                                            cmdMot.CommandType = CommandType.StoredProcedure;
                                            cmdMot.Parameters.AddWithValue("@ACCION", "I");
                                            cmdMot.Parameters.AddWithValue("@Num_Informe", numInformeGenerado);
                                            cmdMot.Parameters.AddWithValue("@Cod_OrdTra", codOrdTra);
                                            cmdMot.Parameters.AddWithValue("@Num_Secuencia", numSecuencia);
                                            cmdMot.Parameters.AddWithValue("@Cod_Area_CC", codArea);
                                            cmdMot.Parameters.AddWithValue("@Cod_Motivo", codMotivo);
                                            cmdMot.Parameters.AddWithValue("@Cod_Motivo_Ant", "");
                                            cmdMot.Parameters.AddWithValue("@CC_Observacion", def.Observacion ?? "");
                                            cmdMot.Parameters.AddWithValue("@Usuario", codUsuario);

                                            var pCodMot = new SqlParameter("@Codigo", SqlDbType.Int) { Direction = ParameterDirection.Output };
                                            var pMsjMot = new SqlParameter("@sMsj", SqlDbType.VarChar, 255) { Direction = ParameterDirection.Output };
                                            cmdMot.Parameters.Add(pCodMot);
                                            cmdMot.Parameters.Add(pMsjMot);

                                            await cmdMot.ExecuteNonQueryAsync();

                                            codMotRes = pCodMot.Value != null && pCodMot.Value != DBNull.Value ? Convert.ToInt32(pCodMot.Value) : 0;
                                        }

                                        if (codMotRes == 1)
                                        {
                                            using (var cmdUpMot = new SqlCommand("UP_Man_Informe_No_Conformidad_Motivo", con))
                                            {
                                                cmdUpMot.CommandType = CommandType.StoredProcedure;
                                                cmdUpMot.Parameters.AddWithValue("@ACCION", "U");
                                                cmdUpMot.Parameters.AddWithValue("@Num_Informe", numInformeGenerado);
                                                cmdUpMot.Parameters.AddWithValue("@Cod_OrdTra", codOrdTra);
                                                cmdUpMot.Parameters.AddWithValue("@Num_Secuencia", numSecuencia);
                                                cmdUpMot.Parameters.AddWithValue("@Cod_Area_CC", codArea);
                                                cmdUpMot.Parameters.AddWithValue("@Cod_Motivo", codMotivo);
                                                cmdUpMot.Parameters.AddWithValue("@Cod_Motivo_Ant", codMotivo);
                                                cmdUpMot.Parameters.AddWithValue("@CC_Observacion", def.Observacion ?? "");
                                                cmdUpMot.Parameters.AddWithValue("@Usuario", codUsuario);

                                                var pCodUp = new SqlParameter("@Codigo", SqlDbType.Int) { Direction = ParameterDirection.Output };
                                                var pMsjUp = new SqlParameter("@sMsj", SqlDbType.VarChar, 255) { Direction = ParameterDirection.Output };
                                                cmdUpMot.Parameters.Add(pCodUp);
                                                cmdUpMot.Parameters.Add(pMsjUp);

                                                await cmdUpMot.ExecuteNonQueryAsync();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    // 4. GUARDAR FOTOS DE EVIDENCIA
                    var archivosGuardados = GuardarFotosEvidencia(numInformeGenerado, req.Articulos);

                    return new ResponseResultado
                    {
                        Success = true,
                        Message = "Informe registrado exitosamente.",
                        Num_Informe = numInformeGenerado,
                        ArchivosGuardados = archivosGuardados
                    };
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error en GuardarTransaccionCompleta");
                    throw;
                }
            }
        }

        private List<string> GuardarFotosEvidencia(string numInforme, List<ArticuloGuardarDto> articulos)

        {

            var savedFiles = new List<string>();

            if (articulos == null || articulos.Count == 0) return savedFiles;

            string formattedInforme = (numInforme ?? "").Trim();

            if (!formattedInforme.StartsWith("NC-", StringComparison.OrdinalIgnoreCase))

            {

                formattedInforme = "NC-" + formattedInforme.PadLeft(6, '0');

            }

            string[] targetDirectories = new string[]

            {

                @"\\192.168.1.36\d$\htdocs\app\noconfornidad",

                @"D:\htdocs\app\noconfornidad"

            };

            foreach (var art in articulos)

            {

                string codTela = (art.Cod_Tela ?? "").Trim();

                if (string.IsNullOrEmpty(codTela)) codTela = "TELA";

                if (art.Defectos != null && art.Defectos.Count > 0)

                {

                    foreach (var def in art.Defectos)

                    {

                        string codMotivo = (def.Cod_Motivo ?? "").Trim();

                        if (string.IsNullOrEmpty(codMotivo)) codMotivo = "DEF";

                        if (def.FotosBase64 != null && def.FotosBase64.Count > 0)

                        {

                            for (int i = 0; i < def.FotosBase64.Count; i++)

                            {

                                var rawBase64 = def.FotosBase64[i];

                                if (string.IsNullOrWhiteSpace(rawBase64)) continue;

                                string extension = ".jpg";

                                if (rawBase64.Contains("image/png")) extension = ".png";

                                else if (rawBase64.Contains("image/webp")) extension = ".webp";

                                string cleanBase64 = rawBase64;

                                int commaIdx = cleanBase64.IndexOf(',');

                                if (commaIdx >= 0) cleanBase64 = cleanBase64.Substring(commaIdx + 1);

                                byte[] imageBytes;

                                try { imageBytes = Convert.FromBase64String(cleanBase64); }

                                catch { continue; }

                                string fileName = i == 0

                                    ? $"{formattedInforme}-{codTela}-{codMotivo}{extension}"

                                    : $"{formattedInforme}-{codTela}-{codMotivo}_{i}{extension}";

                                if (def.NombresFotos == null) def.NombresFotos = new List<string>();

                                def.NombresFotos.Add(fileName);

                                savedFiles.Add(fileName);

                                foreach (var dir in targetDirectories)

                                {

                                    try

                                    {

                                        Directory.CreateDirectory(dir);

                                        string destPath = Path.Combine(dir, fileName);

                                        File.WriteAllBytes(destPath, imageBytes);

                                        _logger.LogInformation("Imagen guardada exitosamente: {Path}", destPath);

                                    }

                                    catch (Exception ex)

                                    {

                                        _logger.LogWarning(ex, "No se pudo guardar imagen en {Dir}", dir);

                                    }

                                }

                            }

                        }

                    }

                }

            }

            return savedFiles;

        }

        private async Task<List<Dictionary<string, object>>> ExecuteReaderToDictListAsync(string spName, Action<SqlCommand> addParams)

        {

            var result = new List<Dictionary<string, object>>();

            using (var con = new SqlConnection(_cn))

            using (var cmd = new SqlCommand(spName, con))

            {

                cmd.CommandType = CommandType.StoredProcedure;

                addParams?.Invoke(cmd);

                await con.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())

                {

                    while (await reader.ReadAsync())

                    {

                        var row = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

                        for (int i = 0; i < reader.FieldCount; i++)

                        {

                            row[reader.GetName(i)] = await reader.IsDBNullAsync(i) ? null! : reader.GetValue(i);

                        }

                        result.Add(row);

                    }

                }

            }

            return result;

        }

    }

}



using System.Data;
using System.Data.SqlClient;
using ic.backend.precotex.web.Data.Repositories.Implementation.Calidad;
using ic.backend.precotex.web.Entity.Entities.Calidad;
using Microsoft.Extensions.Configuration;

namespace ic.backend.precotex.web.Data.Repositories.Calidad
{
    public class NoConformidadesRepository : INoConformidadesRepository
    {
        private readonly string _cn;

        public NoConformidadesRepository(IConfiguration config)
        {
            _cn = config.GetConnectionString("TextilConnection")!;
        }

        public async Task<List<Dictionary<string, object>>> ListarDatosInformeCalidad(string tipo, string cod = "")
        {
            return await ExecuteReaderToDictListAsync("UP_CC_Listar_Datos_Informe_Calidad", cmd =>
            {
                cmd.Parameters.AddWithValue("@Tipo", string.IsNullOrWhiteSpace(tipo) ? "" : tipo.Trim());
            });
        }

        public async Task<List<NoConformidades>> MostrarCabecera(string? numInforme, string? fIni, string? fFin, string? partida)
        {
            var lista = new List<NoConformidades>();

            using (var cn = new SqlConnection(_cn))
            {
                await cn.OpenAsync();
                using (var cmd = new SqlCommand("UP_CC_Muestra_Informe_Calidad_Cabecera", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros exactos del procedimiento almacenado:
                    cmd.Parameters.AddWithValue("@Cod_OrdTra", string.IsNullOrWhiteSpace(partida) ? "" : partida.Trim());
                    cmd.Parameters.AddWithValue("@Fec_Inicio", string.IsNullOrWhiteSpace(fIni) ? "01/01/2020" : fIni.Trim());
                    cmd.Parameters.AddWithValue("@Fec_Fin", string.IsNullOrWhiteSpace(fFin) ? DateTime.Now.ToString("dd/MM/yyyy") : fFin.Trim());
                    cmd.Parameters.AddWithValue("@Cod_Usuario", "");

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lista.Add(new NoConformidades
                            {
                                Numero = reader["Numero"] != DBNull.Value ? reader["Numero"].ToString() : "",
                                NumeroNC = reader["NumeroNC"] != DBNull.Value ? reader["NumeroNC"].ToString() : "",
                                Partida = reader["Partida"] != DBNull.Value ? reader["Partida"].ToString() : "",
                                Fecha = reader["Fecha"] != DBNull.Value ? reader["Fecha"].ToString() : "",
                                CodCliente = reader["CodCliente"] != DBNull.Value ? reader["CodCliente"].ToString() : "",
                                Cliente = reader["Cliente"] != DBNull.Value ? reader["Cliente"].ToString() : "",
                                CodColor = reader["CodColor"] != DBNull.Value ? reader["CodColor"].ToString() : "",
                                Color = reader["Color"] != DBNull.Value ? reader["Color"].ToString() : "",
                                Area = reader["Area"] != DBNull.Value ? reader["Area"].ToString() : "",
                                Usuario = reader["Usuario"] != DBNull.Value ? reader["Usuario"].ToString() : "",
                                Responsable = reader["Responsable"] != DBNull.Value ? reader["Responsable"].ToString() : ""
                            });
                        }
                    }
                }
            }

            return lista;
        }

        public async Task<List<Dictionary<string, object>>> MostrarPartida(string partida, string tipo = "")
        {
            return await ExecuteReaderToDictListAsync("UP_CC_Mostrar_Partida", cmd =>
            {
                cmd.Parameters.AddWithValue("@COD_ORDTRA", string.IsNullOrWhiteSpace(partida) ? "" : partida.Trim());
            });
        }
        public async Task<List<Dictionary<string, object>>> MostrarDetalle(string numInforme = "", string partida = "")
        {
            if (!string.IsNullOrWhiteSpace(numInforme))
            {
                return await ExecuteReaderToDictListAsync("UP_CC_Muestra_Informe_Calidad_Detalle_Afectados", cmd =>
                {
                    cmd.Parameters.AddWithValue("@Num_Informe", numInforme.Trim());
                });
            }
            return await ExecuteReaderToDictListAsync("UP_CC_Muestra_Informe_Calidad_Detalle", cmd =>
            {
                cmd.Parameters.AddWithValue("@Num_Informe", numInforme ?? "");
                cmd.Parameters.AddWithValue("@Cod_OrdTra", partida ?? "");
            });
        }

        public async Task<List<Dictionary<string, object>>> MostrarDetalleMotivo(string numInforme, string partida = "")
        {
            return await ExecuteReaderToDictListAsync("UP_CC_Muestra_Informe_Calidad_Detalle_Motivo", cmd =>
            {
                cmd.Parameters.AddWithValue("@Num_Informe", numInforme ?? "");
                cmd.Parameters.AddWithValue("@Cod_OrdTra", partida ?? "");
            });
        }

        public async Task<List<Dictionary<string, object>>> MostrarEvolutivo()
        {
            return await ExecuteReaderToDictListAsync("UP_CC_Muestra_Informe_Calidad_Evolutivo", null);
        }

        public async Task<List<Dictionary<string, object>>> ReporteNoConformidad(string fIni = "", string fFin = "")
        {
            DateTime dIni;
            DateTime dFin;

            if (!DateTime.TryParseExact(fIni, new[] { "dd/MM/yyyy", "yyyy-MM-dd", "d/M/yyyy" }, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dIni))
            {
                dIni = DateTime.Now.AddDays(-30);
            }

            if (!DateTime.TryParseExact(fFin, new[] { "dd/MM/yyyy", "yyyy-MM-dd", "d/M/yyyy" }, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dFin))
            {
                dFin = DateTime.Now;
            }

            if (dIni < new DateTime(2018, 11, 19))
            {
                dIni = new DateTime(2018, 11, 19);
            }

            if (dFin > DateTime.Now)
            {
                dFin = DateTime.Now;
            }

            if (dIni > dFin)
            {
                dIni = dFin.AddDays(-30);
            }

            return await ExecuteReaderToDictListAsync("UP_CC_Reporte_Informe_No_Conformidad", cmd =>
            {
                cmd.Parameters.Add("@Fec_Ini", SqlDbType.Date).Value = dIni.Date;
                cmd.Parameters.Add("@Fec_Fin", SqlDbType.Date).Value = dFin.Date;
            });
        }

        // ============================================================================
        // TRANSACCIÓN COMPLETA (3 NIVELES: CABECERA -> DETALLE 'U' -> MOTIVOS 'I')
        // ============================================================================
        public async Task<ResponseResultado> GuardarTransaccionCompleta(InformeGuardarRequest req)
        {
            using (var con = new SqlConnection(_cn))
            {
                await con.OpenAsync();
                using (var tran = con.BeginTransaction())
                {
                    try
                    {
                        string numInformeGenerado = req.Num_Informe ?? "";

                        // 1. CABECERA: Inserta cabecera y crea automáticamente todos los ítems de la partida con 0 rollos rechazados
                        using (var cmd = new SqlCommand("UP_Man_Informe_No_Conformidad_Cabecera", con, tran))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Accion", string.IsNullOrEmpty(req.Accion) ? "I" : req.Accion);

                            var pNumInforme = new SqlParameter("@Num_Informe", SqlDbType.Char, 10)
                            {
                                Direction = ParameterDirection.InputOutput,
                                Value = string.IsNullOrWhiteSpace(req.Num_Informe) ? (object)DBNull.Value : req.Num_Informe.Trim()
                            };
                            cmd.Parameters.Add(pNumInforme);

                            cmd.Parameters.AddWithValue("@Cod_OrdTra", (req.Cod_OrdPro ?? "").Trim());
                            cmd.Parameters.AddWithValue("@CC_Usu_Crea", string.IsNullOrWhiteSpace(req.Cod_Usuario) ? "SISTEMAS" : req.Cod_Usuario.Trim());
                            cmd.Parameters.AddWithValue("@Motivo_Anula", req.Observacion ?? "");

                            var pCodigo = new SqlParameter("@Codigo", SqlDbType.Int) { Direction = ParameterDirection.Output };
                            var pMsj = new SqlParameter("@sMsj", SqlDbType.VarChar, 255) { Direction = ParameterDirection.Output };
                            cmd.Parameters.Add(pCodigo);
                            cmd.Parameters.Add(pMsj);

                            await cmd.ExecuteNonQueryAsync();

                            int codCab = pCodigo.Value != null && pCodigo.Value != DBNull.Value ? Convert.ToInt32(pCodigo.Value) : 0;
                            string msjCab = pMsj.Value != null && pMsj.Value != DBNull.Value ? pMsj.Value.ToString()! : "";

                            if (codCab == 0)
                            {
                                throw new Exception($"Error al registrar cabecera: {msjCab}");
                            }

                            if (pNumInforme.Value != null && pNumInforme.Value != DBNull.Value)
                            {
                                numInformeGenerado = pNumInforme.Value.ToString()!.Trim();
                            }
                        }

                        // 2. DETALLE: Actualiza ('U') solo el ítem o ítems seleccionados con sus rollos rechazados
                        if (req.Articulos != null && req.Articulos.Count > 0)
                        {
                            foreach (var art in req.Articulos)
                            {
                                int numSecuencia = 1;
                                if (!int.TryParse(art.Item, out numSecuencia))
                                {
                                    numSecuencia = 1;
                                }

                                using (var cmdArt = new SqlCommand("UP_Man_Informe_No_Conformidad_Detalle", con, tran))
                                {
                                    cmdArt.CommandType = CommandType.StoredProcedure;
                                    cmdArt.Parameters.AddWithValue("@ACCION", "U");
                                    cmdArt.Parameters.AddWithValue("@Num_Informe", numInformeGenerado);
                                    cmdArt.Parameters.AddWithValue("@Cod_OrdTra", (req.Cod_OrdPro ?? "").Trim());
                                    cmdArt.Parameters.AddWithValue("@Num_Secuencia", numSecuencia);
                                    cmdArt.Parameters.AddWithValue("@Rollos_Rechazados", art.Cant_Rollos_Rech);

                                    var pCodArt = new SqlParameter("@Codigo", SqlDbType.Int) { Direction = ParameterDirection.Output };
                                    var pMsjArt = new SqlParameter("@sMsj", SqlDbType.VarChar, 255) { Direction = ParameterDirection.Output };
                                    cmdArt.Parameters.Add(pCodArt);
                                    cmdArt.Parameters.Add(pMsjArt);

                                    await cmdArt.ExecuteNonQueryAsync();

                                    int codDet = pCodArt.Value != null && pCodArt.Value != DBNull.Value ? Convert.ToInt32(pCodArt.Value) : 0;
                                    string msjDet = pMsjArt.Value != null && pMsjArt.Value != DBNull.Value ? pMsjArt.Value.ToString()! : "";

                                    if (codDet == 0)
                                    {
                                        throw new Exception($"Error al actualizar detalle (secuencia {numSecuencia}): {msjDet}");
                                    }
                                }

                                // 3. MOTIVOS: Inserta ('I') los motivos/defectos registrados para cada ítem
                                if (art.Defectos != null && art.Defectos.Count > 0)
                                {
                                    foreach (var def in art.Defectos)
                                    {
                                        string codAreaFinal = (def.Cod_Area ?? "").Trim();
                                        // Asegurar que el código de área sea el código corto de CC_Areas (ej. ACA, HIL, TEJ, TIN)
                                        if (codAreaFinal.Length > 3)
                                        {
                                            using (var cmdArea = new SqlCommand("SELECT TOP 1 Cod_Area_CC FROM CC_Areas WHERE Descripcion = @Nom OR Cod_Area_CC = @Nom", con, tran))
                                            {
                                                cmdArea.Parameters.AddWithValue("@Nom", codAreaFinal);
                                                var valArea = await cmdArea.ExecuteScalarAsync();
                                                if (valArea != null && valArea != DBNull.Value)
                                                {
                                                    codAreaFinal = valArea.ToString().Trim();
                                                }
                                            }
                                        }

                                        using (var cmdDef = new SqlCommand("UP_Man_Informe_No_Conformidad_Motivo", con, tran))
                                        {
                                            cmdDef.CommandType = CommandType.StoredProcedure;
                                            cmdDef.Parameters.AddWithValue("@ACCION", "I");
                                            cmdDef.Parameters.AddWithValue("@Num_Informe", numInformeGenerado);
                                            cmdDef.Parameters.AddWithValue("@Cod_OrdTra", (req.Cod_OrdPro ?? "").Trim());
                                            cmdDef.Parameters.AddWithValue("@Num_Secuencia", numSecuencia);
                                            cmdDef.Parameters.AddWithValue("@Cod_Area_CC", codAreaFinal);
                                            cmdDef.Parameters.AddWithValue("@Cod_Motivo", (def.Cod_Motivo ?? "").Trim());
                                            cmdDef.Parameters.AddWithValue("@Cod_Motivo_Ant", "");
                                            cmdDef.Parameters.AddWithValue("@CC_Observacion", def.Observacion ?? "");
                                            cmdDef.Parameters.AddWithValue("@Usuario", string.IsNullOrWhiteSpace(req.Cod_Usuario) ? "SISTEMAS" : req.Cod_Usuario.Trim());

                                            var pCodDef = new SqlParameter("@Codigo", SqlDbType.Int) { Direction = ParameterDirection.Output };
                                            var pMsjDef = new SqlParameter("@sMsj", SqlDbType.VarChar, 255) { Direction = ParameterDirection.Output };
                                            cmdDef.Parameters.Add(pCodDef);
                                            cmdDef.Parameters.Add(pMsjDef);

                                            await cmdDef.ExecuteNonQueryAsync();

                                            int codMot = pCodDef.Value != null && pCodDef.Value != DBNull.Value ? Convert.ToInt32(pCodDef.Value) : 0;
                                            string msjMot = pMsjDef.Value != null && pMsjDef.Value != DBNull.Value ? pMsjDef.Value.ToString()! : "";

                                            if (codMot == 0)
                                            {
                                                throw new Exception($"Error al registrar motivo ({def.Cod_Motivo}): {msjMot}");
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        tran.Commit();

                        return new ResponseResultado
                        {
                            Success = true,
                            Message = "Informe registrado y procesado exitosamente.",
                            Num_Informe = numInformeGenerado
                        };
                    }
                    catch (Exception ex)
                    {
                        try { tran.Rollback(); } catch { }
                        throw;
                    }
                }
            }
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
                            row[reader.GetName(i)] = await reader.IsDBNullAsync(i) ? null : reader.GetValue(i);
                        }
                        result.Add(row);
                    }
                }
            }
            return result;
        }
    }
}
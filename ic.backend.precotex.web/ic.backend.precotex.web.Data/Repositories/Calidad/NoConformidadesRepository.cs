using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ic.backend.precotex.web.Entity.Entities.Calidad;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ic.backend.precotex.web.Data.Repositories.Calidad
{
    public class NoConformidadesRepository
    {
        private readonly string _cn;
        private readonly ILogger<NoConformidadesRepository> _logger;

        public NoConformidadesRepository(IConfiguration config, ILogger<NoConformidadesRepository> logger)
        {
            _logger = logger;
            _cn = config.GetConnectionString("TextilConnection")
               ?? config.GetConnectionString("DefaultConnection")
               ?? "";
        }

        public List<Dictionary<string, object>> ListarDatosInformeCalidad(string tipo, string cod = "")
        {
            return ExecuteReaderToDictList("UP_CC_Listar_Datos_Informe_Calidad", cmd =>
            {
                cmd.Parameters.AddWithValue("@Tipo", tipo ?? "");
                cmd.Parameters.AddWithValue("@Cod", cod ?? "");
            });
        }

        public List<Dictionary<string, object>> MostrarCabecera(string numInforme = "", string fIni = "", string fFin = "", string partida = "")
        {
            // Limpieza automática por si Swagger envía el nombre del parámetro como texto
            if (numInforme == "numInforme") numInforme = "";
            if (fIni == "fIni") fIni = "";
            if (fFin == "fFin") fFin = "";
            if (partida == "partida") partida = "";
            return ExecuteReaderToDictList("UP_CC_Muestra_Informe_Calidad_Cabecera", cmd =>
            {
                cmd.Parameters.AddWithValue("@Num_Informe", string.IsNullOrWhiteSpace(numInforme) ? "" : numInforme.Trim());
                cmd.Parameters.AddWithValue("@Fec_Ini", string.IsNullOrWhiteSpace(fIni) ? "" : fIni.Trim());
                cmd.Parameters.AddWithValue("@Fec_Fin", string.IsNullOrWhiteSpace(fFin) ? "" : fFin.Trim());
                cmd.Parameters.AddWithValue("@Partida", string.IsNullOrWhiteSpace(partida) ? "" : partida.Trim());
            });
        }

        public List<Dictionary<string, object>> MostrarPartida(string partida, string tipo = "")
        {
            return ExecuteReaderToDictList("UP_CC_Mostrar_Partida", cmd =>
            {
                cmd.Parameters.AddWithValue("@Partida", partida ?? "");
                cmd.Parameters.AddWithValue("@Tipo", tipo ?? "");
            });
        }

        public List<Dictionary<string, object>> MostrarDetalle(string numInforme = "", string partida = "")
        {
            return ExecuteReaderToDictList("UP_CC_Muestra_Informe_Calidad_Detalle", cmd =>
            {
                cmd.Parameters.AddWithValue("@Num_Informe", numInforme ?? "");
                cmd.Parameters.AddWithValue("@Partida", partida ?? "");
            });
        }

        public List<Dictionary<string, object>> MostrarDetalleMotivo(string numInforme, string partida = "")
        {
            return ExecuteReaderToDictList("UP_CC_Muestra_Informe_Calidad_Detalle_Motivo", cmd =>
            {
                cmd.Parameters.AddWithValue("@Num_Informe", numInforme ?? "");
                cmd.Parameters.AddWithValue("@Partida", partida ?? "");
            });
        }

        // ============================================================================
        // TRANSACCIÓN COMPLETA DE GUARDADO (3 NIVELES)
        // ============================================================================
        public ResponseResultado GuardarTransaccionCompleta(InformeGuardarRequest req)
        {
            using (var con = new SqlConnection(_cn))
            {
                con.Open();
                using (var tran = con.BeginTransaction())
                {
                    try
                    {
                        string numInformeGenerado = req.Num_Informe;

                        // 1. Guardar Cabecera
                        using (var cmd = new SqlCommand("UP_Man_Informe_No_Conformidad_Cabecera", con, tran))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Accion", req.Accion);
                            cmd.Parameters.AddWithValue("@Num_Informe", req.Num_Informe ?? "");
                            cmd.Parameters.AddWithValue("@Cod_OrdPro", req.Cod_OrdPro ?? "");
                            cmd.Parameters.AddWithValue("@Cod_Cli", req.Cod_Cli ?? "");
                            cmd.Parameters.AddWithValue("@Nom_Cli", req.Nom_Cli ?? "");
                            cmd.Parameters.AddWithValue("@Cod_Color", req.Cod_Color ?? "");
                            cmd.Parameters.AddWithValue("@Color", req.Color ?? "");
                            cmd.Parameters.AddWithValue("@Kg_Total", req.Kg_Total ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Observacion", req.Observacion ?? "");
                            cmd.Parameters.AddWithValue("@Motivo_Anulacion", req.Motivo_Anulacion ?? "");
                            cmd.Parameters.AddWithValue("@Cod_Usuario", req.Cod_Usuario ?? "");

                            var pOut = new SqlParameter("@Num_Informe_Out", SqlDbType.VarChar, 20)
                            {
                                Direction = ParameterDirection.Output
                            };
                            cmd.Parameters.Add(pOut);

                            cmd.ExecuteNonQuery();

                            if (pOut.Value != null && pOut.Value != DBNull.Value)
                            {
                                numInformeGenerado = pOut.Value.ToString();
                            }
                        }

                        // Si no es anulación, guardar detalles y motivos
                        if (req.Accion != "D" && req.Articulos != null)
                        {
                            int itemIndex = 1;
                            foreach (var art in req.Articulos)
                            {
                                using (var cmdArt = new SqlCommand("UP_Man_Informe_No_Conformidad_Detalle", con, tran))
                                {
                                    cmdArt.CommandType = CommandType.StoredProcedure;
                                    cmdArt.Parameters.AddWithValue("@Accion", art.Accion ?? req.Accion);
                                    cmdArt.Parameters.AddWithValue("@Num_Informe", numInformeGenerado);
                                    cmdArt.Parameters.AddWithValue("@Item", itemIndex);
                                    cmdArt.Parameters.AddWithValue("@Cod_Tela", art.Cod_Tela ?? "");
                                    cmdArt.Parameters.AddWithValue("@Nom_Tela", art.Nom_Tela ?? "");
                                    cmdArt.Parameters.AddWithValue("@Talla", art.Talla ?? "-");
                                    cmdArt.Parameters.AddWithValue("@Cant_Rollos_Asig", art.Cant_Rollos_Asig);
                                    cmdArt.Parameters.AddWithValue("@Cant_Rollos_Rech", art.Cant_Rollos_Rech);
                                    cmdArt.Parameters.AddWithValue("@Kg_Afectados", art.Kg_Afectados);
                                    cmdArt.Parameters.AddWithValue("@Cod_Usuario", req.Cod_Usuario ?? "");
                                    cmdArt.ExecuteNonQuery();
                                }

                                if (art.Defectos != null)
                                {
                                    int motivoIndex = 1;
                                    foreach (var def in art.Defectos)
                                    {
                                        using (var cmdDef = new SqlCommand("UP_Man_Informe_No_Conformidad_Motivo", con, tran))
                                        {
                                            cmdDef.CommandType = CommandType.StoredProcedure;
                                            cmdDef.Parameters.AddWithValue("@Accion", def.Accion ?? req.Accion);
                                            cmdDef.Parameters.AddWithValue("@Num_Informe", numInformeGenerado);
                                            cmdDef.Parameters.AddWithValue("@Item", itemIndex);
                                            cmdDef.Parameters.AddWithValue("@Item_Motivo", motivoIndex);
                                            cmdDef.Parameters.AddWithValue("@Cod_Motivo", def.Cod_Motivo ?? "");
                                            cmdDef.Parameters.AddWithValue("@Nom_Motivo", def.Nom_Motivo ?? "");
                                            cmdDef.Parameters.AddWithValue("@Cod_Area", def.Cod_Area ?? "");
                                            cmdDef.Parameters.AddWithValue("@Nom_Area", def.Nom_Area ?? "");
                                            cmdDef.Parameters.AddWithValue("@Observacion", def.Observacion_Defecto ?? "");
                                            cmdDef.Parameters.AddWithValue("@Cod_Usuario", req.Cod_Usuario ?? "");
                                            cmdDef.ExecuteNonQuery();
                                        }
                                        motivoIndex++;
                                    }
                                }
                                itemIndex++;
                            }
                        }

                        tran.Commit();
                        return new ResponseResultado
                        {
                            Success = true,
                            Message = "Informe procesado exitosamente.",
                            Num_Informe = numInformeGenerado
                        };
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        _logger.LogError(ex, "Error en GuardarTransaccionCompleta");
                        throw;
                    }
                }
            }
        }

        private List<Dictionary<string, object>> ExecuteReaderToDictList(string spName, Action<SqlCommand> addParams)
        {
            var result = new List<Dictionary<string, object>>();
            using (var con = new SqlConnection(_cn))
            using (var cmd = new SqlCommand(spName, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                addParams?.Invoke(cmd);
                con.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var row = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                        }
                        result.Add(row);
                    }
                }
            }
            return result;
        }
    }
}

using Dapper;
using ic.backend.precotex.web.Data.Repositories.Implementation.SecureNorm;
using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.SecureNorm
{
    public class SNProveedorRepository : ISNProveedorRepository
    {
        private readonly string _connectionString;

        public SNProveedorRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnectionSomma") 
                             ?? configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<IEnumerable<SN_Proveedor>?> Listado(string sFiltro)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    sFiltro = sFiltro ?? ""
                };

                var result = await connection.QueryAsync<SN_Proveedor>(
                    "[dbo].[SP_SN_PROVEEDORES_LISTAR]",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

                return result;
            }
        }

        public async Task<(int Codigo, string Mensaje)> Mnto(SN_Proveedor sN_Proveedor, string sTipoTransac)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();

                parametros.Add("@Accion", sTipoTransac ?? "I");
                parametros.Add("@Id", sN_Proveedor.Id);
                parametros.Add("@Razon", sN_Proveedor.Razon ?? "");
                parametros.Add("@Ruc", sN_Proveedor.Ruc ?? "");
                parametros.Add("@Tipo", sN_Proveedor.Tipo ?? "");
                parametros.Add("@Proceso", sN_Proveedor.Proceso ?? "");
                parametros.Add("@Contacto", sN_Proveedor.Contacto ?? "");
                parametros.Add("@Homologacion", sN_Proveedor.Homologacion ?? "");
                parametros.Add("@Desempeno", sN_Proveedor.Desempeno ?? "");

                DateTime dtTemp;
                parametros.Add("@Evaluacion", (!string.IsNullOrWhiteSpace(sN_Proveedor.Evaluacion) && DateTime.TryParse(sN_Proveedor.Evaluacion, out dtTemp)) ? (object)dtTemp.Date : DBNull.Value);
                parametros.Add("@Reeval", (!string.IsNullOrWhiteSpace(sN_Proveedor.Reeval) && DateTime.TryParse(sN_Proveedor.Reeval, out dtTemp)) ? (object)dtTemp.Date : DBNull.Value);
                parametros.Add("@Sctr", (!string.IsNullOrWhiteSpace(sN_Proveedor.Sctr) && DateTime.TryParse(sN_Proveedor.Sctr, out dtTemp)) ? (object)dtTemp.Date : DBNull.Value);
                parametros.Add("@Induccion", (!string.IsNullOrWhiteSpace(sN_Proveedor.Induccion) && DateTime.TryParse(sN_Proveedor.Induccion, out dtTemp)) ? (object)dtTemp.Date : DBNull.Value);
                parametros.Add("@Iperc", (!string.IsNullOrWhiteSpace(sN_Proveedor.Iperc) && DateTime.TryParse(sN_Proveedor.Iperc, out dtTemp)) ? (object)dtTemp.Date : DBNull.Value);
                parametros.Add("@Seguro", (!string.IsNullOrWhiteSpace(sN_Proveedor.Seguro) && DateTime.TryParse(sN_Proveedor.Seguro, out dtTemp)) ? (object)dtTemp.Date : DBNull.Value);

                try
                {
                    var result = await connection.QueryFirstOrDefaultAsync<dynamic>(
                        "[dbo].[SP_SN_PROVEEDORES_MANTO]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                    if (result != null)
                    {
                        int idGenerado = Convert.ToInt32(result.id ?? 0);
                        string mensaje = result.mensaje?.ToString() ?? "OperaciÃ³n completada exitosamente.";
                        return (idGenerado, mensaje);
                    }

                    return (0, "Error desconocido al ejecutar mantenimiento de proveedor.");
                }
                catch (Exception ex)
                {
                    return (0, ex.Message);
                }
            }
        }
    }
}
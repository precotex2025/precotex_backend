using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ic.backend.precotex.web.Data.Repositories.Implementation.CorteEncogimiento;
using ic.backend.precotex.web.Entity.Entities;
using Microsoft.Extensions.Configuration;

namespace ic.backend.precotex.web.Data.Repositories.CorteEncogimiento
{
    public class DCorteEncogimiento : ICorteEncogimiento
    {
        private readonly string _connectionString;
        public DCorteEncogimiento(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnection")!;
        }

        public async Task<IEnumerable<E_Corte_Encogimiento>?> ListaCorteEncogimiento()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var corteEncogimiento = await connection.QueryAsync<E_Corte_Encogimiento>(
                         "[dbo].[SP_Listar_Corte_Encogimiento]",
                         commandType: System.Data.CommandType.StoredProcedure
                     );

                    return corteEncogimiento;
                }
            }
            catch (SqlException sqlEx) // Captura errores de SQL Server
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw; // Relanza la excepción si es necesario
            }
        }

        public async Task<IEnumerable<E_Corte_Encogimiento>?> InsertCorteEncogimiento(string pTipo, string? pCod_Ordtra)
        {
            try
            {

                using (var connection = new SqlConnection(_connectionString))
                {
                    var corteEncogimiento = await connection.QueryAsync<E_Corte_Encogimiento>(
                        "[dbo].[SP_Insert_Corte_Encogimiento]",
                        new { pTipo = pTipo, pCod_Ordtra = pCod_Ordtra },
                        commandType: System.Data.CommandType.StoredProcedure
                    );

                    return corteEncogimiento;
                }
            }
            catch (SqlException sqlEx) // Captura errores de SQL Server
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");

                throw; // Relanza la excepción si es necesario
            }
            
        }

        public async Task<IEnumerable<E_Corte_Encogimiento>?> ListCorteEncogimientoDet(string pTipo, string? pNum_Secuencia, string? pCodPartida, decimal? pAncho_Antes_Lav, decimal? pAlto_Antes_Lav, decimal? pAncho_Despues_Lav, decimal? pAlto_Despues_Lav, decimal? pSesgadura)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var corteEncogimiento = await connection.QueryAsync<E_Corte_Encogimiento>(
                        "[dbo].[SP_List_Corte_Encogimiento_Det]",
                        new { pTipo = pTipo, pNum_Secuencia = pNum_Secuencia, pCodPartida = pCodPartida, pAncho_Antes_Lav = pAncho_Antes_Lav, pAlto_Antes_Lav = pAlto_Antes_Lav , pAncho_Despues_Lav = pAncho_Despues_Lav, pAlto_Despues_Lav  = pAlto_Despues_Lav, pSesgadura = pSesgadura },
                        commandType: System.Data.CommandType.StoredProcedure
                    );

                    return corteEncogimiento;
                }
            }
            catch (SqlException sqlEx) // Captura errores de SQL Server
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw; // Relanza la excepción si es necesario
            }

        }

        public async Task<IEnumerable<E_Corte_Encogimiento>?> BuscarCorteEncogimiento(string pCod_Ordtra)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var corteEncogimiento = await connection.QueryAsync<E_Corte_Encogimiento>(
                        "[dbo].[SP_Buscar_Corte_Encogimiento]",
                        new { pCod_Ordtra = pCod_Ordtra },
                        commandType: System.Data.CommandType.StoredProcedure
                    );

                    return corteEncogimiento;
                }
            }
            catch (SqlException sqlEx) // Captura errores de SQL Server
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw; // Relanza la excepción si es necesario
            }

        }

        public async Task<IEnumerable<E_Corte_Encogimiento>?> UpdateMedidasTablaMaestra(List<E_Corte_Encogimiento> pData)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    // Convertir List<E_Corte_Encogimiento> a DataTable
                    var dataTable = new DataTable();
                    dataTable.Columns.Add("Cod_Partida", typeof(string));
                    dataTable.Columns.Add("Num_Secuencia", typeof(int));

                    foreach (var item in pData)
                    {
                        dataTable.Rows.Add(
                            item.Cod_Partida,
                            item.Num_Secuencia      
                        );
                    }

                    var parameters = new DynamicParameters();
                    parameters.Add("@pData", dataTable.AsTableValuedParameter("dbo.TipoCorteEncogimiento"));

                    var result = await connection.QueryAsync<E_Corte_Encogimiento>(
                        "[dbo].[SP_Update_Medidas_TablaMaestra]",
                        parameters,
                        commandType: System.Data.CommandType.StoredProcedure
                    );

                    return result.ToList();
                }
            }
            catch (SqlException sqlEx) // Captura errores de SQL Server
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw; // Relanza la excepción si es necesario
            }

        }


        public async Task<IEnumerable<E_Corte_Encogimiento>?> BuscarUsuario(string pUsuario)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var corteEncogimiento = await connection.QueryAsync<E_Corte_Encogimiento>(
                        "[dbo].[SP_Buscar_Usuario_Corte_Encogimiento]",
                        new { pUsuario = pUsuario },
                        commandType: System.Data.CommandType.StoredProcedure
                    );

                    return corteEncogimiento;
                }
            }
            catch (SqlException sqlEx) // Captura errores de SQL Server
            {
                Console.WriteLine($"Error de SQL Server: {sqlEx.Message}");
                throw; // Relanza la excepción si es necesario
            }

        }

    }
}

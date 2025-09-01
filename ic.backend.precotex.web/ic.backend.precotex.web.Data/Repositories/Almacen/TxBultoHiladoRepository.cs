using ic.backend.precotex.web.Data.Repositories.Implementation.Almacen;
using ic.backend.precotex.web.Entity.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace ic.backend.precotex.web.Data.Repositories.Almacen
{
    public class TxBultoHiladoRepository : ITxBultoHiladoRepository
    {
        private readonly string _connectionString;

        public TxBultoHiladoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnection")!;
        }

        public async Task<IEnumerable<Tx_Bulto_Hilado>?> ListaBultosUbicacion(string sCodProveedor, string sCodOrdProv, string sNumSemana, string sNomConera)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new
                {
                    Cod_Proveedor = sCodProveedor ?? (object)DBNull.Value,  // Si el parámetro es null, lo asignamos como DBNull
                    Cod_OrdProv = sCodOrdProv ?? (object)DBNull.Value,
                    Num_Semana = sNumSemana ?? (object)DBNull.Value,
                    Nom_Conera = sNomConera ?? (object)DBNull.Value
                };

                var bultoUbicacion = await connection.QueryAsync<Tx_Bulto_Hilado>(
                     "[dbo].[Tx_Listar_Bulto_Hilado_Grupo_Ubicacion]",
                     new { Cod_Proveedor = sCodProveedor, Cod_OrdProv = sCodOrdProv, Num_Semana = sNumSemana == null ? "" : sNumSemana, Nom_Conera = sNomConera == null ? "" : sNomConera },
                     commandType: System.Data.CommandType.StoredProcedure
                 );

                return bultoUbicacion;
            }
        }

        public async Task<IEnumerable<Lg_Proveedor>?> ListaProveedores()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var proveedores = await connection.QueryAsync<Lg_Proveedor>(
                     "[dbo].[Tx_Listar_Proveedores]",
                     commandType: System.Data.CommandType.StoredProcedure
                 );

                return proveedores;
            }
        }
    }
}

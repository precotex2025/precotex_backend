using Dapper;
using ic.backend.precotex.web.Data.Repositories.Implementation.Tejeduria;
using ic.backend.precotex.web.Entity.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ic.backend.precotex.web.Data.Repositories.Tejeduria
{
    public class TxCtrolInventarioHiloTejeduriaRepository : ITxCtrolInventarioHiloTejeduriaRepository
    {

        private readonly string _connectionString;

        public TxCtrolInventarioHiloTejeduriaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnection")!;
        }

        public async Task<(int Codigo, string Mensaje)> CrudCtrolInventarioHiloTejeduria(Tx_Ctrol_Inventario_Hilo_Tejeduria tx_Ctrol_Inventario_Hilo_Tejeduria, string sTipoTransac)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();
                parametros.Add("@Tipo", sTipoTransac);
                parametros.Add("@Lote", tx_Ctrol_Inventario_Hilo_Tejeduria.Lote);
                parametros.Add("@Num_Semana", tx_Ctrol_Inventario_Hilo_Tejeduria.Num_Semana);
                parametros.Add("@Titulo", tx_Ctrol_Inventario_Hilo_Tejeduria.Titulo);
                parametros.Add("@Ser_OrdComp", tx_Ctrol_Inventario_Hilo_Tejeduria.Ser_OrdComp);
                parametros.Add("@Cod_OrdComp", tx_Ctrol_Inventario_Hilo_Tejeduria.Cod_OrdComp);
                parametros.Add("@Color", tx_Ctrol_Inventario_Hilo_Tejeduria.Color);
                parametros.Add("@Hilo_Tipo", tx_Ctrol_Inventario_Hilo_Tejeduria.Hilo_Tipo);
                parametros.Add("@Hilo_Codigo", tx_Ctrol_Inventario_Hilo_Tejeduria.Hilo_Codigo);
                parametros.Add("@Ubicacion", tx_Ctrol_Inventario_Hilo_Tejeduria.Ubicacion);

                parametros.Add("@Cantidad_Cono", tx_Ctrol_Inventario_Hilo_Tejeduria.Cantidad_Cono);
                parametros.Add("@Peso_Tara", tx_Ctrol_Inventario_Hilo_Tejeduria.Peso_Tara);
                parametros.Add("@Peso_Bruto", tx_Ctrol_Inventario_Hilo_Tejeduria.Peso_Bruto);
                parametros.Add("@Peso_Neto", tx_Ctrol_Inventario_Hilo_Tejeduria.Peso_Neto);
                parametros.Add("@Pallet", tx_Ctrol_Inventario_Hilo_Tejeduria.Pallet);
                parametros.Add("@Diferencia", tx_Ctrol_Inventario_Hilo_Tejeduria.Diferencia);

                parametros.Add("@Observacion", tx_Ctrol_Inventario_Hilo_Tejeduria.Observacion);
                parametros.Add("@Proveedor", tx_Ctrol_Inventario_Hilo_Tejeduria.Proveedor);
                parametros.Add("@Cod_Usuario", tx_Ctrol_Inventario_Hilo_Tejeduria.Usu_Registro);

                // Parámetros de salida
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                // Ejecutar el procedimiento almacenado
                connection.Execute(
                    "[dbo].[TX_MANT_CTROL_INVENTARIO_HILO_TEJEDURIA]",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

                //Obtener los valores de salida
                var codigo = parametros.Get<int>("@Codigo");
                var mensaje = parametros.Get<string>("@sMsj");

                return (codigo, mensaje);
            }
        }

        public async Task<IEnumerable<Tx_Ctrol_Inventario_Hilo_Tejeduria>?> ObtenerCtrolInventarioHiloTejeduriaByLote(string? Lote)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Lote = Lote,
                };

                var result = await connection.QueryAsync<Tx_Ctrol_Inventario_Hilo_Tejeduria>(
                     "[dbo].[TX_OBTIENE_CTROL_INVENTARIO_HILO_TEJEDURIA]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }
    }
}

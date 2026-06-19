using Dapper;
using ic.backend.precotex.web.Data.Repositories.Implementation.Tejeduria;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.Memorandum;
using ic.backend.precotex.web.Entity.Entities.Tejeduria;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Tejeduria
{
    public class TjSeguimientoSaldoHiloRepository: ITjSeguimientoSaldoHiloRepository
    {
        private readonly string _connectionString;

        public TjSeguimientoSaldoHiloRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnection")!;
        }

        public async Task<IEnumerable<tj_Muestra_OT_Programada>?> ListaOT_Programada(string Cod_OrdProv, string Cod_HilTel)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Cod_OrdProv = Cod_OrdProv,
                    Cod_HilTel = Cod_HilTel
                };

                var result = await connection.QueryAsync<tj_Muestra_OT_Programada>(
                     "[dbo].[Tj_Muestra_OT_Programadas]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<IEnumerable<tj_Muestra_OT_Terminada>?> ListaOT_Terminada(DateTime Fecha, DateTime Fecha_Fin, string Flg_Pendiente)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    FechaInicio = Fecha,
                    FechaFin = Fecha_Fin,
                    Flg_Pendiente = Flg_Pendiente
                };

                var result = await connection.QueryAsync<tj_Muestra_OT_Terminada>(
                     "[dbo].[Tj_Muestra_OT_Terminadas]"
                     , parametros
                     , commandType: System.Data.CommandType.StoredProcedure
                 );

                return result;
            }
        }

        public async Task<(int Codigo, string Mensaje)> Proceso(tj_seguimiento_saldo_hilo_tela tj_Seguimiento_Saldo_Hilo_Tela, string sTipoTransac)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                // Parametros de SQL
                parametros.Add("@Accion", sTipoTransac);
                parametros.Add("@Num_Traslado", tj_Seguimiento_Saldo_Hilo_Tela.Num_Traslado);
                parametros.Add("@Cod_OrdProv", tj_Seguimiento_Saldo_Hilo_Tela.Cod_OrdProv);
                parametros.Add("@Cod_Ordtra_Ori", tj_Seguimiento_Saldo_Hilo_Tela.Cod_Ordtra_Ori);
                parametros.Add("@Cod_Maquina_Ori", tj_Seguimiento_Saldo_Hilo_Tela.Cod_Maquina_Ori);
                parametros.Add("@Cod_HilTel", tj_Seguimiento_Saldo_Hilo_Tela.Cod_HilTel);
                parametros.Add("@Cod_Color", tj_Seguimiento_Saldo_Hilo_Tela.Cod_Color);
                parametros.Add("@Kg_Programado", tj_Seguimiento_Saldo_Hilo_Tela.Kg_Programado);
                parametros.Add("@Kg_Salida", tj_Seguimiento_Saldo_Hilo_Tela.Kg_Salida);
                parametros.Add("@Kg_Consumo", tj_Seguimiento_Saldo_Hilo_Tela.Kg_Consumo);
                parametros.Add("@Kg_Devolver", tj_Seguimiento_Saldo_Hilo_Tela.Kg_Devolver);
                parametros.Add("@Estado", tj_Seguimiento_Saldo_Hilo_Tela.Estado);
                parametros.Add("@Cod_Ordtra_Des", tj_Seguimiento_Saldo_Hilo_Tela.Cod_Ordtra_Des);
                parametros.Add("@Cod_Maquina_Des", tj_Seguimiento_Saldo_Hilo_Tela.Cod_Maquina_Des);
                parametros.Add("@Cod_Usuario", tj_Seguimiento_Saldo_Hilo_Tela.Cod_Usuario);

                // Parámetros de salida
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                // Ejecutar el procedimiento almacenado
                try
                {
                    connection.Execute(
                        "[dbo].[Tj_Man_Seguimiento_Saldo_Hilo]",
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

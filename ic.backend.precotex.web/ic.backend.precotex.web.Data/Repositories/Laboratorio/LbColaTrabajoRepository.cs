using ic.backend.precotex.web.Entity.Entities.Laboratorio;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ic.backend.precotex.web.Data.Repositories.Implementation.Laboratorio;
using ic.backend.precotex.web.Entity.Entities;

namespace ic.backend.precotex.web.Data.Repositories.Laboratorio
{
    public class LbColaTrabajoRepository: ILbColaTrabajoRepository
    {
        private readonly string _connectionString;

        //DECLARAMOS CADENA DE CONEXION
        public LbColaTrabajoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnection")!;
        }

        //OBTENER DATOS CABECERA
        public async Task<IEnumerable<Lb_ColTra_Cab>?> ListaSDCPorEstado(string Flg_Est_Lab, DateTime Fec_Ini, DateTime Fec_Fin)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                parametros.Add("@Flg_Est_Lab", Flg_Est_Lab);
                parametros.Add("@Fec_Ini", Fec_Ini);
                parametros.Add("@Fec_Fin", Fec_Fin);

                var result = await connection.QueryAsync<Lb_ColTra_Cab>(
                        "[dbo].[PA_LB_CARTACOL_DG_S0001]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        //LISTAR COLORES DETALLE SDC
        public async Task<IEnumerable<Lb_ColTra_Det>?> ListaColoresSDC(int Corr_Carta)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();
                parametros.Add("@Corr_Carta", Corr_Carta);
                var result = await connection.QueryAsync<Lb_ColTra_Det>(
                    "[dbo].[PA_Lb_CartaCol_Detalle_S0001]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        //REGISTRAR DETALLE 
        public async Task<(int Codigo, string Mensaje)> RegistrarDetalleColorSDC(Lb_ColTra_Det lbColaTrabajoDet)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();

                parametros.Add("@Corr_Carta", lbColaTrabajoDet.Corr_Carta);
                parametros.Add("@Sec", lbColaTrabajoDet.Sec);

                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
                
                try
                {
                    var result = await connection.QueryAsync<Lb_ColTra_Det>(
                    "[dbo].[PA_Lb_ColaTrabajoLabDetalle_WB_I0001]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                );
                }
                catch
                {

                }       

                var Codigo = parametros.Get<int>("@Codigo");
                var mensaje = parametros.Get<string>("@sMsj");
                return (Codigo, mensaje);
            }
        }

        //OBTENER DATOS DE TABLA Lb_ColaTrabajoLabDetalle_WB PARA LLENAR EL DESPLEGABLE
        public async Task<IEnumerable<Lb_ColTra_Det>?> LlenarDesplegable()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<Lb_ColTra_Det>(
                    "[dbo].[PA_Lb_ColaTrabajoLabDetalle_WB_S0001]"
                    , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }


    }
}

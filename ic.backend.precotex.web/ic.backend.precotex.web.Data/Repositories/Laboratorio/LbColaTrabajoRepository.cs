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
using ic.backend.precotex.web.Entity.Entities.RetiroRepuestos;
using Microsoft.Graph.Models.TermStore;
using System.ComponentModel;

namespace ic.backend.precotex.web.Data.Repositories.Laboratorio
{
    public class LbColaTrabajoRepository : ILbColaTrabajoRepository
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

        //LLENAR GRILLA DESPLEGABLE EN HOJA DE FORMULACION
        public async Task<IEnumerable<Lb_ColTra_Cab_y_Det>?> LlenarGrillaDesplegable(int Corr_Carta, int Sec)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();
                parametros.Add("@Corr_Carta", Corr_Carta);
                parametros.Add("@Sec", Sec);

                var result = await connection.QueryAsync<Lb_ColTra_Cab_y_Det>(
                    "[dbo].[PA_Lb_ColaTrabajoLabDetalle_WB_S0002]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                );
                return result;

            }
        }

        //ACTUALIZAR ESTADOS
        public async Task<(int Codigo, string Mensaje)> ActualizarEstadoDeColor(Lb_ColTra_Det lb_ColTra_Det)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Corr_Carta", lb_ColTra_Det.Corr_Carta);
                parametros.Add("@Sec", lb_ColTra_Det.Sec);
                parametros.Add("@Flg_Est_Lab", lb_ColTra_Det.Flg_Est_Lab);
                parametros.Add("@Codigo", 0);
                parametros.Add("@sMsj", "");

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Lb_ColaTrabajoLabDetalle_WB_U0001]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                    );
                }
                catch (SqlException ex)
                {

                }

                var Codigo = parametros.Get<int>("@Codigo");
                var mensaje = parametros.Get<string>("@sMsj");
                return (Codigo, mensaje);
            }
        } 

        //INSERTAR DATOS DE OPCION
        public async Task<(int Codigo, string Mensaje)> AgregarOpcionColorante(Lb_AgrOpc_Colorantes lb_AgrOpc_Colorantes) 
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Corr_Carta", lb_AgrOpc_Colorantes.Corr_Carta);
                parametros.Add("@Sec", lb_AgrOpc_Colorantes.Sec);
                parametros.Add("@Procedencia", lb_AgrOpc_Colorantes.Procedencia);
                parametros.Add("@Correlativo", lb_AgrOpc_Colorantes.Correlativo);
                parametros.Add("@Col_Cod", lb_AgrOpc_Colorantes.Col_Cod);
                parametros.Add("@Por_Ini", lb_AgrOpc_Colorantes.Por_Ini);
                parametros.Add("@Por_Aju", lb_AgrOpc_Colorantes.Por_Aju);
                parametros.Add("@Por_Fin", lb_AgrOpc_Colorantes.Por_Fin);
                parametros.Add("@Can_Jabo", lb_AgrOpc_Colorantes.Can_Jabo);
                parametros.Add("@Cur_Jabo", lb_AgrOpc_Colorantes.Cur_Jabo);
                parametros.Add("@Fijado", lb_AgrOpc_Colorantes.Fijado);
                parametros.Add("@Acidulado", lb_AgrOpc_Colorantes.Acidulado);
                parametros.Add("@Rel_Ban", lb_AgrOpc_Colorantes.Rel_Ban);
                parametros.Add("@Pes_Mue", lb_AgrOpc_Colorantes.Pes_Mue);
                parametros.Add("@Volumen", lb_AgrOpc_Colorantes.Volumen);
                parametros.Add("@Car_Gr", lb_AgrOpc_Colorantes.Car_Gr);
                parametros.Add("@Car_Por", lb_AgrOpc_Colorantes.Car_Por);
                parametros.Add("@Sod_Gr", lb_AgrOpc_Colorantes.Sod_Gr);
                parametros.Add("@Sod_Por", lb_AgrOpc_Colorantes.Sod_Por);
                
                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Lb_Colorantes_WB_I0001]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                    );
                }
                catch (SqlException ex)
                {
                    
                }

                var Codigo = parametros.Get<int>("@Codigo");
                var mensaje = parametros.Get<string>("@sMsj");
                return (Codigo, mensaje);
            }
        }

        //CARGAR DATOS EN COMBOBOX
        public async Task<IEnumerable<Lg_Item>?> CargarComboBoxItem()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<Lg_Item>(
                    "[dbo].[PA_Lg_Item_S0004]"
                    , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }
        
        //CARGAR DATOS VENTANA INFORME SDC 
        public async Task<IEnumerable<Lb_Informe_SDC>> CargarInformeSDC(int Corr_Carta, int Sec)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            var parametros = new DynamicParameters();
            parametros.Add("@Corr_Carta", Corr_Carta);
            parametros.Add("@Sec", Sec);

            using var multi = await connection.QueryMultipleAsync(
                "[dbo].[PA_LB_CARTACOL_DG_S0002]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );

            var sdcList = (await multi.ReadAsync<Lb_Informe_SDC>()).ToList();
            var rutas = await multi.ReadAsync<(int CodSDC, string Descripcion)>();
            var solidez = await multi.ReadAsync<(int CodSDC, string DESCRIPCION)>();

            foreach (var sdc in sdcList)
            {
                sdc.Ruta = rutas
                    .Where(r => r.CodSDC == sdc.Corr_Carta)
                    .Select(r => r.Descripcion);

                sdc.Solidez = solidez
                    .Where(s => s.CodSDC == sdc.Corr_Carta)
                    .Select(s => s.DESCRIPCION);
            }

            return sdcList;
        }

        //CARGAR DATOS GRILLA HOJA FORMULACION
        public async Task<IEnumerable<Lb_AgrOpc_Colorantes>?> CargarGridHojaFormulacion(int Corr_Carta, int Sec)
        {
            //using (var connection = new SqlConnection(_connectionString))
            //{
            //    await connection.OpenAsync();

            //    var parametros = new DynamicParameters();
            //    parametros.Add("@Corr_Carta", Corr_Carta);
            //    parametros.Add("@Sec", Sec);

            //    var result = await connection.QueryAsync<Lb_AgrOpc_Colorantes>(
            //        "[dbo].[PA_Lb_Colorantes_WB_S0001]"
            //        , parametros
            //        , commandType: CommandType.StoredProcedure
            //    );
            //    return result;
            //}

            using var connection = new SqlConnection(_connectionString);
             await connection.OpenAsync();
            var parametros = new DynamicParameters();
            parametros.Add("@Corr_Carta", Corr_Carta);
            parametros.Add("@Sec", Sec);

            using var multi = await connection.QueryMultipleAsync(
                "[dbo].[PA_Lb_Colorantes_WB_S0001]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );

            var DatosGenerales = (await multi.ReadAsync<Lb_AgrOpc_Colorantes>()).ToList();
            var rutas = await multi.ReadAsync<Colorantes>();

            foreach (var datos in DatosGenerales)
            {
                datos.Colorantes = rutas
               .Where(r => r.Corr_Carta == datos.Corr_Carta && r.Sec == datos.Sec)
               .ToList();

            }
            return DatosGenerales;

        }

        //LISTAR AHIBAS
        public async Task<IEnumerable<Lb_ColTra_Det>?> ListaAhibas()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                
                var result = await connection.QueryAsync<Lb_ColTra_Det>(
                    "[dbo].[PA_Lb_Ahibas_WB_S0001]"
                    , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        //COPIAR OPCION
        public async Task<(int Codigo, string Mensaje)> CopiarOpcionColorante(Lb_AgrOpc_Colorantes lb_AgrOpc_Colorantes)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Corr_Carta", lb_AgrOpc_Colorantes.Corr_Carta);
                parametros.Add("@Sec", lb_AgrOpc_Colorantes.Sec);
                parametros.Add("@Correlativo", lb_AgrOpc_Colorantes.Correlativo);
                
                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Lb_Colorantes_WB_I0002]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                    );
                }
                catch (SqlException ex)
                {

                }

                var Codigo = parametros.Get<int>("@Codigo");
                var mensaje = parametros.Get<string>("@sMsj");
                return (Codigo, mensaje);
            }
        }
        
        //ELIMINAR OPCION AGREGADA
        public async Task<(int Codigo, string Mensaje)> EliminarOpcionColorante(int Corr_Carta, int Sec, int Correlativo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Corr_Carta", Corr_Carta);
                parametros.Add("@Sec", Sec);
                parametros.Add("@Correlativo", Correlativo);

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Lb_Colorantes_WB_D0001]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                    );
                }
                catch (SqlException ex)
                {

                }

                var Codigo = parametros.Get<int>("@Codigo");
                var mensaje = parametros.Get<string>("@sMsj");
                return (Codigo, mensaje);
            }
        }
    }
}

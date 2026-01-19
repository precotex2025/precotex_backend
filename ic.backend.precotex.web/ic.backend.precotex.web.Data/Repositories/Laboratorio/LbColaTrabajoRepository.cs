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
using Microsoft.Kiota.Http.HttpClientLibrary.Middleware;

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
        public async Task<(int Codigo, string Mensaje)> ActualizarEstadoDeColorTricomia(Lb_AgrOpc_Colorantes _lbAgrOpcColorante)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Corr_Carta", _lbAgrOpcColorante.Corr_Carta);
                parametros.Add("@Sec", _lbAgrOpcColorante.Sec);
                parametros.Add("@Correlativo", _lbAgrOpcColorante.Correlativo);
                parametros.Add("@Flg_Est_Lab", _lbAgrOpcColorante.Flg_Est_Lab);
                parametros.Add("@Codigo", 0);
                parametros.Add("@sMsj", "");

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Lb_Colorantes_WB_U0001]"
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

        public async Task<(int Codigo, string Mensaje)> ActualizarEstadoDeColorTricomiaAutolab(Lb_AgrOpc_Colorantes _lbAgrOpcColorante)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Corr_Carta", _lbAgrOpcColorante.Corr_Carta);
                parametros.Add("@Sec", _lbAgrOpcColorante.Sec);
                parametros.Add("@Correlativo", _lbAgrOpcColorante.Correlativo);
                parametros.Add("@Flg_Est_Autolab", _lbAgrOpcColorante.Flg_Est_Autolab);
                parametros.Add("@Codigo", 0);
                parametros.Add("@sMsj", "");

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Lb_Colorantes_WB_U0002]"
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
                parametros.Add("@Familia", lb_AgrOpc_Colorantes.Familia);
                parametros.Add("@Cambio", lb_AgrOpc_Colorantes.Cambio);
                
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
                    Console.WriteLine("SQL Error: " + ex.Message);
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
                 .Where(r =>
                     r.Corr_Carta == datos.Corr_Carta &&
                     r.Sec == datos.Sec &&
                     r.correlativo == datos.Correlativo
                 )
                 .ToList();

            }
            return DatosGenerales;

        }

        //LISTAR AHIBAS
        public async Task<IEnumerable<Lb_Ahibas>?> ListaAhibas()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                
                var result = await connection.QueryAsync<Lb_Ahibas>(
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

        //LISTAR COLORANTES
        public async Task<IEnumerable<Lb_Colorantes>?> ListarColorantesAgregarOpcion()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<Lb_Colorantes>(
                    "[dbo].[PA_Lb_Tipo_Colorante_HojaFormulacion_S0001]"
                    , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        /*
            JABONADOS 
        */
        public async Task<IEnumerable<Lb_Jabonados>?> ListarJabonados()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<Lb_Jabonados>(
                    "[dbo].[PA_Lb_Jabonados_S0001]"
                    , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        public async Task<IEnumerable<Lb_Jabonados>?> ListarJabonadosCalculado(decimal Colorante_Total, string Familia)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                
                var parameters = new DynamicParameters();
                parameters.Add("@Colorante_Total", Colorante_Total);
                parameters.Add("@Familia", Familia);

                var result = await connection.QueryAsync<Lb_Jabonados>(
                    "[dbo].[PA_Lb_Jabonados_Detalle_S0001]"
                    , parameters
                    , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        /*
            FIJADOS 
        */
        public async Task<IEnumerable<Lb_Fijados>?> ListarFijados()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<Lb_Fijados>(
                    "[dbo].[PA_Lb_Fijados_S0001]"
                    , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        public async Task<IEnumerable<Lb_Fijados>?> ListarFijadosCalculado(decimal Colorante_Total, string Familia)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@Colorante_Total", Colorante_Total);
                parameters.Add("@Familia", Familia);

                var result = await connection.QueryAsync<Lb_Fijados>(
                    "[dbo].[PA_Lb_Fijados_Detalle_S0001]"
                    , parameters
                    , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        /*
            CARBONATO Y SODA
        */

        public async Task<IEnumerable<Lb_Colorantes_Componentes_Extra>?> ListarCarbonatoSodaCalculado(decimal Colorante_Total, string Familia, int Com_Cod_Con)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@Colorante_Total", Colorante_Total);
                parameters.Add("@Familia", Familia);
                parameters.Add("@Com_Cod_Con", Com_Cod_Con);

                var result = await connection.QueryAsync<Lb_Colorantes_Componentes_Extra>(
                    "[dbo].[PA_Lb_Proceso_Colorantes_Componentes_Extra_Valores_S0001]"
                    , parameters
                    , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        /*
            DISPENSADO EN AUTOLAB 
        */

        public async Task<IEnumerable<Lb_ColTra_Det>?> ListarColaAutolab()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<Lb_ColTra_Det>(
                    "[dbo].[PA_Lb_ColaTrabajoLabDetalle_WB_S0003]"
                    , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        //ENVIAR A DISPENSADO
        public async Task<(int Codigo, string Mensaje)> EnviarADispensado(Lb_AgrOpc_Colorantes _lbAgrOpcColorante)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Corr_Carta", _lbAgrOpcColorante.Corr_Carta);
                parametros.Add("@Sec", _lbAgrOpcColorante.Sec);
                parametros.Add("@Correlativo", _lbAgrOpcColorante.Correlativo);
                parametros.Add("@Posicion", _lbAgrOpcColorante.Posicion);
                parametros.Add("@Codigo", 0);
                parametros.Add("@sMsj", "");

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Lb_Colorantes_WB_U0003]"
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

        //LISTAR DISPENSADO
        public async Task<IEnumerable<Lb_ColTra_Det>?> ListarDispensado()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<Lb_ColTra_Det>(
                    "[dbo].[PA_Lb_ColaTrabajoLabDetalle_WB_S0004]"
                    , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        //CARGAR COLORANTES A UN AHIBA
        public async Task<(int Codigo, string Mensaje)> CargarAahiba(Lb_AgrOpc_Colorantes _lbAgrOpcColorante)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Corr_Carta", _lbAgrOpcColorante.Corr_Carta);
                parametros.Add("@Sec", _lbAgrOpcColorante.Sec);
                parametros.Add("@Correlativo", _lbAgrOpcColorante.Correlativo);
                parametros.Add("@Ahi_Id", _lbAgrOpcColorante.Ahi_Id);
                parametros.Add("@Codigo", 0);
                parametros.Add("@sMsj", "");

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Lb_Colorantes_WB_U0004]"
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

        //LISTAR ITEMS EN AHIBA
        public async Task<IEnumerable<Lb_ColTra_Det>?> ListarItemsEnAhiba(int Ahi_Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@Ahi_Id", Ahi_Id);

                var result = await connection.QueryAsync<Lb_ColTra_Det>(
                    "[dbo].[PA_Lb_ColaTrabajoLabDetalle_WB_S0005]"
                    , parameters
                    , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        //ACTUALIZAR PH - INI - FIN - JAB
        public async Task<(int Codigo, string Mensaje)> ActualizarPH(Lb_ColTra_Det lb_ColTra_Det)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Corr_Carta", lb_ColTra_Det.Corr_Carta);
                parametros.Add("@Sec", lb_ColTra_Det.Sec);
                parametros.Add("@Correlativo", lb_ColTra_Det.Correlativo);
                parametros.Add("@Tip_Ph", lb_ColTra_Det.Tip_Ph);
                parametros.Add("@Ph_Val", lb_ColTra_Det.Ph_Val);
                parametros.Add("@Codigo", 0);
                parametros.Add("@sMsj", "");

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Lb_Colorantes_WB_U0006]"
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

        //ENVIAR A AUTOLAB
        public async Task<(int Codigo, string Mensaje)> EnviarAutolab(Lb_AgrOpc_Colorantes _lbAgrOpcColorante)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Corr_Carta", _lbAgrOpcColorante.Corr_Carta);
                parametros.Add("@Sec", _lbAgrOpcColorante.Sec);
                parametros.Add("@Correlativo", _lbAgrOpcColorante.Correlativo);
                parametros.Add("@Codigo", 0);
                parametros.Add("@sMsj", "");

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Lb_Colorantes_WB_U0005]"
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

        //AGREGAR AUXILIARES DESDE AGREGAR OPCION EN LA HOJA DE FORMULACION
        public async Task<(int Codigo, string Mensaje)> AgregarAuxiliaresHojaFormulacion(Lb_AgrOpc_Colorantes _lbAgrOpcColorante)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Corr_Carta", _lbAgrOpcColorante.Corr_Carta);
                parametros.Add("@Sec", _lbAgrOpcColorante.Sec);
                parametros.Add("@Correlativo", _lbAgrOpcColorante.Correlativo);
                parametros.Add("@Familia", _lbAgrOpcColorante.Familia);
                parametros.Add("@Cambio", _lbAgrOpcColorante.Cambio);
                parametros.Add("@Procedencia", _lbAgrOpcColorante.ProcedenciaHardCodeada);

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Lb_Colorantes_WB_I0003]"
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

        public async Task<(int Codigo, string Mensaje)> LlenarTextoFinal(Lb_AgrOpc_Colorantes _lbAgrOpcColorante)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Corr_Carta", _lbAgrOpcColorante.Corr_Carta);
                parametros.Add("@Sec", _lbAgrOpcColorante.Sec);
                parametros.Add("@Correlativo", _lbAgrOpcColorante.Correlativo);

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Lb_Colorantes_WB_Unido_I0001]"
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

        //LISTAR JABONADO
        public async Task<IEnumerable<Lb_ColTra_Det>?> ListarJabonado()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<Lb_ColTra_Det>(
                    "[dbo].[PA_Lb_ColaTrabajoLabDetalle_WB_S0006]"
                    , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        public async Task<IEnumerable<Lb_Colorantes_Componentes_Extra>?> ListarFamiliasProceso()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<Lb_Colorantes_Componentes_Extra>(
                    "[dbo].[PA_Lb_Proceso_Colorantes_Componentes_Extra_S0001]"
                    , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        
        public async Task<IEnumerable<Lb_AgrOpc_Colorantes>?> CargarColoranteParaCopiar(int Corr_Carta, int Sec, int Correlativo)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            var parametros = new DynamicParameters();
            parametros.Add("@Corr_Carta", Corr_Carta);
            parametros.Add("@Sec", Sec);
            parametros.Add("@Correlativo", Correlativo);

            using var multi = await connection.QueryMultipleAsync(
                "[dbo].[PA_Lb_Colorantes_WB_S0002]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );

            var DatosGenerales = (await multi.ReadAsync<Lb_AgrOpc_Colorantes>()).ToList();
            var rutas = await multi.ReadAsync<Colorantes>();

            foreach (var datos in DatosGenerales)
            {
                datos.Colorantes = rutas
                 .Where(r =>
                     r.Corr_Carta == datos.Corr_Carta &&
                     r.Sec == datos.Sec &&
                     r.correlativo == datos.Correlativo
                 )
                 .ToList();

            }
            return DatosGenerales;
        }

        public async Task<IEnumerable<Lb_AgrOpc_Colorantes>?> CargarColoranteParaDetalle(int Corr_Carta, int Sec, int Correlativo)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            var parametros = new DynamicParameters();
            parametros.Add("@Corr_Carta", Corr_Carta);
            parametros.Add("@Sec", Sec);
            parametros.Add("@Correlativo", Correlativo);

            using var multi = await connection.QueryMultipleAsync(
                "[dbo].[PA_Lb_Colorantes_WB_S0003]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );
            //PRIMER SELECT PARA LOS DATOS GENERALES
            var DatosGenerales = (await multi.ReadAsync<Lb_AgrOpc_Colorantes>()).ToList();
            //SEGUNDO SELECT PARA LOS AUXILIARES
            var auxiliares = await multi.ReadAsync<Auxiliares>();
            //TERCER SELECT PARA LA SAL Y CO3
            var quimicos = await multi.ReadAsync<Quimicos>();
            //CUARTO SELECT PARA LOS COLORANTES
            var colorantes = await multi.ReadAsync<Colorantes>();
           
            foreach (var datos in DatosGenerales)
            {
                //AUXILIARES
                datos.Auxiliares = auxiliares
                    .Where(a =>
                        a.Corr_Carta == datos.Corr_Carta &&
                        a.Sec == datos.Sec &&
                        a.correlativo == datos.Correlativo
                    )
                    .ToList();
                
                datos.Quimicos = quimicos
                    .Where(q =>
                        q.Corr_Carta == datos.Corr_Carta &&
                        q.Sec == datos.Sec &&
                        q.correlativo == datos.Correlativo
                    )
                    .ToList();

                datos.Colorantes = colorantes
                 .Where(r =>
                     r.Corr_Carta == datos.Corr_Carta &&
                     r.Sec == datos.Sec &&
                     r.correlativo == datos.Correlativo
                 )
                 .ToList();
            }
            return DatosGenerales;
        }




        /*************************LOGIN**********************/
        public async Task<IEnumerable<Lb_Usuarios>?> GetUsuarioWeb(string Cod_Usuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();
                parametros.Add("@Cod_Usuario", Cod_Usuario);

                var result = await connection.QueryAsync<Lb_Usuarios>(
                    "[dbo].[PA_Lb_Usuarios_WB_S0001]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        public async Task<IEnumerable<Lb_AgrOpc_Colorantes>?> ListarIngresoManual(int Corr_Carta, int Sec, int Correlativo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();
                parametros.Add("@Corr_Carta", Corr_Carta);
                parametros.Add("@Sec", Sec);
                parametros.Add("@Correlativo", Correlativo);

                var result = await connection.QueryAsync<Lb_AgrOpc_Colorantes>(
                    "[dbo].[PA_Lb_Colorantes_WB_S0004]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        public async Task<IEnumerable<Lb_Reporte>?> CargarDatosReporte(int Corr_Carta, int Sec, int Correlativo)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            var parametros = new DynamicParameters();
            parametros.Add("@Corr_Carta", Corr_Carta);
            parametros.Add("@Sec", Sec);
            parametros.Add("@Correlativo", Correlativo);

            using var multi = await connection.QueryMultipleAsync(
                "[dbo].[PA_LB_CARTACOL_DG_S0003]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );

            //CABECERA
            var infoPrincipal = (await multi.ReadAsync<Lb_Reporte>()).ToList();
            //CUERPO
            var colorantes = await multi.ReadAsync <Colorantes_Reporte>();
            //FOOTER
            var rutas = await multi.ReadAsync<Ruta_Reporte>();
            var solidez = await multi.ReadAsync<Solidez_Reporte>();

            foreach (var info in infoPrincipal)
            {
                info.Colorantes_Reporte = colorantes
                    .Where(c =>
                        c.Corr_Carta == info.Corr_Carta &&
                        c.Sec == info.Sec /*&&*/
                        /*c.Correlativo == info.Correlativo*/)
                    .ToList();

                info.Ruta_Reporte = rutas
                    .Where(r => 
                        r.Corr_Carta == info.Corr_Carta)
                    .ToList();

                info.Solidez_Reporte = solidez
                    .Where(s => 
                        s.Corr_Carta == info.Corr_Carta)
                    .ToList();
            }

            return infoPrincipal;
        }

        /******************************MANTENIMIENTOS SOLICITADOS********************************/

        //LISTAR JABONADO
        public async Task<IEnumerable<Lb_Jabonados>?> ListarJabonadoMantenimiento()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<Lb_Jabonados>(
                    "[dbo].[PA_Lb_Jabonados_S0002]"
                    , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        //CREAR NUEVO JABONADO
        public async Task<(int Codigo, string Mensaje)> RegistrarJabonado(Lb_Jabonados _lb_Jabonados)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Jab_Des", _lb_Jabonados.Jab_Des);
                parametros.Add("@Usr_Reg", _lb_Jabonados.Usr_Reg);
                parametros.Add("@Codigo", 0);
                parametros.Add("@sMsj", "");

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Lb_Jabonados_I0001]"
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

        //MODIFICAR JABONADO
        public async Task<(int Codigo, string Mensaje)> ModificarJabonado(Lb_Jabonados _lb_Jabonados)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Jab_Id", _lb_Jabonados.Jab_Id);
                parametros.Add("@Jab_Des", _lb_Jabonados.Jab_Des);
                parametros.Add("@Usr_Mod", _lb_Jabonados.Usr_Mod);
                parametros.Add("@Codigo", 0);
                parametros.Add("@sMsj", "");

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Lb_Jabonados_U0001]"
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

        //ELIMINAR JABONADO
        public async Task<(int Codigo, string Mensaje)> DeshabilitarJabonado(Lb_Jabonados _lb_Jabonados)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Jab_Id", _lb_Jabonados.Jab_Id);
                parametros.Add("@Flg_Status", _lb_Jabonados.Flg_Status);
                parametros.Add("@Codigo", 0);
                parametros.Add("@sMsj", "");

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Lb_Jabonados_U0002]"
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

        //LISTAR JABONADO DETALLE
        public async Task<IEnumerable<Lb_Jabonados_Detalle>?> ListarJabonadosDetalleMantenimiento(int Jab_Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                parametros.Add("@Jab_Id", Jab_Id);

                var result = await connection.QueryAsync<Lb_Jabonados_Detalle>(
                    "[dbo].[PA_Lb_Jabonados_Detalle_S0002]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        //CREAR NUEVO JABONADO DETALLE
        public async Task<(int Codigo, string Mensaje)> RegistrarJabonadoDetalle(Lb_Jabonados_Detalle _lb_Jabonados_Detalle)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Jab_Id", _lb_Jabonados_Detalle.Jab_Id);
                parametros.Add("@Jab_Ran_Ini", _lb_Jabonados_Detalle.Jab_Ran_Ini);
                parametros.Add("@Jab_Ran_Fin", _lb_Jabonados_Detalle.Jab_Ran_Fin);
                parametros.Add("@Jab_Can", _lb_Jabonados_Detalle.Jab_Can);
                parametros.Add("@Familia", _lb_Jabonados_Detalle.Familia);
                parametros.Add("@Usr_Reg", _lb_Jabonados_Detalle.Usr_Reg);
                parametros.Add("@Codigo", 0);
                parametros.Add("@sMsj", "");

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Lb_Jabonados_Detalle_I0001]"
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

        //MODIFICAR JABONADO DETALLE
        public async Task<(int Codigo, string Mensaje)> ModificarJabonadoDetalle(Lb_Jabonados_Detalle _lb_Jabonados_Detalle)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Jab_Id", _lb_Jabonados_Detalle.Jab_Id);
                parametros.Add("@Jab_Ran_Ini", _lb_Jabonados_Detalle.Jab_Ran_Ini);
                parametros.Add("@Jab_Ran_Fin", _lb_Jabonados_Detalle.Jab_Ran_Fin);
                parametros.Add("@Jab_Can", _lb_Jabonados_Detalle.Jab_Can);
                parametros.Add("@Familia", _lb_Jabonados_Detalle.Familia);
                parametros.Add("@Jab_Ran_Ini_Org", _lb_Jabonados_Detalle.Jab_Ran_Ini_Org);
                parametros.Add("@Familia_Org", _lb_Jabonados_Detalle.Familia_Org);
                parametros.Add("@Usr_Mod", _lb_Jabonados_Detalle.Usr_Mod);
                parametros.Add("@Codigo", 0);
                parametros.Add("@sMsj", "");

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Lb_Jabonados_Detalle_U0001]"
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

        //ELIMINAR JABONADO DETALLE
        public async Task<(int Codigo, string Mensaje)> DeshabilitarJabonadoDetalle(Lb_Jabonados_Detalle _lb_Jabonados_Detalle)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Jab_Id", _lb_Jabonados_Detalle.Jab_Id);
                parametros.Add("@Jab_Ran_Ini", _lb_Jabonados_Detalle.Jab_Ran_Ini);
                parametros.Add("@Familia", _lb_Jabonados_Detalle.Familia);
                parametros.Add("@Flg_Status", _lb_Jabonados_Detalle.Flg_Status);
                parametros.Add("@Codigo", 0);
                parametros.Add("@sMsj", "");

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Lb_Jabonados_Detalle_U0002]"
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

        //LISTAR FIJADOS
        public async Task<IEnumerable<Lb_Fijados>?> ListarFijadosMantenimiento()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<Lb_Fijados>(
                    "[dbo].[PA_Lb_Fijados_S0002]"
                    , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        //CREAR NUEVO FIJADO 
        public async Task<(int Codigo, string Mensaje)> RegistrarFijado(Lb_Fijados _lb_Fijados)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Fij_Des", _lb_Fijados.Fij_Des);
                parametros.Add("@Usr_Reg", _lb_Fijados.Usr_Reg);
                parametros.Add("@Codigo", 0);
                parametros.Add("@sMsj", "");

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Lb_Fijados_I0001]"
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

        //MODIFICAR FIJADO
        public async Task<(int Codigo, string Mensaje)> ModificarFijado(Lb_Fijados _lb_Fijados)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Fij_Id", _lb_Fijados.Fij_Id);
                parametros.Add("@Fij_Des", _lb_Fijados.Fij_Des);
                parametros.Add("@Usr_Mod", _lb_Fijados.Usr_Mod);
                parametros.Add("@Codigo", 0);
                parametros.Add("@sMsj", "");

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Lb_Fijados_U0001]"
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

        //ELIMINAR FIJADO
        public async Task<(int Codigo, string Mensaje)> DeshabilitarFijado(Lb_Fijados _lb_Fijados)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Fij_Id", _lb_Fijados.Fij_Id);
                parametros.Add("@Flg_Status", _lb_Fijados.Flg_Status);
                parametros.Add("@Codigo", 0);
                parametros.Add("@sMsj", "");

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Lb_Fijados_U0002]"
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

        //LISTAR FIJADOS DETALLE
        public async Task<IEnumerable<Lb_Fijados_Detalle>?> ListarFijadosDetalleMantenimiento(int Fij_Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                parametros.Add("@Fij_Id", Fij_Id);

                var result = await connection.QueryAsync<Lb_Fijados_Detalle>(
                    "[dbo].[PA_Lb_Fijados_Detalle_S0002]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        //CREAR NUEVO FIJADO DETALLE
        public async Task<(int Codigo, string Mensaje)> RegistrarFijadoDetalle(Lb_Fijados_Detalle _lb_Fijados_Detalle)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Fij_Id", _lb_Fijados_Detalle.Fij_Id);
                parametros.Add("@Fij_Ran_Ini", _lb_Fijados_Detalle.Fij_Ran_Ini);
                parametros.Add("@Fij_Ran_Fin", _lb_Fijados_Detalle.Fij_Ran_Fin);
                parametros.Add("@Familia", _lb_Fijados_Detalle.Familia);
                parametros.Add("@Usr_Reg", _lb_Fijados_Detalle.Usr_Reg);
                parametros.Add("@Codigo", 0);
                parametros.Add("@sMsj", "");

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Lb_Fijados_Detalle_I0001]"
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

        //MODIFICAR FIJADO DETALLE
        public async Task<(int Codigo, string Mensaje)> ModificarFijadoDetalle(Lb_Fijados_Detalle _lb_Fijados_Detalle)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Fij_Id", _lb_Fijados_Detalle.Fij_Id);
                parametros.Add("@Fij_Ran_Ini", _lb_Fijados_Detalle.Fij_Ran_Ini);
                parametros.Add("@Fij_Ran_Fin", _lb_Fijados_Detalle.Fij_Ran_Fin);
                parametros.Add("@Familia", _lb_Fijados_Detalle.Familia);
                parametros.Add("@Fij_Ran_Ini_Org", _lb_Fijados_Detalle.Fij_Ran_Ini_Org);
                parametros.Add("@Familia_Org", _lb_Fijados_Detalle.Familia_Org);
                parametros.Add("@Usr_Mod", _lb_Fijados_Detalle.Usr_Mod);
                parametros.Add("@Codigo", 0);
                parametros.Add("@sMsj", "");

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Lb_Fijados_Detalle_U0001]"
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

        //ELIMINAR FIJADO DETALLE
        public async Task<(int Codigo, string Mensaje)> DeshabilitarFijadoDetalle(Lb_Fijados_Detalle _lb_Fijados_Detalle)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Jab_Id", _lb_Fijados_Detalle.Fij_Id);
                parametros.Add("@Jab_Ran_Ini", _lb_Fijados_Detalle.Fij_Ran_Ini);
                parametros.Add("@Familia", _lb_Fijados_Detalle.Familia);
                parametros.Add("@Flg_Status", _lb_Fijados_Detalle.Flg_Status);
                parametros.Add("@Codigo", 0);
                parametros.Add("@sMsj", "");

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Lb_Fijados_Detalle_U0002]"
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

        //CREAR NUEVO PROCESO
        public async Task<(int Codigo, string Mensaje)> RegistrarProceso(ComponentesExtra _ComponentesExtra)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Pro_Cod", _ComponentesExtra.Pro_Cod);
                parametros.Add("@Pro_Des", _ComponentesExtra.Pro_Des);
                parametros.Add("@Usr_Reg", _ComponentesExtra.Usr_Reg);
                parametros.Add("@Codigo", 0);
                parametros.Add("@sMsj", "");

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Lb_Proceso_Colorantes_Componentes_Extra_I0001]"
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

        //MODIFICAR PROCESO
        public async Task<(int Codigo, string Mensaje)> ModificarProceso(ComponentesExtra _ComponentesExtra)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Pro_Cod", _ComponentesExtra.Pro_Cod);
                parametros.Add("@Pro_Cod_Org", _ComponentesExtra.Pro_Cod_Org);
                parametros.Add("@Pro_Des", _ComponentesExtra.Pro_Des);
                parametros.Add("@Usr_Mod", _ComponentesExtra.Usr_Mod);
                parametros.Add("@Codigo", 0);
                parametros.Add("@sMsj", "");

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Lb_Proceso_Colorantes_Componentes_Extra_U0001]"
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

        //ELIMINAR PROCESO
        public async Task<(int Codigo, string Mensaje)> DeshabilitarProceso(ComponentesExtra _ComponentesExtra)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Pro_Cod", _ComponentesExtra.Pro_Cod);
                parametros.Add("@Codigo", 0);
                parametros.Add("@sMsj", "");

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Lb_Proceso_Colorantes_Componentes_Extra_U0002]"
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

        //LISTAR PROCESOS VALOR
        public async Task<IEnumerable<ComponentesExtraValores>?> ListarProcesoValor(string Pro_Cod)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                parametros.Add("@Pro_Cod", Pro_Cod);

                var result = await connection.QueryAsync<ComponentesExtraValores>(
                    "[dbo].[PA_Lb_Proceso_Colorantes_Componentes_Extra_Valores_S0002]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }


        //CREAR NUEVO PROCESO VALOR
        public async Task<(int Codigo, string Mensaje)> RegistrarProcesoValor(ComponentesExtraValores _ComponentesExtraValores)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA

                parametros.Add("@Pro_Cod", _ComponentesExtraValores.Pro_Cod);
                parametros.Add("@Com_Cod_Con", _ComponentesExtraValores.Com_Cod_Con);
                parametros.Add("@Com_Ran_Ini", _ComponentesExtraValores.Com_Ran_Ini);
                parametros.Add("@Com_Ran_Fin", _ComponentesExtraValores.Com_Ran_Fin);
                parametros.Add("@Com_Cod_Extra1", _ComponentesExtraValores.Com_Cod_Extra1);
                parametros.Add("@Com_Can_Extra1", _ComponentesExtraValores.Com_Can_Extra1);
                parametros.Add("@Com_Cod_Extra2", _ComponentesExtraValores.Com_Cod_Extra2);
                parametros.Add("@Com_Can_Extra2", _ComponentesExtraValores.Com_Can_Extra2);
                parametros.Add("@Com_Cod_Extra3", _ComponentesExtraValores.Com_Cod_Extra3);
                parametros.Add("@Com_Can_Extra3", _ComponentesExtraValores.Com_Can_Extra3);
                parametros.Add("@Com_Cod_Extra4", _ComponentesExtraValores.Com_Cod_Extra4);
                parametros.Add("@Com_Can_Extra4", _ComponentesExtraValores.Com_Can_Extra4);
                parametros.Add("@Com_Cod_Extra5", _ComponentesExtraValores.Com_Cod_Extra5);
                parametros.Add("@Com_Can_Extra5", _ComponentesExtraValores.Com_Can_Extra5);
                parametros.Add("@Com_Cod_Extra6", _ComponentesExtraValores.Com_Cod_Extra6);
                parametros.Add("@Com_Can_Extra6", _ComponentesExtraValores.Com_Can_Extra6);
                parametros.Add("@Com_Cod_Extra7", _ComponentesExtraValores.Com_Cod_Extra7);
                parametros.Add("@Com_Can_Extra7", _ComponentesExtraValores.Com_Can_Extra7);
                parametros.Add("@Com_Cod_Extra8", _ComponentesExtraValores.Com_Cod_Extra8);
                parametros.Add("@Com_Can_Extra8", _ComponentesExtraValores.Com_Can_Extra8);
                parametros.Add("@Com_Cod_Extra9", _ComponentesExtraValores.Com_Cod_Extra9);
                parametros.Add("@Com_Can_Extra9", _ComponentesExtraValores.Com_Can_Extra9);
                parametros.Add("@Com_Cod_Extra10", _ComponentesExtraValores.Com_Cod_Extra10);
                parametros.Add("@Com_Can_Extra10", _ComponentesExtraValores.Com_Can_Extra10);
                parametros.Add("@Com_Cod_Extra11", _ComponentesExtraValores.Com_Cod_Extra11);
                parametros.Add("@Com_Can_Extra11", _ComponentesExtraValores.Com_Can_Extra11);
                parametros.Add("@Com_Cod_Extra12", _ComponentesExtraValores.Com_Cod_Extra12);
                parametros.Add("@Com_Can_Extra12", _ComponentesExtraValores.Com_Can_Extra12);
                parametros.Add("@Com_Cod_Extra13", _ComponentesExtraValores.Com_Cod_Extra13);
                parametros.Add("@Com_Can_Extra13", _ComponentesExtraValores.Com_Can_Extra13);
                parametros.Add("@Com_Cod_Extra14", _ComponentesExtraValores.Com_Cod_Extra14);
                parametros.Add("@Com_Can_Extra14", _ComponentesExtraValores.Com_Can_Extra14);
                parametros.Add("@Com_Cod_Extra15", _ComponentesExtraValores.Com_Cod_Extra15);
                parametros.Add("@Com_Can_Extra15", _ComponentesExtraValores.Com_Can_Extra15);
                parametros.Add("@Com_Cod_Extra16", _ComponentesExtraValores.Com_Cod_Extra16);
                parametros.Add("@Com_Can_Extra16", _ComponentesExtraValores.Com_Can_Extra16);
                parametros.Add("@Usr_Reg", _ComponentesExtraValores.Usr_Reg);
                parametros.Add("@Codigo", 0);
                parametros.Add("@sMsj", "");

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Lb_Proceso_Colorantes_Componentes_Extra_Valores_I0001]"
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

        //MODIFICAR PROCESO VALOR
        public async Task<(int Codigo, string Mensaje)> ModificarProcesoValor(ComponentesExtraValores _ComponentesExtraValores)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA

                parametros.Add("@Pro_Cod", _ComponentesExtraValores.Pro_Cod);
                parametros.Add("@Pro_Cod_Org", _ComponentesExtraValores.Pro_Cod_Org);
                parametros.Add("@Com_Cod_Con", _ComponentesExtraValores.Com_Cod_Con);
                parametros.Add("@Com_Cod_Con_Org", _ComponentesExtraValores.Com_Cod_Con_Org);
                parametros.Add("@Com_Ran_Ini", _ComponentesExtraValores.Com_Ran_Ini);
                parametros.Add("@Com_Ran_Ini_Org", _ComponentesExtraValores.Com_Ran_Ini_Org);
                parametros.Add("@Com_Ran_Fin", _ComponentesExtraValores.Com_Ran_Fin);
                parametros.Add("@Com_Cod_Extra1", _ComponentesExtraValores.Com_Cod_Extra1);
                parametros.Add("@Com_Can_Extra1", _ComponentesExtraValores.Com_Can_Extra1);
                parametros.Add("@Com_Cod_Extra2", _ComponentesExtraValores.Com_Cod_Extra2);
                parametros.Add("@Com_Can_Extra2", _ComponentesExtraValores.Com_Can_Extra2);
                parametros.Add("@Com_Cod_Extra3", _ComponentesExtraValores.Com_Cod_Extra3);
                parametros.Add("@Com_Can_Extra3", _ComponentesExtraValores.Com_Can_Extra3);
                parametros.Add("@Com_Cod_Extra4", _ComponentesExtraValores.Com_Cod_Extra4);
                parametros.Add("@Com_Can_Extra4", _ComponentesExtraValores.Com_Can_Extra4);
                parametros.Add("@Com_Cod_Extra5", _ComponentesExtraValores.Com_Cod_Extra5);
                parametros.Add("@Com_Can_Extra5", _ComponentesExtraValores.Com_Can_Extra5);
                parametros.Add("@Com_Cod_Extra6", _ComponentesExtraValores.Com_Cod_Extra6);
                parametros.Add("@Com_Can_Extra6", _ComponentesExtraValores.Com_Can_Extra6);
                parametros.Add("@Com_Cod_Extra7", _ComponentesExtraValores.Com_Cod_Extra7);
                parametros.Add("@Com_Can_Extra7", _ComponentesExtraValores.Com_Can_Extra7);
                parametros.Add("@Com_Cod_Extra8", _ComponentesExtraValores.Com_Cod_Extra8);
                parametros.Add("@Com_Can_Extra8", _ComponentesExtraValores.Com_Can_Extra8);
                parametros.Add("@Com_Cod_Extra9", _ComponentesExtraValores.Com_Cod_Extra9);
                parametros.Add("@Com_Can_Extra9", _ComponentesExtraValores.Com_Can_Extra9);
                parametros.Add("@Com_Cod_Extra10", _ComponentesExtraValores.Com_Cod_Extra10);
                parametros.Add("@Com_Can_Extra10", _ComponentesExtraValores.Com_Can_Extra10);
                parametros.Add("@Com_Cod_Extra11", _ComponentesExtraValores.Com_Cod_Extra11);
                parametros.Add("@Com_Can_Extra11", _ComponentesExtraValores.Com_Can_Extra11);
                parametros.Add("@Com_Cod_Extra12", _ComponentesExtraValores.Com_Cod_Extra12);
                parametros.Add("@Com_Can_Extra12", _ComponentesExtraValores.Com_Can_Extra12);
                parametros.Add("@Com_Cod_Extra13", _ComponentesExtraValores.Com_Cod_Extra13);
                parametros.Add("@Com_Can_Extra13", _ComponentesExtraValores.Com_Can_Extra13);
                parametros.Add("@Com_Cod_Extra14", _ComponentesExtraValores.Com_Cod_Extra14);
                parametros.Add("@Com_Can_Extra14", _ComponentesExtraValores.Com_Can_Extra14);
                parametros.Add("@Com_Cod_Extra15", _ComponentesExtraValores.Com_Cod_Extra15);
                parametros.Add("@Com_Can_Extra15", _ComponentesExtraValores.Com_Can_Extra15);
                parametros.Add("@Com_Cod_Extra16", _ComponentesExtraValores.Com_Cod_Extra16);
                parametros.Add("@Com_Can_Extra16", _ComponentesExtraValores.Com_Can_Extra16);
                parametros.Add("@Usr_Mod", _ComponentesExtraValores.Usr_Mod);
                parametros.Add("@Codigo", 0);
                parametros.Add("@sMsj", "");

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Lb_Proceso_Colorantes_Componentes_Extra_U0001]"
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

        //ELIMINAR PROCESO VALOR
        public async Task<(int Codigo, string Mensaje)> DeshabilitarProcesoValor(ComponentesExtraValores _ComponentesExtraValores)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                //PARAMETROS ENTRADA
                parametros.Add("@Pro_Cod", _ComponentesExtraValores.Pro_Cod);
                parametros.Add("@Com_Cod_Con", _ComponentesExtraValores.Com_Cod_Con);
                parametros.Add("@Com_Ran_Ini", _ComponentesExtraValores.Com_Ran_Ini);
                parametros.Add("@Flg_Status", _ComponentesExtraValores.Flg_Status);
                parametros.Add("@Codigo", 0);
                parametros.Add("@sMsj", "");

                //PARAMETROS SALIDA
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                try
                {
                    //EJECUTAR EL STORED PROCEDURE
                    connection.Execute(
                        "[dbo].[PA_Lb_Proceso_Colorantes_Componentes_Extra_U0002]"
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

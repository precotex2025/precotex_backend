using Dapper;
using ic.backend.precotex.web.Entity.Entities.ReporteNC;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ic.backend.precotex.web.Entity.Entities.Personas;
using ic.backend.precotex.web.Data.Repositories.Implementation.Personas;
using ZXing;

namespace ic.backend.precotex.web.Data.Repositories.Personas
{
    public class TxPersonasRepository: ITxPersonasRepository
    {
        private readonly string _connectionString;
        private readonly string _connectionStringCamara;

        //DECLARAMOS CADENA DE CONEXION
        public TxPersonasRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnection")!;
            _connectionStringCamara = configuration.GetConnectionString("ConnectionCamaras")!;
        }

        //OBTENER NOMBRE X DNI
        public async Task<IEnumerable<Tx_Personas>?> ObtenerNombre(string Nro_Dni)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                parametros.Add("@Nro_Dni", Nro_Dni);

                var result = await connection.QueryAsync<Tx_Personas>(
                        "[dbo].[PA_TX_PERSONAS_FOTO_S0001]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                );

                foreach (var persona in result) { 
                    if (!string.IsNullOrEmpty(persona.Foto_Ruta) && File.Exists(persona.Foto_Ruta)) { 

                        byte[] bytes = File.ReadAllBytes(persona.Foto_Ruta); 

                        string base64 = Convert.ToBase64String(bytes); 

                        persona.FotoBase64 = "data:image/jpg;base64," + base64; 
                    } 
                }


                return result;
            }
        }

        //REGISTRAR DNI Y FOTO
        public async Task<(int Codigo, string Mensaje)> RegistrarDniFoto(Tx_Personas valores)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                parametros.Add("@Foto_Nro_Dni", valores.Foto_Nro_Dni);
                parametros.Add("@Foto_Ruta", valores.Foto_Ruta);
                parametros.Add("@Usr_Reg", valores.Usr_Reg);
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                try
                {
                    connection.Execute(
                        "[dbo].[PA_TX_PERSONAS_FOTO_I0001]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                        );
                }
                catch (Exception ex) 
                {

                }

                var Codigo = parametros.Get<int>("@Codigo");
                var Mensaje = parametros.Get<string>("@sMsj");

                return (Codigo, Mensaje);
            }
        }

        //ACTUALIZAR FOTO DEL DNI
        public async Task<(int Codigo, string Mensaje)> ActualizarDniFoto(Tx_Personas valores)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                parametros.Add("@Foto_Nro_Dni", valores.Foto_Nro_Dni);
                parametros.Add("@Foto_Ruta", valores.Foto_Ruta);
                parametros.Add("@Usr_Mod", valores.Usr_Mod);
                parametros.Add("@Codigo", 0);
                parametros.Add("@sMsj", "");
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255,direction: ParameterDirection.Output);

                try
                {
                    connection.Execute(
                        "[dbo].[PA_TX_PERSONAS_FOTO_U0001]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                        );
                }
                catch (Exception ex)
                {

                }

                var Codigo = parametros.Get<int>("@Codigo");
                var Mensaje = parametros.Get<string>("@sMsj");
                return (Codigo, Mensaje);
            }
        }

        public async Task<IEnumerable<Seg_Camara>?> ObtenerDatosRegistro(string Nro_Dni)
        {
            using (var connection = new SqlConnection(_connectionStringCamara))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                parametros.Add("@Nro_Dni", Nro_Dni);

                try
                {
                    var personas = await connection.QueryAsync<Seg_Camara>(
                        "[dbo].[PA_Seg_Camara_Marcacion_S0001]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                    );

                    foreach (var persona in personas)
                    {
                        if (!string.IsNullOrEmpty(persona.Foto_Ruta) && File.Exists(persona.Foto_Ruta))
                        {

                            byte[] bytes = File.ReadAllBytes(persona.Foto_Ruta);

                            string base64 = Convert.ToBase64String(bytes);

                            persona.FotoBase64 = "data:image/jpg;base64," + base64;
                        }
                    }
                    return personas;
                }
                catch
                {
                    return null;
                }
            }
        }





    }
}

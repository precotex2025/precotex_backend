using Dapper;
using ic.backend.precotex.web.Entity.Entities.RetiroRepuestos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ic.backend.precotex.web.Data.Repositories.Implementation.Login;
using ic.backend.precotex.web.Entity.Entities.Login;


namespace ic.backend.precotex.web.Data.Repositories.Login
{
    public class TxLoginRepository: ITxLoginRepository
    {
        private readonly string _connectionString;

        //DECLARAMOS CADENA DE CONEXION
        public TxLoginRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnection")!;
        }

        public async Task<IEnumerable<Tx_Login>?> GetUsuarioHabilitado(string Cod_Usuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Cod_Usuario = Cod_Usuario
                };
                var result = await connection.QueryAsync<Tx_Login>(
                        "[SEGURIDAD].[dbo].[Seg_ValidaUsuarioDeshabilitado_Web]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        public async Task<IEnumerable<Tx_Login>?> GetUsuarioWeb(string Cod_Usuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Cod_Usuario = Cod_Usuario
                };
                var result = await connection.QueryAsync<Tx_Login>(
                        "[SEGURIDAD].[dbo].[SG_VALIDAR_USUARIO_WEB]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }


    }
}

using Dapper;
using ic.backend.precotex.web.Data.Repositories.Implementation.Administracion.AccesoUsuario;
using ic.backend.precotex.web.Entity.Entities;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ic.backend.precotex.web.Data.Repositories.Administracion.AccesoUsuario
{
    public class AccesoUsuarioRepository : IAccesoUsuarioRepository
    {
        private readonly string _connectionString;
        private readonly string _connectionStringSeguridad;

        public AccesoUsuarioRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnection")!;
            _connectionStringSeguridad = configuration.GetConnectionString("TextilConnectionSeguridad")!;
        }

        public async Task<IEnumerable<ComboGral>?> ListarPerfilesLab()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<ComboGral>(
                    "[dbo].[Up_Listar_Usuarios_Acceso_WB]"
                    , commandType: CommandType.StoredProcedure
                    );
                return result;
            }
        }

        public async Task<(int Codigo, string Mensaje)> AsignarPerfilUsuarioLab(string Cod_Usuario, string Cod_PerfilUsuarioLab)
        {
            using (var connection = new SqlConnection(_connectionStringSeguridad))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();
                parametros.Add("@Cod_Usuario", Cod_Usuario);
                parametros.Add("@Cod_PerfilUsuarioLab", Cod_PerfilUsuarioLab);

                try
                {
                    await connection.ExecuteAsync(
                        "[dbo].[Up_RegistrarPerfilUsuarioLab]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                    );
                    return (0, "Operación realizada correctamente.");
                }
                catch (SqlException ex)
                {
                    return (ex.Number, ex.Message);
                }
            }
        }

        public async Task<(int Codigo, string Mensaje)> MantenimientoUsuarioLab(string Accion, string Cod_Usuario, string Nom_Usuario, string Password, string Tip_Trabajador, string Cod_Trabajador, string Acc_Cod)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();
                parametros.Add("@Accion", Accion);
                parametros.Add("@Cod_Usuario", Cod_Usuario);
                parametros.Add("@Nom_Usuario", Nom_Usuario);
                parametros.Add("@Password", Password);
                parametros.Add("@Tip_Trabajador", Tip_Trabajador);
                parametros.Add("@Cod_Trabajador", Cod_Trabajador);
                parametros.Add("@Acc_Cod", Acc_Cod);

                try
                {
                    await connection.ExecuteAsync(
                        "[dbo].[Up_Man_Lb_Usuarios_WB]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                    );
                    return (0, "Operación realizada correctamente.");
                }
                catch (SqlException ex)
                {
                    return (ex.Number, ex.Message);
                }
            }
        }
    }

}
using Dapper;
using ic.backend.precotex.web.Data.Repositories.Implementation.SecureNorm;
using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.SecureNorm
{
    public class SNPermisoRepository : ISNPermisoRepository
    {
        private readonly string _connectionString;

        public SNPermisoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnectionSomma")!;
        }

        public async Task<IEnumerable<SN_Permiso_Politica_Nivel>> ListarPoliticas()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<SN_Permiso_Politica_Nivel>(
                    "[dbo].[SN_Permisos_Politica_Listar]",
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public async Task<bool> GuardarPolitica(SN_Permiso_Politica_Nivel item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Modulo = item.Modulo,
                    Nivel = item.Nivel,
                    Accion = item.Accion,
                    Flg_Permitido = item.Flg_Permitido
                };
                await connection.ExecuteAsync(
                    "[dbo].[SN_Permisos_Politica_Guardar]",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );
                return true;
            }
        }

        public async Task<IEnumerable<SN_Permiso_Usuario_Modulo>> ListarUsuarioModulo(string sCodigo_Puesto_Usuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new { Codigo_Puesto_Usuario = sCodigo_Puesto_Usuario ?? "" };
                return await connection.QueryAsync<SN_Permiso_Usuario_Modulo>(
                    "[dbo].[SN_Permisos_Usuario_Modulo_Listar]",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public async Task<bool> GuardarUsuarioModulo(SN_Permiso_Usuario_Modulo item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Codigo_Puesto_Usuario = item.Codigo_Puesto_Usuario,
                    Modulo_Clave = item.Modulo_Clave,
                    Nivel_Acceso = item.Nivel_Acceso
                };
                await connection.ExecuteAsync(
                    "[dbo].[SN_Permisos_Usuario_Modulo_Guardar]",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );
                return true;
            }
        }

        public async Task<IEnumerable<SN_Permiso_Usuario_Detalle>> ListarUsuarioDetalle(string sCodigo_Puesto_Usuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new { Codigo_Puesto_Usuario = sCodigo_Puesto_Usuario ?? "" };
                return await connection.QueryAsync<SN_Permiso_Usuario_Detalle>(
                    "[dbo].[SN_Permisos_Usuario_Detalle_Listar]",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public async Task<bool> GuardarUsuarioDetalle(SN_Permiso_Usuario_Detalle item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parametros = new
                {
                    Codigo_Puesto_Usuario = item.Codigo_Puesto_Usuario,
                    Modulo = item.Modulo,
                    Contenido = item.Contenido,
                    Accion = item.Accion,
                    Flg_Permitido = item.Flg_Permitido
                };
                await connection.ExecuteAsync(
                    "[dbo].[SN_Permisos_Usuario_Detalle_Guardar]",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );
                return true;
            }
        }
    }
}

using Dapper;
using ic.backend.precotex.web.Data.Repositories.Implementation.SecureNorm;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.SecureNorm
{
    public class SNUsuarioRepository : ISNUsuarioRepository
    {
        private readonly string _connectionString;
        private readonly string _connectionStringTextil;

        public SNUsuarioRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TextilConnectionSomma")!;
            _connectionStringTextil = configuration.GetConnectionString("TextilConnection")!;
        }

        public async Task<IEnumerable<SN_Usuario>?> Listado(string? sCodigo_Organizacion, string? sCodigo_Sede, string? sCodigo_Proceso)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var rawList = await connection.QueryAsync<SN_Usuario>(
                     "[dbo].[SP_SN_USUARIO_LISTADO_PUESTOS]",
                     commandType: CommandType.StoredProcedure
                 );

                // Normalizar campos derivados para el listado integrado
                var list = new List<SN_Usuario>();
                foreach (var u in rawList)
                {
                    u.Id = !string.IsNullOrEmpty(u.Codigo_Puesto) ? ("P-" + u.Codigo_Puesto) : ("U-" + u.Id_Usuario);
                    u.Usuario = !string.IsNullOrEmpty(u.Nom_Usuario) ? u.Nom_Usuario : (u.Usuario ?? "â€”");
                    u.UserCode = !string.IsNullOrEmpty(u.Cod_Usuario) ? u.Cod_Usuario : (u.UserCode ?? "");
                    u.Proceso = !string.IsNullOrEmpty(u.Proceso_Nombre) ? u.Proceso_Nombre : (u.Proceso ?? "Sistemas");
                    u.Nivel = !string.IsNullOrEmpty(u.Nivel_Descripcion) ? u.Nivel_Descripcion : (u.Nivel ?? "Operativo");
                    if (u.Fecha_Registro_Puesto.HasValue)
                    {
                        u.Fecha_Registro = u.Fecha_Registro_Puesto.Value.ToString("dd/MM/yyyy");
                    }
                    else if (string.IsNullOrEmpty(u.Fecha_Registro))
                    {
                        u.Fecha_Registro = "-";
                    }
                    list.Add(u);
                }

                return list;
            }
        }

        public async Task<(int Codigo, string Mensaje)> ProcesoMnto(SN_Usuario sN_Usuario, string sTipoTransac)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();

                // Parametros de SQL
                parametros.Add("@Accion", sTipoTransac);
                parametros.Add("@Cod_Usuario", sN_Usuario.Cod_Usuario ?? "");
                parametros.Add("@Password", sN_Usuario.Password ?? "Precotex2026!");
                parametros.Add("@Nom_Usuario", sN_Usuario.Nom_Usuario ?? "");
                parametros.Add("@Cod_Rol", sN_Usuario.Cod_Rol.HasValue ? sN_Usuario.Cod_Rol.Value.ToString() : "2");
                parametros.Add("@Des_Rol", sN_Usuario.Des_Rol ?? "");
                parametros.Add("@Cod_Empresa", string.IsNullOrEmpty(sN_Usuario.Cod_Empresa) ? "01" : sN_Usuario.Cod_Empresa);
                parametros.Add("@Empresa", string.IsNullOrEmpty(sN_Usuario.Empresa) ? "PRECOTEX S.A.C." : sN_Usuario.Empresa);
                parametros.Add("@Tip_Trabajador", string.IsNullOrEmpty(sN_Usuario.Tip_Trabajador) ? "E" : sN_Usuario.Tip_Trabajador);
                parametros.Add("@Cod_Trabajador", sN_Usuario.Cod_Trabajador ?? "");
                parametros.Add("@Email", sN_Usuario.Email ?? "");
                parametros.Add("@Denominacion", sN_Usuario.Denominacion ?? sN_Usuario.Puesto ?? "");
                parametros.Add("@Codigo_Proceso", string.IsNullOrEmpty(sN_Usuario.Codigo_Proceso) ? "005" : sN_Usuario.Codigo_Proceso);
                parametros.Add("@Codigo_Nivel", string.IsNullOrEmpty(sN_Usuario.Codigo_Nivel) ? "003" : sN_Usuario.Codigo_Nivel);
                parametros.Add("@Id_Usuario", sN_Usuario.Id_Usuario);
                parametros.Add("@Flg_Activo", sN_Usuario.Flg_Activo == false ? 0 : 1, DbType.Int32);
                parametros.Add("@Estado", string.IsNullOrEmpty(sN_Usuario.Estado) ? "Activo" : sN_Usuario.Estado);
                parametros.Add("@Primer_Ingreso", sN_Usuario.Primer_Ingreso == false ? 0 : 1, DbType.Int32);

                // ParÃ¡metros de salida
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                // Ejecutar el procedimiento almacenado
                try
                {
                    connection.Execute(
                        "[dbo].[SP_SN_USUARIO_MANTENIMIENTO]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );
                }
                catch (Exception ex) { }

                // Obtener los valores de salida
                var codigo = parametros.Get<int>("@Codigo");
                var mensaje = parametros.Get<string>("@sMsj");

                return (codigo, mensaje);
            }
        }

        public async Task<IEnumerable<SN_Trabajador_Spring>?> ListadoTrabajadoresSpring()
        {
            var fallback = new List<SN_Trabajador_Spring>();
            //{
            //    new SN_Trabajador_Spring { Tipo = "E", Codigo = "4996", NombreCompleto = "MEDINA MURAYARI, HENRY", Correo = "hmedina@precotexperu.com", Cargo = "Analista de Negocios" },
            //    new SN_Trabajador_Spring { Tipo = "E", Codigo = "2741", NombreCompleto = "ABAD VICENTE, PEDRO EULER", Correo = "fhuamani@precotexperu.com", Cargo = "Almacenero" },
            //    new SN_Trabajador_Spring { Tipo = "E", Codigo = "2315", NombreCompleto = "MACHA MORENO, ERIKA LIZET", Correo = "emacha@precotexperu.com", Cargo = "Asistente SIG" },
            //    new SN_Trabajador_Spring { Tipo = "E", Codigo = "2150", NombreCompleto = "ROMERO HUAMAN, ALBERT MELVIN", Correo = "aromero@precotexperu.com", Cargo = "Analista de Seguridad" },
            //    new SN_Trabajador_Spring { Tipo = "E", Codigo = "1001", NombreCompleto = "HUAMANI QUISPE, FERNANDO", Correo = "fhuamani@precotexperu.com", Cargo = "Analista SIG" },
            //    new SN_Trabajador_Spring { Tipo = "E", Codigo = "1002", NombreCompleto = "SORIA PAREDES, MAX", Correo = "msoria@precotexperu.com", Cargo = "Analista de Sistemas" },
            //    new SN_Trabajador_Spring { Tipo = "E", Codigo = "1003", NombreCompleto = "FLORES CHAVEZ, KAREM", Correo = "kflores@precotexperu.com", Cargo = "Gerente Comercial" },
            //    new SN_Trabajador_Spring { Tipo = "E", Codigo = "1004", NombreCompleto = "ALDANA GARCIA, LUIS", Correo = "laldana@precotexperu.com", Cargo = "Jefe SSOMA" },
            //    new SN_Trabajador_Spring { Tipo = "E", Codigo = "1005", NombreCompleto = "HUARANGA SAYDA", Correo = "shuaranga@precotexperu.com", Cargo = "Supervisora SST" }
            //};

            try
            {
                //string springConnStr = "Server=192.168.1.86;Database=SPRING_PRODUCCION;User Id=sa;Password=sa;TrustServerCertificate=True;Timeout=5;";
                using (var conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    var list = await conn.QueryAsync<SN_Trabajador_Spring>(
                        "[dbo].[UP_MuestraDatosTrabajador]",
                        commandType: CommandType.StoredProcedure
                    );
                    if (list != null && list.Any()) return list;
                }
            }
            catch { }

            return fallback;
        }

        public async Task<IEnumerable<ComboGral>?> ListadoNivelJerarquico()
        {
            var fallback = new List<ComboGral>();
            //{
               // new SN_Nivel_Jerarquico { Codigo_Nivel = "001", Descripcion_Nivel = "Gerencial", Nivel = "Gerencial", Flg_Activo = true },
               // new SN_Nivel_Jerarquico { Codigo_Nivel = "002", Descripcion_Nivel = "Jefatura / Mando Medio", Nivel = "Jefatura", Flg_Activo = true },
               // new SN_Nivel_Jerarquico { Codigo_Nivel = "003", Descripcion_Nivel = "Operativo", Nivel = "Operativo", Flg_Activo = true }
            //};

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    var list = await conn.QueryAsync<ComboGral>(
                        "[dbo].[SN_Nivel_Jerarquico_Listado]",
                        commandType: CommandType.StoredProcedure
                    );
                    if (list != null && list.Any()) return list;
                }
            }
            catch { }

            return fallback;
        }

        public async Task<IEnumerable<SN_Log_Acceso>?> ListadoLogAccesos(int top, bool soloUltimo)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = @"SELECT TOP (@Top) 
                                    Id_Log as Id, 
                                    Cod_Usuario, 
                                    Nom_Usuario, 
                                    Puesto, 
                                    Cod_Rol, 
                                    CONVERT(VARCHAR(19), Fec_Acceso, 120) AS Fec_Acceso, 
                                    Ip_Acceso, 
                                    Estado, 
                                    Flg_Activo 
                                 FROM dbo.SN_Log_Acceso WITH (NOLOCK)
                                 WHERE Flg_Activo = 1
                                 ORDER BY Fec_Acceso DESC";

                return await conn.QueryAsync<SN_Log_Acceso>(query, new { Top = top > 0 ? top : 20 });
            }
        }

        public async Task<(int Codigo, string Mensaje)> RegistrarLogAcceso(SN_Log_Acceso log)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = @"INSERT INTO dbo.SN_Log_Acceso (
                                    Cod_Usuario, Nom_Usuario, Puesto, Cod_Rol, 
                                    Fec_Acceso, Ip_Acceso, Estado, Flg_Activo
                                 ) VALUES (
                                    @Cod_Usuario, @Nom_Usuario, @Puesto, @Cod_Rol, 
                                    GETDATE(), @Ip_Acceso, @Estado, 1
                                 )";

                await conn.ExecuteAsync(query, new
                {
                    Cod_Usuario = log.Cod_Usuario ?? "",
                    Nom_Usuario = log.Nom_Usuario ?? "",
                    Puesto = log.Puesto ?? "",
                    Cod_Rol = log.Cod_Rol ?? "",
                    Ip_Acceso = log.Ip_Acceso ?? "192.168.1.36",
                    Estado = log.Estado ?? "Inicio de sesion"
                });

                return (1, "Log de acceso registrado exitosamente.");
            }
        }

        public async Task<(int Codigo, string Mensaje)> EnviarCredencialesCorreo(string destinatario, string nombre, string usuario, string claveTemporal)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@Email", destinatario);
            parametros.Add("@Usuario", usuario);
            parametros.Add("@Contrasena", claveTemporal);
            parametros.Add("@Nombre", nombre);

            // 1. Intentar con BDSecureNorm (192.168.1.139, perfil Database Mail ALERTAS)
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    await conn.ExecuteAsync("dbo.Sp_Envia_Correo_Alerta_Usuario", parametros, commandType: CommandType.StoredProcedure);
                    return (1, $"Correo de credenciales enviado exitosamente a {destinatario}");
                }
            }
            catch (Exception ex1)
            {
                // 2. Fallback con servidor PRECOTEX_TEXTIL (192.168.1.9, perfil SQL MAIL)
                try
                {
                    string fallbackConn = "Data Source=192.168.1.9;Initial Catalog=PRECOTEX_TEXTIL;Integrated Security=True;";
                    using (var conn2 = new SqlConnection(fallbackConn))
                    {
                        await conn2.OpenAsync();
                        await conn2.ExecuteAsync("dbo.Sp_Envia_Correo_Alerta_Usuario", parametros, commandType: CommandType.StoredProcedure);
                        return (1, $"Correo de credenciales enviado exitosamente a {destinatario}");
                    }
                }
                catch (Exception ex2)
                {
                    return (0, $"Error al enviar correo: {ex1.Message} | Fallback: {ex2.Message}");
                }
            }
        }

        public async Task<(int Codigo, string Mensaje)> CambiarPasswordPrimerIngreso(string codUsuario, string password)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var parametros = new DynamicParameters();
                parametros.Add("@Accion", "P");
                parametros.Add("@Cod_Usuario", codUsuario);
                parametros.Add("@Password", password);
                parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                await conn.ExecuteAsync("dbo.SP_SN_USUARIO_MANTENIMIENTO", parametros, commandType: CommandType.StoredProcedure);

                var codigo = parametros.Get<int>("@Codigo");
                var mensaje = parametros.Get<string>("@sMsj");

                return (codigo > 0 ? codigo : 1, string.IsNullOrEmpty(mensaje) ? "Contrasena actualizada exitosamente." : mensaje);
            }
        }

        public async Task<SN_Validar_Primer_Ingreso?> ValidarPrimerIngreso(string codUsuario)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = @"SELECT TOP 1 
                                    Cod_Usuario,
                                    Nom_Usuario,
                                    ISNULL(Estado, 'Activo') AS Estado,
                                    ISNULL(Primer_Ingreso, 0) AS Primer_Ingreso
                                 FROM dbo.SN_Usuario WITH (NOLOCK)
                                 WHERE Cod_Usuario = @Cod_Usuario AND Flg_Activo = 1";

                var user = await conn.QueryFirstOrDefaultAsync<dynamic>(query, new { Cod_Usuario = codUsuario });
                if (user != null)
                {
                    string estado = user.Estado ?? "Activo";
                    bool primerIngreso = Convert.ToBoolean(user.Primer_Ingreso);
                    bool requiereCambio = primerIngreso || estado.Equals("Pendiente de activacion", StringComparison.OrdinalIgnoreCase);

                    return new SN_Validar_Primer_Ingreso
                    {
                        Cod_Usuario = user.Cod_Usuario,
                        Nom_Usuario = user.Nom_Usuario,
                        Estado = estado,
                        Primer_Ingreso = primerIngreso,
                        RequiereCambioPassword = requiereCambio
                    };
                }

                return null;
            }
        }
    }
}


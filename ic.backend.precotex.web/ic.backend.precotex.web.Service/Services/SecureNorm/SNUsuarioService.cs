using ic.backend.precotex.web.Data.Repositories.Implementation.SecureNorm;
using ic.backend.precotex.web.Data.Repositories.SecureNorm;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.SecureNorm;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.SecureNorm
{
    public class SNUsuarioService : ISNUsuarioService
    {
        private readonly ISNUsuarioRepository _sNUsuarioRepository;

        public SNUsuarioService(ISNUsuarioRepository sNUsuarioRepository)
        {
            _sNUsuarioRepository = sNUsuarioRepository;
        }

        public async Task<ServiceResponseList<SN_Usuario>?> Listado(string? sCodigo_Organizacion, string? sCodigo_Sede, string? sCodigo_Proceso)
        {
            var result = new ServiceResponseList<SN_Usuario>();
            try
            {
                var resultData = await _sNUsuarioRepository.Listado(sCodigo_Organizacion, sCodigo_Sede, sCodigo_Proceso);
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No existe informaciÃ³n";
                    return result;
                }

                result.Success = true;
                result.Elements = resultData.ToList();
                result.TotalElements = resultData.ToList().Count();
                return result;
            }
            catch (SqlException sql)
            {
                result.Message = "Error en Servidor: " + sql.Message;
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Ocurrio una excepciÃ³n" + ex.Message;
                return result;
            }
        }

        public async Task<ServiceResponse<int>> ProcesoMnto(SN_Usuario sN_Usuario, string sTipoTransac)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _sNUsuarioRepository.ProcesoMnto(sN_Usuario, sTipoTransac);
                if (resultData.Codigo > 0)
                {
                    result.Message = resultData.Mensaje;
                    result.Success = true;
                    result.CodeTransacc = resultData.Codigo;

                    return result;
                }

                result.Message = resultData.Mensaje;
                result.Success = false;
                return result;
            }
            catch (SqlException sql)
            {
                result.Message = "Error en Servidor: " + sql.Message;
                result.Success = false;
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Ocurrio una excepciÃ³n" + ex.Message;
                result.Success = false;
                return result;
            }
        }

        public async Task<ServiceResponseList<SN_Trabajador_Spring>?> ListadoTrabajadoresSpring()
        {
            var result = new ServiceResponseList<SN_Trabajador_Spring>();
            try
            {
                var data = await _sNUsuarioRepository.ListadoTrabajadoresSpring();
                var list = data?.ToList() ?? new List<SN_Trabajador_Spring>();
                result.Success = true;
                result.Elements = list;
                result.TotalElements = list.Count;
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Success = false;
                return result;
            }
        }

        public async Task<ServiceResponseList<ComboGral>?> ListadoNivelJerarquico()
        {
            var result = new ServiceResponseList<ComboGral>();
            try
            {
                var data = await _sNUsuarioRepository.ListadoNivelJerarquico();
                var list = data?.ToList() ?? new List<ComboGral>();
                result.Success = true;
                result.Elements = list;
                result.TotalElements = list.Count;
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Success = false;
                return result;
            }
        }

        public async Task<ServiceResponseList<SN_Log_Acceso>?> ListadoLogAccesos(int top, bool soloUltimo)
        {
            var result = new ServiceResponseList<SN_Log_Acceso>();
            try
            {
                var data = await _sNUsuarioRepository.ListadoLogAccesos(top, soloUltimo);
                var list = data?.ToList() ?? new List<SN_Log_Acceso>();
                result.Success = true;
                result.Elements = list;
                result.TotalElements = list.Count;
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Success = false;
                return result;
            }
        }

        public async Task<ServiceResponse<int>> RegistrarLogAcceso(SN_Log_Acceso log)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var res = await _sNUsuarioRepository.RegistrarLogAcceso(log);
                result.Success = res.Codigo > 0;
                result.Message = res.Mensaje;
                result.CodeTransacc = res.Codigo;
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Success = false;
                return result;
            }
        }

        public async Task<ServiceResponse<bool>> EnviarCredencialesCorreo(string destinatario, string nombre, string usuario, string claveTemporal)
        {
            var result = new ServiceResponse<bool>();
            try
            {
                var res = await _sNUsuarioRepository.EnviarCredencialesCorreo(destinatario, nombre, usuario, claveTemporal);
                result.Success = res.Codigo > 0;
                result.Message = res.Mensaje;
                result.Element = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Aviso al enviar correo: " + ex.Message;
                result.Success = false;
                return result;
            }
        }

        public async Task<ServiceResponse<int>> CambiarPasswordPrimerIngreso(string codUsuario, string password)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var res = await _sNUsuarioRepository.CambiarPasswordPrimerIngreso(codUsuario, password);
                result.Success = res.Codigo > 0;
                result.Message = res.Mensaje;
                result.CodeTransacc = res.Codigo;
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Success = false;
                return result;
            }
        }

        public async Task<ServiceResponse<SN_Validar_Primer_Ingreso>?> ValidarPrimerIngreso(string codUsuario)
        {
            var result = new ServiceResponse<SN_Validar_Primer_Ingreso>();
            try
            {
                var data = await _sNUsuarioRepository.ValidarPrimerIngreso(codUsuario);
                if (data != null)
                {
                    result.Success = true;
                    result.Element = data;
                    result.Message = "Usuario validado con exito";
                }
                else
                {
                    result.Success = false;
                    result.Message = "Usuario no encontrado";
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Success = false;
                return result;
            }
        }
    }
}
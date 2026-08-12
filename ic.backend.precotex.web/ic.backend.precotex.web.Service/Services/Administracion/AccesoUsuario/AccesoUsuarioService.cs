using ic.backend.precotex.web.Data.Repositories.Implementation.Administracion.AccesoUsuario;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.Administracion.AccesoUsuario;
using System.Data.SqlClient;

namespace ic.backend.precotex.web.Service.Services.Administracion.AccesoUsuario
{
    public class AccesoUsuarioService : IAccesoUsuarioService
    {
        private readonly IAccesoUsuarioRepository _repository;

        public AccesoUsuarioService(IAccesoUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponseList<ComboGral>?> ListarPerfilesLab()
        {
            var result = new ServiceResponseList<ComboGral>();
            try
            {
                var resultData = await _repository.ListarPerfilesLab();
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No existe información";
                }
                result.Success = true;
                result.Message = "Completado con éxito";
                result.Elements = resultData.ToList();
                result.TotalElements = resultData.ToList().Count();
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Excepción no controlada " + ex.Message;
                return result;
            }
        }

        public async Task<ServiceResponse<int>> AsignarPerfilUsuarioLab(string Cod_Usuario, string Cod_PerfilUsuarioLab)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _repository.AsignarPerfilUsuarioLab(Cod_Usuario, Cod_PerfilUsuarioLab);
                if (resultData.Codigo == 0)
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
                result.Message = "Ocurrio una excepción" + ex.Message;
                result.Success = false;
                return result;
            }
        }

        public async Task<ServiceResponse<int>> MantenimientoUsuarioLab(string Accion, string Cod_Usuario, string Nom_Usuario, string Password, string Tip_Trabajador, string Cod_Trabajador, string Acc_Cod)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _repository.MantenimientoUsuarioLab(Accion, Cod_Usuario, Nom_Usuario, Password, Tip_Trabajador, Cod_Trabajador, Acc_Cod);
                if (resultData.Codigo == 0)
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
                result.Message = "Ocurrio una excepción" + ex.Message;
                result.Success = false;
                return result;
            }
        }

        




    }
}

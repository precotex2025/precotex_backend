using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.common;

namespace ic.backend.precotex.web.Service.Services.Implementacion.Administracion.AccesoUsuario
{
    public interface IAccesoUsuarioService
    {
        Task<ServiceResponseList<ComboGral>?> ListarPerfilesLab();
        Task<ServiceResponse<int>> AsignarPerfilUsuarioLab(string Cod_Usuario, string Cod_PerfilUsuarioLab);
        Task<ServiceResponse<int>> MantenimientoUsuarioLab(string Accion, string Cod_Usuario, string Nom_Usuario, string Password, string Tip_Trabajador, string Cod_Trabajador, string Acc_Cod);
    }
}

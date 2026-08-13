
using ic.backend.precotex.web.Entity.Entities;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.Administracion.AccesoUsuario
{
    public interface IAccesoUsuarioRepository
    {
        Task<IEnumerable<ComboGral>?> ListarPerfilesLab();
        Task<(int Codigo, string Mensaje)> AsignarPerfilUsuarioLab(string Cod_Usuario, string Cod_PerfilUsuarioLab);
        Task<(int Codigo, string Mensaje)> MantenimientoUsuarioLab(string Accion, string Cod_Usuario, string Nom_Usuario, string Password, string Tip_Trabajador, string Cod_Trabajador, string Acc_Cod);
    }
}

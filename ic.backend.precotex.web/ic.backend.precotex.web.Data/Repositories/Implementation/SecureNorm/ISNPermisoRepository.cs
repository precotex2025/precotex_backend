using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.SecureNorm
{
    public interface ISNPermisoRepository
    {
        Task<IEnumerable<SN_Permiso_Politica_Nivel>> ListarPoliticas();
        Task<bool> GuardarPolitica(SN_Permiso_Politica_Nivel item);
        Task<IEnumerable<SN_Permiso_Usuario_Modulo>> ListarUsuarioModulo(string sCodigo_Puesto_Usuario);
        Task<bool> GuardarUsuarioModulo(SN_Permiso_Usuario_Modulo item);
        Task<IEnumerable<SN_Permiso_Usuario_Detalle>> ListarUsuarioDetalle(string sCodigo_Puesto_Usuario);
        Task<bool> GuardarUsuarioDetalle(SN_Permiso_Usuario_Detalle item);
    }
}

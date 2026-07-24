using ic.backend.precotex.web.Data.Repositories.Implementation.SecureNorm;
using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Service.Services.Implementacion.SecureNorm;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.SecureNorm
{
    public class SNPermisoService : ISNPermisoService
    {
        private readonly ISNPermisoRepository _repository;

        public SNPermisoService(ISNPermisoRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<SN_Permiso_Politica_Nivel>> ListarPoliticas()
        {
            return _repository.ListarPoliticas();
        }

        public Task<bool> GuardarPolitica(SN_Permiso_Politica_Nivel item)
        {
            return _repository.GuardarPolitica(item);
        }

        public Task<IEnumerable<SN_Permiso_Usuario_Modulo>> ListarUsuarioModulo(string sCodigo_Puesto_Usuario)
        {
            return _repository.ListarUsuarioModulo(sCodigo_Puesto_Usuario);
        }

        public Task<bool> GuardarUsuarioModulo(SN_Permiso_Usuario_Modulo item)
        {
            return _repository.GuardarUsuarioModulo(item);
        }

        public Task<IEnumerable<SN_Permiso_Usuario_Detalle>> ListarUsuarioDetalle(string sCodigo_Puesto_Usuario)
        {
            return _repository.ListarUsuarioDetalle(sCodigo_Puesto_Usuario);
        }

        public Task<bool> GuardarUsuarioDetalle(SN_Permiso_Usuario_Detalle item)
        {
            return _repository.GuardarUsuarioDetalle(item);
        }
    }
}

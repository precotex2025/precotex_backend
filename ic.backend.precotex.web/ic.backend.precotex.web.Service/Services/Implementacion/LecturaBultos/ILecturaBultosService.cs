using ic.backend.precotex.web.Entity;
using ic.backend.precotex.web.Service.common;

namespace ic.backend.precotex.web.Service;

public interface ILecturaBultosService
{
    Task<ServiceResponseList<Lg_LecturaBultos_Almacenes>?> ListarAlmacenesDisponibles();
}

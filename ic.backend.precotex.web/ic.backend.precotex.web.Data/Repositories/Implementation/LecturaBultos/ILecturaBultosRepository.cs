using ic.backend.precotex.web.Entity;

namespace ic.backend.precotex.web.Data;

public interface ILecturaBultosRepository
{
    Task<IEnumerable<Lg_LecturaBultos_Almacenes>?> ListarAlmacenesDisponibles();
}

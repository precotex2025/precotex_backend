using ic.backend.precotex.web.Entity;
using ic.backend.precotex.web.Service.common;

namespace ic.backend.precotex.web.Service;

public interface ILecturaBultosService
{
    Task<ServiceResponseList<Lg_LecturaBultos_Almacenes>?> ListarAlmacenesDisponibles();
    Task<ServiceResponseList<Lg_LecturaBultos>?> ListarMovimientos(string? Num_MovStk, string? Cod_Almacen, DateTime? Fec_MovStk);
    Task<ServiceResponseList<Lg_Bultos>?> ListarBultos(string? Num_MovStk, string? Cod_Almacen);
    Task<ServiceResponse<int>> LecturarBulto(Lg_Bultos valores);
}

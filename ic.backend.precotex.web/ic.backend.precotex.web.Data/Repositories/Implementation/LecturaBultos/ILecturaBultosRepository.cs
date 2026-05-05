using ic.backend.precotex.web.Entity;

namespace ic.backend.precotex.web.Data;

public interface ILecturaBultosRepository
{
    Task<IEnumerable<Lg_LecturaBultos_Almacenes>?> ListarAlmacenesDisponibles();
    Task<IEnumerable<Lg_LecturaBultos>?> ListarMovimientos(string? Num_MovStk, string? Cod_Almacen, DateTime? Fec_MovStk);
    Task<IEnumerable<Lg_Bultos>?> ListarBultos(string? Num_MovStk, string? Cod_Almacen);
    Task<(int Codigo, string Mensaje)> LecturarBulto(Lg_Bultos valores);
}

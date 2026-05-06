using ic.backend.precotex.web.Entity;

namespace ic.backend.precotex.web.Data;

public interface ILecturaBultosRepository
{
    Task<IEnumerable<Lg_LecturaBultos_Almacenes>?> ListarAlmacenesDisponibles();
    Task<IEnumerable<Lg_LecturaBultos>?> ListarMovimientos(string? Cod_Almacen, string? Num_MovStk, string? Fec_MovStk, string? Flg_Pendiente);
    Task<IEnumerable<Lg_Bultos>?> ListarBultos(string? Num_MovStk, string? Cod_Almacen);
    Task<(int Codigo, string Mensaje)> LecturarBulto(Lg_Bultos valores);
}

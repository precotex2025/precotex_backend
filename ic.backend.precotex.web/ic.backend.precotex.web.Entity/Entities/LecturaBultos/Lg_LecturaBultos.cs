namespace ic.backend.precotex.web.Entity;

public class Lg_LecturaBultos
{
    public DateTime? Fec_MovStk { get; set; }
    public string? Num_MovStk { get; set; }
    public int? Can_Bulto { get; set; }
    public decimal? Peso_Neto { get; set; }
    public string? Flg_Lecturado { get; set; }
}

public class Lg_LecturaBultos_Almacenes
{
    public string? Codigo { get; set; }
    public string? Descripcion { get; set; }
}

public class Lg_Bultos
{
    public string? Num_MovStk { get; set; }
    public string? Cod_Almacen { get; set; }
    public string? Num_Corre { get; set; }
    public decimal? Peso_Neto { get; set; }
    public DateTime? Fec_Registro { get; set; }
    public string? Cod_Usuario { get; set; }
    public string? Flg_Lecturado { get; set; }
}
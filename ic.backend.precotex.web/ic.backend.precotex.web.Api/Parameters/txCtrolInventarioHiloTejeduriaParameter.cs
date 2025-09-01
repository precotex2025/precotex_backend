using ic.backend.precotex.web.Entity.Entities;

namespace ic.backend.precotex.web.Api.Parameters
{
    public class txCtrolInventarioHiloTejeduriaParameter
    {
        public string Tipo { get; set; } = null!;
        public string? Lote { get; set; } = null!;
        public string? Num_Semana { get; set; } = null!;
        public string? Titulo { get; set; } = null!;
        public string? Ser_OrdComp { get; set; } = null!;
        public string? Cod_OrdComp { get; set; } = null!;
        public string? Color { get; set; } = null!;
        public string? Hilo_Tipo { get; set; } = null!;
        public string? Hilo_Codigo { get; set; } = null!;
        public string? Ubicacion { get; set; } = null!;

        public decimal? Cantidad_Cono { get; set; } = null!;
        public decimal? Peso_Tara { get; set; } = null!;
        public decimal? Peso_Bruto { get; set; } = null!;
        public decimal? Peso_Neto { get; set; } = null!;
        public decimal? Pallet { get; set; } = null!;
        public decimal? Diferencia { get; set; } = null!;

        public string? Observacion { get; set; } = null!;
        public string? Proveedor { get; set; } = null!;
        public string? Usu_Registro { get; set; } = null!;
        //public Tx_Ctrol_Inventario_Hilo_Tejeduria tx_Ctrol_Inventario_Hilo_Tejeduria { get; set; } = null!;
    }
}

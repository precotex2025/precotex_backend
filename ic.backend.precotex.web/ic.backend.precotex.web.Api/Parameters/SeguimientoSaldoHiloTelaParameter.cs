namespace ic.backend.precotex.web.Api.Parameters
{
    public class SeguimientoSaldoHiloTelaParameter
    {
        public string? Accion { get; set; }
        public string? Num_Traslado { get; set; }
        public string? Cod_OrdProv { get; set; }
        public string? Cod_Ordtra_Ori { get; set; }
        public string? Cod_Maquina_Ori { get; set; }
        public string? Cod_HilTel { get; set; }
        public string? Cod_Color { get; set; }
        public decimal? Kg_Programado { get; set; }
        public decimal? Kg_Salida { get; set; }
        public decimal? Kg_Consumo { get; set; }
        public decimal? Kg_Devolver { get; set; }
        public string? Estado { get; set; }
        public string? Cod_Ordtra_Des { get; set; }
        public string? Cod_Maquina_Des { get; set; }
        public string? Cod_Usuario { get; set; }
    }
}

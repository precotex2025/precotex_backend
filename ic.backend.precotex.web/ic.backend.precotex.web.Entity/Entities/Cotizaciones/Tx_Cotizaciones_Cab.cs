
namespace ic.backend.precotex.web.Entity.Entities.Cotizaciones
{
    public class Tx_Cotizaciones_Cab
    {
        public int IdCotizacion_Cab { get; set; }
        public int? Num_Cotizacion { get; set; }
        public int? Num_Version { get; set; }
        public int Id_Unidad_NegocioKey { get; set; }
        public string? Cod_Tipo_Orden_tinto { get; set; } = string.Empty;
        public string? Cod_Cliente_Tex { get; set; } = string.Empty;
        public string? Cod_Tela { get; set; } = string.Empty;
        public string? Cod_Ruta { get; set; } = string.Empty;
        public string? Cod_Color { get; set; } = string.Empty;
        public string? Cod_RecetaAcabado { get; set; } = string.Empty;
        public int Tiempo_Referencia { get; set; }
        public decimal Precio_Referencia { get; set; }
        public string? SDC_Referencia { get; set; } = string.Empty;
        public string? Flg_Estatus { get; set; } = string.Empty;
        public DateTime Fec_Registro { get; set; }
        public string Usu_Registro { get; set; } = string.Empty;
        public DateTime? Fec_Modifica { get; set; }
        public string? Usu_Modifica { get; set; }
        public string? Cod_Equipo { get; set; }
        public List<Tx_Cotizaciones_Det> Detalles { get; set; } = new List<Tx_Cotizaciones_Det>();
    }
}

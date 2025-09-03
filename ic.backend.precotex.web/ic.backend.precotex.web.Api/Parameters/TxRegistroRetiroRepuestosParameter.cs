namespace ic.backend.precotex.web.Api.Parameters
{
    public class TxRegistroRetiroRepuestosParameter
    {
        public int Num_Requerimiento { get; set; }
        public DateTime? Fec_Requerimiento { get; set; }
        public string? Cod_Seguridad { get; set; }
        public string? Cod_Mantenimiento { get; set; }
        public string? Nro_Precinto_Apertura { get; set; }
        public string? Nro_Precinto_Cierre { get; set; }     
    }

    public class TxRegistroRetiroRepuestosParameter_Detalle
    {
        
    }
}

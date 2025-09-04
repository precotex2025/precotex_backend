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
        public int Num_Requerimiento { get; set; }
        public int Nro_Secuencia { get; set; }
        public string? Itm_Codigo { get; set; }
        public string? Itm_Descripcion { get; set; }
        public double? Itm_Cantidad { get; set; }
        public string? Itm_Unidad_Medida { get; set; }
        public string? Rpt_Cambio { get; set; }
        public string? Itm_Foto { get; set; }
    }
}

namespace ic.backend.precotex.web.Entity.Entities.SecureNorm
{
    public class SN_Proveedor_ConfigPeso
    {
        public int Id_Area { get; set; }
        public string Des_Area { get; set; } = string.Empty;
        public decimal Num_Peso { get; set; } = 1.00m;
        public decimal Num_Umbral_Minimo { get; set; } = 14.00m;
        public bool Flg_Activo { get; set; } = true;
        public string? Cod_Usuario { get; set; }
    }
}

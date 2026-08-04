namespace ic.backend.precotex.web.Entity.Entities.SecureNorm
{
    public class SN_Permiso_Usuario_Modulo
    {
        public int Id_Acceso_Modulo { get; set; }
        public string Codigo_Puesto_Usuario { get; set; } = null!;
        public string Modulo_Clave { get; set; } = null!;
        public string Nivel_Acceso { get; set; } = null!;
        public DateTime? Fec_Modificacion { get; set; }
    }
}

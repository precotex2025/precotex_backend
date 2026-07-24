namespace ic.backend.precotex.web.Entity.Entities.SecureNorm
{
    public class SN_Permiso_Usuario_Detalle
    {
        public int Id_Permiso_Detalle { get; set; }
        public string Codigo_Puesto_Usuario { get; set; } = null!;
        public string Modulo { get; set; } = null!;
        public string Contenido { get; set; } = null!;
        public string Accion { get; set; } = null!;
        public bool Flg_Permitido { get; set; }
        public DateTime? Fec_Modificacion { get; set; }
    }
}

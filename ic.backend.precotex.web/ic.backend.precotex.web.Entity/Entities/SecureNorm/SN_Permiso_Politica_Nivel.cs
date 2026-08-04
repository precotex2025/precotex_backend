namespace ic.backend.precotex.web.Entity.Entities.SecureNorm
{
    public class SN_Permiso_Politica_Nivel
    {
        public int Id_Politica { get; set; }
        public string Modulo { get; set; } = null!;
        public string Nivel { get; set; } = null!;
        public string Accion { get; set; } = null!;
        public bool Flg_Permitido { get; set; }
        public DateTime? Fec_Modificacion { get; set; }
    }
}

using System;

namespace ic.backend.precotex.web.Entity.Entities.SecureNorm
{
    public class SN_Req_Legal
    {
        public int Id_Req { get; set; }
        public string? Codigo { get; set; }
        public string? Requisito { get; set; }
        public string? Ambito { get; set; }
        public string? Tipo { get; set; }
        public string? Norma { get; set; }
        public string? Entidad { get; set; }
        public string? Obligacion { get; set; }
        public string? Estado { get; set; }
        public string? Responsable { get; set; }
        public DateTime? Evaluacion { get; set; }
        public DateTime? Proxeval { get; set; }
        public DateTime? Vencimiento { get; set; }
        public string? Evidencia { get; set; }
        public string? Usuario_Registro { get; set; }
        public DateTime? Fecha_Registro { get; set; }
        public bool? flg_Activo { get; set; }
    }
}

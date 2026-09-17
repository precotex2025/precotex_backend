using System;

namespace ic.backend.precotex.web.Entity.Entities.SecureNorm
{
    public class SN_Req_Legal
    {
        public int Id { get; set; }
        public int Id_Req { get; set; }
        public string? Codigo { get; set; }
        public string? Item { get; set; }
        public string? Requisito { get; set; }
        public string? Tema { get; set; }
        public string? Ambito { get; set; }
        public string? Tipo { get; set; }
        public string? Norma { get; set; }
        public string? Articulo { get; set; }
        public string? Entidad { get; set; }
        public string? Obligacion { get; set; }
        public string? Evidenciadoc { get; set; }
        public string? Estado { get; set; }
        public string? Responsable { get; set; }
        public string? Frecuencia { get; set; }
        public string? Evaluacion { get; set; }
        public string? Proxeval { get; set; }
        public string? Vencimiento { get; set; }
        public string? Observaciones { get; set; }
        public string? Evidencia { get; set; }
        public string? Usuario_Registro { get; set; }
        public string? Flg_Estado { get; set; }
        public DateTime? Fecha_Registro { get; set; }
        public DateTime? Fec_Creacion { get; set; }
        public bool? flg_Activo { get; set; }
    }
}

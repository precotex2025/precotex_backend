using System;

namespace ic.backend.precotex.web.Api.Parameters
{
    public class SNReqLegalParameter
    {
        public string? Accion { get; set; }
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
    }
}

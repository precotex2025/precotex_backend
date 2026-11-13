using System;

namespace ic.backend.precotex.web.Entity.Entities.SecureNorm
{
    public class SN_Proveedor
    {
        public int Id_Proveedor { get; set; }
        public string? Ruc { get; set; }
        public string Razon_Social { get; set; } = string.Empty;
        public string? Bien_Servicio { get; set; }
        public string? Tipo_Proveedor { get; set; }
        public string? Proceso { get; set; }
        public string? Areas { get; set; }
        public string? Ids_Areas { get; set; }
        public string? Areas_Referentes { get; set; }
        public string? Evaluador_Referente { get; set; }
        public string? Contacto_Nombre { get; set; }
        public string? Contacto_Telefono { get; set; }
        public string? Contacto_Correo { get; set; }
        public bool Flg_Homologado { get; set; }
        public DateTime? Fec_Homologacion { get; set; }
        public bool Flg_Activo { get; set; } = true;
        public int Tot_Eval_S1 { get; set; }
        public int Tot_Eval_S2 { get; set; }
        public int Tot_Incidentes { get; set; }
        public int Docs_Vencidos { get; set; }
        public string? Cod_Usuario { get; set; }
        public DateTime? Fec_Registro { get; set; }
    }
}

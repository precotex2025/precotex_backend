using System;

namespace ic.backend.precotex.web.Entity.Entities.SecureNorm
{
    public class SN_Log_Acceso
    {
        public int Id { get; set; }
        public string? Cod_Usuario { get; set; }
        public string? Nom_Usuario { get; set; }
        public string? Puesto { get; set; }
        public string? Cod_Rol { get; set; }
        public string? Fec_Acceso { get; set; }
        public string? Ip_Acceso { get; set; }
        public string? Estado { get; set; }
        public bool? Flg_Activo { get; set; }
    }
}
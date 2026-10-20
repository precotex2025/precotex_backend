using System;

namespace ic.backend.precotex.web.Entity.Entities.SecureNorm
{
    public class SN_Validar_Primer_Ingreso
    {
        public string? Cod_Usuario { get; set; }
        public string? Nom_Usuario { get; set; }
        public string? Estado { get; set; }
        public bool Primer_Ingreso { get; set; }
        public bool RequiereCambioPassword { get; set; }
    }
}
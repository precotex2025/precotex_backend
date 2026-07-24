using System;

namespace ic.backend.precotex.web.Entity.Entities.SecureNorm
{
    public class SN_Objetivo_Medicion
    {
        public int Id_Obj_Medicion { get; set; }
        public int Id_Objetivo { get; set; }
        public string? Codigo_Objetivo { get; set; }
        public string? Nombre_Objetivo { get; set; }
        public string? Proceso { get; set; }
        public decimal Meta { get; set; }
        public string Periodo { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public string? Usuario_Registro { get; set; }
        public DateTime? Fecha_Registro { get; set; }
        public string? Estado { get; set; }
    }
}

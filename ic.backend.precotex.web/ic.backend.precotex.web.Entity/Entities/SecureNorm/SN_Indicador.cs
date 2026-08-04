using System;

namespace ic.backend.precotex.web.Entity.Entities.SecureNorm
{
    public class SN_Indicador
    {
        public int Id_Indicador { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public string? Codigo_Proceso { get; set; }
        public string? Unidad_Medida { get; set; }
        public decimal Meta { get; set; }
        public string? Frecuencia { get; set; }
        public string? Usuario_Registro { get; set; }
        public DateTime? Fecha_Registro { get; set; }
        public bool? flg_Activo { get; set; }

        public string? Nombre_Proceso { get; set; }
    }
}

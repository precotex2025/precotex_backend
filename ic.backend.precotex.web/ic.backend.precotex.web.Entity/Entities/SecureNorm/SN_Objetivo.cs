using System;

namespace ic.backend.precotex.web.Entity.Entities.SecureNorm
{
    public class SN_Objetivo
    {
        public int Id_Objetivo { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Proceso { get; set; }
        public string? Norma { get; set; }
        public string? Periodo { get; set; }
        public string? Responsable_Proceso { get; set; }
        public DateTime? Fecha_Inicio { get; set; }
        public DateTime? Fecha_Fin { get; set; }
        public string? Responsable_Seguimiento { get; set; }
        public string? Medio_Verificacion { get; set; }
        public string? Indicador { get; set; }
        public string? Formula_Calculo { get; set; }
        public string? Unidad_Medida { get; set; }
        public string? Base { get; set; }
        public decimal? Meta { get; set; }
        public decimal? Avance { get; set; }
        public string? Frecuencia { get; set; }
        public string? Estado { get; set; }
        public string? Descripcion { get; set; }
        public bool Flg_Activo { get; set; } = true;
        public DateTime Fecha_Registro { get; set; } = DateTime.Now;
        public string? Usuario_Registro { get; set; }
    }

}

using System;

namespace ic.backend.precotex.web.Entity.Entities.SecureNorm
{
    public class SN_Indicador
    {
        public int Id_Indicador { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public string? Tipo { get; set; }
        public string? Sede { get; set; }
        public string? Norma { get; set; }
        public string? Frecuencia { get; set; }
        public decimal Meta { get; set; }
        public string? Unidad_Medida { get; set; }
        public string? Tipo_Meta { get; set; }
        public string? Sentido { get; set; }
        public string? Linea_Base { get; set; }
        public string? Formula { get; set; }
        public string? Codigo_Proceso { get; set; }
        public string? Nombre_Proceso { get; set; }
        public string? Responsable { get; set; }
        public string? Resp_Medicion { get; set; }
        public string? Fuente_Datos { get; set; }
        public DateTime? Fecha_Inicio { get; set; }
        public DateTime? Fecha_Fin { get; set; }
        public DateTime? Fec_Inicio { get; set; }
        public DateTime? Fec_Fin { get; set; }
        public string? Areas_Acceso { get; set; }
        public string? Estado { get; set; }
        public bool? flg_Activo { get; set; }
        public DateTime? Fecha_Registro { get; set; }
        public string? Usuario_Registro { get; set; }
    }
}
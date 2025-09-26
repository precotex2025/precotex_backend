namespace ic.backend.precotex.web.Api.Parameters
{
    public class LbColaTrabajoParameter
    {
        public string? Cod_Cliente { get; set; }
        public string? Des_Cliente { get; set; }
        public int? Corr_Carta { get; set; }
        public string? Cod_Tela { get; set; }
        public string? Des_Tela { get; set; }
        public DateTime? Fec_creacion { get; set; }
        public string? Lab_Dias { get; set; }
        public DateTime? Fec_compromiso { get; set; }
        public int? Dias_Falt_Compromiso { get; set; }
        public string? Estado { get; set; }
        public string? Flg_Est_Lab { get; set; }
    }

    public class LbColaTrabajoParameter_Detalle
    {
        public int Corr_Carta { get; set; }
        public int Sec { get; set; }
        public string? Descripcion_Color { get; set; }
        public string? Cod_Color { get; set; }
        public string? Estandar_Tono_Comer { get; set; }
        public string? Formulado { get; set; }
        public string? Flg_Est_Lab { get; set; }
    }
}

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
        public int? Corr_Carta { get; set; }
        public int? Sec { get; set; }
        public string? Descripcion_Color { get; set; }
        public string? Cod_Color { get; set; }
        public string? Estandar_Tono_Comer { get; set; }
        public string? Formulado { get; set; }
        public string? Flg_Est_Lab { get; set; }
        public int? Cur_Ten { get; set; }
        public string? Usr_Cod { get; set; }
    }

    public class LbColaTrabajoParameter_Colorantes
    {
        public int? Corr_Carta { get; set; }
        public int? Sec { get; set; }
        public int? id_secuencia { get; set; }
        public string? Col_Cod { get; set; }
        public double? Por_Ini { get; set; }
        public double? Por_Aju { get; set; }
        public double? Por_Fin { get; set; }
        public int? Can_Jabo { get; set; }
        public string? Cur_Jabo { get; set; }
        public string? Fijado { get; set; }
        public double? Rel_Ban { get; set; }
        public double? Pes_Mue { get; set; }
        public double? Volumen { get; set; }
        public double? Car_Gr { get; set; }
        public double? Car_Por { get; set; }
        public double? Sod_Gr { get; set; }
        public double? Sod_Por { get; set; }
    }
}

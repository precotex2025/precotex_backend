using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities
{
    public class Tx_Visor_Permanencia_Tela_Cruda
    {
        public string? Almacen { get; set; }
        public string? Partida { get; set; }
        public string? Cod_Cliente { get; set; }
        public string? Cliente { get; set; }
        public string? Cod_tela { get; set; }
        public decimal? Stock { get; set; }
        public string? Proceso { get; set; }
        public string? Cod_OrdTra { get; set; }
        public string? Cod_TipTejido { get; set; }
        public DateTime? Fec_1er_Ingreso { get; set; }
        public DateTime? Fec_Maximo_Permanencia { get; set; }
        public int? Dias_Transcurridos { get; set; }
        public int? Horas_Transcurridos { get; set; }
        public int? Minutos_Transcurridos { get; set; }
        public int? Dias_Tiempo_Espera { get; set; }
        public string? Ser_OrdComp_Tinto { get; set; }
        public string? Tipo { get; set; }
        public string? Flg_Alerta { get; set; }
        public string? Preparado { get; set; }
    }
}

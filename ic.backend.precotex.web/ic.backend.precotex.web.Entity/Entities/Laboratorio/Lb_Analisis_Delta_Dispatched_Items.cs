using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Laboratorio
{
    public class Lb_Analisis_Delta_Dispatched_Items
    {

        //BLOQUE #1
        public int Num_Bloque { get; set; }
        public int SampleId { get; set; }
        public string? Receta { get; set; }
        public string? Partida { get; set; }
        public string? DyelotRef { get; set; }
        public string? Proceso { get; set; }
        public decimal RB { get; set; }
        public string? Cliente { get; set; }
        public string? Maquina { get; set; }
        public string? Lote_Hilado { get; set; }
        public DateTime Fec_Fin_Teñido { get; set; }


        //BLOQUE #3
        public decimal CIE_DL{ get; set; }
        public decimal CIE_DA { get; set; }
        public decimal CIE_DB { get; set; }
        public decimal CMC_DE_2_1 { get; set; }
        public decimal FOR_DL { get; set; }
        public decimal FOR_DA { get; set; }
        public decimal FOR_DB { get; set; }
        public decimal FOR_DE_2_1 { get; set; }


        //BLOQUE #4
        public string? Especularidad { get; set; }
        public string? Filtro_UV { get; set; }

        /*
         

SampleId	Receta	Partida	DyelotRef	Proceso	RB	Cliente	Maquina	Lote_Hilado	Fec_Fin_Teñido
57978	00-K1578-JE000111	K1578	NULL	NULL	NULL	NULL	NULL	NULL	NULL

         * */

    }
}

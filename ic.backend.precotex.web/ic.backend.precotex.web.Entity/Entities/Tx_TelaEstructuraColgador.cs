using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities
{
    public class Tx_TelaEstructuraColgador
    {
        /*
            Encog_Lenght	Encog_Width	    Width_BW	Width_AW	Weight_BW	Weight_AW	Yield	Des_Galga	Diametro	DesComposicion	    Des_FamTela
                -8.50	        -8.50	        1.86	    1.72	    210	        230	    2.560	GALGA 24	30	        ALG 68% / TEN 32%	JERSEY         
        */
        public decimal? Encog_Lenght { get; set; }
        public decimal? Encog_Width { get; set; }
        public decimal? Width_BW { get; set; }
        public decimal? Width_AW { get; set; }
        public int? Weight_BW { get; set; }
        public int? Weight_AW { get; set; }
        public decimal? Yield { get; set; }
        public string? Des_Galga { get; set; }
        public int? Diametro { get; set; }
        public string? DesComposicion { get; set; }
        public string? Des_FamTela { get; set; }

        /*Otros Datos Complementarios*/
        public string? Cod_Tela { get; set; }
        public string? Cod_OrdTra { get; set; }
        public string? Cod_Ruta { get; set; }
        public string? Nom_Cliente { get; set; }
        public string? Fabric { get; set; }
        public string? Yarn { get; set; }
        public string? Des_Color { get; set; }
        public string? Des_Fabric_Finish { get; set; }
        public string? Des_Fabric_Wash { get; set; }


    }
}

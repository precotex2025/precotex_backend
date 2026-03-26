using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Laboratorio
{
    public class Lb_Partidas_Agrupadas
    {
        public string? Tip_Trabajador { get; set; }
	    public string? Cod_Trabajador { get; set; }
	    public string? Nom_Analista { get; set; }
	    public string? Tip_Partida { get; set; }
	    public string? Color { get; set; }
	    public string? Cod_Cliente_Tex { get; set; }
	    public string? Nom_Cliente { get; set; }
	    public string? Partidas { get; set; }
        public string? Ref_Pedid { get; set; }
    }
}

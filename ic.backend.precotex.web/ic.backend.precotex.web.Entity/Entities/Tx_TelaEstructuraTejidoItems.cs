using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities
{
    public class Tx_TelaEstructuraTejidoItems
    {
        public int? Num_Correlativo { get; set; }
        public string? Cod_Tela { get; set; }
        public int? Id_Version { get; set; }
        public string? Titulo { get; set; }
        public string? Cabos { get; set; }
        public string? LongitudMalla { get; set; }
        public string? Color { get; set; }
        public int? Pasadas { get; set; }
        public string? Estructura { get; set; }
        public string? Repeticiones { get; set; }
        public decimal? Rec_Pasadas_Real { get; set; }
        public decimal? Longitud_Malla_Real { get; set; }
        /*
Cod_Tela	Cod_Tela
Num_Correlativo	int
Id_Version	int
Titulo	varchar
Cabos	varchar
LongitudMalla	varchar
Color	varchar
Pasadas	int
Estructura	varchar
Observacion	varchar
Elaborado	varchar
Cod_UsuarioCreacion	Cod_Usuario
Fec_UsuarioCreacion	datetime
Cod_UsuarioModifica	Cod_Usuario
Fec_UsuarioModifica	datetime
Repeticiones	varchar
titulo01	varchar
titulo02	varchar         
         */
    }
}

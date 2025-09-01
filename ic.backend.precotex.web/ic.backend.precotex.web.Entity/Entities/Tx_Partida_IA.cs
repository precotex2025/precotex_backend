using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities
{
    public class Tx_Partida_IA
    {
		public string? Cod_OrdTra		{ get; set; }
		public int? Num_Secuencia	{ get; set; }
		public decimal? Largo			{ get; set; }
		public decimal? Ancho			{ get; set; }
		public string? Flg_Estatus		{ get; set; }
		public DateTime? Fec_Registro	{ get; set; }
		public string? Usu_Registro	{ get; set; }
		public DateTime? Fec_Modifica	{ get; set; }
		public string? Usu_Modifica	{ get; set; }
		public string? Cod_Equipo		{ get; set; }

        //OTROS
        public string? Cod_Talla { get; set; }
        public string? Cod_Tela { get; set; }
    }
}

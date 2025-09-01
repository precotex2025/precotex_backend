using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Memorandum
{
    public class Tx_Memorandum_Detalle
    {
        
        //public int Id_Memorandum_Detalle { get; set; }
        public string? Num_Memo_Detalle { get; set; }
        public string? Num_Memo { get; set; }
        //public string? Cod_Material_Memo { get; set; }
        public string? Glosa { get; set; }
        public int? Cantidad { get; set; }
        public string? Flg_Estatus { get; set; }
        public DateTime? Fec_Registro { get; set; }
        public string? Usu_Registro { get; set; }
        //public DateTime? Fec_Modifica { get; set; }
        //public string? Usu_Modifica { get; set; }
        public string? Cod_Equipo { get; set; }

        /*
	Num_Memo			VARCHAR(10) NOT NULL PRIMARY KEY,
	Cod_Material_Memo	CHAR(2)		NOT NULL,
	Cantidad			INT			NOT NULL,
	Flg_Estatus			CHAR(1)		NOT NULL, --1=NO RECEPCIONADO, 2=RECEPCIONADO
	Fec_Registro		DATETIME	NOT NULL,
	Usu_Registro		VARCHAR(20) 	NOT NULL,
	Fec_Modifica		DATETIME	NULL,
	Usu_Modifica		VARCHAR(20) 	NULL,
	Cod_Equipo	        VARCHAR(50),         
         
         */
    }
}

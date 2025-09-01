using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities
{
    public class Tx_Maquinas_Revisadoras
    {
        public string? Cod_Maquina_Revisadora { get; set; }
        public string? Des_Maquina_Revisadora { get; set; }
        public string? Flg_Estatus { get; set; }
        public DateTime? Fec_Registro { get; set; }
        public string? Usu_Registro { get; set; }
        public DateTime? Fec_Modifica { get; set; }
        public string? Usu_Modifica { get; set; }
        public string? Cod_Equipo { get; set; }

        /*
	[Cod_Maquina_Revisadora] CHAR(2) PRIMARY KEY,
	[Des_Maquina_Revisadora] VARCHAR(100),
	[Flg_Estatus]	CHAR(1)				,
	[Fec_Registro]	DATETIME NOT NULL	,
	[Usu_Registro]	CHAR(15) NOT NULL	,
	[Fec_Modifica]	DATETIME NULL		,
	[Usu_Modifica]	CHAR(15) NULL		,
	[Cod_Equipo]	VARCHAR(50) NULL	         
         */


    }
}

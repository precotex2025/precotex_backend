using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Memorandum
{
    public class Tx_Memorandum
    {

        public string? Num_Memo { get; set; }
        public string? Cod_Usuario_Emisor { get; set; }
        public DateTime? Fecha_Emisor { get; set; }
        public string? Cod_Usuario_Receptor { get; set; }
        public DateTime? Fecha_Receptor { get; set; }
        public string? Num_Planta_Origen { get; set; }
        public string? Num_Planta_Destino { get; set; }
        public string? Cod_Usuario_Seguridad_Emisor { get; set; }
        public DateTime? Fecha_Seguridad_Emisor { get; set; }
        public string? Cod_Usuario_Seguridad_Receptor { get; set; }
        public DateTime? Fecha_Seguridad_Receptor { get; set; }
        public string? Cod_Estado_Memo { get; set; }
        public string? Cod_Tipo_Memo { get; set; }
        public string? Cod_Motivo_Memo { get; set; }
        public string? Usu_Modifica { get; set; }
        public DateTime? Fec_Modifica { get; set; }
        public string? Cod_Equipo { get; set; }

        //NUEVOS CAMPOS
        public string? Cod_Tipo_Movimiento { get; set; }
        public string? Datos_Externo { get; set; }
        public string? Direccion_Externo { get; set; }

        //Otros Datos
        public string? Emisor { get; set; }
        public string? Receptor { get; set; }
        public string? Planta_Origen { get; set; }
        public string? Planta_Destino { get; set; }
        public string? Usuario_Seg_Origen { get; set; }
        public string? Usuario_Seg_Destino { get; set; }
        public string? Descripcion_Estado_Memo { get; set; }
        public string? Descripcion_Tipo_Memorandum { get; set; }
        public string? Glosa { get; set; }
        public int? Cantidad { get; set; }
        public string? Descripcion_Tipo_Motivo { get; set; }
        public string? Descripcion_Tipo_Movimiento { get; set; }

        /*
         
			Num_Memo                    VARCHAR(10) NOT NULL PRIMARY KEY,
			Cod_Usuario_Emisor          VARCHAR(20) NOT NULL,
			Fecha_Emisor	            DATETIME	NOT NULL,
			Cod_Usuario_Receptor        VARCHAR(20) NOT NULL,
			Fecha_Receptor				DATETIME	NOT NULL,
			Num_Planta_Origen           INT,
			Num_Planta_Destino          INT,
			Cod_Usuario_Seguridad_Emisor    VARCHAR(20) NOT NULL,
			Fecha_Seguridad_Emisor          DATETIME NOT NULL,		
			Cod_Usuario_Seguridad_Receptor  VARCHAR(20) NOT NULL,
			Fecha_Seguridad_Receptor        DATETIME NOT NULL,
			Cod_Estado_Memo                 CHAR(2),
			Cod_Tipo_Memo              CHAR(2) NOT NULL,
			Usu_Modifica			   VARCHAR(20) NOT NULL,
			Fec_Modifica               DATETIME NULL,
			Cod_Equipo                 VARCHAR(50),        
          
         
         */
    }
}

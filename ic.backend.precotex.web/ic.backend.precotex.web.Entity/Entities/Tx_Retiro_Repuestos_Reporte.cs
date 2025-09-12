using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities
{
    public class Tx_Retiro_Repuestos_Reporte
    {
        public DateTime Fec_Aprobacion { get; set; }
        public string Hora_Aprobacion { get; set; }
        public string Nom_Seguridad { get; set; }
        public DateTime Fec_Creacion { get; set; }
        public string Nom_Mantenimiento { get; set; }
        public string Nro_Precinto_Apertura { get; set; }
        public string Nro_Precinto_Cierre { get; set; }
        public int Num_Requerimiento { get; set; }
        public int Nro_Secuencia { get; set; }
        public string Cod_Item { get; set; }
        public string Des_Item { get; set; }
        public double Can_Requerida { get; set; }
        public string Cod_UniMed { get; set; }
        public string Rpt_Cambio { get; set; }
        public string Itm_Foto { get; set; }

    }
}

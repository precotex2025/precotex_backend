using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.RetiroRepuestos
{
    public class Tx_Retiro_Repuestos
    {
        public int Num_Requerimiento { get; set; }
        public DateTime? Fec_Creacion { get; set; }
        public DateTime? Fec_Aprobacion { get; set; }
        public string? Hora_Aprobacion { get; set; }
        public int? Cod_Seguridad { get; set; }
        public int? Cod_Mantenimiento { get; set; }
        public string? Nro_Precinto_Apertura {  get; set; }
        public string? Nro_Precinto_Cierre {  get; set; }
        public string? Nom_Seguridad { get; set; }
        public string? Nom_Mantenimiento { get; set;}
        
    }

}

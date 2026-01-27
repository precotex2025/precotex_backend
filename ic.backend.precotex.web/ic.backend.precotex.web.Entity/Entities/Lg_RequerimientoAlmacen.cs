using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities
{
    public class Lg_RequerimientoAlmacen
    {
        public string? Estado { get; set; }
        public string? Responsable { get; set; }
        public string? Solicitante { get; set; }
        public string? Num_Requerimiento { get; set; }
        public string? Fecha_Creacion { get; set; }
        public string? Cod_Item { get; set; }
        public string? Des_Item{ get; set; }
        public int? Can_Requerida { get; set; }
        public int? Can_Stock { get; set; }
        public string? Tiene_Stock { get; set; }
        public string? Req_Compra { get; set; }
        public string? Enviado_CI { get; set; }
        public string? Des_StatusReq { get; set; }
        public string? Logistico { get; set; }
        public string? Aprobacion_OC { get; set; }
        public string? Fec_Ing { get; set; }
        public string? Ing_Alm { get; set; }
        public string? Entregado_Almacen { get; set; }
    }
}

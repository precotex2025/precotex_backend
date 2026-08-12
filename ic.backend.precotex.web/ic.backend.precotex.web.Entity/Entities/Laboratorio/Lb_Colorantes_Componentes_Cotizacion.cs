using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Laboratorio
{
    public class Lb_Colorantes_Componentes_Cotizacion
    {
        public string? Cod_Item { get; set; }
        public string? Des_Item { get; set; }
        public decimal CanComp { get; set; }
        public int OrdenImp { get; set; }
        public decimal Iteracion_01 { get; set; }
        public string? IdTipoFibra { get; set; }
        public string? Referencia { get; set; }

         

    }
}

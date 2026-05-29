using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Cotizaciones
{
    public class Tx_Cotizaciones
    {
        public string? ExisteCotizacion { get; set; }
        public int? Pro_Id  {get; set;}
        public int? Pro_Cen_Cos  {get; set;}
        public string? Cen_Cos_Des { get; set; }
        public string? Pro_Hover  {get; set;}
        public string? Pro_Des  {get; set;}
        public int? Pro_Factor  {get; set;}
        public double? Pro_Cos_Kg  {get; set;}
        public double? Pro_Tot  {get; set;}
        public double? Pro_Tot_Com { get; set; }
        public double? Pro_Aju { get; set; }
        public double? Pro_Cotizacion { get; set; }
        public double? Pro_Por { get; set; }
        public string? Pro_Tip { get; set; }
        public int? Nivel { get; set; }
        public int? Cod_Subtotal { get; set; }
        public string? Cod_Proceso_Tex { get; set; }
        public string? Cod_ProcesoPadre { get; set; }
        public string? Cod_SubProceso { get; set; }
        public int? ParteEntera { get; set; }
        public int? ParteDecimal { get; set; }
        public string? Observacion { get; set; }


    }
}

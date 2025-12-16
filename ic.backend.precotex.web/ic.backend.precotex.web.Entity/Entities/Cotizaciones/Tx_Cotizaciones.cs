using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Cotizaciones
{
    public class Tx_Cotizaciones
    {
        public int? Pro_Id  {get; set;}
        public string? Pro_Cen_Cos  {get; set;}
        public string? Pro_Hover  {get; set;}
        public string? Pro_Des  {get; set;}
        public int? Pro_Factor  {get; set;}
        public double? Pro_Cos_Kg  {get; set;}
        public double? Pro_Tot  {get; set;}
        public double? Pro_Aju  {get; set;}
        public double? Pro_Cotizacion { get; set; }
    }
}

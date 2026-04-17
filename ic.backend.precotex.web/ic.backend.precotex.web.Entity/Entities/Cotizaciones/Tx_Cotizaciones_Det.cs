using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Cotizaciones
{
    public class Tx_Cotizaciones_Det
    {
        public int IdCotizacion_Det { get; set; }
        public int IdCotizacion_Cab { get; set; }
        public int? Pro_Cen_Cos { get; set; }
        public string? Pro_Hover { get; set; }
        public string? Pro_Des { get; set; }
        public int? Pro_Factor { get; set; }
        public decimal? Pro_Cos_Kg { get; set; }
        public decimal? Pro_Tot { get; set; }
        public decimal? Pro_Aju { get; set; }
        public decimal? Pro_Cotizacion { get; set; }
        public string? Pro_Tip { get; set; }
        public decimal? Pro_Tot_Com { get; set; }
        public decimal? Pro_Por { get; set; }
        public string Flg_Estatus { get; set; } = string.Empty;
        public DateTime Fec_Registro { get; set; }
        public string Usu_Registro { get; set; } = string.Empty;
        public DateTime? Fec_Modifica { get; set; }
        public string? Usu_Modifica { get; set; }
        public string? Cod_Equipo { get; set; }
        // Relación con detalles
    }
}

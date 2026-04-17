using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Cotizaciones
{
    public class Tx_Cotizaciones_Cab
    {
        public int IdCotizacion_Cab { get; set; }
        public int? Pro_Id { get; set; }
        public int Cen_Cos_Cod { get; set; }
        public string? Cod_Tipo { get; set; } = string.Empty;
        public string? Cod_Cliente_Tex { get; set; } = string.Empty;
        public string? Cod_Tela { get; set; } = string.Empty;
        public string? Cod_Ruta { get; set; } = string.Empty;
        public string? Cod_Color { get; set; } = string.Empty;
        public string? Flg_Estatus { get; set; } = string.Empty;
        public DateTime Fec_Registro { get; set; }
        public string Usu_Registro { get; set; } = string.Empty;
        public DateTime? Fec_Modifica { get; set; }
        public string? Usu_Modifica { get; set; }
        public string? Cod_Equipo { get; set; }

        // Relación con detalles
        public List<Tx_Cotizaciones_Det> Detalles { get; set; } = new List<Tx_Cotizaciones_Det>();
    }
}

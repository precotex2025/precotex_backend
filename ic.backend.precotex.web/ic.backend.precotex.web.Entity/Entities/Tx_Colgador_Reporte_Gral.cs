using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities
{
    public class Tx_Colgador_Reporte_Gral
    {
        public string? CodigoBarra_Colgador { get; set; }
        public string? Cod_Tela { get; set; }
        public string? Cod_OrdTra { get; set; }
        public string? Des_Color { get; set; }
        public string? CodigoBarra_TipoUbicacion { get; set; }
        public string? Des_Ubicacion { get; set; }
        public int? Nro_Caja { get; set; }
        public int? Nro_Colgadores { get; set; }
        public string? Des_Ruta { get; set; }
        public string? Nom_Cliente { get; set; }
        public string? Fabric { get; set; }
        public string? Yarn { get; set; }
        public string? Composicion { get; set; }
        public decimal? Shrinkage_lenght { get; set; }
        public decimal? Shrinkage_width { get; set; }
        public decimal? Width_bw_mts { get; set; }
        public decimal? Width_aw_mts { get; set; }
        public decimal? Weight_bw_mt2 { get; set; }
        public decimal? Weight_aw_mt2 { get; set; }
        public decimal? Yield_mts_kg { get; set; }
        public string? Gague { get; set; }
        public int? Diametro { get; set; }
        public string? Fabric_Finish { get; set; }
        public string? Fabric_Wash { get; set; }
    }
}

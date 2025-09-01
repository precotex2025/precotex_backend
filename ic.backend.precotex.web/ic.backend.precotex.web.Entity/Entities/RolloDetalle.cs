using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities
{
    public class RolloDetalle
    {
        public int CheckID { get; set; }
        public string Rollo { get; set; }
        public bool FlgDespacho { get; set; }
        public decimal KGS_CRUDO { get; set; }
        public string OT_Tejeduria { get; set; }
        public string TELA_COMB { get; set; }
        public string Calidad_Teje { get; set; }
        public string Def_Crudo { get; set; }
        public decimal MedStd { get; set; }
        public decimal MedReal { get; set; }
        public int UndCrudo { get; set; }
        public int UndReal { get; set; }
        public decimal Mtrs2T { get; set; }
        public decimal Mtrs2R { get; set; }
        public decimal Ancho { get; set; }
        public decimal Inclinacion { get; set; }
        public string Calidad { get; set; }
        public string CalfAuto { get; set; }
        public string DefAcab { get; set; }
        public string Inspector { get; set; }
        public string Observacion { get; set; }
        public DateTime UltimaModificacion { get; set; }
        public string TipoOrigenRollo { get; set; }
        public decimal MtrsFacturadoEstdig { get; set; }
        public int NumSecuenciaTinto { get; set; }
        public DateTime FechaDespacho { get; set; }
        public string TipoRollo { get; set; }
        public DateTime FecIngMtrsEstDig { get; set; }

    }

}

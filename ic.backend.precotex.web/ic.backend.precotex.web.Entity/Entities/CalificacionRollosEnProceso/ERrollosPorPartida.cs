using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.CalificacionRollosEnProceso
{
    public class ERrollosPorPartida
    {
        public string Articulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string CodDefecto { get; set; } = string.Empty;
        public decimal Metros { get; set; }
        public int Puntos { get; set; }

        public bool Sel { get; set; }
        public string Id { get; set; } = string.Empty;
        public string Rollo { get; set; } = string.Empty;

        public string Despacho { get; set; } = string.Empty;
        public string Flg_Despacho { get; set; } = string.Empty;
        public decimal Mtrs2_T { get; set; }
        public int Calidad { get; set; }
        public int Secuencia { get; set; }

        public string Tela_Comb { get; set; } = string.Empty;
        public string Und_Crudo { get; set; } = string.Empty;
        public string Med_Std { get; set; } = string.Empty;

        public decimal Kgs_Crudo{ get; set; }

        public string Ot_Tejeduria { get; set; } = string.Empty;

        public string Img_Des { get; set; } = string.Empty;


        //nuevo
        public decimal Mtrs2_R { get; set; }
        public decimal Ancho { get; set; }
        public string Observacion { get; set; }
        public string Responsable { get; set; }
        public string Inspector { get; set; }
        public string Reproceso { get; set; }
        public string Maquina { get; set; }


    }
}

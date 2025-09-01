using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities
{
    public class Tx_Bulto_Hilado_Grupo
    {
        public int? Id_Bulto_Hilado_Grupo { get; set; }
        public string? Grupo { get; set; }
        public DateTime? Fec_Registro { get; set; }
        public string? Usu_Registro { get; set; }
        public DateTime? Fec_Modifica { get; set; }
        public string? Usu_Modifica { get; set; }
        public string? Cod_Equipo { get; set; }
        public int? Id_MaestroEstadoBultoGrupo { get; set; }

        //Otros
        public string? sEstado { get; set; }
        public int? nTotBolsas { get; set; }
        public string? Accion { get; set; }
        public string? Num_Corre { get; set; }
        public int? Cantidad_Cono { get; set; }
        public decimal? Peso_Neto { get; set; }
        public decimal? Peso_Tara { get; set; }
        public decimal? Peso_Bruto { get; set; }
        public int? Capa_Total_Bolsas { get; set; }
        public string? Cod_Ubicacion { get; set; }

    }
}

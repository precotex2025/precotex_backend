using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities
{
    public class Tx_Ots_Hojas_Arranque_Versiones
    {
        public int? Id_Ots_Hojas_Arranque_Versiones { get; set; }
        public string? Cod_Ordtra { get; set; }
        public int? Num_Secuencia { get; set; }
        public int? Version { get; set; }
    }

    public class Tx_Ots_Hojas_Arranque_Versiones_Listado
    {
        public string? Turno { get; set; }
        public string? Nom_Usuario { get; set; }
        public DateTime? Fecha_Creacion { get; set; }
        public string? Cod_Maquina_Tejeduria { get; set; }
        public string? Cod_Ordtra { get; set; }
        public int? Num_Secuencia { get; set; }
        public string? Ser_OrdComp { get; set; }
        public string? Cod_OrdComp { get; set; }
        public string? Estado { get; set; }
        public string? Nom_Cliente { get; set; }
        public string? Observaciones { get; set; }

        //Nuevos Campos
        public string? Descripcion_Motivo { get; set; }
        public string? Descripcion_Area { get; set; }
        public DateTime? Fch_Hora_Entrega { get; set; }
        public int? DiferenciaEnMinutos { get; set; }
        public DateTime? Fecha_Produccion { get; set; }

    }
}

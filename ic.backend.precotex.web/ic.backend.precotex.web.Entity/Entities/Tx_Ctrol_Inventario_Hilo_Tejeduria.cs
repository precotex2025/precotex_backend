using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities
{
    public class Tx_Ctrol_Inventario_Hilo_Tejeduria
    {
        public string? Tipo { get; set; }
        public string? Lote { get; set; }
        public string? Num_Semana { get; set; }
        public string? Titulo { get; set; }
        public string? Ser_OrdComp { get; set; }
        public string? Cod_OrdComp { get; set; }
        public string? Color { get; set; }
        public string? Hilo_Tipo { get; set; }
        public string? Hilo_Codigo { get; set; }
        public string? Ubicacion { get; set; }

        public decimal? Cantidad_Cono { get; set; }
        public decimal? Peso_Tara { get; set; }
        public decimal? Peso_Bruto { get; set; }
        public decimal? Peso_Neto { get; set; }
        public decimal? Pallet { get; set; }
        public decimal? Diferencia { get; set; }

        public string? Observacion { get; set; }
        public string? Proveedor { get; set; }
        public DateTime? Fec_Registro { get; set; }
        public string? Usu_Registro { get; set; }
        public DateTime? Fec_Modifica { get; set; }
        public string? Usu_Modifica { get; set; }
        public string? Cod_Equipo { get; set; }

    }
}

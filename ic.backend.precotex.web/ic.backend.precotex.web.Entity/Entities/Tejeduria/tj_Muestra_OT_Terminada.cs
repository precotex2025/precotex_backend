using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Tejeduria
{
    public class tj_Muestra_OT_Terminada
    {
        public string? Cod_Maquina { get; set; }
        public string? OT { get; set; }
        public string? Lote { get; set; }
        public string? Fibra { get; set; }
        public string? Titulo { get; set; }
        public DateTime? Fec_Termino { get; set; }
        public string? Cod_Hilado { get; set; }
        public string? Articulo { get; set; }       
        public string? Cod_Color { get; set; }
    }

    public class tj_Muestra_OT_Programada
    {
        public DateTime? Fec_Inicio { get; set; }
        public string? OT { get; set; }
        public string? Familia { get; set; }
        public string? Cod_Maquina { get; set; }
        public decimal? Can_Salida { get; set; }
        public decimal? Can_Teorico { get; set; }
        public decimal? Can_PorPedir { get; set; }
        public string? Lote { get; set; }
        public string? Cod_Hilado { get; set; }
        public string? Cod_Color { get; set; }
    }

    public class tj_seguimiento_saldo_hilo_tela
    {
        public string? Num_Traslado { get; set; } 
        public string? Cod_OrdProv { get; set; } 
        public string? Cod_Ordtra_Ori { get; set; } 
        public string? Cod_Maquina_Ori { get; set; } 
        public string? Cod_HilTel { get; set; } 
        public string? Cod_Color { get; set; } 
        public decimal? Kg_Programado { get; set; } 
        public decimal? Kg_Salida { get; set; } 
        public decimal? Kg_Consumo { get; set; } 
        public decimal? Kg_Devolver { get; set; }
        public string? Estado { get; set; }
        public string? Cod_Ordtra_Des { get; set; }
        public string? Cod_Maquina_Des { get; set; }
        public string? Cod_Usuario { get; set; }

    }
}

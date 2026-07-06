using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Tejeduria
{
    public class Tj_Muestra_Solicitud_Devolucion
    {
        public int? Solicitud { get; set; }
        public string? Lote { get; set; }
        public string? Semana { get; set; }
        public int? Bultos { get; set; }
        public string? Estado { get; set; }
        public string? Proveedor { get; set; }
        public string? Hilo { get; set; }
        public string? Color { get; set; }
        public string? Marca { get; set; }
        public string? Conera { get; set; }
        public string? OT { get; set; }
        public string? OC { get; set; }
        public string? Tipo { get; set; }
    }

    public class Tj_Muestra_Solicitud_Devolucion_Bultos
    {
        public string? Num_Corre { get; set; }
        public decimal? Peso_Bruto { get; set; }
        public decimal? Peso_Neto { get; set; }
        public decimal? Cantidad_Cono { get; set; }        
    }

    public class Tj_Mantenimiento_Solicitud_Devolucion
    {
        public int? Num_Solicitud { get; set; }
        public string? Lote { get; set; }
        public string? Semana { get; set; }
        public string? Color { get; set; }
        public string? Marca { get; set; }
        public string? Conera { get; set; }
        public string? Estado { get; set; }
        public string? Tipo { get; set; }
        public string? Cod_Usuario { get; set; }
    }
}

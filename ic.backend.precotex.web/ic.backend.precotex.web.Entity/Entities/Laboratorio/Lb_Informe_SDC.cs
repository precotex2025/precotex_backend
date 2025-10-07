using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Laboratorio
{
    public class Lb_Informe_SDC
    {
        public int? Corr_Carta { get; set; } 
        public string? Descripcion { get; set; } 
        public string? Descripcion_Color { get; set; } 
        public string? Pantone { get; set; } 
        public string? Com_Comer { get; set; } 
        public IEnumerable<string>? Ruta { get; set; }
        public IEnumerable<string>? Solidez { get; set; }

    }

    public class Ruta
    {
        public int? CodSDC { get; set; }
        public string? Descripcion { get; set; }
    }

    public class Solidez
    {
        public int? CodSDC { get; set; }
        public string? Descripcion { get; set; }
    }
}

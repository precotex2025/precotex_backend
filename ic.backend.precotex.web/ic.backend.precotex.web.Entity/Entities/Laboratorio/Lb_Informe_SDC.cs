using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Laboratorio
{
    public class Lb_Informe_SDC
    {
        public string? Corr_Carta { get; set; } 
        public string? Descripcion { get; set; } 
        public string? Descripcion_Color { get; set; } 
        public string? Pantone { get; set; } 
        public string? Com_Comer { get; set; } 
        public IEnumerable<string>? Ruta { get; set; }
        public IEnumerable<string>? Solidez { get; set; }

        //PRODUCCION
        public string? Cur_Ten  { get; set; }
        public string? Cla_Oc   { get; set; }
        public string? Temporada  { get; set; }
        public string? Estilo   { get; set; }
        public string? OP  { get; set; }
        public string? Cod_GrupoTex     { get; set; }
        public string? OC  { get; set; }
        public string? Maq_Tinto    { get; set; }
        public string? Ref_Par  { get; set; }
        public string? Ref_Com  { get; set; }
        public string? Lote  { get; set; }
        public string? Obs { get; set; }


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

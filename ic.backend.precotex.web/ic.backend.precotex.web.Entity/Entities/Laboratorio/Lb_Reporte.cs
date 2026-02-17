using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Laboratorio
{
    public class Lb_Reporte
    {
        public string? Analista                                     { get; set; }
        public int? Corr_Carta                                      { get; set; }
        public string? Nom_Cliente                                  { get; set; }
        public string? Articulo                                     { get; set; }
        public string? Descripcion_Color                            { get; set; }
        public string? Previo                                       { get; set; }
        public double? Enz_Can                                      { get; set; }
        public string? Enz_Den_Bno                                  { get; set; }
        public string? Tip_Ten                                      { get; set; }
        public string? Cur_Ten                                      { get; set; }
        public string? Cur_Jab                                      { get; set; }
        public int? Can_Jab                                         { get; set; }
        public string? Fijado                                       { get; set; }
        public string? Acabado                                      { get; set; }
        public int? Sec                                             { get; set; }
        public int? Correlativo                                     { get; set; }
        public IEnumerable<string>? Ruta                            { get; set; }
        public IEnumerable<Colorantes_Reporte>? Colorantes_Reporte  { get; set; }
        public IEnumerable<Ruta_Reporte>? Ruta_Reporte              { get; set; }
        public IEnumerable<Solidez_Reporte>? Solidez_Reporte        { get; set; }
    }

    public class Colorantes_Reporte
    {
        public int? Corr_Carta                  { get; set; }
        public int? Sec                         { get; set; }
        public string? Col_Cod                  { get; set; }
        public string? Col_Des                  { get; set; }
        public double? Por_Fin                  { get; set; }
        public int? id_secuencia                { get; set; }
        public int? Correlativo                 { get; set; }
        public int? Ingreso_Manual              { get; set; }
    }

    public class Ruta_Reporte
    {
        public int? Corr_Carta                  { get; set; }
        public string? Descripcion              { get; set; }
    }

    public class Solidez_Reporte
    {
        public int? Corr_Carta                  { get; set; }
        public string? Descripcion              { get; set; }
    }
}

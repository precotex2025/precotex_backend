using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace ic.backend.precotex.web.Entity.Entities.Laboratorio
{
    public class Lb_Reporte
    {
        public string? Analista                                     { get; set; }
        public string? Corr_Carta                                   { get; set; }
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
        public decimal? Ph_Ini                                      { get; set; }               
        public IEnumerable<string>? Ruta                            { get; set; }
        public IEnumerable<Colorantes_Reporte>? Colorantes_Reporte  { get; set; }
        public IEnumerable<Ruta_Reporte>? Ruta_Reporte              { get; set; }
        public IEnumerable<Solidez_Reporte>? Solidez_Reporte        { get; set; }
        public decimal? Kgs_Prod                                    { get; set; }
        public decimal? R_B_Prod                                    { get; set; }
        public string? Maquina                                      { get; set; }
        public string? Temporada                                    { get; set; }
        public string? TipoPartida                                  { get; set; }
        public string? Cod_Color                                    { get; set; }
        public string? PartidasAgrupadas                            { get; set; }
        public string? Familia                                      { get; set; }
        public string? Pro_Des                                      { get; set; }
        public decimal? Rel_Ban                                     { get; set; }
        public string? Descarga                                     { get; set; }
        public string? Lote_Hilado                                  { get; set; }
        public string? Partida_Agrupada_Tinto                       { get; set; }
        public decimal?        Rel_Ban_Sige { get; set; } //Nuevo
        public decimal         Pes_Mue { get; set; }      //Nuevo
        public string? Flg_Est_Lab { get; set; } //Nuevo

        /*CAMPOS NUEVOS MOSTRAR CAMPOS INI EN EL REPORTE*/

        //public decimal ? Ph_Ini { get; set; }
        public decimal ? Ph_Fin { get; set; }
        public decimal ? Ph_Jab { get; set; }
        public decimal? Ph_Des { get; set; }

    }

    public class Colorantes_Reporte
    {
        public string? Corr_Carta               { get; set; }
        public int? Sec                         { get; set; }
        public string? Col_Cod                  { get; set; }
        public string? Col_Des                  { get; set; }
        public double? Por_Fin                  { get; set; }
        public int? id_secuencia                { get; set; }
        public int? Correlativo                 { get; set; }
        public int? Ingreso_Manual              { get; set; }

        //Campos nuevos
        public double? Por_Ini { get; set; }
        public double? Por_Aju { get; set; }
        public string? Procedencia { get; set; }
    }

    public class Ruta_Reporte
    {
        public string? Corr_Carta               { get; set; }
        public string? Descripcion              { get; set; }
    }

    public class Solidez_Reporte
    {
        public string? Corr_Carta               { get; set; }
        public string? Descripcion              { get; set; }
    }

    public class Reporte_ph
    {
        public decimal? Ph_Ini { get; set; }
        public decimal? Ph_Fin { get; set; }
        public decimal? Ph_Jab { get; set; }
        public decimal? Ph_Des { get; set; }
        public decimal? Ph_Jab2 { get; set; }
        public decimal? Ph_Jab3 { get; set; }
    }
}

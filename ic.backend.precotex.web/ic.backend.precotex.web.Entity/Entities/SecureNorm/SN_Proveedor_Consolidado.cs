using System;
using System.Collections.Generic;

namespace ic.backend.precotex.web.Entity.Entities.SecureNorm
{
    public class SN_Proveedor_ConsolidadoDetalle
    {
        public int Id_Evaluacion { get; set; }
        public int Id_Proveedor { get; set; }
        public string Razon_Social { get; set; } = string.Empty;
        public int Id_Area { get; set; }
        public string Des_Area { get; set; } = string.Empty;
        public decimal Num_Peso { get; set; }
        public decimal Num_Umbral_Minimo { get; set; }
        public byte Num_Semestre { get; set; }
        public DateTime? Fec_Evaluacion { get; set; }
        public decimal Num_Total { get; set; }
        public int Num_Maximo { get; set; }
        public decimal Num_Puntaje_Norm { get; set; }
        public bool Flg_Aprobado { get; set; }
        public string Des_Clasificacion { get; set; } = string.Empty;
        public decimal Puntaje_Ponderado { get; set; }
    }

    public class SN_Proveedor_ConsolidadoResumen
    {
        public int Id_Proveedor { get; set; }
        public string Razon_Social { get; set; } = string.Empty;
        public string Bien_Servicio { get; set; } = string.Empty;
        public int Num_Ciclo { get; set; }
        public byte Num_Semestre { get; set; }
        public string Tipo_Evaluacion { get; set; } = string.Empty;
        public int Areas_Evaluadas { get; set; }
        public decimal Nota_Final_Ponderada { get; set; }
        public string Clasificacion_Consolidada { get; set; } = string.Empty;
        public bool Flg_Aprobado { get; set; }
    }

    public class SN_Proveedor_ConsolidadoResponse
    {
        public List<SN_Proveedor_ConsolidadoDetalle> Detalle { get; set; } = new List<SN_Proveedor_ConsolidadoDetalle>();
        public List<SN_Proveedor_ConsolidadoResumen> Resumen { get; set; } = new List<SN_Proveedor_ConsolidadoResumen>();
    }
}

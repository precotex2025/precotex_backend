using System;

namespace ic.backend.precotex.web.Api.Parameters
{
    public class SNProveedorEvaluacionParameter
    {
        public int Id_Proveedor { get; set; }
        public int Id_Area { get; set; }
        public string Cod_Evaluador { get; set; } = string.Empty;
        public DateTime? Fec_Evaluacion { get; set; }

        public byte? Nota_Calidad { get; set; }
        public bool Flg_Na_Calidad { get; set; }
        public string? Obs_Calidad { get; set; }

        public byte? Nota_Tiempos { get; set; }
        public bool Flg_Na_Tiempos { get; set; }
        public string? Obs_Tiempos { get; set; }

        public byte? Nota_Seguridad { get; set; }
        public bool Flg_Na_Seguridad { get; set; }
        public string? Obs_Seguridad { get; set; }

        public byte? Nota_Atencion { get; set; }
        public bool Flg_Na_Atencion { get; set; }
        public string? Obs_Atencion { get; set; }

        public byte? Nota_Precio { get; set; }
        public bool Flg_Na_Precio { get; set; }
        public string? Obs_Precio { get; set; }

        public string? Des_Acciones { get; set; }
        public string? Cod_Usuario { get; set; }
    }
}

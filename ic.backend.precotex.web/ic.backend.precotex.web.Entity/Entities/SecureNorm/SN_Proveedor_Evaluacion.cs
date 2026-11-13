using System;

namespace ic.backend.precotex.web.Entity.Entities.SecureNorm
{
    public class SN_Proveedor_Evaluacion
    {
        public int Id_Evaluacion { get; set; }
        public int Id_Proveedor { get; set; }
        public string? Des_Proveedor { get; set; }
        public string? Bien_Servicio { get; set; }
        public int Id_Area { get; set; }
        public string? Des_Area { get; set; }
        public string Cod_Evaluador { get; set; } = string.Empty;
        public DateTime? Fec_Evaluacion { get; set; }
        public int Num_Ciclo { get; set; }
        public byte Num_Semestre { get; set; }

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

        public int Num_Maximo { get; set; }
        public decimal Num_Total { get; set; }
        public decimal Num_Puntaje_Norm { get; set; }
        public decimal Num_Peso_Aplicado { get; set; }
        public decimal Num_Umbral_Aplicado { get; set; }
        public string? Des_Clasificacion { get; set; }
        public bool Flg_Aprobado { get; set; }
        public string? Des_Acciones { get; set; }
        public string? Cod_Usuario { get; set; }
        public DateTime? Fec_Registro { get; set; }
    }

    public class SN_Proveedor_EvaluacionResultado
    {
        public int Id_Evaluacion { get; set; }
        public decimal Puntaje_Normalizado { get; set; }
        public string Clasificacion { get; set; } = string.Empty;
        public bool Aprobado { get; set; }
    }
}

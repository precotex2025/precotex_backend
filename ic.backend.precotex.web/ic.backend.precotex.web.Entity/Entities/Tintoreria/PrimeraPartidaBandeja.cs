using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Tintoreria
{
    public class PrimeraPartidaBandeja
    {
        public string? Cliente { get; set; }
        public string? Grupo { get; set; }
        public string? PrimeraPartida { get; set; }
        public string? CodigoTela { get; set; }
        public string? DescripcionTela { get; set; }
        public string? Color { get; set; }
        public string? Combo { get; set; }
        public string? Talla { get; set; }

        // Grupo 1
        public string? G1_Estado { get; set; }
        public string? G1_Responsable { get; set; }
        public DateTime? G1_FechaHora { get; set; }

        // Grupo 2
        public string? G2_Estado { get; set; }
        public string? G2_Responsable { get; set; }
        public DateTime? G2_FechaHora { get; set; }

        // Grupo 3
        public string? G3_Estado { get; set; }
        public string? G3_Inspeccionado { get; set; }
        public string? G3_Supervisado { get; set; }
        public DateTime? G3_FechaHora { get; set; }
        public string? G3_Observacion { get; set; }

        // Grupo 4
        public string? G4_Estado { get; set; }
        public string? G4_Responsable { get; set; }
        public DateTime? G4_FechaHora { get; set; }
        public decimal G4_Kgs { get; set; }
        public string? G4_Comentario { get; set; }
        public string? G4_NuevaPartida { get; set; }

        // Otros campos
        public string? PartidasAsociadas { get; set; }
        public decimal Kgs_Totales { get; set; }
        public DateTime? PrimeraFechaEntrega { get; set; }
        public int Num_Secuencia { get; set; }
        public decimal? Kgs_Asignados { get; set; }
        public string? Cod_Cliente_Tex { get; set; }
        public string? Ser_OrdComp { get; set; }
        public string? Cod_OrdComp { get; set; }
        public string? Sec_OrdComp { get; set; }

    }
}

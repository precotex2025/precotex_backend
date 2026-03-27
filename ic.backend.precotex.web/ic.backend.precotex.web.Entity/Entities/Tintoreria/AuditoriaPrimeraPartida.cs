using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.Tintoreria
{
    public class AuditoriaPrimeraPartida
    {
        public string? Cod_Cliente_Tex { get; set; }
        public string? Ser_OrdComp { get; set; }
        public string? Cod_OrdComp { get; set; }
        public string? Sec_OrdComp { get; set; }
        public string? Cod_Ordtra { get; set; } 
        public string? Cod_Tela { get; set; } 
        public int Num_Secuencia { get; set; } 
        public decimal Kgs_Tenidos { get; set; } 
        public string? Comentario { get; set; } 
        public string? Cod_Ordtra_Nueva { get; set; } 
        public string? Flg_Status { get; set; } 
        public DateTime Fec_Registro { get; set; } 
        public string? Usu_Registro { get; set; } 
        //public string? Cod_Equipo { get; set; } 
    }
}

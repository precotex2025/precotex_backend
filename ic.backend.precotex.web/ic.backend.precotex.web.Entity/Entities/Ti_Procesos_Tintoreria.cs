using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities
{
    public class Ti_Procesos_Tintoreria
    {
        public string? Cod_Maquina_Tinto { get; set; }
        public string? Cod_Ordtra { get; set; }
        public DateTime? Fecha_Inicio { get; set; }
        public DateTime? Fecha_Fin { get; set; }
        public string? Descripcion { get; set; }
        public string? IdOrgatexUnico { get; set; }
        public string? Url_Dureza { get; set; }
        public string? Url_Peroxido { get; set; }
        public string? Flg_Registro { get; set; }
        public string? Flg_Dureza { get; set; }
        public string? Flg_Peroxido { get; set; }
        public string? Flg_Tobera { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.SecureNorm
{
    public class SN_Norma
    {
        public string? Codigo_Norma { get; set; }
        public string? Norma { get; set; }
        public string? Descripcion { get; set; }
        public string? Flg_Activo { get; set; }
        public string? Cod_Usuario { get; set; }
        public DateTime? Fec_Registro { get; set; }
        public string? Cod_Usuario_Modifico { get; set; }
        public DateTime? Fec_Modificacion { get; set; }
    }
}

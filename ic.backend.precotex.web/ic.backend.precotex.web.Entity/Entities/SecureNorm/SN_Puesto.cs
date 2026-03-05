using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.SecureNorm
{
    public class SN_Puesto
    {
        public string? Codigo_Puesto { get; set; }
        public string Codigo_Organizacion { get; set; } = null!;
        public string Codigo_Sede { get; set; } = null!;
        public string Denominacion { get; set; } = null!;
        public string? Codigo_Nivel_Riesgo { get; set; }
        public string? Validacion_Periodica { get; set; }
        public string? Puesto_Descripcion { get; set; }
        public string? Puesto_Funciones { get; set; }
        public string? Puesto_Requisitos { get; set; }
        public string? Puesto_Caracteristicas { get; set; }
        public string? Caracteristicas_Visible { get; set; }
        public string? Flg_Activo { get; set; }
        public string Cod_Usuario { get; set; } = null!;
        public DateTime? Fec_Registro { get; set; }
        public string? Cod_Usuario_Modifico { get; set; }
        public DateTime? Fec_Modificacion { get; set; }
    }
}

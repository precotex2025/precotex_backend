using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.SecureNorm
{
    public class SN_Organizacion
    {
        public string? Codigo_Organizacion { get; set; }
        public string? Denominacion { get; set; }
        public string? Direccion { get; set; }
        public string? Localidad { get; set; }
        public string? Provincia { get; set; }
        public string? Pais { get; set; }
        //public string? Denominacion_Sede_Principal { get; set; }
        //public string? Acronimo { get; set; }
        //public string? Sede_Direccion { get; set; }
        //public string? Sede_Localidad { get; set; }
        //public string? Sede_Provincia { get; set; }
        //public string? Sede_Pais { get; set; }
        public string? Flg_Activo { get; set; }
        public string? Cod_Usuario { get; set; }
        public DateTime? Fec_Registro { get; set; }
        public string? Cod_Usuario_Modifico { get; set; }
        public DateTime? Fec_Modificacion { get; set; }

    }
}

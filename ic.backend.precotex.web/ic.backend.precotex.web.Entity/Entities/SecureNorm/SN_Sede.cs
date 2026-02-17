using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.SecureNorm
{
    public class SN_Sede
    {
        public string? Codigo_Sede { get; set; }
        public string? Codigo_Organizacion { get; set; }
        public string? Denominacion { get; set; }
        public string? Acronimo { get; set; }
        public string? Direccion { get; set; }
        public string? Localidad { get; set; }
        public string? Provincia { get; set; }
        public string? Pais { get; set; }
        public string? Flg_Activo { get; set; }
        public string? Cod_Usuario { get; set; }
        public DateTime? Fec_Registro { get; set; }
        public string? Cod_Usuario_Modifico { get; set; }
        public DateTime? Fec_Modificacion { get; set; }

        /*
            Codigo_Sede CHAR(3) NOT NULL PRIMARY KEY,
            Codigo_Organizacion CHAR(3) NOT NULL,
            Denominacion NVARCHAR(200) NOT NULL,
            Acronimo NVARCHAR(50) NULL,
            Direccion NVARCHAR(250) NULL,
            Localidad NVARCHAR(150) NULL,
            Provincia NVARCHAR(150) NULL,
            Pais NVARCHAR(150) NULL,
            Flg_Activo BIT NOT NULL DEFAULT 1,
            Cod_Usuario NVARCHAR(50) NOT NULL,
            Fec_Registro DATETIME2 NOT NULL DEFAULT GETDATE(),
            Cod_Usuario_Modifico NVARCHAR(50) NULL,
            Fec_Modificacion DATETIME2 NULL, 
         */
    }
}

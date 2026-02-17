using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.SecureNorm
{
    public class SN_Proceso
    {
        public string? Codigo_Proceso { get; set; }
        public string? Codigo_Sede { get; set; }
        public string? Proceso { get; set; }
        public string? Codigo_Tipo_Proceso { get; set; }
        public string? Descripcion { get; set; }
        public string? Nombre_Adjunto { get; set; }
        public string? Ruta_Adjunto { get; set; }
        public string? Flg_Activo { get; set; }
        public string? Cod_Usuario { get; set; }
        public DateTime? Fec_Registro { get; set; }
        public string? Cod_Usuario_Modifico { get; set; }
        public DateTime? Fec_Modificacion { get; set; }

        /*
    Codigo_Proceso CHAR(3) PRIMARY KEY,
	Codigo_Sede    CHAR(3) NOT NULL,
    Proceso NVARCHAR(200) NOT NULL,
    Codigo_Tipo_Proceso CHAR(2) NOT NULL,  -- Ej: MI, ES, AP, etc.
    Descripcion NVARCHAR(MAX) NULL,
    Nombre_Adjunto NVARCHAR(255) NULL,
    Ruta_Adjunto NVARCHAR(500) NULL,
    Flg_Activo BIT NOT NULL DEFAULT 1,
    Cod_Usuario NVARCHAR(50) NOT NULL,
    Fec_Registro DATETIME2 NOT NULL DEFAULT GETDATE(),
    Cod_Usuario_Modifico NVARCHAR(50) NULL,
    Fec_Modificacion DATETIME2 NULL,          
         */
    }
}

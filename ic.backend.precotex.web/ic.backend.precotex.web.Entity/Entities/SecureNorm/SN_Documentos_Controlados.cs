using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.SecureNorm
{
    public class SN_Documentos_Controlados
    {
        public string Codigo_Documentos_Controlados { get; set; } = null!; // CHAR(3), PK
        public int Codigo_Proceso { get; set; }
        public string Codigo_Carpeta_Control { get; set; } = null!;
        public string Codigo_Normas { get; set; } = null!;
        public string Codigo_Tiempo_Conservacion { get; set; } = null!;
        public string Codigo_Tipo_Descarga { get; set; } = null!;
        public string Denominacion { get; set; } = null!;
        public string Codigo_Documento { get; set; } = null!;
        public string Version_Documento { get; set; } = null!;
        public string Ruta_Adjunto { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public bool? bRegistroAsociado { get; set; } // BIT, NULL
        public bool? bRequiereRevision { get; set; } // BIT, NULL
        public string? Flg_Estado { get; set; } // VARCHAR(20), NULL

        public string? Cod_Usuario_Elabora { get; set; } // NVARCHAR(50), NULL
        public DateTime? Fec_Elabora { get; set; } // DATETIME2, NULL
        public string? Cod_Usuario_Revisa { get; set; } // NVARCHAR(50), NULL
        public DateTime? Fec_Revisa { get; set; } // DATETIME2, NULL
        public string? Cod_Usuario_Aprueba { get; set; } // NVARCHAR(50), NULL
        public DateTime? Fec_Aprueba { get; set; } // DATETIME2, NULL
        public DateTime? Fec_Vencimiento { get; set; } // DATETIME2, NULL

        public bool Flg_Activo { get; set; } = true; // BIT, default 1
        public string Cod_Usuario { get; set; } = null!; // NVARCHAR(50), NOT NULL
        public DateTime Fec_Registro { get; set; } = DateTime.Now; // DATETIME2, default GETDATE()
        public string? Cod_Usuario_Modifico { get; set; } // NVARCHAR(50), NULL
        public DateTime? Fec_Modificacion { get; set; } // DATETIME2, NULL
    }
}

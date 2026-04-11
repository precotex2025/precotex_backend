using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.SecureNorm
{
    public class SN_Documentos_Controlados
    {
        public string CodigoDocumentosControlados { get; set; } = null!; // CHAR(3), PK
        public string? CodigoCarpetaControl { get; set; } // CHAR(3), NULL
        public string CodigoNorma { get; set; } = null!; // CHAR(3), NOT NULL
        public string CodigoTiempoConservacion { get; set; } = null!; // VARCHAR(20), NOT NULL
        public string CodigoTipoDescarga { get; set; } = null!; // VARCHAR(20), NOT NULL
        public string Denominacion { get; set; } = null!; // VARCHAR(200), NOT NULL
        public string? CodigoDocumento { get; set; } // VARCHAR(200), NULL
        public string? VersionDocumento { get; set; } // VARCHAR(200), NULL
        public string? RutaAdjunto { get; set; } // NVARCHAR(MAX), NULL
        public string? Descripcion { get; set; } // NVARCHAR(MAX), NULL
        public bool? bRegistroAsociado { get; set; } // BIT, NULL
        public bool? bRequiereRevision { get; set; } // BIT, NULL
        public string? FlgEstado { get; set; } // VARCHAR(20), NULL

        public string? CodUsuarioElabora { get; set; } // NVARCHAR(50), NULL
        public DateTime? FecElabora { get; set; } // DATETIME2, NULL
        public string? CodUsuarioRevisa { get; set; } // NVARCHAR(50), NULL
        public DateTime? FecRevisa { get; set; } // DATETIME2, NULL
        public string? CodUsuarioAprueba { get; set; } // NVARCHAR(50), NULL
        public DateTime? FecAprueba { get; set; } // DATETIME2, NULL
        public DateTime? FecVencimiento { get; set; } // DATETIME2, NULL

        public bool FlgActivo { get; set; } = true; // BIT, default 1
        public string CodUsuario { get; set; } = null!; // NVARCHAR(50), NOT NULL
        public DateTime FecRegistro { get; set; } = DateTime.Now; // DATETIME2, default GETDATE()
        public string? CodUsuarioModifico { get; set; } // NVARCHAR(50), NULL
        public DateTime? FecModificacion { get; set; } // DATETIME2, NULL
    }
}

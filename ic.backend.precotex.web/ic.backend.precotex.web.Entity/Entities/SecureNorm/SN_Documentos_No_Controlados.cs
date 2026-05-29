using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Entity.Entities.SecureNorm
{
    public class SN_Documentos_No_Controlados
    {
        public string? Codigo_Documentos_No_Controlados { get; set; }
        public string? Codigo_Carpeta_Control { get; set; }
        public string? Codigo_Tipo_Descarga { get; set; }
        public string? Denominacion { get; set; }
        public string? Codigo_Documento { get; set; }
        public string? Version_Documento { get; set; }
        public string? Ruta_Adjunto { get; set; }
        public string? Descripcion { get; set; }
        public bool bRequiereRevision { get; set; }
        public bool Flg_Activo { get; set; }
        public string? Cod_Usuario { get; set; }
        public DateTime Fec_Registro { get; set; }
        public string? Cod_Usuario_Modifico { get; set; }
        public DateTime? Fec_Modificacion { get; set; }

        /*
	[Codigo_Documentos_No_Controlados] [char](3) NOT NULL,
	[Codigo_Carpeta_Control] [char](3)			 NULL,
	[Codigo_Tipo_Descarga] [varchar](20)         NOT NULL,
	[Denominacion] [varchar](200)				 NOT NULL,
	[Codigo_Documento] [varchar](200)	NULL,
	[Version_Documento] [varchar](200)	NULL,
	[Ruta_Adjunto] [nvarchar](max)		NULL,
	[Descripcion] [nvarchar](max)		NULL,
	[bRequiereRevision] [bit]			NULL,
	[Flg_Activo] [bit]				NOT NULL,
	[Cod_Usuario] [nvarchar](50)	NOT NULL,
	[Fec_Registro] [datetime2](7)	NOT NULL,
	[Cod_Usuario_Modifico] [nvarchar](50) NULL,
	[Fec_Modificacion] [datetime2](7) NULL,          
         */
    }
}

using Microsoft.Graph.Models;

namespace ic.backend.precotex.web.Api.Parameters
{
    public class SNCarpetaMntoProcesoParameter
    {
        public string Accion { get; set; } = null!;
        public string Codigo_Documentos_Controlados { get; set; } = null!;
        public string Codigo_Carpeta_Control { get; set; } = null!;
        public string Codigo_Normas { get; set; } = null!;
        public string Codigo_Tiempo_Conservacion { get; set; } = null!;
        public string Codigo_Tipo_Descarga { get; set; } = null!;
        public string Denominacion { get; set; } = null!;
        public string Codigo_Documento { get; set; } = null!;
        public string Version_Documento { get; set; } = null!;
        public string Ruta_Adjunto { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public bool bRegistroAsociado { get; set; }
        public bool bRequiereRevision { get; set; }
        public string Flg_Estado { get; set; } = null!;

        public string Cod_Usuario_Elabora { get; set; } = null!;
        public string Fec_Elabora { get; set; } = null!;
        public string Cod_Usuario_Revisa { get; set; } = null!;
        public string Fec_Revisa { get; set; } = null!;
        public string Cod_Usuario_Aprueba { get; set; } = null!;
        public string Fec_Aprueba { get; set; } = null!;
        public string Fec_Vencimiento { get; set; } = null!;
        public bool Flg_Activo { get; set; } 
    } 

    public class SNDocumentosControladosParameter
    {
        public string Accion { get; set; } = null!;

        public string Codigo_Documentos_Controlados { get; set; } = null!;
        public int Codigo_Proceso { get; set; } = 0!;
        public string Codigo_Carpeta_Control { get; set; } = null!;
        public string Codigo_Normas { get; set; } = null!;
        public string Codigo_Tiempo_Conservacion { get; set; } = null!;
        public string Codigo_Tipo_Descarga { get; set; } = null!;
        public string Denominacion { get; set; } = null!;
        public string Codigo_Documento { get; set; } = null!;
        public string Version_Documento { get; set; } = null!;
        public string Ruta_Adjunto { get; set; } = null!;
        public string Descripcion { get; set; } = null!;

        public bool bRegistroAsociado { get; set; }
        public bool bRequiereRevision { get; set; }
        public string Flg_Estado { get; set; }
        public string? Cod_Usuario_Elabora { get; set; } = null!;
        public string? Fec_Elabora { get; set; } = null!;
        public string? Cod_Usuario_Revisa { get; set; } = null!;
        public string? Fec_Revisa { get; set; } = null!;
        public string? Cod_Usuario_Aprueba { get; set; } = null!;
        public string? Fec_Aprueba { get; set; } = null!;
        public string? Fec_Vencimiento { get; set; } = null!;
        public bool Flg_Activo { get; set; }
        public string Cod_Usuario { get; set; } = null!;
    }
}

using System;

namespace ic.backend.precotex.web.Api.Parameters
{
    public class SNManualParameter
    {
        public string? Accion { get; set; }
        public int? Id_Manual { get; set; }
        public string? Codigo { get; set; }
        public string? Titulo { get; set; }
        public string? Subtitulo { get; set; }
        public string? Descripcion { get; set; }
        public string? Autor { get; set; }
        public string? Fecha_Publicacion { get; set; }
        public string? Version { get; set; }
        public string? Color { get; set; }
        public string? Icono { get; set; }
        public string? Archivo { get; set; }
        public string? Usuario_Registro { get; set; }
    }
}

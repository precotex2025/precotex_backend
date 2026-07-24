using System;

namespace ic.backend.precotex.web.Entity.Entities.SecureNorm
{
    public class SN_Manual
    {
        public int Id_Manual { get; set; }
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
        public int? Descargas { get; set; }
        public string? Usuario_Registro { get; set; }
        public DateTime? Fecha_Registro { get; set; }
        public bool? flg_Activo { get; set; }
    }
}

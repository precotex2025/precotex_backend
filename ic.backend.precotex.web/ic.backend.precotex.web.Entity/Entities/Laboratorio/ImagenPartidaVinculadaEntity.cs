namespace ic.backend.precotex.web.Entity.Entities.Laboratorio
{
    public class ImagenPartidaVinculadaEntity
    {
        public byte[] Contenido { get; set; } = [];
        public string ContentType { get; set; } = string.Empty;
        public DateTimeOffset LastModified { get; set; }
        public string ETag { get; set; } = string.Empty;
    }
}

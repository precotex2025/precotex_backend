namespace ic.backend.precotex.web.Entity.Entities.Laboratorio
{
    public class CotizacionColorantesDetalleEntity
    {
        public IEnumerable<CotizacionColoranteItemEntity>? Colorante { get; set; } 
        public IEnumerable<CotizacionColoranteItemEntity>? Descarga { get; set; }
        public IEnumerable<CotizacionColoranteItemEntity>? Fijado { get; set; }
        public IEnumerable<CotizacionColoranteItemEntity>? Jabonado1 { get; set; }
        public IEnumerable<CotizacionColoranteItemEntity>? Jabonado2 { get; set; }
    }
}

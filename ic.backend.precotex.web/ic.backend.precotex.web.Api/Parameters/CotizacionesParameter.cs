using ic.backend.precotex.web.Entity.Entities.Cotizaciones;
using ic.backend.precotex.web.Entity.Entities.Memorandum;

namespace ic.backend.precotex.web.Api.Parameters
{
    public class CotizacionesParameter
    {
        public int IdCotizacion_Cab { get; set; }
        public int? Pro_Id { get; set; }
        public int Cen_Cos_Cod { get; set; }
        public string? Cod_Tipo { get; set; } = string.Empty;
        public string? Cod_Cliente_Tex { get; set; } = string.Empty;
        public string? Cod_Tela { get; set; } = string.Empty;
        public string? Cod_Ruta { get; set; } = string.Empty;
        public string? Cod_Color { get; set; } = string.Empty;
        public string? Flg_Estatus { get; set; } = string.Empty;
        public string Usu_Registro { get; set; } = string.Empty;
        public string? Accion { get; set; }
        public List<Tx_Cotizaciones_Det>? Detalles { get; set; }
    }
}

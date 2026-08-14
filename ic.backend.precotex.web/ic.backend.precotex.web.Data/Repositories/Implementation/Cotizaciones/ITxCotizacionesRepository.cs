using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.Cotizaciones;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.Cotizaciones
{
    public interface ITxCotizacionesRepository
    {
        Task<IEnumerable<ComboGral>?> ListaUnidadNegocio();
        Task<IEnumerable<ComboGral>?> ListaUnidadNegocioTipo(int Id_Unidad_NegocioKey);
        Task<IEnumerable<Tx_Cotizaciones_Telas>?> ListaTelas(string Cod_Tela);
        Task<IEnumerable<Tx_Cotizaciones_Rutas>?> RutaXCodTela(string Cod_Tela);







        Task<IEnumerable<ComboGral>?> ListaRecetasAntipilling();
        Task<IEnumerable<ComboGral>?> ValidaColorExiste(string Cod_Color);   
        
        Task<IEnumerable<ComboGral>?> ListaColoresXCliente(string Cod_Cliente);
        Task<IEnumerable<Tx_PreciosColor>?> ListaPrecioXColor(string Tipo_Busqueda, int Pro_Cen_Cos, string Tipo, string Cod_Cliente_Tex, string Cod_Tela, string Cod_Ruta, string? Cod_Color);
        Task<IEnumerable<Tx_Cotizaciones>?> ListarProcesosExportacion(int Pro_Cen_Cos, string Tipo, string Cod_Cliente_Tex, string Cod_Tela, string Cod_Ruta, string? Cod_Color, decimal precio, int tiempo, int IdCotizacion_Cab);
        Task<(int Codigo, string Mensaje)> ProcesoCotizacion(Tx_Cotizaciones_Cab tx_Cotizaciones_Cab, List<Tx_Cotizaciones_Det> detalle, string sTipoTransac);
        Task<Tx_Cotizaciones_Cab?> ObtenerNuevoCorrelativoVersion(int Id_Unidad_NegocioKey, string Cod_Tipo_Orden_tinto, string Cod_Cliente_Tex, string Cod_Tela, string Cod_Ruta, string? Cod_Color, string? SDC_Referencia);











        Task<IEnumerable<Tx_Cotizaciones>?> ListarProcesosExportacionFooter(int Pro_Cen_Cos);
        Task<IEnumerable<Tx_Cotizaciones_Rutas_Detalle>?> RutaXCodTelaDetalle(string Cod_Tela, string Cod_Ruta);
        Task<IEnumerable<Tx_Cotizaciones_Centro_Costo>?> ListaCentroCosto();
        Task<IEnumerable<ComboGral>?> ListaIntensidad(int Id_Unidad_NegocioKey);
        Task<IEnumerable<Tx_HilosTel>?> ListaHiladoxTela(string Cod_Tela);
        Task<IEnumerable<Tx_Cotizaciones_Cab>?> ValidaExistenciaHistorialxColor(int Pro_Cen_Cos, string Tipo, string Cod_Cliente_Tex, string Cod_Tela, string Cod_Ruta, string? Cod_Color, string? Cod_Receta);

    }
}

using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.Cotizaciones;
using ic.backend.precotex.web.Entity.Entities.Memorandum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.Cotizaciones
{
    public interface ITxCotizacionesRepository
    {
        Task<IEnumerable<Tx_Cotizaciones>?> ListarProcesosExportacion(int Pro_Cen_Cos, string Tipo, string Cod_Cliente_Tex, string Cod_Tela, string Cod_Ruta, string? Cod_Color);
        Task<IEnumerable<Tx_Cotizaciones>?> ListarProcesosExportacionFooter(int Pro_Cen_Cos);
        Task<IEnumerable<Tx_Cotizaciones_Rutas>?> RutaXCodTela(string Cod_Tela);
        Task<IEnumerable<Tx_Cotizaciones_Rutas_Detalle>?> RutaXCodTelaDetalle(string Cod_Tela, string Cod_Ruta);
        Task<IEnumerable<Tx_Cotizaciones_Telas>?> ListaTelas(string Cod_Tela);
        Task<IEnumerable<Tx_Cotizaciones_Centro_Costo>?> ListaCentroCosto();
        Task<IEnumerable<ComboGral>?> ListaUnidadNegocio();
        Task<IEnumerable<ComboGral>?> ListaIntensidad(int Id_Unidad_NegocioKey);
        Task<(int Codigo, string Mensaje)> ProcesoCotizacion(Tx_Cotizaciones_Cab tx_Cotizaciones_Cab, List<Tx_Cotizaciones_Det> detalle, string sTipoTransac);
        Task<IEnumerable<ComboGral>?> ValidaColorExiste(string Cod_Color);
        Task<IEnumerable<Tx_HilosTel>?> ListaHiladoxTela(string Cod_Tela);
        Task<IEnumerable<ComboGral>?> ListaUnidadNegocioTipo(int Id_Unidad_NegocioKey);
        Task<IEnumerable<ComboGral>?> ListaColoresXCliente(string Cod_Cliente);
        Task<IEnumerable<Tx_PreciosColor>?> ListaPrecioXColor(string Cod_Color);
        Task<IEnumerable<ComboGral>?> ListaRecetasAntipilling();

    }
}

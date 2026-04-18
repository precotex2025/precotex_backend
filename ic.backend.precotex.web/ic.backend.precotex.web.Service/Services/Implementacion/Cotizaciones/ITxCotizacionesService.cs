using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.Cotizaciones;
using ic.backend.precotex.web.Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.Cotizaciones
{
    public interface ITxCotizacionesService
    {
        Task<ServiceResponseList<Tx_Cotizaciones>?> ListarProcesosExportacion(int Pro_Cen_Cos, string Tipo, string Cod_Cliente_Tex, string Cod_Tela, string Cod_Ruta, string? Cod_Color);
        Task<ServiceResponseList<Tx_Cotizaciones>?> ListarProcesosExportacionFooter(int Pro_Cen_Cos);
        Task<ServiceResponseList<Tx_Cotizaciones_Rutas>?> RutaXCodTela(string Cod_Tela);
        Task<ServiceResponseList<Tx_Cotizaciones_Rutas_Detalle>?> RutaXCodTelaDetalle(string Cod_Tela, string Cod_Ruta);
        Task<ServiceResponseList<Tx_Cotizaciones_Telas>?> ListaTelas(string Cod_Tela);
        Task<ServiceResponseList<Tx_Cotizaciones_Centro_Costo>?> ListaCentroCosto();
        Task<ServiceResponse<int>> ProcesoCotizacion(Tx_Cotizaciones_Cab tx_Cotizaciones_Cab, List<Tx_Cotizaciones_Det> detalle, string sTipoTransac);
        Task<ServiceResponseList<ComboGral>?> ValidaColorExiste(string Cod_Color);
    }
}

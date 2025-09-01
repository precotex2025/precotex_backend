using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.DDT
{
    public interface ITxUbicacionColgadorService
    {
        Task<ServiceResponse<int>> CrudUbicacionColgador(Tx_Ubicacion_Colgador tx_Ubicacion_Colgador, string sTipoTransac);
        Task<ServiceResponseList<Tx_Ubicacion_Colgador>?> ListadoUbicacionColgador(DateTime FecIni, DateTime FecFin, int Id_Tipo_Ubicacion_Colgador);
        Task<ServiceResponseList<Tx_Tipo_Ubicacion_Colgador>?> ListadoTipoUbicacionColgador();
        Task<ServiceResponseList<Tx_FamTela>?> ListadoTipoFamTela();
        Task<ServiceResponseList<Tx_TipoUbicacionControl>?> ObtenerCorrelativo(int Id_Tipo_Ubicacion_Colgador, string Cod_FamTela);

        //Ubicacion de colgadores
        Task<ServiceResponse<int>> CrudUbicacionColgadorItems(Tx_Ubicacion_Colgador_Items tx_Ubicacion_Colgador_Items, string CodigoBarra, string sTipoTransac);
        Task<ServiceResponseList<Tx_Ubicacion_Colgador>?> ObtenerUbicacionColgadorQR(string CodigoBarra);
        Task<ServiceResponseList<Tx_Ubicacion_Colgador>?> ObtenerUbicacionColgadorById(int Id_Tx_Ubicacion_Colgador);

        Task<ServiceResponseList<Tx_Ubicacion_Colgador>?> ListadoTotalColgadoresxTipoUbicaciones(DateTime? FecCrea);
        Task<ServiceResponseList<Tx_Colgador_Registro_Cab>?> ListadoColgadoresxUbicacion(int Id_Tx_Ubicacion_Colgador);
        Task<ServiceResponseList<Tx_Ubicacion_Colgador>?> ListadoTotalColgadoresxCodigoBarra(string CodigoBarra);

        //Reubicaicon de Cajas
        Task<ServiceResponseList<Tx_Ubicacion_Fisica>?> ListadoUbicacioFisica();
        Task<ServiceResponseList<Tx_Ubicacion_Colgador>?> ObtenerInformacionTotalCajasxUbicacion(int Id_Tx_Ubicacion_Fisica);
        Task<ServiceResponseList<Tx_Ubicacion_Colgador>?> ObtenerInformacionCajasxUbicacion(int Id_Tx_Ubicacion_Fisica);

        //Impresora
        Task<ServiceResponseList<Tx_Ubicacion_Impresora_Activa>?> ObtenerImpresoraPredeterminada();
        Task<ServiceResponseList<Tx_Cliente>?> ObtieneAbreviaturaCliente(string Cod_Tela, string Cod_Ruta, string Cod_OrdTra);

        //Reporte
        Task<ServiceResponseList<Tx_Colgador_Reporte_Gral>?> ReporteColgadoresGralDetallado();
    }
}

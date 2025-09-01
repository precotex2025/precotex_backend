using ic.backend.precotex.web.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.DDT
{
    public interface ITxUbicacionColgadorRepository
    {
        Task<(int Codigo, string Mensaje)> CrudUbicacionColgador(Tx_Ubicacion_Colgador tx_Ubicacion_Colgador, string sTipoTransac);
        Task<IEnumerable<Tx_Ubicacion_Colgador>?> ListadoUbicacionColgador(DateTime FecIni, DateTime FecFin, int Id_Tipo_Ubicacion_Colgador);
        Task<IEnumerable<Tx_Tipo_Ubicacion_Colgador>?> ListadoTipoUbicacionColgador();
        Task<IEnumerable<Tx_FamTela>?> ListadoTipoFamTela();
        Task<IEnumerable<Tx_TipoUbicacionControl>?> ObtenerCorrelativo(int Id_Tipo_Ubicacion_Colgador, string Cod_FamTela);

        //Ubicacion de colgadores
        Task<(int Codigo, string Mensaje)> CrudUbicacionColgadorItems(Tx_Ubicacion_Colgador_Items tx_Ubicacion_Colgador_Items, string CodigoBarra, string sTipoTransac);
        Task<IEnumerable<Tx_Ubicacion_Colgador>?> ObtenerUbicacionColgadorQR(string CodigoBarra);
        Task<IEnumerable<Tx_Ubicacion_Colgador>?> ObtenerUbicacionColgadorById(int Id_Tx_Ubicacion_Colgador);

        Task<IEnumerable<Tx_Ubicacion_Colgador>?> ListadoTotalColgadoresxTipoUbicaciones(DateTime? FecCrea);
        Task<IEnumerable<Tx_Colgador_Registro_Cab>?> ListadoColgadoresxUbicacion(int Id_Tx_Ubicacion_Colgador);
        Task<IEnumerable<Tx_Ubicacion_Colgador>?> ListadoTotalColgadoresxCodigoBarra(string CodigoBarra);

        //Reubicaicon de Cajas
        Task<IEnumerable<Tx_Ubicacion_Fisica>?> ListadoUbicacioFisica();
        Task<IEnumerable<Tx_Ubicacion_Colgador>?> ObtenerInformacionTotalCajasxUbicacion(int Id_Tx_Ubicacion_Fisica);
        Task<IEnumerable<Tx_Ubicacion_Colgador>?> ObtenerInformacionCajasxUbicacion(int Id_Tx_Ubicacion_Fisica);

        //Impresora
        Task<IEnumerable<Tx_Ubicacion_Impresora_Activa>?> ObtenerImpresoraPredeterminada();
        Task<IEnumerable<Tx_Cliente>?> ObtieneAbreviaturaCliente(string Cod_Tela, string Cod_Ruta, string Cod_OrdTra);
        Task<IEnumerable<Tx_Colgador_Reporte_Gral>?> ReporteColgadoresGralDetallado();
       

    }
}

using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.Memorandum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.Memorandum
{
    public interface ITxProcesoMemorandumRepository
    {
        Task<IEnumerable<Tx_Memorandum>?> ObtieneInformacionMemorandum(DateTime FecIni, DateTime FecFin, string NumMemo, string codUsuario, string CodPlantaGarita);
        Task<(int Codigo, string Mensaje)> ProcesoMntoMemorandum(Tx_Memorandum tx_Memorandum, List<Tx_Memorandum_Detalle> detalle, string sTipoTransac);
        Task<IEnumerable<Sg_Planta>?> Plantas();
        Task<IEnumerable<Tx_Tipo_Memorandum>?> TipoMemorandum();
        Task<IEnumerable<Tx_Material_Memorandum>?> Materiales();
        Task<IEnumerable<SEG_Usuarios>?> Usuario(string Cod_Trabajador, string Tip_Trabajador);
        Task<IEnumerable<Tx_Motivo_Memorandum>?> Motivos();
        Task<IEnumerable<Tx_Memorandum_Detalle>?> ObtieneDetalleMemorandumByNumMemo(string NumMemo);
        Task<(int Codigo, string Mensaje)> AvanzaEstadoMemorandum(string sCodUsuario, string sNumMemo, string sObservaciones);
        Task<IEnumerable<Tx_Transicion_Memorandum>?> ObtenerPermisosMemorandum(string sCodUsuario, string sNumMemo);
        Task<IEnumerable<Tx_Roles>?> ObtenerRolUsuarioMemorandum(string sCodUsuario, string sNumMemo);
        Task<(int Codigo, string Mensaje)> RevertirEstadoMemorandum(string sCodUsuario, string sNumMemo, string sObservaciones);
        Task<IEnumerable<Tx_Movimiento_Memorandum>?> HistorialMovimientoMemorandum(string sNumMemo);
        Task<(int Codigo, string Mensaje)> DevolverMemorandum(Tx_Memorandum tx_Memorandum);
        Task<IEnumerable<Tx_Roles>?> ObtenerInfoUsuarioMemorandum(string sCodUsuario);
        Task<IEnumerable<Tx_Memorandum_Detalle_Exportacion>?> ExportarInformacionMemorandumDetalle(DateTime FecIni, DateTime FecFin);
        Task<IEnumerable<Tx_Memorandum_Linea_Tiempo>?> ObtieneLineaTempoMemorandum(string NumMemo);
        Task<IEnumerable<Tx_Memorandum>?> ObtieneInformacionMemorandumDetalle(string NumMemo);
        
    }
}

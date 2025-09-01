using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.Memorandum;
using ic.backend.precotex.web.Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.Memorandum
{
    public interface ITxProcesoMemorandumService
    {
        Task<ServiceResponseList<Tx_Memorandum>?>ObtieneInformacionMemorandum(DateTime FecIni, DateTime FecFin, string NumMemo, string codUsuario, string CodPlantaGarita);
        Task<ServiceResponse<int>> ProcesoMntoMemorandum(Tx_Memorandum tx_Memorandum, List<Tx_Memorandum_Detalle> detalle, string sTipoTransac);
        Task<ServiceResponseList<Sg_Planta>?> Plantas();
        Task<ServiceResponseList<Tx_Tipo_Memorandum>?> TipoMemorandum();
        Task<ServiceResponseList<Tx_Material_Memorandum>?> Materiales();
        Task<ServiceResponseList<SEG_Usuarios>?> Usuario(string Cod_Trabajador, string Tip_Trabajador);
        Task<ServiceResponseList<Tx_Motivo_Memorandum>?> Motivos();
        Task<ServiceResponseList<Tx_Memorandum_Detalle>?> ObtieneDetalleMemorandumByNumMemo(string NumMemo);
        Task<ServiceResponse<int>> AvanzaEstadoMemorandum(string sCodUsuario, string sNumMemo, string sObservaciones);
        Task<ServiceResponseList<Tx_Transicion_Memorandum>?> ObtenerPermisosMemorandum(string sCodUsuario, string sNumMemo);
        Task<ServiceResponseList<Tx_Roles>?> ObtenerRolUsuarioMemorandum(string sCodUsuario, string sNumMemo);
        Task<ServiceResponse<int>> RevertirEstadoMemorandum(string sCodUsuario, string sNumMemo, string sObservaciones);
        Task<ServiceResponseList<Tx_Movimiento_Memorandum>?> HistorialMovimientoMemorandum(string sNumMemo);
        Task<ServiceResponse<int>> DevolverMemorandum(Tx_Memorandum tx_Memorandum);
        Task<ServiceResponseList<Tx_Roles>?> ObtenerInfoUsuarioMemorandum(string sCodUsuario);
    }
}

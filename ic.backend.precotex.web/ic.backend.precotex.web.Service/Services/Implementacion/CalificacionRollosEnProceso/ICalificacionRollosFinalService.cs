using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ic.backend.precotex.web.Entity.Entities.CalificacionRollosEnProceso;
using ic.backend.precotex.web.Service.common;

namespace ic.backend.precotex.web.Service.Services.Implementacion.CalificacionRollosFinal
{
    public interface ICalificacionRollosFinalService
    {
        Task<ServiceResponseList<EDefectos>?> ObtenerDefecto(EDefectos filtro);
        Task<ServiceResponseList<EMaquina>?> ObtenerMaquina();
        Task<ServiceResponseList<EAuditor>?> ObtenerSupervisor();
        Task<ServiceResponseList<EAuditor>?> ObtenerAuditor();
        Task<ServiceResponseList<ETurno>?> ObtenerTurno();
        Task<ServiceResponseList<EUnidadNegocio>?> ObtenerUnidadNegocio();
        Task<ServiceResponseList<EEstadoPartida>?> ObtenerEstadoPartida();
        Task<ServiceResponseList<EEstadoPartida>?> ObtenerProcesoAuditado();
        Task<ServiceResponseList<ECalificacion>?> ObtenerCalificacion();
        Task<ServiceResponseList<ECalificacion>?> ObtenerEstadoProceso();
        Task<ServiceResponseList<ERrollosPorPartida>?> BuscarPorPartida(string partida);
        Task<ServiceResponseList<ERrollosPorPartida>?> BuscarRolloPorPartidaDetalle(string partida, string articulo, string sObs, string sCodUsu, string sReco, string sIns, string sResDig, string sObsRec, string sCodCal, string sCodTel, int Reproceso);
        Task<ServiceResponseList<EPartidaCab>?> GuardarPartida(EPartidaCab partida);
        Task<ServiceResponseList<EPartidaPorRollo>?> BuscarPartidaPorRollo(string partida, string usuario);
        Task<ServiceResponseList<EPartidaPorRollo>?> updatePartidaPorRollo(string partida, int id);
        Task<ServiceResponseList<EUnionRollos>?> ObtenerDatosUnionRollos(EUnionRollos filtro);

        Task<ServiceResponseList<EGuardarUnioRollo>?> guardarDatosUnionRollos(EGuardarUnioRollo unionRollos);
        Task<ServiceResponseList<EDefectosRegistrados>?> ObtenerDefectosRegistradosPorRollo(string Cod_OrdTra, string Cod_Tela, string PrefijoMaquina, string CodigoRollo);
        Task<ServiceResponseList<EPartidaCab>?> GuardarDefectosPartida(EPartidaCab partida);
        Task<ServiceResponse<int>> EliminarDefectoRollo(string CodOrdTra, string CodigoRollo, string CodMotivo);
        Task<ServiceResponseList<EReproceso>?> ObtenerReproceso();
    }
}

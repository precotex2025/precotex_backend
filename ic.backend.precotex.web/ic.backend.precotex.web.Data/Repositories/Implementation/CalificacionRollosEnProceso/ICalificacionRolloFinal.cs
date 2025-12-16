using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ic.backend.precotex.web.Entity.Entities.CalificacionRollosEnProceso;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.CalificacionRollosFinal
{
    public interface ICalificacionRolloFinal
    {
        Task<IEnumerable<EDefectos>?> ObtenerDefecto(EDefectos filtro);
        Task<IEnumerable<EMaquina>?> ObtenerMaquina();
        Task<IEnumerable<EAuditor>?> ObtenerSupervisor();
        Task<IEnumerable<EAuditor>?> ObtenerAuditor();
        Task<IEnumerable<ETurno>?> ObtenerTurno();
        Task<IEnumerable<EUnidadNegocio>?> ObtenerUnidadNegocio();
        Task<IEnumerable<EEstadoPartida>?> ObtenerEstadoPartida();
        Task<IEnumerable<EEstadoPartida>?> ObtenerProcesoAuditado();
        Task<IEnumerable<ECalificacion>?> ObtenerCalificacion();
        Task<IEnumerable<ECalificacion>?> ObtenerEstadoProceso();
        Task<IEnumerable<ERrollosPorPartida>?> BuscarPorPartida(string partida);
        Task<IEnumerable<ERrollosPorPartida>?> BuscarRolloPorPartidaDetalle(string partida, string articulo, string sObs, string sCodUsu, string sReco, string sIns, string sResDig, string sObsRec, string sCodCal, string sCodTel, int Reproceso, string Maquina);
        Task<IEnumerable<EPartidaCab>?> GuardarPartida(EPartidaCab filtro);
        Task<IEnumerable<EPartidaPorRollo>?> BuscarPartidaPorRollo(string partida, string usuario);
        Task<IEnumerable<EPartidaPorRollo>?> updatePartidaPorRollo(string partida, int id);
        Task<IEnumerable<EUnionRollos>?> ObtenerDatosUnionRollos(EUnionRollos filtro);
        Task<IEnumerable<EGuardarUnioRollo>?> guardarDatosUnionRollos(EGuardarUnioRollo filtro);
        Task<IEnumerable<EPartidaCab>?> reprocesarPartidaRollos(EPartidaCab filtro);
        Task<IEnumerable<EDefectosRegistrados>?> ObtenerDefectosRegistradosPorRollo(string Cod_OrdTra, string Cod_Tela, string PrefijoMaquina, string CodigoRollo);
        Task<IEnumerable<EPartidaCab>?> GuardarDefectosPartida(EPartidaCab filtro);
        Task<(int Codigo, string Mensaje)> EliminarDefectoRollo(string CodOrdTra, string CodigoRollo, string CodMotivo);
        Task<IEnumerable<EReproceso>?> ObtenerReproceso();
    }
}

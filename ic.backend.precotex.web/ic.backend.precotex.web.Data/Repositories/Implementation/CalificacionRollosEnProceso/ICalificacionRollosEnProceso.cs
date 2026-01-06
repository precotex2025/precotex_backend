using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.CalificacionRollosEnProceso;
using ic.backend.precotex.web.Entity.Entities.Desglose;
using ic.backend.precotex.web.Entity.Entities.QR;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.CalificacionRollosEnProceso
{
    public interface ICalificacionRollosEnProceso
    {
        Task<IEnumerable<EDefectos>?> ObtenerDefecto(EDefectos filtro);
        Task<IEnumerable<EMaquina>?> ObtenerMaquina();
        Task<IEnumerable<EMaquina>?> ObtenerProveedores();
        Task<IEnumerable<EAuditor>?> ObtenerSupervisor();
        Task<IEnumerable<EAuditor>?> ObtenerAuditor();
        Task<IEnumerable<ETurno>?> ObtenerTurno();
        Task<IEnumerable<EUnidadNegocio>?> ObtenerUnidadNegocio();
        Task<IEnumerable<EEstadoPartida>?> ObtenerEstadoPartida();
        Task<IEnumerable<EEstadoPartida>?> ObtenerProcesoAuditado();
        Task<IEnumerable<ECalificacion>?> ObtenerCalificacion();
        Task<IEnumerable<ECalificacion>?> ObtenerEstadoProceso();
        Task<IEnumerable<ERrollosPorPartida>?> BuscarPorPartida(string partida); 
        Task<IEnumerable<ERrollosPorPartida>?> BuscarArticuloPorPartida(string partida);
        Task<IEnumerable<ERrollosPorPartida>?> BuscarRolloPorPartidaDetalle(string partida, string articulo);
        Task<IEnumerable<EPartidaCab>?> GuardarPartida(EPartidaCab filtro);
        Task<IEnumerable<EPartidaPorRollo>?> BuscarPartidaPorRollo(string partida, string usuario);
        Task<IEnumerable<EPartidaPorRollo>?> updatePartidaPorRollo(string partida, int id);

        //REGISTRO QR
        Task<IEnumerable<E_RegistroQR>?> GrabarQR(E_RegistroQR request);
        Task<IEnumerable<Tx_Maquinas_Gral_QR_P2>?> ObtenerMaquinaQRP2(string CodMaquina);

        //REGISTRO DE SERVICIO DE DESGLOSE
        Task<IEnumerable<String>?> ObtenerDni(string usuario);
        Task<IEnumerable<ERrollosPorPartida>?> BuscarPartida(string partida);
        Task<IEnumerable<E_RegistroDesgloseRequest>?> RegistrarDesglose(E_RegistroDesgloseRequest model);
        Task<IEnumerable<ListaDesgloseDetalle>?> ListarDesglose();
        Task<IEnumerable<E_DesgloseItem>?> ListarDesgloseItem(string id_Desglose);

        Task<IEnumerable<E_UpdateDesglose>?> ActualizarDesgloseItem(E_UpdateDesglose model);

        Task<IEnumerable<E_RegistroDesgloseRequest>?> EliminarDesglose(int id);


        //BUSQUEDA DATOS CABECERA
        Task<IEnumerable<EPartidaCab>?> ObtenerDatosCabeceraEnProceso(string Cod_OrdTra);
    }
}

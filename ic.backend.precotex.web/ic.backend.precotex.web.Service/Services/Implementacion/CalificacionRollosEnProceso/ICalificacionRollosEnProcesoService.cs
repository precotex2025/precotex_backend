using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.CalificacionRollosEnProceso;
using ic.backend.precotex.web.Entity.Entities.Desglose;
using ic.backend.precotex.web.Entity.Entities.QR;
using ic.backend.precotex.web.Service.common;

namespace ic.backend.precotex.web.Service.Services.Implementacion.CalificacionRollosEnProceso
{
    public  interface ICalificacionRollosEnProcesoService
    {
        Task<ServiceResponseList<EDefectos>?> ObtenerDefecto(EDefectos filtro);
        Task<ServiceResponseList<EMaquina>?> ObtenerMaquina();
        Task<ServiceResponseList<EMaquina>?> ObtenerProveedores();
        Task<ServiceResponseList<EAuditor>?> ObtenerSupervisor();
        Task<ServiceResponseList<EAuditor>?> ObtenerAuditor();
        Task<ServiceResponseList<ETurno>?> ObtenerTurno();
        Task<ServiceResponseList<EUnidadNegocio>?> ObtenerUnidadNegocio();
        Task<ServiceResponseList<EEstadoPartida>?> ObtenerEstadoPartida();
        Task<ServiceResponseList<EEstadoPartida>?> ObtenerProcesoAuditado();
        Task<ServiceResponseList<ECalificacion>?> ObtenerCalificacion();
        Task<ServiceResponseList<ECalificacion>?> ObtenerEstadoProceso();
        Task<ServiceResponseList<ERrollosPorPartida>?> BuscarPorPartida(string partida);     
        Task<ServiceResponseList<ERrollosPorPartida>?> BuscarArticuloPorPartida(string partida);
        Task<ServiceResponseList<ERrollosPorPartida>?> BuscarRolloPorPartidaDetalle(string partida, string articulo);
        Task<ServiceResponseList<EPartidaCab>?> GuardarPartida(EPartidaCab partida);
        Task<ServiceResponseList<EPartidaPorRollo>?> BuscarPartidaPorRollo(string partida, string usuario);
        Task<ServiceResponseList<EPartidaPorRollo>?> updatePartidaPorRollo(string partida, int id);

        //REGISTRO QR
        Task<ServiceResponseList<E_RegistroQR>?> GrabarQR(E_RegistroQR request);
        Task<ServiceResponseList<Tx_Maquinas_Gral_QR_P2>?> ObtenerMaquinaQRP2(string CodMaquina);

        //REGISTRO DE SERVICIO DE DESGLOSE
        Task<ServiceResponseList<string>> ObtenerDni(string usuario);
        Task<ServiceResponseList<ERrollosPorPartida>?> BuscarPartida(string partida);
        Task<ServiceResponseList<E_RegistroDesgloseRequest>?> RegistrarDesglose(E_RegistroDesgloseRequest model);
        Task<ServiceResponseList<ListaDesgloseDetalle>?> ListarDesglose();
        Task<ServiceResponseList<E_DesgloseItem>?> ListarDesgloseItem(string id_Desglose);
        Task<ServiceResponseList<E_UpdateDesglose>?> ActualizarDesgloseItem(E_UpdateDesglose model);

        Task<ServiceResponseList<E_RegistroDesgloseRequest>?> EliminarDesglose(int id);

        //BUSQUEDA DATOS CABECERA
        Task<ServiceResponseList<EPartidaCab>?> ObtenerDatosCabeceraEnProceso(string Cod_OrdTra);
        Task<ServiceResponseList<EAuditor>?> ObtenerAuditor(string Cod_Usuario);
    }
}

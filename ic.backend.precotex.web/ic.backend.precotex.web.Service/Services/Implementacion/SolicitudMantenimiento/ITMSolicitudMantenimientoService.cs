using ic.backend.precotex.web.Entity.Entities.SolicitudMantenimiento;
using ic.backend.precotex.web.Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.SolicitudMantenimiento
{
    public interface ITMSolicitudMantenimientoService
    {
        Task<ServiceResponse<int>> ProcesoMntoSolicitudMantenimiento(TM_Solicitud_Mantenimiento tM_Solicitud_Mantenimiento, string sTipoTransac);
        Task<ServiceResponseList<TM_Maquina>?> ObtieneInformacionMaquinas(string sCodMaquina);
        Task<ServiceResponseList<TM_Solicitud_Mantenimiento>?> ObtieneInformacionSolicitudMantenimiento(DateTime FecIni, DateTime FecFin, string codUsuario);
        /*VISOR*/
        Task<ServiceResponseList<TM_Solicitud_Mantenimiento>?> ObtieneInformacionSolicitudesVisor();
        Task<ServiceResponse<int>> AvanzaEstadoSolicitudMantenimiento(string sCodUsuario, string sCodSolicitud, string sObservaciones, string sDatosLider);
        Task<ServiceResponse<int>> ProcesoMntoTiempoManMquina(TM_Tiempo_Mantenimiento tM_Tiempo_Mantenimiento, string sTipoTransac);
        Task<ServiceResponseList<TM_Solicitud_Mantenimiento>?> ObtieneInformacionSolicitudMantenimientoByNumero(string sCodSolicitud);
    }
}

using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.Memorandum;
using ic.backend.precotex.web.Entity.Entities.SolicitudMantenimiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.SolicitudMantenimiento
{
    public interface ITMSolicitudMantenimientoRepository
    {
        Task<(int Codigo, string Mensaje)> ProcesoMntoSolicitudMantenimiento(TM_Solicitud_Mantenimiento tM_Solicitud_Mantenimiento, string sTipoTransac);
        Task<IEnumerable<TM_Maquina>?> ObtieneInformacionMaquinas(string sCodMaquina);
        Task<IEnumerable<TM_Solicitud_Mantenimiento>?> ObtieneInformacionSolicitudMantenimiento(DateTime FecIni, DateTime FecFin, string codUsuario);
<<<<<<< HEAD

        /*VISOR*/
        Task<IEnumerable<TM_Solicitud_Mantenimiento>?> ObtieneInformacionSolicitudesVisor();


=======
        Task<(int Codigo, string Mensaje)> AvanzaEstadoSolicitudMantenimiento(string sCodUsuario, string sCodSolicitud, string sObservaciones);
>>>>>>> main
    }
}

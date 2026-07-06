using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ic.backend.precotex.web.Entity.Entities.Tejeduria;


namespace ic.backend.precotex.web.Data.Repositories.Implementation.Tejeduria
{
    public interface ITjSolicitudDevolucionAuditoriaRepository
    {
        Task<IEnumerable<Tj_Muestra_Solicitud_Devolucion>?> ListaSolicitudDevolucion(int NumSolicitud, string Lote, DateTime Fecha_Ini, DateTime Fecha_Fin, string Estado);
        Task<IEnumerable<Tj_Muestra_Solicitud_Devolucion_Bultos>?> ListaSolicitudDevolucionBultos(int NumSolicitud, string Lote, string Semana, string Color, string Marca, string Conera);
        Task<(int Codigo, string Mensaje)> Proceso(Tj_Mantenimiento_Solicitud_Devolucion Mant_SolicitudDevolucion, string sTipoTransac);
    }
}

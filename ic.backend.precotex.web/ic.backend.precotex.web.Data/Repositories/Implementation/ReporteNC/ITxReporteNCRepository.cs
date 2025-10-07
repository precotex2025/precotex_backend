using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.ReporteNC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.ReporteNC
{
    public interface ITxReporteNCRepository
    {
        Task<IEnumerable<Tx_ReporteNC>?> ListarRegistro(int Rep_ID);
        Task<(int Codigo, string Mensaje)> RegistrarReporteNC(Tx_ReporteNC tx_ReporteNC);
        Task<IEnumerable<Sg_Planta>?> ListarPlantas();
        Task<IEnumerable<Tx_ReportesNC_Clasificacion>?> ListarClasificaciones();
        Task<(int Codigo, string Mensaje)> ActualizarEstado(Tx_ReporteNC tx_ReporteNC);
        Task<IEnumerable<Tx_ReporteNC>?> ListarDatosResolvedor(int Rep_ID);
        Task<(int Codigo, string Mensaje)> ActualizarReporteNC(Tx_ReporteNC tx_ReporteNC);
        Task<IEnumerable<Tx_ReportesNC_Estados>?> ListarEstados();
        Task<(int Codigo, string Mensaje)> ActualizarReporteNCOriginal(Tx_ReporteNC tx_ReporteNC);
    }
}

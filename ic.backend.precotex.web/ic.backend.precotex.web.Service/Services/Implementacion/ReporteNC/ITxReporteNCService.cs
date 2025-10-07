using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.ReporteNC;
using ic.backend.precotex.web.Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.ReporteNC
{
    public interface ITxReporteNCService
    {
        Task<ServiceResponseList<Tx_ReporteNC>?> ListarRegistro(int Rep_ID);
        Task<ServiceResponse<int>> RegistrarReporteNC(Tx_ReporteNC tx_ReporteNC);
        Task<ServiceResponseList<Sg_Planta>?> ListarPlantas();
        Task<ServiceResponseList<Tx_ReportesNC_Clasificacion>?> ListarClasificaciones();
        Task<ServiceResponse<int>> ActualizarEstado(Tx_ReporteNC tx_ReporteNC);
        Task<ServiceResponseList<Tx_ReporteNC>?> ListarDatosResolvedor(int Rep_ID);
        Task<ServiceResponse<int>> ActualizarReporteNC(Tx_ReporteNC tx_ReporteNC);
        Task<ServiceResponseList<Tx_ReportesNC_Estados>?> ListarEstados();
        Task<ServiceResponse<int>> ActualizarReporteNCOriginal(Tx_ReporteNC tx_ReporteNC);
    }
}

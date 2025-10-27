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
        Task<ServiceResponse<int>> RegistrarImagendeReporteNC(int Rep_Id, string Img_Des, int Img_Fam);
        Task<ServiceResponseList<Tx_ReporteNC_Img>?> ObtenerImagenes(int Rep_Id, int Img_Fam);
        Task<ServiceResponse<int>> EliminarImagenes(int Img_Id);
        Task<ServiceResponseList<Tx_ReporteNC>?> BuscarRegistros(int Num_Planta, int Are_Id, int Resp_Id, int Rep_Niv_Rgo, int Rep_Est);
        Task<ServiceResponseList<Tx_ReportesNC_Riesgos>?> ListarRiesgos();
        Task<ServiceResponse<int>> EliminarImagenParaElPatch(string Img_Des);

        /*AREAS*/
        Task<ServiceResponseList<Tx_ReportesNC_Areas>?> ObtenerAreas(int Are_Id);
        Task<ServiceResponse<int>> RegistrarArea(Tx_ReportesNC_Areas _txReportesNCAreas);
        Task<ServiceResponse<int>> ActualizarArea(Tx_ReportesNC_Areas _txReportesNCAreas);
        Task<ServiceResponse<int>> EliminarArea(int Are_Id);
        Task<ServiceResponseList<Tx_ReportesNC_Areas>?> ObtenerAreaXSede(int Num_Planta, int Are_Id);

        /*RESPONSABLES*/
        Task<ServiceResponseList<Tx_ReportesNC_Responsables>?> ObtenerResponsables(int Resp_Id);
        Task<ServiceResponse<int>> RegistrarResponsable(Tx_ReportesNC_Responsables _txReportesNCResponsables);
        Task<ServiceResponse<int>> ActualizarResponsable(Tx_ReportesNC_Responsables _txReportesNCResponsables);
        Task<ServiceResponse<int>> EliminarResponsable(int Resp_Id);

        /*USUARIOS*/
        Task<ServiceResponseList<Tx_ReportesNC_Usuarios>?> ObtenerUsuarios(string Usr_Cod);
    }
}

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
        Task<(int Codigo, string Mensaje)> RegistrarImagendeReporteNC(int Rep_Id, string Img_Des, int Img_Fam);
        Task<IEnumerable<Tx_ReporteNC_Img>?> ObtenerImagenes(int Rep_Id, int Img_Fam);
        Task<(int Codigo, string Mensaje)> EliminarImagenes(int Img_Id);
        Task<IEnumerable<Tx_ReporteNC>?> BuscarRegistros(int Num_Planta, int Are_Id, int Resp_Id, int Rep_Niv_Rgo, int Rep_Est);
        Task<IEnumerable<Tx_ReportesNC_Riesgos>?> ListarRiesgos();
        Task<(int Codigo, string Mensaje)> EliminarImagenParaElPatch(string Img_Des);

        /*AREAS*/
        Task<IEnumerable<Tx_ReportesNC_Areas>?> ObtenerAreas(int Are_Id);
        Task<(int Codigo, string Mensaje)> RegistrarArea(Tx_ReportesNC_Areas _txReportesNCAreas);
        Task<(int Codigo, string Mensaje)> ActualizarArea(Tx_ReportesNC_Areas _txReportesNCAreas);
        Task<(int Codigo, string Mensaje)> EliminarArea(int Are_Id);
        Task<IEnumerable<Tx_ReportesNC_Areas>?> ObtenerAreaXSede(int Num_Planta, int Are_Id);

        /*RESPONSABLES*/
        Task<IEnumerable<Tx_ReportesNC_Responsables>?> ObtenerResponsables(int Resp_Id);
        Task<(int Codigo, string Mensaje)> RegistrarResponsable(Tx_ReportesNC_Responsables _txReporteNCResponsable);
        Task<(int Codigo, string Mensaje)> ActualizarResponsable(Tx_ReportesNC_Responsables _txReporteNCResponsable);
        Task<(int Codigo, string Mensaje)> EliminarResponsable(int Resp_Id);

        /*USUARIOS*/
        Task<IEnumerable<Tx_ReportesNC_Usuarios>?> ObtenerUsuarios(string Usr_Cod);
    }
}

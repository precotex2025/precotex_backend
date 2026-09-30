using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.SecureNorm
{
    public interface ISNUsuarioService
    {
        Task<ServiceResponse<int>> ProcesoMnto(SN_Usuario sN_Usuario, string sTipoTransac);
        Task<ServiceResponseList<SN_Usuario>?> Listado(string? sCodigo_Organizacion, string? sCodigo_Sede, string? sCodigo_Proceso);
        Task<ServiceResponseList<SN_Trabajador_Spring>?> ListadoTrabajadoresSpring();
        Task<ServiceResponseList<ComboGral>?> ListadoNivelJerarquico();
        Task<ServiceResponseList<SN_Log_Acceso>?> ListadoLogAccesos(int top, bool soloUltimo);
        Task<ServiceResponse<int>> RegistrarLogAcceso(SN_Log_Acceso log);
        Task<ServiceResponse<bool>> EnviarCredencialesCorreo(string destinatario, string nombre, string usuario, string claveTemporal);
        Task<ServiceResponse<int>> CambiarPasswordPrimerIngreso(string codUsuario, string password);
        Task<ServiceResponse<SN_Validar_Primer_Ingreso>?> ValidarPrimerIngreso(string codUsuario);
    }
}
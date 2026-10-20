using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.SecureNorm
{
    public interface ISNUsuarioRepository
    {
        Task<(int Codigo, string Mensaje)> ProcesoMnto(SN_Usuario sN_Usuario, string sTipoTransac);
        Task<IEnumerable<SN_Usuario>?> Listado(string? sCodigo_Organizacion, string? sCodigo_Sede, string? sCodigo_Proceso);
        Task<IEnumerable<SN_Trabajador_Spring>?> ListadoTrabajadoresSpring();
        Task<IEnumerable<ComboGral>?> ListadoNivelJerarquico();
        Task<IEnumerable<SN_Log_Acceso>?> ListadoLogAccesos(int top, bool soloUltimo);
        Task<(int Codigo, string Mensaje)> RegistrarLogAcceso(SN_Log_Acceso log);
        Task<(int Codigo, string Mensaje)> EnviarCredencialesCorreo(string destinatario, string nombre, string usuario, string claveTemporal);
        Task<(int Codigo, string Mensaje)> CambiarPasswordPrimerIngreso(string codUsuario, string password);
        Task<SN_Validar_Primer_Ingreso?> ValidarPrimerIngreso(string codUsuario);
    }
}
using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.SecureNorm
{
    public interface ISNPuestoService
    {
        Task<ServiceResponse<int>> ProcesoMnto(SN_Puesto sN_Proceso, string sTipoTransac);
        Task<ServiceResponseList<SN_Puesto>?> Listado(string sCodigo_Organizacion, string sCodigo_Sede, string sCodigo_Nivel_Riesgo);
    }
}

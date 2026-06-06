using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.SecureNorm
{
    public interface ISNDocumentosControladosService
    {
        Task<ServiceResponse<int>> ProcesoCarpetaCtrolMnto(SN_Carpeta_Control sN_Carpeta_Control, string sTipoTransac);
        Task<ServiceResponse<int>> ProcesoMnto(SN_Documentos_Controlados sN_Documentos_Controlados, string sTipoTransac);
        Task<ServiceResponseList<SN_Documentos_Controlados>?>Listado(string sCodigo_Organizacion, string sCodigo_Sede, string sCodigo_Puesto, string sCodigo_Proceso);
    }
}

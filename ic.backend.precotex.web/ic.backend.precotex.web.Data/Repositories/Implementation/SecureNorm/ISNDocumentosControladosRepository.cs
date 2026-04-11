using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.SecureNorm
{
    public interface ISNDocumentosControladosRepository
    {
        Task<(int Codigo, string Mensaje)> ProcesoCarpetaCtrolMnto(SN_Carpeta_Control sN_Carpeta_Control, string sTipoTransac);
        Task<(int Codigo, string Mensaje)> ProcesoMnto(SN_Documentos_Controlados sN_Documentos_Controlados, string sTipoTransac);
        Task<IEnumerable<SN_Documentos_Controlados>?> Listado(string sCodigo_Organizacion, string sCodigo_Sede, string sCodigo_Puesto, string sCodigo_Proceso);
    }
}

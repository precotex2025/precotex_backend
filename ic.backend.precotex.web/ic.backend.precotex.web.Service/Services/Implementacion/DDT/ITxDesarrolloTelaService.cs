using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.DDT
{
    public interface ITxDesarrolloTelaService
    {

        Task<ServiceResponseList<Tx_Desarrollo_Telas>?> ListadoDesarrolloTelas(string sAccion, string sCodTela, string sCodVersion, string sNomVersion, string sComentario, string sRutaArchivo, string sCodMotivoSolicitud, string sComentarioSolicitud, string sCodUsuario);
        Task<ServiceResponse<int>> ProcesoDesarrolloTela(string sAccion, string sCodTela,
                                                                       string sCodVersion, string sNomVersion,
                                                                       string sComentario, string sRutaArchivo,
                                                                       string sCodMotivoSolicitud, string sComentarioSolicitud,
                                                                       string sCodUsuario);
    }
}

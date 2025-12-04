using ic.backend.precotex.web.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.DDT
{
    public interface ITxDesarrolloTelaRepository
    {
        Task<IEnumerable<Tx_Desarrollo_Telas>?> ListadoDesarrolloTelas(string sAccion, string sCodTela, 
                                                                       string sCodVersion, string sNomVersion, 
                                                                       string sComentario, string sRutaArchivo,
                                                                       string sCodMotivoSolicitud, string sComentarioSolicitud,
                                                                       string sCodUsuario);

        Task<(int Codigo, string Mensaje)> ProcesoDesarrolloTela(string sAccion, string sCodTela,
                                                                       string sCodVersion, string sNomVersion,
                                                                       string sComentario, string sRutaArchivo,
                                                                       string sCodMotivoSolicitud, string sComentarioSolicitud,
                                                                       string sCodUsuario);


    }
}

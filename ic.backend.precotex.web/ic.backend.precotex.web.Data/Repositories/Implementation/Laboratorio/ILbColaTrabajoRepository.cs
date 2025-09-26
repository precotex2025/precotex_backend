using ic.backend.precotex.web.Entity.Entities.Laboratorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.Laboratorio
{
    public interface ILbColaTrabajoRepository
    {
        /*
            CABECERA 
        */
        Task<IEnumerable<Lb_ColTra_Cab>?> ListaSDCPorEstado();


        /*
            DETALLE 
        */
        Task<IEnumerable<Lb_ColTra_Det>?> ListaColoresSDC(int Corr_Carta);

    }
}

using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.Laboratorio;
using ic.backend.precotex.web.Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.Laboratorio
{
    public interface ILbColaTrabajoService
    {
        /*
            CABECERA 
        */
        Task<ServiceResponseList<Lb_ColTra_Cab>?> ListaSDCPorEstado(string Flg_Est_Lab);



        /*
            DETALLE 
        */
        Task<ServiceResponseList<Lb_ColTra_Det>?> ListaColoresSDC(int Corr_Carta);
    }
}

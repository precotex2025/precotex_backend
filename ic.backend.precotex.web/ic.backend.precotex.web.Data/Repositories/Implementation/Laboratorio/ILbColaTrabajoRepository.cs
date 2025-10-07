using ic.backend.precotex.web.Entity.Entities;
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
        Task<IEnumerable<Lb_ColTra_Cab>?> ListaSDCPorEstado(string Flg_Est_Lab, DateTime Fec_Ini, DateTime Fec_Fin);


        /*
            DETALLE 
        */
        Task<IEnumerable<Lb_ColTra_Det>?> ListaColoresSDC(int Corr_Carta);
        Task<(int Codigo, string Mensaje)> RegistrarDetalleColorSDC(Lb_ColTra_Det lbColaTrabajoDet);
        Task<IEnumerable<Lb_ColTra_Det>?> LlenarDesplegable();
        Task<IEnumerable<Lb_ColTra_Cab_y_Det>?> LlenarGrillaDesplegable(int Corr_Carta, int Sec);
        Task<(int Codigo, string Mensaje)> ActualizarEstadoDeColor(Lb_ColTra_Det lb_ColTra_Det);


        /*
            COLORANTES
        */
        Task<(int Codigo, string Mensaje)> AgregarOpcionColorante(Lb_AgrOpc_Colorantes lb_AgrOpc_Colorantes);
        Task<IEnumerable<Lg_Item>?> CargarComboBoxItem();

        /*
            INFORMACION SDC
        */
        //Task<Lb_Informe_SDC?> CargarInformeSDC(int Corr_Carta, int Sec);
        Task<IEnumerable<Lb_Informe_SDC>> CargarInformeSDC(int Corr_Carta, int Sec);
        Task<IEnumerable<Lb_AgrOpc_Colorantes>?> CargarGridHojaFormulacion(int Corr_Carta, int Sec);
    }
}

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
        Task<ServiceResponseList<Lb_ColTra_Cab>?> ListaSDCPorEstado(string Flg_Est_Lab, DateTime Fec_Ini, DateTime Fec_Fin);



        /*
            DETALLE 
        */
        Task<ServiceResponseList<Lb_ColTra_Det>?> ListaColoresSDC(int Corr_Carta);
        Task<ServiceResponse<int>> RegistrarDetalleColorSDC(Lb_ColTra_Det lbColaTraDet);
        Task<ServiceResponseList<Lb_ColTra_Det>?> LlenarDesplegable();
        Task<ServiceResponseList<Lb_ColTra_Cab_y_Det>?> LlenarGrillaDesplegable(int Corr_Carta, int Sec);
        Task<ServiceResponse<int>> ActualizarEstadoDeColor(Lb_ColTra_Det lb_ColTra_Det);

        /*
            COLORANTES
        */
        Task<ServiceResponse<int>> AgregarOpcionColorante(Lb_AgrOpc_Colorantes lb_AgrOpc_Colorantes);
        Task<ServiceResponseList<Lg_Item>?> CargarComboBoxItem();

        /*
            INFORMACION SDC 
        */
        Task<ServiceResponseList<Lb_Informe_SDC>?> CargarInformeSDC(int Corr_Carta, int Sec);

        /*
             HOJA DE FORMULACION
        */
        Task<ServiceResponseList<Lb_AgrOpc_Colorantes>?> CargarGridHojaFormulacion(int Corr_Carta, int Sec);
    }
}

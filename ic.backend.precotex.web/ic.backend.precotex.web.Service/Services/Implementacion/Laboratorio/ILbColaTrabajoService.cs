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
        Task<ServiceResponseList<Lb_ColTra_Cab>?> ListaSDCPorEstado(string Flg_Est_Lab, DateTime Fec_Ini, DateTime Fec_Fin, string Usr_Cod);



        /*
            DETALLE 
        */
        Task<ServiceResponseList<Lb_ColTra_Det>?> ListaColoresSDC(int Corr_Carta);
        Task<ServiceResponse<int>> RegistrarDetalleColorSDC(Lb_ColTra_Det lbColaTraDet);
        Task<ServiceResponseList<Lb_ColTra_Det>?> LlenarDesplegable();
        Task<ServiceResponseList<Lb_ColTra_Cab_y_Det>?> LlenarGrillaDesplegable(int Corr_Carta, int Sec);
        Task<ServiceResponse<int>> ActualizarEstadoDeColor(Lb_ColTra_Det lb_ColTra_Det);
        Task<ServiceResponse<int>> ActualizarEstadoDeColorTricomia(Lb_AgrOpc_Colorantes _lbAgrOpcColorante);
        Task<ServiceResponse<int>> ActualizarEstadoDeColorTricomiaAutolab(Lb_AgrOpc_Colorantes _lbAgrOpcColorante);

        /*
            COLORANTES
        */
        Task<ServiceResponse<int>> AgregarOpcionColorante(Lb_AgrOpc_Colorantes lb_AgrOpc_Colorantes);
        Task<ServiceResponseList<Lg_Item>?> CargarComboBoxItem();
        Task<ServiceResponseList<Lb_AgrOpc_Colorantes>?> ListarIngresoManual(int Corr_Carta, int Sec, int Correlativo);

        /*
            INFORMACION SDC 
        */
        Task<ServiceResponseList<Lb_Informe_SDC>?> CargarInformeSDC(int Corr_Carta, int Sec);

        /*
             HOJA DE FORMULACION
        */
        Task<ServiceResponseList<Lb_AgrOpc_Colorantes>?> CargarGridHojaFormulacion(int Corr_Carta, int Sec);
        Task<ServiceResponse<int>> CopiarOpcionColorante(Lb_AgrOpc_Colorantes lb_AgrOpc_Colorantes);
        Task<ServiceResponse<int>> EliminarOpcionColorante(int Corr_Carta, int Sec, int Correlativo);
        Task<ServiceResponseList<Lb_Colorantes>?> ListarColorantesAgregarOpcion();

        /*
            JABONADOS 
        */
        Task<ServiceResponseList<Lb_Jabonados>?> ListarJabonados();
        Task<ServiceResponseList<Lb_Jabonados>?> ListarJabonadosCalculado(decimal Colorante_Total, string Familia);
        Task<ServiceResponseList<Lb_Jabonados>?> ListarJabonadoMantenimiento();
        Task<ServiceResponse<int>> RegistrarJabonado(Lb_Jabonados lb_Jabonados);
        Task<ServiceResponse<int>> ModificarJabonado(Lb_Jabonados lb_Jabonados);
        Task<ServiceResponse<int>> DeshabilitarJabonado(Lb_Jabonados lb_Jabonados);
        Task<ServiceResponseList<Lb_Jabonados_Detalle>?> ListarJabonadosDetalleMantenimiento(int Jab_Id);
        Task<ServiceResponse<int>> RegistrarJabonadoDetalle(Lb_Jabonados_Detalle lb_Jabonados_Detalle);
        Task<ServiceResponse<int>> ModificarJabonadoDetalle(Lb_Jabonados_Detalle lb_Jabonados_Detalle);
        Task<ServiceResponse<int>> DeshabilitarJabonadoDetalle(Lb_Jabonados_Detalle lb_Jabonados_Detalle);

        /*
            FIJADOS
        */
        Task<ServiceResponseList<Lb_Fijados>?> ListarFijados();
        Task<ServiceResponseList<Lb_Fijados>?> ListarFijadosCalculado(decimal Colorante_Total, string Familia);
        Task<ServiceResponseList<Lb_Fijados>?> ListarFijadosMantenimiento();
        Task<ServiceResponse<int>> RegistrarFijado(Lb_Fijados lb_Fijados);
        Task<ServiceResponse<int>> ModificarFijado(Lb_Fijados lb_Fijados);
        Task<ServiceResponse<int>> DeshabilitarFijado(Lb_Fijados lb_Fijados);
        Task<ServiceResponseList<Lb_Fijados_Detalle>?> ListarFijadosDetalleMantenimiento(int Fij_Id);
        Task<ServiceResponse<int>> RegistrarFijadoDetalle(Lb_Fijados_Detalle lb_Fijados_Detalle);
        Task<ServiceResponse<int>> ModificarFijadoDetalle(Lb_Fijados_Detalle lb_Fijados_Detalle);
        Task<ServiceResponse<int>> DeshabilitarFijadoDetalle(Lb_Fijados_Detalle lb_Fijados_Detalle);

        /*
            CARBONATO Y SODA 
        */
        Task<ServiceResponseList<Lb_Colorantes_Componentes_Extra>?> ListarCarbonatoSodaCalculado(decimal Colorante_Total, string Familia, int Com_Cod_Con);

        /*
            COLA AUTOLAB
        */
        Task<ServiceResponseList<Lb_ColTra_Det>?> ListarColaAutolab();
        Task<ServiceResponse<int>> EnviarADispensado(Lb_AgrOpc_Colorantes lb_AgrOpc_Colorantes);
        Task<ServiceResponseList<Lb_ColTra_Det>?> ListarDispensado();

        /*
            AHIBA
        */
        Task<ServiceResponseList<Lb_Ahibas>?> ListaAhibas();
        Task<ServiceResponse<int>> CargarAahiba(Lb_AgrOpc_Colorantes _lbAgrOpcColorante);
        Task<ServiceResponseList<Lb_ColTra_Det>?> ListarItemsEnAhiba(int Ahi_Id);
        Task<ServiceResponse<int>> ProcesoAhiba(Lb_Ahibas _Ahibas);



        Task<ServiceResponse<int>> ActualizarPH(Lb_ColTra_Det lb_ColTra_Det);
        Task<ServiceResponse<int>> EnviarAutolab(Lb_AgrOpc_Colorantes _lbAgrOpcColorante);
        Task<ServiceResponse<int>> AgregarAuxiliaresHojaFormulacion(Lb_AgrOpc_Colorantes _lbAgrOpcColorante);
        Task<ServiceResponse<int>> LlenarTextoFinal(Lb_AgrOpc_Colorantes _lbAgrOpcColorante);
        Task<ServiceResponseList<Lb_ColTra_Det>?> ListarJabonado();
        Task<ServiceResponseList<Lb_ColTra_Det>?> ListarJabonadoExcluido();
        Task<ServiceResponseList<Lb_Colorantes_Componentes_Extra>?> ListarFamiliasProceso();
        Task<ServiceResponseList<Lb_AgrOpc_Colorantes>?> CargarColoranteParaCopiar(int Corr_Carta, int Sec, int Correlativo);
        Task<ServiceResponseList<Lb_AgrOpc_Colorantes>?> CargarColoranteParaDetalle(int Corr_Carta, int Sec, int Correlativo);


        /*
            LOGIN 
        */
        Task<ServiceResponseList<Lb_Usuarios>?> GetUsuarioWeb(string Cod_Usuario);

        /*
            REPORTE   
        */
        Task<ServiceResponseList<Lb_Reporte>?> CargarDatosReporte(int Corr_Carta, int Sec, int Correlativo);

        /*
            COMPONENTES EXTRA   
        */
        Task<ServiceResponse<int>> RegistrarProceso(ComponentesExtra _ComponentesExtra);
        Task<ServiceResponse<int>> ModificarProceso(ComponentesExtra _ComponentesExtra);
        Task<ServiceResponse<int>> DeshabilitarProceso(ComponentesExtra _ComponentesExtra);
        Task<ServiceResponseList<ComponentesExtraValores>?> ListarProcesoValor(string Pro_Cod);
        Task<ServiceResponse<int>> RegistrarProcesoValor(ComponentesExtraValores _ComponentesExtraValores);
        Task<ServiceResponse<int>> ModificarProcesoValor(ComponentesExtraValores _ComponentesExtraValores);
        Task<ServiceResponse<int>> DeshabilitarProcesoValor(ComponentesExtraValores _ComponentesExtraValores);
        Task<ServiceResponseList<Lb_Curvas>?> ListarCurvas(string Pro_Cod);

    }
}

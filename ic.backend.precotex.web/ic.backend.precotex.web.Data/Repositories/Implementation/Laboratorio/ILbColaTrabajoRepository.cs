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
        Task<(int Codigo, string Mensaje)> ActualizarEstadoDeColorTricomia(Lb_AgrOpc_Colorantes _lbAgrOpcColorante);
        Task<(int Codigo, string Mensaje)> ActualizarEstadoDeColorTricomiaAutolab(Lb_AgrOpc_Colorantes _lbAgrOpcColorante);

        /*
            COLORANTES
        */
        Task<(int Codigo, string Mensaje)> AgregarOpcionColorante(Lb_AgrOpc_Colorantes lb_AgrOpc_Colorantes);
        Task<IEnumerable<Lg_Item>?> CargarComboBoxItem();
        Task<(int Codigo, string Mensaje)> CopiarOpcionColorante(Lb_AgrOpc_Colorantes lb_AgrOpc_Colorantes);
        Task<(int Codigo, string Mensaje)> EliminarOpcionColorante(int Corr_Carta, int Sec, int Correlativo);
        Task<IEnumerable<Lb_Colorantes>?> ListarColorantesAgregarOpcion();
        Task<IEnumerable<Lb_AgrOpc_Colorantes>?> ListarIngresoManual(int Corr_Carta, int Sec, int Correlativo);

        /*
            INFORMACION SDC
        */
        //Task<Lb_Informe_SDC?> CargarInformeSDC(int Corr_Carta, int Sec);
        Task<IEnumerable<Lb_Informe_SDC>> CargarInformeSDC(int Corr_Carta, int Sec);
        Task<IEnumerable<Lb_AgrOpc_Colorantes>?> CargarGridHojaFormulacion(int Corr_Carta, int Sec);

        /*
            JABONADOS
        */
        Task<IEnumerable<Lb_Jabonados>?> ListarJabonados();
        Task<IEnumerable<Lb_Jabonados>?> ListarJabonadosCalculado(decimal Colorante_Total, string Familia);
        Task<(int Codigo, string Mensaje)> RegistrarJabonado(Lb_Jabonados _lb_Jabonados);
        Task<(int Codigo, string Mensaje)> ModificarJabonado(Lb_Jabonados _lb_Jabonados);
        Task<(int Codigo, string Mensaje)> DeshabilitarJabonado(Lb_Jabonados _lb_Jabonados);
        Task<(int Codigo, string Mensaje)> RegistrarJabonadoDetalle(Lb_Jabonados_Detalle _lb_Jabonados_Detalle);
        Task<(int Codigo, string Mensaje)> ModificarJabonadoDetalle(Lb_Jabonados_Detalle _lb_Jabonados_Detalle);
        Task<(int Codigo, string Mensaje)> DeshabilitarJabonadoDetalle(Lb_Jabonados_Detalle _lb_Jabonados_Detalle);

        /*
            FIJADOS
        */
        Task<IEnumerable<Lb_Fijados>?> ListarFijados();
        Task<IEnumerable<Lb_Fijados>?> ListarFijadosCalculado(decimal Colorante_Total, string Familia);
        Task<(int Codigo, string Mensaje)> RegistrarFijado(Lb_Fijados _lb_Fijados);
        Task<(int Codigo, string Mensaje)> ModificarFijado(Lb_Fijados _lb_Fijados);
        Task<(int Codigo, string Mensaje)> DeshabilitarFijado(Lb_Fijados _lb_Fijados);
        Task<(int Codigo, string Mensaje)> RegistrarFijadoDetalle(Lb_Fijados_Detalle _lb_Fijados_Detalle);
        Task<(int Codigo, string Mensaje)> ModificarFijadoDetalle(Lb_Fijados_Detalle _lb_Fijados_Detalle);
        Task<(int Codigo, string Mensaje)> DeshabilitarFijadoDetalle(Lb_Fijados_Detalle _lb_Fijados_Detalle);

        /*
            CARBONATO Y SODA 
        */
        Task<IEnumerable<Lb_Colorantes_Componentes_Extra>?> ListarCarbonatoSodaCalculado(decimal Colorante_Total, string Familia, int Com_Cod_Con);

        /*
            COLA AUTOLAB
        */
        Task<IEnumerable<Lb_ColTra_Det>?> ListarColaAutolab();
        Task<(int Codigo, string Mensaje)> EnviarADispensado(Lb_AgrOpc_Colorantes _lbAgrOpcColorante);
        Task<IEnumerable<Lb_ColTra_Det>?> ListarDispensado();

        /*
            AHIBAS 
        */
        Task<IEnumerable<Lb_Ahibas>?> ListaAhibas();
        Task<(int Codigo, string Mensaje)> CargarAahiba(Lb_AgrOpc_Colorantes _lbAgrOpcColorante);
        Task<IEnumerable<Lb_ColTra_Det>?> ListarItemsEnAhiba(int Ahi_Id);


        Task<(int Codigo, string Mensaje)> ActualizarPH(Lb_ColTra_Det lb_ColTra_Det);
        Task<(int Codigo, string Mensaje)> EnviarAutolab(Lb_AgrOpc_Colorantes _lbAgrOpcColorante);
        Task<(int Codigo, string Mensaje)> AgregarAuxiliaresHojaFormulacion(Lb_AgrOpc_Colorantes _lbAgrOpcColorante);
        Task<(int Codigo, string Mensaje)> LlenarTextoFinal(Lb_AgrOpc_Colorantes _lbAgrOpcColorante);
        Task<IEnumerable<Lb_ColTra_Det>?> ListarJabonado();
        Task<IEnumerable<Lb_Colorantes_Componentes_Extra>?> ListarFamiliasProceso();
        Task<IEnumerable<Lb_AgrOpc_Colorantes>?> CargarColoranteParaCopiar(int Corr_Carta, int Sec, int Correlativo);
        Task<IEnumerable<Lb_AgrOpc_Colorantes>?> CargarColoranteParaDetalle(int Corr_Carta, int Sec, int Correlativo);




        /*
             LOGIN
        */
        Task<IEnumerable<Lb_Usuarios>?> GetUsuarioWeb(string Cod_Usuario);



        /*
            REPORTE 
        */
        Task<IEnumerable<Lb_Reporte>?> CargarDatosReporte(int Corr_Carta, int Sec, int Correlativo);



    }
}

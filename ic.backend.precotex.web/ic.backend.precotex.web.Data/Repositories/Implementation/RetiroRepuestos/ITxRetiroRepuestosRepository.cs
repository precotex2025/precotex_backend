using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.RetiroRepuestos;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.RetiroRepuestos
{
    public interface ITxRetiroRepuestosRepository
    {
        Task<IEnumerable<Tx_Retiro_Repuestos>?> ListaRetiros(DateTime FecIni, DateTime FecFin);
        Task<IEnumerable<Tx_Retiro_Repuestos>?> ListaRetirosPorNumRequerimiento(int Num_Requerimiento);
        Task<(int Codigo, string Mensaje)> RegistrarRequerimiento(Tx_Retiro_Repuestos tx_Retiro_Repuestos);
        Task<(int Codigo, string Mensaje)> ActualizarRequerimiento(Tx_Retiro_Repuestos tx_Retiro_Repuestos);
        Task<(int Codigo, string Mensaje)> ActualizarRequerimientoPrecintoCierre(Tx_Retiro_Repuestos tx_Retiro_Repuestos);
        Task<IEnumerable<Tx_Retiro_Repuestos_Detalle>?> ListaRetiroDetallePorNumRequerimiento(int Num_Requerimiento);
        Task<IEnumerable<Tx_Retiro_Repuestos_Detalle>?> ListaRetiroDetallePorNumReqySecuencia(int Num_Requerimiento, int Nro_Secuencia);
        
        Task<(int Codigo, string Mensaje)> RegistrarRequerimientoDetalle(string nNum_Requerimiento, string sCod_Item, string nCan_Requerida, string sRpt_Cambio, string nombreArchivo);
        //Task<(int Codigo, string Mensaje)> RegistrarRequerimientoDetalle(Tx_Retiro_Repuestos_Detalle tx_Retiro_Repuestos_Detalle);
        Task<(int Codigo, string Mensaje)> ActualizarRequerimientoDetalle(string nNum_Requerimiento, string nNum_Secuencia, string nCan_Requerida, string sRpt_Cambio, string sNombreArchivo);


        /*****************************************************COMPLEMENTARIOS**********************************************************/
        Task<IEnumerable<Lg_Item>?> ListaItems(string Cod_Item);
        Task<IEnumerable<Lg_Item>?> ListaItemsCompletos();
        Task<IEnumerable<Lg_Retiro_Repuesto_Usuario>?> ListaRetiroRepuestoUsuario(int Id_Usuario);
        Task<IEnumerable<Lg_Retiro_Repuesto_Usuario>?> ListaRetiroRepuestoUsuarioPorTipo(int Tip_Usuario);
        Task<IEnumerable<Lg_Retiro_Repuesto_Usuario>?> ListaRetiroRepuestoUsuarioSeguridadNombres();
        Task<IEnumerable<Lg_Retiro_Repuesto_Usuario>?> ListaRetiroRepuestoUsuarioMantenimientoNombres();
        Task<IEnumerable<Lg_Item>?> ListaDatosItemsPorNumReqySecuencia(int Num_Requerimiento, int Nro_Secuencia);
        Task<IEnumerable<Tx_Retiro_Repuestos_Reporte>?> ListaDatosReporte(DateTime FecIni, DateTime FecFin);
        Task<IEnumerable<Tx_Retiro_Repuestos_Reporte>?> ListaRetiroRepuestosPorIdRequerimientoMAX();
        Task<(int Codigo, string Mensaje)> EnviarCorreo();
    }
}

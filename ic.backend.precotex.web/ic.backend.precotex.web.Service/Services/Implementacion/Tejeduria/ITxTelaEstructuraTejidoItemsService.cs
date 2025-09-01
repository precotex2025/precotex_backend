using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.Tejeduria
{
    public interface ITxTelaEstructuraTejidoItemsService
    {
        Task<ServiceResponseList<Tx_TelaEstructuraTejidoItems>?> ObtieneEstructuraTejidoItem(string? codTela, string? Cod_Ordtra, string? Num_Secuencia);
        Task<ServiceResponse<int>> InsertarEstructuraTejidoItem(string Cod_Ordtra, int Num_Secuencia, string Cod_Comb, string Cod_Talla, string Cod_Usuario, string XMLData);
        //Task<ServiceResponse<int>> ModificarMedida(string Cod_Ordtra, int Num_Secuencia);

        //MEDIDAS
        Task<ServiceResponseList<Tx_TelaMed>?> ObtieneTelaMedidaHist(string? codTela, string? Cod_Ordtra, string? Num_Secuencia, string? Cod_Comb, string? Cod_Talla);
        Task<ServiceResponse<int>> InsertarTelaMedida(string Cod_Ordtra, int Num_Secuencia, string Cod_Tela, string Cod_Comb, string Cod_Talla, string Cod_Usuario, string XMLData);
        Task<ServiceResponseList<Tx_TelaMed>?> ObtieneTelaMedida(string? codTela, string? Cod_Talla);

        //CARGA DE ESTRUCTURA TEJIDO
        Task<ServiceResponse<int>> InsertarCargaEstructuraTejido(string NombreVersion, string Cod_Tela, string Servicio, string Observacion, string Elaborado, string Revisado, string Cod_Usuario, string XMLData);

        //VERSIONES
        Task<ServiceResponseList<Tx_Ots_Hojas_Arranque_Versiones>?> GeneraVersionHojasArranque(string? Cod_Ordtra, string? Num_Secuencia, string? Cod_Talla);
        Task<ServiceResponseList<Tx_Ots_Hojas_Arranque_Versiones>?> ObtenerVersionHojasArranque(string? Cod_Ordtra, string? Num_Secuencia);
        Task<ServiceResponse<int>> ValidaVersionHojasArranque(string Cod_Ordtra, int Num_Secuencia, int Version, string Flg_Rectilineo);
        Task<ServiceResponseList<Tx_Ots_Hojas_Arranque_Versiones_Listado>?> ListadoVersionHojasArranqueHist(DateTime FecIni, DateTime FecFin, string Cod_Ordtra);
        Task<ServiceResponseList<Tx_Ots_Hojas_Arranque_Versiones_Listado>?> ListadoVersionHojasArranqueHistDetail(DateTime FecIni, DateTime FecFin, string Cod_Ordtra);

        //REGISTRO DE CALIDAD
        Task<ServiceResponseList<Tx_Maquinas_Revisadoras>?> ListaMaquinaRevisadora();

        //Arranque Ctrol
        Task<ServiceResponse<int>> CrudArranqueCtrol(Tx_Ots_Hojas_Arranque_Ctrol tx_Ots_Hojas_Arranque_Ctrol, string sTipoTransac);
        Task<ServiceResponseList<Tx_Ots_Hojas_Arranque_Ctrol>?> ObtenerArranqueCtrol(string Cod_OrdTra, int Num_Secuencia, int Version);
        Task<ServiceResponseList<Tx_Ots_Hojas_Arranque_Ctrol>?> ObtenerArranqueCtrolSinVersion(string Cod_OrdTra, int Num_Secuencia);
    }
}

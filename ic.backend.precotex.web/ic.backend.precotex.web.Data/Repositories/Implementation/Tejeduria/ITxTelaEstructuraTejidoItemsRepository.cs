using ic.backend.precotex.web.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.Tejeduria
{
    public interface ITxTelaEstructuraTejidoItemsRepository
    {
        Task<IEnumerable<Tx_TelaEstructuraTejidoItems>?> ObtieneEstructuraTejidoItem(string? codTela, string? Cod_Ordtra, string? Num_Secuencia);
        Task<(int Codigo, string Mensaje)> InsertarEstructuraTejidoItem(string Cod_Ordtra, int Num_Secuencia, string Cod_Comb, string Cod_Talla, string Cod_Usuario, string XMLData);
        //Task<(int Codigo, string Mensaje)> ModificarMedida(string Cod_Ordtra, int Num_Secuencia);

        //MEDIDAS
        Task<(int Codigo, string Mensaje)> InsertarTelaMedida(string Cod_Ordtra, int Num_Secuencia, string Cod_Tela, string Cod_Comb, string Cod_Talla, string Cod_Usuario, string XMLData);
        Task<IEnumerable<Tx_TelaMed>?> ObtieneTelaMedidaHist(string? codTela, string? Cod_Ordtra, string? Num_Secuencia, string? Cod_Comb, string? Cod_Talla);
        Task<IEnumerable<Tx_TelaMed>?> ObtieneTelaMedida(string? codTela, string? Cod_Talla);

        //CARGA DE ESTRUCTURA TEJIDO
        Task<(int Codigo, string Mensaje)> InsertarCargaEstructuraTejido(string NombreVersion, string Cod_Tela, string Servicio, string Observacion, string Elaborado, string Revisado, string Cod_Usuario, string XMLData);

        //VERSIONES
        Task<IEnumerable<Tx_Ots_Hojas_Arranque_Versiones>?> GeneraVersionHojasArranque(string? Cod_Ordtra, string? Num_Secuencia, string? Cod_Talla);
        Task<IEnumerable<Tx_Ots_Hojas_Arranque_Versiones>?> ObtenerVersionHojasArranque(string? Cod_Ordtra, string? Num_Secuencia);
        Task<(int Codigo, string Mensaje)> ValidaVersionHojasArranque(string Cod_Ordtra, int Num_Secuencia, int Version, string Flg_Rectilineo);
        Task<IEnumerable<Tx_Ots_Hojas_Arranque_Versiones_Listado>?> ListadoVersionHojasArranqueHist(DateTime FecIni, DateTime FecFin, string Cod_Ordtra);
        Task<IEnumerable<Tx_Ots_Hojas_Arranque_Versiones_Listado>?> ListadoVersionHojasArranqueHistDetail(DateTime FecIni, DateTime FecFin, string Cod_Ordtra);

        //REGISTRO DE CALIDAD
        Task<IEnumerable<Tx_Maquinas_Revisadoras>?> ListaMaquinaRevisadora();

        //Arranque Ctrol
        Task<(int Codigo, string Mensaje)> CrudArranqueCtrol(Tx_Ots_Hojas_Arranque_Ctrol tx_Ots_Hojas_Arranque_Ctrol, string sTipoTransac);
        Task<IEnumerable<Tx_Ots_Hojas_Arranque_Ctrol>?> ObtenerArranqueCtrol(string Cod_OrdTra, int Num_Secuencia, int Version);
        Task<IEnumerable<Tx_Ots_Hojas_Arranque_Ctrol>?> ObtenerArranqueCtrolSinVersion(string Cod_OrdTra, int Num_Secuencia);
    }
}

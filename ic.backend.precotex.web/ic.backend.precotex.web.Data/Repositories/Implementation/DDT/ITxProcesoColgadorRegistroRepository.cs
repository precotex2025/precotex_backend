using ic.backend.precotex.web.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.DDT
{
    public interface ITxProcesoColgadorRegistroRepository
    {
        Task<IEnumerable<Tx_TelaEstructuraColgador>?> ObtieneInformacionTelaColgador(string Cod_Tela);
        Task<IEnumerable<Tx_TelaEstructuraRuta>?> ObtieneInformacionRutaColgador(string Cod_Tela);
        Task<IEnumerable<Tx_Cliente>?> ObtieneInformacionClienteColgador();
        Task<(int Codigo, string Mensaje)> ProcesoMntoColgador(Tx_Colgador_Registro_Cab tx_Colgador_Registro_Cab, List<Tx_Colgador_Registro_Det> detalle, string sTipoTransac);
        Task<IEnumerable<Tx_Colgador_Registro_Cab>?> ListadoColgadoresBandeja(DateTime FecIni, DateTime FecFin, string Cod_Tela);
        Task<IEnumerable<Tx_Colgador_Registro_Det>?> ObtieneInformacionTelaColgadorDet(int Id_Tx_Colgador_Registro_Cab);
        Task<(int Codigo, string Mensaje)> ProcesoEliminarColgador(int Id_Tx_Colgador_Registro_Cab, string Usu_Registro);
    }
}

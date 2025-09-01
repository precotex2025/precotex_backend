using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.DDT
{
    public interface ITxProcesoColgadorRegistroService
    {
        Task<ServiceResponseList<Tx_TelaEstructuraColgador>?> ObtieneInformacionTelaColgador(string Cod_Tela);
        Task<ServiceResponseList<Tx_TelaEstructuraRuta>?> ObtieneInformacionRutaColgador(string Cod_Tela);
        Task<ServiceResponseList<Tx_Cliente>?> ObtieneInformacionClienteColgador();
        Task<ServiceResponse<int>> ProcesoMntoColgador(Tx_Colgador_Registro_Cab tx_Colgador_Registro_Cab, List<Tx_Colgador_Registro_Det> detalle, string sTipoTransac);
        Task<ServiceResponseList<Tx_Colgador_Registro_Cab>?> ListadoColgadoresBandeja(DateTime FecIni, DateTime FecFin, string Cod_Tela);
        Task<ServiceResponseList<Tx_Colgador_Registro_Det>?> ObtieneInformacionTelaColgadorDet(int Id_Tx_Colgador_Registro_Cab);
        Task<ServiceResponse<int>> ProcesoEliminarColgador(int Id_Tx_Colgador_Registro_Cab, string Usu_Registro);
    }
}

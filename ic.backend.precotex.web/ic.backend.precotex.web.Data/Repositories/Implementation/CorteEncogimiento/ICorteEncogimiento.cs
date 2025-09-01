using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ic.backend.precotex.web.Entity.Entities;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.CorteEncogimiento
{
    public interface ICorteEncogimiento
    {
        Task<IEnumerable<E_Corte_Encogimiento>?> ListaCorteEncogimiento();
        Task<IEnumerable<E_Corte_Encogimiento>?> InsertCorteEncogimiento(string pTipo, string? pCod_Ordtra);

        Task<IEnumerable<E_Corte_Encogimiento>?> ListCorteEncogimientoDet(string pTipo, string? pNum_Secuencia, string? pCodPartida, decimal? pAncho_Antes_Lav, decimal? pAlto_Antes_Lav, decimal? pAncho_Despues_Lav, decimal? pAlto_Despues_Lav, decimal? pSesgadura);

        Task<IEnumerable<E_Corte_Encogimiento>?> BuscarCorteEncogimiento(string pCod_Ordtra);

        Task<IEnumerable<E_Corte_Encogimiento>?> UpdateMedidasTablaMaestra(List<E_Corte_Encogimiento> pData);

        Task<IEnumerable<E_Corte_Encogimiento>?> BuscarUsuario(string pUsuario);

    }
}

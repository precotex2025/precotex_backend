using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.common;

namespace ic.backend.precotex.web.Service.Services.Implementacion.CorteEncogimiento
{
    public interface ICorteEncogimientoService
    {
        Task<ServiceResponseList<E_Corte_Encogimiento>?> ListaCorteEncogimiento();

        Task<ServiceResponseList<E_Corte_Encogimiento>?> InsertCorteEncogimiento(string pTipo, string? pCod_Ordtra);

        Task<ServiceResponseList<E_Corte_Encogimiento>?> ListCorteEncogimientoDet(string pTipo, string? pNum_Secuencia, string? pCodPartida, decimal? pAncho_Antes_Lav, decimal? pAlto_Antes_Lav, decimal? pAncho_Despues_Lav, decimal? pAlto_Despues_Lav, decimal? pSesgadura);

        Task<ServiceResponseList<E_Corte_Encogimiento>?> BuscarCorteEncogimiento(string pCod_Ordtra);
        Task<ServiceResponseList<E_Corte_Encogimiento>?> UpdateMedidasTablaMaestra(List<E_Corte_Encogimiento> pData);

        Task<ServiceResponseList<E_Corte_Encogimiento>?> BuscarUsuario(string pUsuario);
    }
}

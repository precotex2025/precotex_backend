using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.common;

namespace ic.backend.precotex.web.Service.Services.Implementacion.RegistroPartidaParihuela
{
    public interface IRegistroPartidaParihuelaService
    {
        Task<ServiceResponseList<E_RegistroPartidaParihuela>?> ObtenerDetPartida(string pCod_Partida, string pOpcion);

        Task<ServiceResponseList<E_RegistroPartidaParihuela>?> UpdateDetPartida(List<E_RegistroPartidaParihuela> pData, string pCod_Usuario, string pEstadoParihuela, string pReposicion);

        Task<ServiceResponseList<E_Complemento>?> ObtenerCategoriasPorId(string pIdPartida);

        Task<ServiceResponseList<string>?> ValidarMerma(string pCod_Partida);

        Task<ServiceResponseList<string>?> UpdateEstadoMermaAsync(string pCod_Partida);

        Task<ServiceResponseList<string>?> EnviarDespacho(string pCod_Partida);

        Task<ServiceResponse<int>> EnviarCabecera(string pCod_Partida);
    }
}

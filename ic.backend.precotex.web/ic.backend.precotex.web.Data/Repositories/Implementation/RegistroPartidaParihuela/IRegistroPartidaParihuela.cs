using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ic.backend.precotex.web.Entity.Entities;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.RegistroPartidaParihuela
{
    public interface IRegistroPartidaParihuela
    {
        Task<IEnumerable<E_RegistroPartidaParihuela>?> ObtenerDetPartida(string pCod_Partida, string pOpcion);

        Task<IEnumerable<E_RegistroPartidaParihuela>?> UpdateDetPartida(List<E_RegistroPartidaParihuela> pData, string pCod_Usuario, string pEstadoParihuela, string pReposicion);

        Task<IEnumerable<E_Complemento>?> ObtenerCategoriasPorId(string pIdPartida);

        Task<IEnumerable<string>?> validarMerma(string pCod_Partida);
        Task<IEnumerable<string>?> UpdateEstadoMermaAsync(string pCod_Partida);

        Task<IEnumerable<string>?> EnviarDespacho(string pCod_Partida);

        Task<(int Codigo, string Mensaje)> EnviarCabecera(string pCod_Partida);
    }
}

using ic.backend.precotex.web.Entity.common;
using ic.backend.precotex.web.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.Almacen
{
    public interface ITxBultoHiladoGrupoRepository
    {
        Task<(int Codigo, string Mensaje)> Insertar(Tx_Bulto_Hilado_Grupo tx_Bulto_Hilado_Grupo);
        Task<IEnumerable<Tx_Bulto_Hilado_Grupo>?> Lista(DateTime? FecCrea, string? Grupo);
        Task<IEnumerable<Tx_Bulto_Hilado_Grupo>?> ListaByIdUbicacion(string? CodUbicacion);
        Task<IEnumerable<Tx_Bulto_Hilado_Grupo>?> ListaDet(string? Grupo);
        Task<IEnumerable<Tx_Bulto_Hilado_Grupo>?> Obtener(int? IdBultoHiladoGrupo);
        Task<(int Codigo, string Mensaje)> Validar(string? Grupo);
        Task<IEnumerable<Tx_Bulto_Hilado_Grupo>?> ObtenerByCode(string? Grupo);
        Task<IEnumerable<Tx_Bulto_Hilado_Grupo>?> ListaDetById(int? IdBultoHiladoGrupo);
        Task<(int Codigo, string Mensaje)> UbicarReubicar(Tx_Bulto_Hilado_Grupo tx_Bulto_Hilado_Grupo);
    }
}

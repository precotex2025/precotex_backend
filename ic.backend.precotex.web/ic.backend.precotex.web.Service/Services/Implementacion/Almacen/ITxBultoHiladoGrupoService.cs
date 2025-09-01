using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.Almacen
{
    public interface ITxBultoHiladoGrupoService
    {
        Task<ServiceResponse<int>> Insertar(Tx_Bulto_Hilado_Grupo tx_Bulto_Hilado_Grupo);
        Task<ServiceResponseList<Tx_Bulto_Hilado_Grupo>?> Lista(DateTime? FecCrea, string? Grupo);
        Task<ServiceResponseList<Tx_Bulto_Hilado_Grupo>?> ListaByIdUbicacion(string? CodUbicacion);
        Task<ServiceResponseList<Tx_Bulto_Hilado_Grupo>?> ListaDet(string? Grupo);
        Task<ServiceResponseList<Tx_Bulto_Hilado_Grupo>?> Obtener(int? IdBultoHiladoGrupo);
        Task<ServiceResponse<int>> Validar(string? Grupo);
        Task<ServiceResponseList<Tx_Bulto_Hilado_Grupo>?> ObtenerByCode(string? Grupo);
        Task<ServiceResponseList<Tx_Bulto_Hilado_Grupo>?> ListaDetById(int? IdBultoHiladoGrupo);
        Task<ServiceResponse<int>> UbicarReubicar(Tx_Bulto_Hilado_Grupo tx_Bulto_Hilado_Grupo);
    }
}

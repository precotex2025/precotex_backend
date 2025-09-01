using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ic.backend.precotex.web.Entity.Entities.CalificacionRollosEnProceso;
using ic.backend.precotex.web.Entity.Entities.QuejasReclamos;
using static ic.backend.precotex.web.Entity.Entities.QuejasReclamos.Clientes;


namespace ic.backend.precotex.web.Data.Repositories.Implementation.QuejasReclamos
{
    public interface IQuejasReclamos
    {
        Task<IEnumerable<Cliente>?> ObtenerClintes();
        Task<IEnumerable<EstadoDto>?> ObtenerEstado();
        Task<IEnumerable<UnidadNegocioDto>?> ObtenerUnidadNegocio();
        Task<IEnumerable<ResponsableDto>?> ObtenerResponsable();
        Task<IEnumerable<UnidadNegocioDto>?> ObtenerMotivo();
        Task<IEnumerable<ReclamoClienteDto>?> GuardarReclamo(List<ReclamoClienteDto> reclamo, bool isNew);
        Task<IEnumerable<FiltroReclamoDto>?> ObtenerReclamos(FiltroReclamoDto filtro);
        Task<IEnumerable<ReclamoClienteDto>?> ObtenerDetReclamos(string nroCaso);
        Task<IEnumerable<bool>?> EliminarReclamos(string nroCaso);
        Task<IEnumerable<string>?> EliminarReclamoDetalle(string id);

        //Nuevos
        Task<IEnumerable<ArticuloDto>?> BuscarPorPartida(string partida);
        Task<IEnumerable<UnidadNegocioDto>?> ListaUnidadNegocio();

    }
}

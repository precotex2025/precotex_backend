using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ic.backend.precotex.web.Entity.Entities.CalificacionRollosEnProceso;
using ic.backend.precotex.web.Entity.Entities.QuejasReclamos;
using ic.backend.precotex.web.Service.common;
using static ic.backend.precotex.web.Entity.Entities.QuejasReclamos.Clientes;

namespace ic.backend.precotex.web.Service.Services.Implementacion.QuejasReclamos 
{ 

    public interface IQuejasReclamosService
    {
        Task<ServiceResponseList<Cliente>?> ObtenerClintes();
        Task<ServiceResponseList<EstadoDto>?> ObtenerEstado();
        Task<ServiceResponseList<UnidadNegocioDto>?> ObtenerUnidadNegocio();
        Task<ServiceResponseList<ResponsableDto>?> ObtenerResponsable();
        Task<ServiceResponseList<UnidadNegocioDto>?> ObtenerMotivo();
        Task<ServiceResponseList<ReclamoClienteDto>?> GuardarReclamo(List<ReclamoClienteDto> reclamo, bool isNew);
        Task<ServiceResponseList<FiltroReclamoDto>?> ObtenerReclamos(FiltroReclamoDto filtro);
        Task<ServiceResponseList<ReclamoClienteDto>?> ObtenerDetReclamos(string nroCaso);
        Task<ServiceResponseList<bool>?> EliminarReclamos(string nroCaso);
        Task<ServiceResponseList<string>?> EliminarReclamoDetalle(string id);
        //Nuevos
        Task<ServiceResponseList<ArticuloDto>?> BuscarPorPartida(string partida);
        Task<ServiceResponseList<UnidadNegocioDto>?> ListaUnidadNegocio();
    }
}

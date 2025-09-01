using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ic.backend.precotex.web.Entity.Entities.QuejasReclamos;
using ic.backend.precotex.web.Service.common;
using static ic.backend.precotex.web.Entity.Entities.QuejasReclamos.Clientes;


namespace ic.backend.precotex.web.Service.Services.Implementacion.QuejasReclamosv2Service
{
    public interface IQuejasReclamosv2Service
    {
        Task<ServiceResponseList<EstadoDto>?> ObtenerEstado();

    }
}

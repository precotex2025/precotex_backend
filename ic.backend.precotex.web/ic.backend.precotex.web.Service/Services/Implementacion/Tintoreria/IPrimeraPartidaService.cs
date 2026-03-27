using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.SecureNorm;
using ic.backend.precotex.web.Entity.Entities.Tintoreria;
using ic.backend.precotex.web.Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.Tintoreria
{
    public interface IPrimeraPartidaService
    {
        Task<ServiceResponse<int>> ProcesoMnto(AuditoriaPrimeraPartida auditoriaPrimeraPartida);

        Task<ServiceResponseList<PrimeraPartidaBandeja>?> Lista(DateTime? Fecha_Ini, DateTime? Fecha_Fin);

    }
}

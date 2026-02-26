using ic.backend.precotex.web.Entity.Entities.AgendaTelefonica;
using ic.backend.precotex.web.Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.AgendaTelefonica
{
    public interface ICnAgendaService
    {
        Task<ServiceResponseList<Cn_Agenda>?> ObtenerNumeros();
    }
}

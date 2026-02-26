using ic.backend.precotex.web.Entity.Entities.AgendaTelefonica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.AgendaTelefonica
{
    public interface ICnAgendaRepository
    {
        Task<IEnumerable<Cn_Agenda>?> ObtenerNumeros();
    }
}

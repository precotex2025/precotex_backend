using ic.backend.precotex.web.Data.Repositories.Implementation.AgendaTelefonica;
using ic.backend.precotex.web.Entity.Entities.AgendaTelefonica;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.AgendaTelefonica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.AgendaTelefonica
{
    public class CnAgendaService: ICnAgendaService
    {
        private readonly ICnAgendaRepository _repository;

        public CnAgendaService(ICnAgendaRepository repository) {
            _repository = repository;
        }

        public async Task<ServiceResponseList<Cn_Agenda>?> ObtenerNumeros()
        {
            var result = new ServiceResponseList<Cn_Agenda>();
            try
            {
                var resultData = await _repository.ObtenerNumeros();
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No existe información";
                }
                result.Success = true;
                result.Message = "Completado con éxito";
                result.Elements = resultData.ToList();
                result.TotalElements = resultData.ToList().Count();
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Excepción no controlada " + ex.Message;
                return result;
            }
        }


    }
}

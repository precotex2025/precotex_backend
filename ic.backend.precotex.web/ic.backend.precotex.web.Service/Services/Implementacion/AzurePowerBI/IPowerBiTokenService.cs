using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.AzurePowerBI
{
    public interface IPowerBiTokenService
    {
        Task<string> GetAccessTokenAsync();
        Task<ServiceResponseList<PowerBiReport>> GetReportsAsync();
        Task<string> GetEmbedUrlAsync(string reportId);
    }
}

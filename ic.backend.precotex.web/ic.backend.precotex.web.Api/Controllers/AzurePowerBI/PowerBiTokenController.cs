using ic.backend.precotex.web.Service.Services.Implementacion.AzurePowerBI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ic.backend.precotex.web.Api.Controllers.AzurePowerBI
{
    public class PowerBiTokenController : ControllerBase
    {
        private readonly IPowerBiTokenService _tokenService;

        public PowerBiTokenController(IPowerBiTokenService tokenService)
        {
            _tokenService = tokenService;
        }

        //[HttpGet("token")]
        //public async Task<IActionResult> GetToken()
        //{
        //    try
        //    {
        //        var token = await _tokenService.GetAccessTokenAsync();
        //        return Ok(new { AccessToken = token });
        //    }
        //    catch (ApplicationException ex)
        //    {
        //        return BadRequest(new { Error = ex.Message });
        //    }
        //}

        [HttpGet("reports")]
        public async Task<IActionResult> GetReports()
        {
            try
            {
                var reports = await _tokenService.GetReportsAsync();
                return Ok(reports);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("report/{reportId}")]
        public async Task<IActionResult> GetReportEmbedUrl(string reportId)
        {
            try
            {
                var embedUrl = await _tokenService.GetEmbedUrlAsync(reportId);
                return Ok(new { EmbedUrl = embedUrl });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

    }
}

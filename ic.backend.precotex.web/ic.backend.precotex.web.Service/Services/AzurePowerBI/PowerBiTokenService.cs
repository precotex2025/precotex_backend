using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.AzurePowerBI;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ic.backend.precotex.web.Entity.Entities;

namespace ic.backend.precotex.web.Service.Services.AzurePowerBI
{
    public class PowerBiTokenService : IPowerBiTokenService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public PowerBiTokenService(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            string clientId = _configuration["AzureAd:ClientId"]!;
            string clientSecret = _configuration["AzureAd:ClientSecret"]!;
            string tenantId = _configuration["AzureAd:TenantId"]!;
            string powerBiResource = _configuration["AzureAd:PowerBiResource"]!;

            var app = ConfidentialClientApplicationBuilder.Create(clientId)
                .WithClientSecret(clientSecret)
                .WithAuthority(new Uri($"https://login.microsoftonline.com/{tenantId}"))
                .Build();

            string[] scopes = new string[] { $"{powerBiResource}/.default" };

            try
            {
                var result = await app.AcquireTokenForClient(scopes).ExecuteAsync();
                return result.AccessToken;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al obtener el token de acceso", ex);
            }
        }

        public async Task<ServiceResponseList<PowerBiReport>> GetReportsAsync()
        {
            var accessToken = await GetAccessTokenAsync();
            var result = new ServiceResponseList<PowerBiReport>();
            string apiBaseUrl = _configuration["AzureAd:ApiBaseUrl"];
            string url = $"{apiBaseUrl}/reports";

            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    using var jsonDoc = JsonDocument.Parse(jsonResponse);

                    var reports = new List<PowerBiReport>();
                    foreach (var item in jsonDoc.RootElement.GetProperty("value").EnumerateArray())
                    {
                        reports.Add(new PowerBiReport
                        {
                            Id = item.GetProperty("id").GetString(),
                            Name = item.GetProperty("name").GetString(),
                            EmbedUrl = item.GetProperty("embedUrl").GetString()
                        });
                    }

                    return result;
                }
                else
                {
                    result.Success = false;
                    result.Message = $"Error: {response.ReasonPhrase}";
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Exception: {ex.Message}";
                return result;
            }

        }

        public async Task<string> GetEmbedUrlAsync(string reportId)
        {
            var accessToken = await GetAccessTokenAsync();
            string apiBaseUrl = _configuration["AzureAd:ApiBaseUrl"];
            string url = $"{apiBaseUrl}/reports/{reportId}";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                using var jsonDoc = JsonDocument.Parse(jsonResponse);
                return jsonDoc.RootElement.GetProperty("embedUrl").GetString();
            }

            throw new ApplicationException($"Error al obtener la URL embebida del informe: {response.ReasonPhrase}");
        }

    }
}

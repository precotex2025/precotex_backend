using ic.backend.precotex.web.Service.Services.Implementacion.WallyChat;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.WallyChat
{
    public class WaliChatService: IWaliChatService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        //private const string _token = "bfa8f57eaabdb74719aea75157c1bdddfb4a334875ebd6d504af35a5c41867b75eb787951b7bde92";
        //private const string _token = "1910c0f413dd975a74d0df732702599982a6839aab5f5a766c1d8cc5346a8d2e89d25e55fdc9388a";

        public WaliChatService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> EnviarMensajeAsync(string groupId, string message)
        {
            /*
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://api.wali.chat/v1/messages"),
                Headers =
                {
                    { "Token", _token },
                },
                Content = new StringContent(
                $"{{\"group\":\"{groupId}\",\"message\":\"{message}\"}}", Encoding.UTF8, "application/json")
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
                return body;
            }
            */

            //var apiKey = _token; // tu API Key de WaliChat
            var apiKey = _configuration["WaliChat:Token"]!;
            var url = $"https://api.wali.chat/v1/messages?token={apiKey}";

            using (var client = new HttpClient())
            {
                var payload = new
                {
                    group = groupId,
                    message = message
                };

                var json = JsonConvert.SerializeObject(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);
                var body = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {body}");
                    throw new Exception($"Error al enviar mensaje: {body}");
                }

                return body;
            }
        }



        public async Task<string> EnviarMensajeImageAsync(string groupId, string message, string imageUrl, bool viewOnce)
        {
            /*
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://api.wali.chat/v1/messages"),
                Headers =
                {
                    { "Token", _token } // tu token aquí
                },
                Content = new StringContent(
                    $@"{{
                            ""group"": ""{groupId}@g.us"",
                            ""message"": ""{message}"",
                            ""media"": {{
                                            ""url"": ""{imageUrl}"",
                                            ""viewOnce"": {viewOnce.ToString().ToLower()}
                                        }}
                       }}",
                    Encoding.UTF8,
                    "application/json"
                )
            };

            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return body;
            }
            */

            //var apiKey = _token; // Tu API Key de WaliChat
            var apiKey = _configuration["WaliChat:Token"]!;
            var url = $"https://api.wali.chat/v1/messages?token={apiKey}";

            using (var client = new HttpClient())
            {
                // Estructura del cuerpo según la documentación oficial de WaliChat
                var payload = new
                {
                    group = groupId, // usa el groupId completo, sin agregar @g.us si ya está incluido
                    message = message,
                    media = new
                    {
                        url = imageUrl,
                        viewOnce = viewOnce
                    }
                };

                var json = JsonConvert.SerializeObject(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);
                var body = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error al enviar imagen: {response.StatusCode}");
                    Console.WriteLine(body);
                    throw new Exception($"Error al enviar imagen: {body}");
                }

                Console.WriteLine($"Imagen enviada correctamente: {body}");
                return body;
            }

        }

        public async Task<string> EnviarMensajeImagePhoneAsync(string phoneNumber, string message, string imageUrl)
        {
            var apiKey = _configuration["WaliChat:Token"]!;
            var url = $"https://api.wali.chat/v1/messages?token={apiKey}";

            using (var client = new HttpClient())
            {
                // Estructura del cuerpo según la documentación oficial de WaliChat
                var payload = new
                {
                    phone = phoneNumber, // usa el groupId completo, sin agregar @g.us si ya está incluido
                    message = message,
                    media = new
                    {
                        url = imageUrl,
                        viewOnce = false
                    }
                };

                var json = JsonConvert.SerializeObject(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);
                var body = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error al enviar imagen a phone: {response.StatusCode}");
                    Console.WriteLine(body);
                    throw new Exception($"Error al enviar imagen a phone: {body}");
                }

                Console.WriteLine($"Imagen enviada correctamente: {body}");
                return body;
            }
        }
    }
}

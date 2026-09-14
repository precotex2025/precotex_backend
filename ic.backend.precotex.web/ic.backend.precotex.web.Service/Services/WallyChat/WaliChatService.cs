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
            var apiKey = _configuration["WaliChat:Token"]!;
            var url = $"https://api.wali.chat/v1/messages?token={apiKey}";
            var deviceId = _configuration["WaliChat:Device"];

            using (var client = new HttpClient())
            {
                var payload = new Dictionary<string, object>
                {
                    { "group", groupId },
                    { "message", message }
                };

                if (!string.IsNullOrWhiteSpace(deviceId))
                {
                    payload["device"] = deviceId.Trim();
                }

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
            var apiKey = _configuration["WaliChat:Token"]!;
            var url = $"https://api.wali.chat/v1/messages?token={apiKey}";
            var deviceId = _configuration["WaliChat:Device"];

            using (var client = new HttpClient())
            {
                object mediaObj;
                if (!string.IsNullOrEmpty(imageUrl) && imageUrl.Length == 24 && System.Text.RegularExpressions.Regex.IsMatch(imageUrl, "^[0-9A-Fa-f]{24}$"))
                {
                    mediaObj = new { file = imageUrl, viewOnce = viewOnce };
                }
                else if (!string.IsNullOrEmpty(imageUrl) && System.IO.File.Exists(imageUrl))
                {
                    var bytes = System.IO.File.ReadAllBytes(imageUrl);
                    var fname = System.IO.Path.GetFileName(imageUrl);
                    var fId = SubirArchivoAsync(bytes, fname).GetAwaiter().GetResult();
                    if (!string.IsNullOrEmpty(fId))
                    {
                        mediaObj = new { file = fId, viewOnce = viewOnce };
                    }
                    else
                    {
                        mediaObj = new { url = imageUrl, viewOnce = viewOnce };
                    }
                }
                else
                {
                    mediaObj = new { url = imageUrl, viewOnce = viewOnce };
                }

                var payload = new Dictionary<string, object>
                {
                    { "group", groupId },
                    { "message", message },
                    { "media", mediaObj }
                };

                if (!string.IsNullOrWhiteSpace(deviceId))
                {
                    payload["device"] = deviceId.Trim();
                }

                var json = JsonConvert.SerializeObject(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);
                var body = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error al enviar imagen: {response.StatusCode} - {body}");
                    throw new Exception($"Error al enviar imagen: {body}");
                }

                Console.WriteLine($"Imagen enviada correctamente: {body}");
                return body;
            }
        }

        public async Task<string?> SubirArchivoAsync(byte[] fileBytes, string fileName, string contentType = "image/jpeg")

        {

            try

            {

                var apiKey = _configuration["WaliChat:Token"]!;

                var url = "https://api.wali.chat/v1/files";

                using (var client = new HttpClient())

                {

                    client.DefaultRequestHeaders.Add("Token", apiKey);

                    using (var form = new MultipartFormDataContent())

                    {

                        var fileContent = new ByteArrayContent(fileBytes);

                        fileContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);

                        form.Add(fileContent, "file", fileName);

                        var response = await client.PostAsync(url, form);

                        var body = await response.Content.ReadAsStringAsync();

                        if (!response.IsSuccessStatusCode)

                        {

                            Console.WriteLine($"Error al subir archivo a WaliChat: {body}");

                            return null;

                        }

                        using (var doc = JsonDocument.Parse(body))

                        {

                            if (doc.RootElement.ValueKind == JsonValueKind.Array && doc.RootElement.GetArrayLength() > 0)

                            {

                                if (doc.RootElement[0].TryGetProperty("id", out var idProp))

                                {

                                    return idProp.GetString();

                                }

                            }

                            else if (doc.RootElement.ValueKind == JsonValueKind.Object)

                            {

                                if (doc.RootElement.TryGetProperty("id", out var idProp))

                                {

                                    return idProp.GetString();

                                }

                            }

                        }

                    }

                }

            }

            catch (Exception ex)

            {

                Console.WriteLine($"Excepción al subir archivo a WaliChat: {ex.Message}");

            }

            return null;

        }

        public async Task<string> EnviarMensajeMediaFileAsync(string groupId, string message, string fileId)

        {

            var apiKey = _configuration["WaliChat:Token"]!;

            var url = $"https://api.wali.chat/v1/messages?token={apiKey}";

            var deviceId = _configuration["WaliChat:Device"];

            using (var client = new HttpClient())

            {

                var payload = new Dictionary<string, object>

                {

                    { "group", groupId },

                    { "message", message },

                    { "media", new { file = fileId } }

                };

                if (!string.IsNullOrWhiteSpace(deviceId))

                {

                    payload["device"] = deviceId.Trim();

                }

                var json = JsonConvert.SerializeObject(payload);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);

                var body = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)

                {

                    Console.WriteLine($"Error al enviar mensaje con archivo a WaliChat: {response.StatusCode} - {body}");

                    throw new Exception($"Error al enviar imagen: {body}");

                }

                Console.WriteLine($"Mensaje con imagen enviado correctamente a WaliChat: {body}");

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

        public async Task<string> EnviarMensajePhoneAsync(string phoneNumber, string message)

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


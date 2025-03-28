using System;
using System.Net.Http.Headers;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Text.Json;


namespace ProjectsMecsaSPA.Services
{
    public class Bitrix24ClientService
    {
        private readonly HttpClient _httpClient;
        private readonly string _urlTaskAdd;
        private readonly IConfiguration configuration;

        public Bitrix24ClientService(HttpClient httpClient, IConfiguration _configuration)
        {
            _httpClient = httpClient;
            _urlTaskAdd = _configuration["Bitrix24:UrlTaskAdd"]
                          ?? throw new ArgumentNullException("Bitrix24:UrlTaskAdd no está configurado");
            configuration = _configuration;

        }

        /// <summary>
        /// Send a new commentary in a task in Bitrix24
        /// </summary>
        /// <param name="taskId">Task to add the commentary</param>
        /// <param name="message">message to send</param>
        /// <param name="userId">id of the user</param>
        /// <returns></returns>
        public async Task SendTaskCommentAsync(int taskId, string message, int userId, string[] filesId)
        {
            var requestBody = new
            {
                fields = new
                {
                    AUTHOR_ID = userId,
                    POST_MESSAGE = $"Proyectos - 🤖: {message}",
                    UF_FORUM_MESSAGE_DOC = filesId.Select(fileId => "n" + fileId).ToArray()

                }
            };

            string json = JsonSerializer.Serialize(requestBody);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync($"{_urlTaskAdd}?TASKID={taskId}", content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Comentario enviado exitosamente.");
            }
            else
            {
                Console.WriteLine($"Error en el envío: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
            }
        }

        public async Task SendTaskCommentAsync(int taskId, string message, int userId)
        {
            var requestBody = new
            {
                fields = new
                {
                    AUTHOR_ID = userId,
                    POST_MESSAGE = $"Proyectos - 🤖: {message}"
                }
            };

            string json = JsonSerializer.Serialize(requestBody);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync($"{_urlTaskAdd}?TASKID={taskId}", content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Comentario enviado exitosamente.");
            }
            else
            {
                Console.WriteLine($"Error en el envío: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
            }
        }


        /// <summary>
        /// Create a new subfolder in a parent folder in Bitrix24
        /// </summary>
        /// <param name="parentFolderId"></param>
        /// <param name="subfolderName"></param>
        /// <returns></returns>
        public async Task<int> CreateSubfolderAsync(int parentFolderId = 1116310, string subfolderName = "demo")
        {
            var url = configuration["Bitrix24:UrlCreateFolder"]
                         ?? throw new ArgumentNullException("Bitrix24:UrlTaskAdd no está configurado");


            var requestData = new
            {
                id = parentFolderId,
                data = new
                {
                    NAME = subfolderName
                }
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, jsonContent);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(responseBody);
            var root = doc.RootElement;

            if (root.TryGetProperty("result", out JsonElement result) && result.TryGetProperty("ID", out JsonElement idElement))
            {
                return idElement.GetInt32();
            }

            throw new Exception("No se pudo obtener el ID de la respuesta.");
        }

        public async Task<string> UploadFileAsync(int folderId, string fileName, byte[] fileData)
        {
            try
            {

                var urlBase = "https://grupomecsa.bitrix24.es/rest/107/pf3z28pdm99vsumm/disk.folder.uploadfile.json";

                // Paso 1: Obtener el uploadUrl
                var uploadUrl = await GetUploadUrl(urlBase, folderId, fileName);

                

                if (string.IsNullOrEmpty(uploadUrl))
                {
                    throw new Exception("No se pudo obtener el uploadUrl.");
                }

                var Url = new Uri(uploadUrl.Replace("\\",""));

                // Paso 2: Subir el archivo
                return await UploadFileToUrl(Url, fileName, fileData);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        private async Task<string> GetUploadUrl(string urlBase,int folderId, string fileName)
        {
            try
            {
                // Realiza una solicitud POST para obtener el URL de carga
                var url = $"{urlBase}?id={folderId}&data[NAME]={fileName}";

                var response = await _httpClient.PostAsync(url, null);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Error al obtener el uploadUrl.");
                }

                // Leer la respuesta y extraer el uploadUrl
                var content = await response.Content.ReadAsStringAsync();
                var uploadUrl = ExtractUploadUrl(content);  // Necesitarás una función para extraer el uploadUrl desde el JSON

                return uploadUrl;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        private string ExtractUploadUrl(string jsonResponse)
        {
            try
            {
                // Aquí debes analizar el JSON para obtener el uploadUrl
                // Este es un ejemplo básico de cómo podrías hacerlo:
                var start = jsonResponse.IndexOf("\"uploadUrl\":\"") + 13;
                var end = jsonResponse.IndexOf("\"", start);
                return jsonResponse.Substring(start, end - start);

            }
            catch (Exception er)
            {

                throw;
            }        }

        private async Task<string> UploadFileToUrl(Uri uploadUrl, string fileName, byte[] fileData)
        {
            try
            {
                // Crear el contenido de la solicitud multipart
                using (var formData = new MultipartFormDataContent())
                {
                    formData.Add(new ByteArrayContent(fileData), "file", fileName);

                    // Realizar la solicitud POST para cargar el archivo
                    var response = await _httpClient.PostAsync(uploadUrl, formData);

                    Console.WriteLine(response.Content.ReadAsStringAsync());
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("Error al cargar el archivo.");
                    }

                    // Leer la respuesta (puedes modificar esto según lo que esperes recibir)
                    var responseContent = await response.Content.ReadAsStringAsync();

                    // Regresar la respuesta (puede ser el archivo cargado o algún mensaje de éxito)
                    return responseContent;
                }
            }
            catch (Exception ef)
            {

                throw;
            }
        }
    }

}

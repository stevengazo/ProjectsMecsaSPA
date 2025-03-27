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
            var url = "https://grupomecsa.bitrix24.es/rest/107/nusehi6j9orv0j8i/disk.folder.uploadfile.json";

            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(new StringContent(folderId.ToString()), "id");
                formData.Add(new StringContent(JsonSerializer.Serialize(new { NAME = fileName })), "data");
                formData.Add(new ByteArrayContent(fileData), "fileContent", fileName);

                var response = await _httpClient.PostAsync(url, formData);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
        }
    }

}

using System.Text;

namespace ProjectsMecsaSPA.Services
{
    public class Bitrix24ClientServices
    {
        private  readonly string UrlTaskAdd = "";

        public async Task SendTaskCommentAsync(int taskId, string message)
        {
            using (HttpClient client = new HttpClient())
            {
                var requestBody = new
                {
                    fields = new
                    {
                        AUTHOR_ID = 186,
                        POST_MESSAGE = $"SPA - comentario automático: {message}" 
                    }
                };

                string json = System.Text.Json.JsonSerializer.Serialize(requestBody);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync($"{UrlTaskAdd}?TASKID={taskId}", content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Comentario enviado exitosamente.");
                }
                else
                {
                    Console.WriteLine($"Error en el envío: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                }
            }
        }
    }
}

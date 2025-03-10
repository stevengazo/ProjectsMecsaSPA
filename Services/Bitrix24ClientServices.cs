using System.Text;

namespace ProjectsMecsaSPA.Services
{
    public class Bitrix24ClientServices
    {
        private  readonly string UrlTaskAdd = "https://grupomecsa.bitrix24.es/rest/107/lnnu4xzcwspoclru/task.commentitem.add.json";

        public async Task SendTaskCommentAsync(int taskId, string message,int  userid)
        {
            using (HttpClient client = new HttpClient())
            {
                var requestBody = new
                {
                    fields = new
                    {
                        AUTHOR_ID = userid,
                        POST_MESSAGE = $"Proyectos - 🤖: {message}" 
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

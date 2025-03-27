using System.Xml.Linq;

namespace ProjectsMecsaSPA.Services
{
    public class CentralBankService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CentralBankService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<decimal> ObtenerTipoCambioAsync(DateTime fechaConsulta)
        {
            try
            {
                string nombre = _configuration["BancoCentral:Nombre"];
                string correo = _configuration["BancoCentral:CorreoElectronico"];
                string token = _configuration["BancoCentral:Token"];

                string url = $"https://gee.bccr.fi.cr/Indicadores/Suscripciones/WS/wsindicadoreseconomicos.asmx/ObtenerIndicadoresEconomicos?Indicador=318&FechaInicio={fechaConsulta:dd/MM/yyyy}&FechaFinal={fechaConsulta:dd/MM/yyyy}&Nombre={nombre}&SubNiveles=N&CorreoElectronico={correo}&Token={token}";

                HttpResponseMessage response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error en la solicitud: {response.StatusCode}");
                }

                string xmlResponse = await response.Content.ReadAsStringAsync();
                return ExtraerTipoCambio(xmlResponse) ?? 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return 0;
            }
        }

        private decimal? ExtraerTipoCambio(string xml)
        {
            try
            {
                XDocument doc = XDocument.Parse(xml);
                string? numValor = doc.Descendants().FirstOrDefault(x => x.Name.LocalName == "NUM_VALOR")?.Value;
                if (decimal.TryParse(numValor?.Replace(".", ","), out decimal tipoCambio))
                {
                    return Math.Round(tipoCambio, 2);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}

using Microsoft.Extensions.Logging;

namespace Services.Services.SmsService
{

    public class SmsService : ISmsService
    {
        private readonly ILogger<SmsService> _logger;
        private readonly HttpClient _httpClient;
        public SmsService(ILogger<SmsService> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<bool> SendSMS(string phoneNumber, string message)
        {
            var encodedMessage = Uri.EscapeDataString(message);
            var url = $"https://www.textnow.com/api/v3/sms/send?username=INSERT_USERNAME&password=INSERT_PASSWORD&phone={phoneNumber}&message={encodedMessage}";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return true;
            }

            return false;
        }
    }
}

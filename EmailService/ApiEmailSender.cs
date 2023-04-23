using RestSharp;

namespace EmailService
{
    public class ApiEmailSender : IEmailSenderStrategy
    {
        private readonly string _apiUrl;
        private readonly string _apiKey;
        private readonly EmailConfiguration _emailConfig;

        public ApiEmailSender(string apiUrl, string apiKey, EmailConfiguration emailConfiguration)
        {
            _apiUrl = apiUrl;
            _apiKey = apiKey;
            _emailConfig = emailConfiguration;
        }

        public void SendEmailAsync(Message message)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("", Method.Post);
            request.AddParameter("from", _emailConfig.From);
            request.AddParameter("to", string.Join(",", message.To));
            request.AddParameter("subject", message.Subject);
            request.AddParameter("body", message.Content);
            request.AddHeader("X-Api-Key", _apiKey);
            client.Execute(request);
        }
    }
}

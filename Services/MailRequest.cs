namespace RecorridoHistoricoApi.Services
{
    public class MailRequest
    {
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string CC { get; set; } = string.Empty;
        public bool IsBodyHtml { get; set; }
    }
}

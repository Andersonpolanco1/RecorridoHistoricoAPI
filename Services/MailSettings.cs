namespace EdecanesV2.Services
{
    public class MailSettings
    {
        public string From { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public bool EnableSsl { get; set; }
    }
}

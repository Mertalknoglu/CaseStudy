namespace LogService.Models
{
    public class LogMessage
    {
        public string Level { get; set; } // Info, Warning, Error
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public string Endpoint { get; set; } // Log'un hangi API endpoint'inden geldiği
    }
}

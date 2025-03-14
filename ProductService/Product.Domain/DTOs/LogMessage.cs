namespace Product.Domain.DTOs
{
    public class LogMessage
    {
        public string Level { get; set; } // Info, Error, Warning
        public string Message { get; set; } // Açıklama
        public string Exception { get; set; } // Varsa hata detayı
        public DateTime Timestamp { get; set; } = DateTime.UtcNow; // UTC Zaman
        public string Endpoint { get; set; } // Hangi endpoint'ten log geldiği
    }
}

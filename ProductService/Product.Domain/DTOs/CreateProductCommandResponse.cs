namespace Product.Domain.DTOs
{
    public class CreateProductCommandResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object ProductId { get; set; }
    }
}

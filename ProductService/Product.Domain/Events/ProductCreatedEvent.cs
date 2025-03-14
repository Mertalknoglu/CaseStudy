namespace Product.Domain.Events
{
    public class ProductCreatedEvent
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public ProductCreatedEvent(int productId, string productName)
        {
            ProductId = productId;
            ProductName = productName;
        }
    }
}

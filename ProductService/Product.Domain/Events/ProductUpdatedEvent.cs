namespace Product.Domain.Events
{
    public class ProductUpdatedEvent
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public ProductUpdatedEvent(int productId, string productName)
        {
            ProductId = productId;
            ProductName = productName;

        }
    }

}
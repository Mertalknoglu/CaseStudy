namespace Product.Domain.Events
{
    public class ProductDeletedEvent
    {
        public int ProductId { get; set; }

        public ProductDeletedEvent(int productId)
        {
            ProductId = productId;
        }
    }
}

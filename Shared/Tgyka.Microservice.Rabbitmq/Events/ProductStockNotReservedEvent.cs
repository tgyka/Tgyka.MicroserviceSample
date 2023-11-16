namespace Tgyka.Microservice.Rabbitmq.Events
{
    public class ProductStockNotReservedEvent
    {
        public ProductStockNotReservedEvent(int productId, int orderId, string userId)
        {
            ProductId = productId;
            OrderId = orderId;
            UserId = userId;
        }

        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public string UserId { get; set; }
    }
}

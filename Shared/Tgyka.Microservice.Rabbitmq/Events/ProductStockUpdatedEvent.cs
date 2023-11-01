namespace Tgyka.Microservice.Rabbitmq.Events
{
    public class ProductStockUpdatedEvent
    {
        public ProductStockUpdatedEvent(int productId, int orderId)
        {
            ProductId = productId;
            OrderId = orderId;
        }

        public int ProductId { get; set; }
        public int OrderId { get; set; }
    }
}

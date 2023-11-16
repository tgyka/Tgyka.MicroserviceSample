namespace Tgyka.Microservice.Rabbitmq.Events
{
    public class ProductStockUpdatedEvent
    {
        public ProductStockUpdatedEvent(int[] productIds, int orderId, string userId)
        {
            ProductIds = productIds;
            OrderId = orderId;
            UserId = userId;
        }

        public int[] ProductIds { get; set; }
        public int OrderId { get; set; }
        public string UserId { get; set; }
    }
}

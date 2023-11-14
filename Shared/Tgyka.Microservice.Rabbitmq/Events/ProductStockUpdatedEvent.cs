namespace Tgyka.Microservice.Rabbitmq.Events
{
    public class ProductStockUpdatedEvent
    {
        public ProductStockUpdatedEvent(int[] productIds, int orderId)
        {
            ProductIds = productIds;
            OrderId = orderId;
        }

        public int[] ProductIds { get; set; }
        public int OrderId { get; set; }
    }
}

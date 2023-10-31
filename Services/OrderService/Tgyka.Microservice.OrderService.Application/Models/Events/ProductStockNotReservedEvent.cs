namespace Tgyka.Microservice.OrderService.Application.Models.Events
{
    public class ProductStockNotReservedEvent
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
    }
}

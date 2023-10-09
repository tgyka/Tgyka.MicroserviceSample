namespace Tgyka.Microservice.BasketService.Models.Dtos
{
    public class BasketItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int ProductPrice { get; set; }
        public string CategoryName { get; set; }

    }
}

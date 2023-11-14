namespace Tgyka.Microservice.BasketService.Models.Dtos
{
    public class BasketResponseDto
    {
        public string UserId { get; set; }
        public List<BasketItemDto> Items { get; set; }
        public int ItemsCount { get; set; } 
        public int ItemsPriceSum { get; set; }
    }
}

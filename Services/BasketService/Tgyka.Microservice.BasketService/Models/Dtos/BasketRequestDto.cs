namespace Tgyka.Microservice.BasketService.Models.Dtos
{
    public class BasketRequestDto
    {
        public int UserId { get; set; }
        public List<BasketItemDto> Items { get; set; }
        public int ItemsCount
        {
            get
            {
                return Items != null ? Items.Count : 0;
            }
        }

        public int ItemsPriceSum
        {
            get
            {
                return Items != null ? Items.Sum(r => r.ProductPrice) : 0;
            }
        }
    }
}

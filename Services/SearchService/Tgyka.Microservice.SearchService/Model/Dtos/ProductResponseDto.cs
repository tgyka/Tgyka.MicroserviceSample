namespace Tgyka.Microservice.SearchService.Model.Dtos
{
    public class ProductResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public int Price { get; set; }
    }
}

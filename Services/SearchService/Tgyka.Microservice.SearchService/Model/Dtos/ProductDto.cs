namespace Tgyka.Microservice.SearchService.Model.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
    }
}

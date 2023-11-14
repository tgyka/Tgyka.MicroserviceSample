using Tgyka.Microservice.MssqlBase.Model.Dtos;

namespace Tgyka.Microservice.ProductService.Model.Dtos.Product.Responses
{
    public class ProductPanelResponseDto: GetDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int CategoryId { get; set; }
        public int Stock { get; set; }
    }
}

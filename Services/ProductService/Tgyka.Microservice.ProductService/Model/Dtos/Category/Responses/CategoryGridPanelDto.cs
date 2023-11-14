using Tgyka.Microservice.MssqlBase.Model.Dtos;

namespace Tgyka.Microservice.ProductService.Model.Dtos.Category.Responses
{
    public class CategoryGridPanelResponseDto: GetDto
    {
        public string Name { get; set; }
        public int ProductCount { get; set; }

    }
}

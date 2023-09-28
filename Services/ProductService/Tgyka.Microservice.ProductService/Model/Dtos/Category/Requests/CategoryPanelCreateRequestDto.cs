using Tgyka.Microservice.MssqlBase.Model.Dtos;

namespace Tgyka.Microservice.ProductService.Model.Dtos.Category.Requests
{
    public class CategoryPanelCreateRequestDto: CreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

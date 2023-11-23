using Tgyka.Microservice.MssqlBase.Model.Dtos;

namespace Tgyka.Microservice.ProductService.Model.Dtos.Category
{
    public class CategoryPanelDto : GetDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

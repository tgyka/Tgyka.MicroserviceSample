using Tgyka.Microservice.MssqlBase.Model.Dtos;

namespace Tgyka.Microservice.ProductService.Model.Dtos.Category.Responses
{
    public class CategoryPageDto: GetDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

using Tgyka.Microservice.MssqlBase.Data.Entity;

namespace Tgyka.Microservice.ProductService.Data.Entities
{
    public class Product: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}

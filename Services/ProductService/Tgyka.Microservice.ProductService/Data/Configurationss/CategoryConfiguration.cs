using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tgyka.Microservice.ProductService.Data.Entities;

namespace Tgyka.Microservice.ProductService.Data.Configurationss
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(300);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(200);
            builder.Property(x => x.ModifiedBy).HasMaxLength(200);

        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tgyka.Microservice.OrderService.Domain.Entities;

namespace Tgyka.Microservice.OrderService.Infrastructure.Configurations
{
    public class AddressConfiguration: IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Street).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Province).IsRequired().HasMaxLength(100);
            builder.Property(x => x.District).IsRequired().HasMaxLength(100);
            builder.Property(x => x.ZipCode).IsRequired().HasMaxLength(20);
            builder.Property(x => x.FullText).IsRequired().HasMaxLength(1000);

            builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(200);
            builder.Property(x => x.ModifiedBy).HasMaxLength(200);
        }
    }
}

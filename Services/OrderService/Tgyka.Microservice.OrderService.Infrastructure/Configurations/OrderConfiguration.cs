using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Tgyka.Microservice.OrderService.Domain.Aggregates.OrderAggreegate;

namespace Tgyka.Microservice.OrderService.Infrastructure.Configurations
{
    public class OrderConfiguration: IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.BuyerId).IsRequired().HasMaxLength(200);
            builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(200);
            builder.Property(x => x.ModifiedBy).HasMaxLength(200);

            builder.OwnsOne(o => o.Address, a =>
            {
                a.Property(x => x.Street).IsRequired().HasMaxLength(100);
                a.Property(x => x.Province).IsRequired().HasMaxLength(100);
                a.Property(x => x.District).IsRequired().HasMaxLength(100);
                a.Property(x => x.ZipCode).IsRequired().HasMaxLength(20);
                a.Property(x => x.FullText).IsRequired().HasMaxLength(1000);
                a.WithOwner();
            });

        }
    }
}

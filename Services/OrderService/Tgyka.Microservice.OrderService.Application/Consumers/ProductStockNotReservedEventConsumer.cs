using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tgyka.Microservice.OrderService.Domain.Aggregates.OrderAggreegate;
using Tgyka.Microservice.OrderService.Infrastructure;
using Tgyka.Microservice.Rabbitmq.Events;

namespace Tgyka.Microservice.OrderService.Application.Consumers
{
    public class ProductStockNotReservedEventConsumer : IConsumer<ProductStockNotReservedEvent>
    {
        private readonly OrderServiceDbContext _context;

        public ProductStockNotReservedEventConsumer(OrderServiceDbContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<ProductStockNotReservedEvent> context)
        {
            var order = _context.Orders.FirstOrDefault(r => r.Id == context.Message.OrderId);

            if(order == null)
            {
                return;
            }

            order.SetOrderStatus(OrderStatus.StockNotReserved);
            order.SetModified(context.Message.UserId);
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
    }
}

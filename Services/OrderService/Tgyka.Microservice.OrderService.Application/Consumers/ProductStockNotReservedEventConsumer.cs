using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tgyka.Microservice.MssqlBase.Data.Repository;
using Tgyka.Microservice.MssqlBase.Data.UnitOfWork;
using Tgyka.Microservice.OrderService.Application.Models.Events;
using Tgyka.Microservice.OrderService.Domain.Entities;
using Tgyka.Microservice.OrderService.Domain.Repositories;

namespace Tgyka.Microservice.OrderService.Application.Consumers
{
    internal class ProductStockNotReservedEventConsumer : IConsumer<ProductStockNotReservedEvent>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;


        public ProductStockNotReservedEventConsumer(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<ProductStockNotReservedEvent> context)
        {
            var order = _orderRepository.Get(r => r.Id == context.Message.OrderId);


            if(order == null)
            {

            }

            order.Status = OrderStatus.StockNotReserved;
            _orderRepository.Set(order, CommandState.Update);
            await _unitOfWork.CommitAsync();
        }
    }
}

using MassTransit;
using Tgyka.Microservice.MssqlBase.Data.Repository;
using Tgyka.Microservice.MssqlBase.Data.UnitOfWork;
using Tgyka.Microservice.ProductService.Data.Entities;
using Tgyka.Microservice.ProductService.Data.Repositories.Abstractions;
using Tgyka.Microservice.Rabbitmq.Events;

namespace Tgyka.Microservice.ProductService.Consumers
{
    public class ProductStockUpdatedEventConsumer : IConsumer<ProductStockUpdatedEvent>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPublishEndpoint _publishEndpoint;

        public ProductStockUpdatedEventConsumer(IProductRepository productRepository, IUnitOfWork unitOfWork, IPublishEndpoint publishEndpoint)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<ProductStockUpdatedEvent> context)
        {
            var productIds = context.Message.ProductIds;
            var products = _productRepository.List(r => productIds.Contains(r.Id));

            if(products == null || products.Count == 0)
            {
                return;
            }

            foreach(var product in products.DataList)
            {
                if (product.Stock <= 0)
                {
                    _publishEndpoint.Publish(new ProductStockNotReservedEvent(product.Id, context.Message.OrderId,context.Message.UserId));
                    products.DataList = products.DataList.Where(r => r.Id != product.Id).ToList();
                    continue;
                }

                product.Stock -= 1;
            }

            _productRepository.Set(products.DataList,CommandState.Update, context.Message.UserId);
            await _unitOfWork.CommitAsync();
        }
    }
}

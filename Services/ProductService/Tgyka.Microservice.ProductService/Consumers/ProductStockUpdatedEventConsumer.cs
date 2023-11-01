using MassTransit;
using Tgyka.Microservice.MssqlBase.Data.Repository;
using Tgyka.Microservice.MssqlBase.Data.UnitOfWork;
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
            var productId = context.Message.ProductId;
            var product = _productRepository.Get(r => r.Id == productId);

            if(product == null)
            {

            }

            if(product.Stock <= 0)
            {
                await _publishEndpoint.Publish(new ProductStockNotReservedEvent(productId,context.Message.OrderId));
                return;
            }

            product.Stock = -1;
            _productRepository.Set(product,CommandState.Update);
            await _unitOfWork.CommitAsync();
        }
    }
}

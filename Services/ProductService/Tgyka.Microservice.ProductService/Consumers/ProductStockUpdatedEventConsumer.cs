using MassTransit;
using Tgyka.Microservice.MssqlBase.Data.Repository;
using Tgyka.Microservice.MssqlBase.Data.UnitOfWork;
using Tgyka.Microservice.ProductService.Data.Repositories.Abstractions;
using Tgyka.Microservice.ProductService.Model.Events;

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
            var product = _productRepository.Get(r => r.Id == context.Message.ProductId);

            if(product == null)
            {

            }

            if(product.Stock <= 0)
            {
                _publishEndpoint.Publish(new ProductStockNotReservedEvent { ProductId = context.Message.ProductId });
                return;
            }

            product.Stock = -1;
            _productRepository.Set(product,CommandState.Update);
            await _unitOfWork.CommitAsync();
        }
    }
}

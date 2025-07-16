using MassTransit;
using Tgyka.Microservice.MssqlBase.Data.Enum;
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
            var products = _productRepository.GetAll(r => productIds.Contains(r.Id));

            if(products == null || products.Count == 0)
            {
                return;
            }

            foreach(var product in products)
            {
                if (product.Stock <= 0)
                {
                    throw new Exception("Product stock is not enough for order. ProductId: " + product.Id);
                }

                product.Stock -= 1;
            }

            _productRepository.SetEntityState(products,EntityCommandType.Update, context.Message.UserId);
            await _unitOfWork.CommitAsync();
        }
    }
}

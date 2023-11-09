using AutoMapper;
using MassTransit;
using Tgyka.Microservice.Rabbitmq.Events;
using Tgyka.Microservice.SearchService.Model.Dtos;
using Tgyka.Microservice.SearchService.Services.Abstractions;

namespace Tgyka.Microservice.SearchService.Consumers
{
    public class ProductDeletedEventConsumer : IConsumer<ProductDeletedEvent>
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductDeletedEventConsumer(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public Task Consume(ConsumeContext<ProductDeletedEvent> context)
        {
            return _productService.DeleteProduct(context.Message.Id);
        }
    }
}

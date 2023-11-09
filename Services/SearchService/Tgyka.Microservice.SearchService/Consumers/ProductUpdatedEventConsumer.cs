using AutoMapper;
using MassTransit;
using Tgyka.Microservice.Rabbitmq.Events;
using Tgyka.Microservice.SearchService.Model.Dtos;
using Tgyka.Microservice.SearchService.Services.Abstractions;

namespace Tgyka.Microservice.SearchService.Consumers
{
    public class ProductUpdatedEventConsumer : IConsumer<ProductUpdatedEvent>
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductUpdatedEventConsumer(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public Task Consume(ConsumeContext<ProductUpdatedEvent> context)
        {
            var product = _mapper.Map<ProductDto>(context.Message);

            return _productService.UpdateProduct(product);
        }
    }
}

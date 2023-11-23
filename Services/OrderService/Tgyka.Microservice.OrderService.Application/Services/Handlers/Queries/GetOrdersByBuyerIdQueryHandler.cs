using MediatR;
using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.MssqlBase.Model.RepositoryDtos;
using Tgyka.Microservice.OrderService.Application.Models.Dtos.Order;
using Tgyka.Microservice.OrderService.Application.Services.Queries;
using Tgyka.Microservice.OrderService.Domain.Repositories;

namespace Tgyka.Microservice.OrderService.Application.Services.Handlers.Queries
{
    public class GetOrdersByBuyerIdQueryHandler : IRequestHandler<GetOrdersByBuyerIdQuery, ApiResponse<PaginationModel<OrderDto>>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrdersByBuyerIdQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<ApiResponse<PaginationModel<OrderDto>>> Handle(GetOrdersByBuyerIdQuery request, CancellationToken cancellationToken)
        {
            var data = _orderRepository.GetAllMapped<OrderDto>(predicate: r => r.BuyerId == request.BuyerId, includes: new List<System.Linq.Expressions.Expression<Func<Domain.Entities.Order, object>>> { r => r.Address, r => r.OrderItems }, page: request.Page, size: request.Size);
            return ApiResponse<PaginationModel<OrderDto>>.Success(200,data);
        }
    }
}

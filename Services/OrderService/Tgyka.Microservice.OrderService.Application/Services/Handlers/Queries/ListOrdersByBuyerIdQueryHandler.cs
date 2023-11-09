using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.MssqlBase.Model.RepositoryDtos;
using Tgyka.Microservice.OrderService.Application.Models.Dtos.Order;
using Tgyka.Microservice.OrderService.Application.Services.Queries;
using Tgyka.Microservice.OrderService.Domain.Repositories;

namespace Tgyka.Microservice.OrderService.Application.Services.Handlers.Queries
{
    public class ListOrdersByBuyerIdQueryHandler : IRequestHandler<ListOrdersByBuyerIdQuery, ApiResponse<PaginationList<OrderDto>>>
    {
        private readonly IOrderRepository _orderRepository;

        public ListOrdersByBuyerIdQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<ApiResponse<PaginationList<OrderDto>>> Handle(ListOrdersByBuyerIdQuery request, CancellationToken cancellationToken)
        {
            var data = _orderRepository.ListWithMapper<OrderDto>(predicate: r => r.BuyerId == request.BuyerId, includes: new List<System.Linq.Expressions.Expression<Func<Domain.Entities.Order, object>>> { r => r.Address, r => r.OrderItems }, page: request.Page, size: request.Size);
            return ApiResponse<PaginationList<OrderDto>>.Success(200,data);
        }
    }
}

using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;
using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.MssqlBase.Model.RepositoryDtos;
using Tgyka.Microservice.OrderService.Application.Models.Dtos.Order;

namespace Tgyka.Microservice.OrderService.Application.Services.Queries.GetOrderByBuyerId
{
    public class GetOrdersByBuyerIdQueryHandler : IRequestHandler<GetOrdersByBuyerIdQuery, ApiResponse<PaginationModel<OrderDto>>>
    {
        public GetOrdersByBuyerIdQueryHandler()
        {

        }

        public async Task<ApiResponse<PaginationModel<OrderDto>>> Handle(GetOrdersByBuyerIdQuery request, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(request.ConnectionString))
            {
                var query = @"SELECT TOP @top * FROM Orders o 
                    LEFT JOIN OrderItems oi ON  o.Id = oi.orderid
                    WHERE BuyerId= @buyerId";
                var data = (await connection.QueryAsync<OrderDto>(query,new { buyerId =  request.BuyerId , top = (request.Page-1) * request.Size })).ToList();
                var count = (await connection.QueryAsync<OrderDto>(query, new { buyerId = request.BuyerId })).Count();
                var pagModel = new PaginationModel<OrderDto>(data, count, request.Page, request.Size);
                return ApiResponse<PaginationModel<OrderDto>>.Success(200, pagModel);

            }

        }
    }
}

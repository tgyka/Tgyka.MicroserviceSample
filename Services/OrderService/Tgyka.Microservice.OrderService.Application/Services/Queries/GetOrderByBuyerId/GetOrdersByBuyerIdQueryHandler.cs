using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.MssqlBase.Model.RepositoryDtos;
using Tgyka.Microservice.OrderService.Application.Models.Dtos.Address;
using Tgyka.Microservice.OrderService.Application.Models.Dtos.Order;
using Tgyka.Microservice.OrderService.Application.Models.Dtos.OrderItem;

namespace Tgyka.Microservice.OrderService.Application.Services.Queries.GetOrderByBuyerId
{
    public class GetOrdersByBuyerIdQueryHandler : IRequestHandler<GetOrdersByBuyerIdQuery, ApiResponse<PaginationModel<OrderDto>>>
    {
        private readonly string _connectionString;


        public GetOrdersByBuyerIdQueryHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlServer");
        }

        public async Task<ApiResponse<PaginationModel<OrderDto>>> Handle(GetOrdersByBuyerIdQuery request, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var dataSql = @"
                    SELECT
                        o.Id, o.BuyerId, o.OrderDate, o.Total,
                        a.Id, a.Street, a.City, a.ZipCode,
                        oi.Id, oi.OrderId, oi.ProductId, oi.Quantity, oi.UnitPrice
                    FROM Orders o
                    LEFT JOIN Addresses a   ON o.AddressId = a.Id
                    LEFT JOIN OrderItems oi ON o.Id        = oi.OrderId
                    WHERE o.BuyerId = @BuyerId
                    ORDER BY o.Id
                    OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;
                    ";

                var countSql = @"
                    SELECT COUNT(DISTINCT o.Id)
                    FROM Orders o
                    WHERE o.BuyerId = @BuyerId;
                    ";

                var parameters = new
                {
                    BuyerId = request.BuyerId,
                    Offset = (request.Page - 1) * request.Size,
                    Size = request.Size
                };

                var orderDict = new Dictionary<int, OrderDto>();
                var items = await connection.QueryAsync<OrderDto, AddressDto, OrderItemDto, OrderDto>(
                    dataSql,
                    map: (order, address, item) =>
                    {
                        if (!orderDict.TryGetValue(order.Id, out var existing))
                        {
                            existing = order;
                            existing.Address = address;
                            existing.OrderItems = new List<OrderItemDto>();
                            orderDict.Add(order.Id, existing);
                        }
                        if (item != null)
                            existing.OrderItems.Add(item);
                        return existing;
                    },
                    param: parameters,
                    splitOn: "Id,Id" 
                );
                var pagedData = orderDict.Values.ToList();

                var totalCount = await connection.ExecuteScalarAsync<int>(countSql, new { BuyerId = request.BuyerId });

                var pagModel = new PaginationModel<OrderDto>(pagedData, totalCount, request.Page, request.Size);
                return ApiResponse<PaginationModel<OrderDto>>.Success(200, pagModel);
            }

        }
    }
}

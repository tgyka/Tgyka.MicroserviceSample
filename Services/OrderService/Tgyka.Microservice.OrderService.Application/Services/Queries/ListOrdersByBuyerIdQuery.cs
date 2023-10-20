using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.OrderService.Application.Models.Dtos.Order;

namespace Tgyka.Microservice.OrderService.Application.Services.Queries
{
    public class ListOrdersByBuyerIdQuery: IRequest<ApiResponseDto<List<OrderDto>>>
    {
        public int BuyerId { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }


    }
}

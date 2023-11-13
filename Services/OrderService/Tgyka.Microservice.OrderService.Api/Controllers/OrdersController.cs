using MediatR;
using Microsoft.AspNetCore.Mvc;
using MssqlRestApi.Base.Controller;
using Tgyka.Microservice.OrderService.Application.Services.Commands;
using Tgyka.Microservice.OrderService.Application.Services.Queries;

namespace Tgyka.Microservice.OrderService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController: TgykaMicroserviceControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ListOrdersByBuyerId(string buyerId, int page , int size)
        {
            return ApiActionResult(await _mediator.Send(new ListOrdersByBuyerIdQuery { BuyerId = buyerId, Page = page, Size = size }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand command)
        {
            return ApiActionResult(await _mediator.Send(command));
        }
    }
}

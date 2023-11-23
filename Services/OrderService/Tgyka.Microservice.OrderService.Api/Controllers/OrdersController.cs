using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MssqlRestApi.Base.Controller;
using Tgyka.Microservice.Base.Model.Token;
using Tgyka.Microservice.OrderService.Application.Services.Commands;
using Tgyka.Microservice.OrderService.Application.Services.Queries;

namespace Tgyka.Microservice.OrderService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController: TgykaMicroserviceControllerBase
    {
        private readonly IMediator _mediator;
        private readonly TokenUser _tokenUser;

        public OrdersController(IMediator mediator, TokenUser tokenUser)
        {
            _mediator = mediator;
            _tokenUser = tokenUser;
        }

        [HttpGet]
        public async Task<IActionResult> ListOrdersByBuyerId(int page , int size)
        {
            return ApiActionResult(await _mediator.Send(new GetOrdersByBuyerIdQuery { BuyerId = _tokenUser.Id, Page = page, Size = size }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand command)
        {
            return ApiActionResult(await _mediator.Send(command));
        }
    }
}

using AutoMapper;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.Base.Model.Token;
using Tgyka.Microservice.OrderService.Application.Models.Dtos.Order;
using Tgyka.Microservice.OrderService.Application.Services.Commands;
using Tgyka.Microservice.OrderService.Domain.Aggregates.OrderAggreegate;
using Tgyka.Microservice.OrderService.Infrastructure;
using Tgyka.Microservice.Rabbitmq.Events;

namespace Tgyka.Microservice.OrderService.Application.Services.Handlers.Commands;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ApiResponse<OrderDto>>
{
    

    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly TokenUser _tokenUser;
    private readonly OrderServiceDbContext _context;

    public CreateOrderCommandHandler(IMapper mapper, IPublishEndpoint publishEndpoint, TokenUser tokenUser, OrderServiceDbContext context)
    {
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
        _tokenUser = tokenUser;
        _context = context;
    }

    public async Task<ApiResponse<OrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var address = new Address(request.Address.Street, request.Address.District, request.Address.Province, request.Address.ZipCode, request.Address.FullText);
        var order = new Order(_tokenUser.Id,request.Status,address);
        
        request.OrderItems.ForEach(x =>
        {
            order.AddOrderItem(x.ProductId, x.ProductName, x.Price, x.ImageUrl);
        });

        order.SetCreated(_tokenUser.Id);

        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();

        _publishEndpoint.Publish(new ProductStockUpdatedEvent(order.OrderItems.Select(r => r.ProductId).ToArray(), order.Id,order.BuyerId));

        return ApiResponse<OrderDto>.Success(201,_mapper.Map<OrderDto>(order));
    }
}

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
using Tgyka.Microservice.MssqlBase.Data.Enum;
using Tgyka.Microservice.MssqlBase.Data.Repository;
using Tgyka.Microservice.MssqlBase.Data.UnitOfWork;
using Tgyka.Microservice.OrderService.Application.Models.Dtos.Order;
using Tgyka.Microservice.OrderService.Application.Models.Dtos.OrderItem;
using Tgyka.Microservice.OrderService.Application.Services.Commands;
using Tgyka.Microservice.OrderService.Domain.Entities;
using Tgyka.Microservice.OrderService.Domain.Repositories;
using Tgyka.Microservice.Rabbitmq.Events;

namespace Tgyka.Microservice.OrderService.Application.Services.Handlers.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ApiResponse<OrderDto>>
    {
        IOrderRepository _orderRepository;
        IAddressRepository _addressRepository;
        IOrderItemRepository _orderItemRepository;
        IUnitOfWork _unitOfWork;
        IMapper _mapper;
        IPublishEndpoint _publishEndpoint;
        TokenUser _tokenUser;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IAddressRepository addressRepository, IOrderItemRepository orderItemRepository, IUnitOfWork unitOfWork, IMapper mapper, IPublishEndpoint publishEndpoint, TokenUser tokenUser)
        {
            _orderRepository = orderRepository;
            _addressRepository = addressRepository;
            _orderItemRepository = orderItemRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _tokenUser = tokenUser;
        }

        public async Task<ApiResponse<OrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(request);
            order.BuyerId = _tokenUser.Id;

            _orderItemRepository.SetEntityState(order.OrderItems, EntityCommandType.Create);
            _addressRepository.SetEntityState(order.Address, EntityCommandType.Create);
            _orderRepository.SetEntityState(order, EntityCommandType.Create);
            await _unitOfWork.CommitAsync();

            _publishEndpoint.Publish(new ProductStockUpdatedEvent(order.OrderItems.Select(r => r.ProductId).ToArray(), order.Id,order.BuyerId));

            return ApiResponse<OrderDto>.Success(201,_mapper.Map<OrderDto>(order));
        }
    }
}

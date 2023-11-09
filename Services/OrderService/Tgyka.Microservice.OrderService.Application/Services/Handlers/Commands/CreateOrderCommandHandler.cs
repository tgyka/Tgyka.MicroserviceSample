using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.MssqlBase.Data.Repository;
using Tgyka.Microservice.MssqlBase.Data.UnitOfWork;
using Tgyka.Microservice.OrderService.Application.Models.Dtos.Order;
using Tgyka.Microservice.OrderService.Application.Models.Dtos.OrderItem;
using Tgyka.Microservice.OrderService.Application.Services.Commands;
using Tgyka.Microservice.OrderService.Domain.Entities;
using Tgyka.Microservice.OrderService.Domain.Repositories;

namespace Tgyka.Microservice.OrderService.Application.Services.Handlers.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ApiResponse<OrderDto>>
    {
        IOrderRepository _orderRepository;
        IAddressRepository _addressRepository;
        IOrderItemRepository _orderItemRepository;
        IUnitOfWork _unitOfWork;
        IMapper _mapper;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IAddressRepository addressRepository, IOrderItemRepository orderItemRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _addressRepository = addressRepository;
            _orderItemRepository = orderItemRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<OrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(request);

            _orderItemRepository.Set(order.OrderItems, CommandState.Create);
            _addressRepository.Set(order.Address, CommandState.Create);
            _orderRepository.Set(order, CommandState.Create);
            await _unitOfWork.CommitAsync();

            return ApiResponse<OrderDto>.Success(201,_mapper.Map<OrderDto>(order));
        }
    }
}

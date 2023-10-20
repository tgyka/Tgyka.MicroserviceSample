using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.MssqlBase.Data.UnitOfWork;
using Tgyka.Microservice.OrderService.Application.Models.Dtos.Order;
using Tgyka.Microservice.OrderService.Application.Models.Dtos.OrderItem;
using Tgyka.Microservice.OrderService.Application.Services.Commands;
using Tgyka.Microservice.OrderService.Domain.Entities;
using Tgyka.Microservice.OrderService.Domain.Repositories;

namespace Tgyka.Microservice.OrderService.Application.Services.Handlers.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ApiResponseDto<OrderDto>>
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

        public async Task<ApiResponseDto<OrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(request);

            _orderItemRepository.Set(order.OrderItems, Microsoft.EntityFrameworkCore.EntityState.Added);
            _addressRepository.Set(order.Address, Microsoft.EntityFrameworkCore.EntityState.Added);
            _orderRepository.Set(order, Microsoft.EntityFrameworkCore.EntityState.Added);
            await _unitOfWork.CommitAsync();

            return ApiResponseDto<OrderDto>.Success(201,_mapper.Map<OrderDto>(order));
        }
    }
}

using FluentValidation;
using Tgyka.Microservice.OrderService.Application.Models.Dtos.OrderItem;
using Tgyka.Microservice.OrderService.Application.Services.Commands;

namespace Tgyka.Microservice.OrderService.Application.Services.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Address).NotNull();
        RuleFor(x => x.Address.Street).NotEmpty();
        RuleFor(x => x.Address.District).NotEmpty();
        RuleFor(x => x.Address.Province).NotEmpty();
        RuleFor(x => x.Address.ZipCode).NotEmpty();
        RuleFor(x => x.OrderItems).NotNull().NotEmpty();
        RuleForEach(x => x.OrderItems).SetValidator(new OrderItemCreateDtoValidator());
    }
}

public class OrderItemCreateDtoValidator : AbstractValidator<OrderItemCreateDto>
{
    public OrderItemCreateDtoValidator()
    {
        RuleFor(x => x.ProductId).GreaterThan(0);
        RuleFor(x => x.ProductName).NotEmpty();
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.ImageUrl).NotEmpty();
    }
}

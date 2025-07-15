using FluentValidation;
using Tgyka.Microservice.OrderService.Application.Models.Dtos.OrderItem;

namespace Tgyka.Microservice.OrderService.Application.Validators;

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

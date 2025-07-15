using FluentValidation;
using Tgyka.Microservice.OrderService.Application.Services.Commands;

namespace Tgyka.Microservice.OrderService.Application.Validators;

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

using DAL.Models;
using FluentValidation;

namespace WebStorePrototype.Server.Services.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator() {
            RuleFor(x => x.OrderDate).NotEmpty().WithMessage("You must have an order date");
            RuleFor(x => x.Products).NotEmpty().WithMessage("You need at least 1 product to create order");
            RuleFor(x => x.OrderNumber).NotEmpty().WithMessage("You need order number to create order");
            RuleFor(x => x.TotalAmount).InclusiveBetween(1, 1000).NotEmpty().WithMessage("You must have at least 1 product in cart");
        }
    }
}

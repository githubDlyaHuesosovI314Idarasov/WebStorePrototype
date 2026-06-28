using DAL.Models;
using FluentValidation;

namespace WebStorePrototype.Server.Models.Validators
{
    public class StockValidator : AbstractValidator<Stock>
    {
        public StockValidator() {
            RuleFor(x => x.Quantity).InclusiveBetween(0, Int32.MaxValue).WithMessage("Quantity cannot be lower than 0");
            RuleFor(x => x.Location).NotEmpty().WithMessage("Location must have at least 1 character");
        }

    }
}

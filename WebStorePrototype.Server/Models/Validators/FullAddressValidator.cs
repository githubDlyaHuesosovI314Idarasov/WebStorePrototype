using DAL.Models;
using FluentValidation;

namespace WebStorePrototype.Server.Models.Validators
{
    public class FullAddressValidator : AbstractValidator<FullAddress>
    {
        public FullAddressValidator() {
            RuleFor(x => x.PostalCode).NotEmpty().WithMessage("Postal code must not be empty");
            RuleFor(x => x.Street).NotEmpty().WithMessage("Street must not be empty");
            RuleFor(x => x.HouseNumber).InclusiveBetween(1, Int32.MaxValue).WithMessage("House number must be at least 1");
            RuleFor(x => x.City).NotEmpty().WithMessage("City must not be empty");
        }
    }
}

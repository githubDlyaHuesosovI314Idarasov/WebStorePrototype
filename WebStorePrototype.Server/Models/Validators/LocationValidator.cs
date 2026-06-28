using DAL.Models;
using FluentValidation;

namespace WebStorePrototype.Server.Models.Validators
{
    public class LocationValidator : AbstractValidator<Location>
    {
        public LocationValidator() {
            RuleFor(x => x.Address).NotEmpty();
            
        }
    }
}

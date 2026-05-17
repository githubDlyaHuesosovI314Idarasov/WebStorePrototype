using DAL.Models;
using FluentValidation;

namespace WebStorePrototype.Server.Services.Validators
{
    public class LocationValidator : AbstractValidator<Location>
    {
        public LocationValidator() {
            RuleFor(x => x.Address).NotEmpty();
            
        }
    }
}

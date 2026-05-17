using DAL.Models;
using FluentValidation;

namespace WebStorePrototype.Server.Services.Validators
{
    public class SubcategoryValidator : AbstractValidator<Subcategory>
    {
        public SubcategoryValidator() 
        {
            RuleFor(x => x.Name).MinimumLength(3).MaximumLength(80).WithMessage("Subcategory must have at least 3 characters");
            RuleFor(x => x.Route).NotEmpty().WithMessage("Subcategory must have a route to search by it");
        }
    }
}

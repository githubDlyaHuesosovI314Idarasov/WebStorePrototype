using DAL.Models;
using FluentValidation;

namespace WebStorePrototype.Server.Models.Validators
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator() { 
        
            RuleFor(x => x.Icon).NotEmpty().WithMessage("Must have Icon string");
            RuleFor(x => x.Name).MinimumLength(3).MaximumLength(80).WithMessage("Must have name at least 3 letters");
            RuleFor(x => x.Route).NotEmpty().WithMessage("Must have Route to route by that category");
            RuleFor(x => x.Subcategories).NotEmpty().WithMessage("Must have at least 1 subcategory");
        }
    }
}

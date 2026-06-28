using DAL.Models;
using FluentValidation;

namespace WebStorePrototype.Server.Models.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator() {

            RuleFor(x => x.Name).NotEmpty().WithMessage("Product name must not be empty");
            RuleFor(x => x.SKU).NotEmpty().WithMessage("SKU must not be empty");
            RuleFor(x => x.Price).InclusiveBetween(1, Int64.MaxValue).WithMessage("Price must be at least 1 price unit");
            RuleFor(x => x.Brand).NotEmpty().WithMessage("Brand must not be emtpy");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description must not be empty");
            RuleFor(x => x.DiscountedPrice).InclusiveBetween(1, Int64.MaxValue).WithMessage("Discounted price must be at least 1 price unit");
        }
    }
}

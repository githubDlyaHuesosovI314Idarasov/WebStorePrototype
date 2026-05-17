using DAL.Models;
using FluentValidation;

namespace WebStorePrototype.Server.Services.Validators
{
    public class ReviewValidator : AbstractValidator<Review>
    {
        public ReviewValidator() {
            RuleFor(x => x.CreatedAt).NotEmpty().WithMessage("Review always must have date of creation");
            RuleFor(x => x.Comment).NotEmpty().WithMessage("Comment must have at least 1 character");
        }
    }
}

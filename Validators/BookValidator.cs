using Diligent.MinimalAPI.Models;
using FluentValidation;

namespace Diligent.MinimalAPI.Validators
{
    public class BookValidator : AbstractValidator<Book>
    {

        public BookValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.ISBN).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Publisher).NotEmpty();
            RuleFor(x => x.PageCount).NotNull();
            RuleFor(x => x.ReleasedDate).NotNull();
        }
    }
}

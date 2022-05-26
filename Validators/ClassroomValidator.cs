using Diligent.MinimalAPI.Models;
using FluentValidation;

namespace Diligent.MinimalAPI.Validators
{
    public class ClassroomValidator : AbstractValidator<Classroom>
    {
        public ClassroomValidator()
        {
            RuleFor(x => x.Identifier).NotEmpty();
            RuleFor(x => x.Floor).NotNull();
            RuleFor(x => x.Capacity).NotNull();
            RuleFor(x => x.Sector).NotEmpty();
            RuleFor(x => x.IsCopmuterLab).NotNull();
        }


    }
}

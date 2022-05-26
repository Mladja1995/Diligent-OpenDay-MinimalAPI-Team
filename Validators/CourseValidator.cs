using Diligent.MinimalAPI.Models;
using FluentValidation;

namespace Diligent.MinimalAPI.Validators
{
    public class CourseValidator: AbstractValidator<Course>
    {
        public CourseValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Points).NotNull();
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Semester).NotNull();
            RuleFor(x => x.IsOptional).NotNull();
        }
    }
}

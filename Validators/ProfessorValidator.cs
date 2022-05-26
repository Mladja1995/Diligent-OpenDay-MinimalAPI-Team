using Diligent.MinimalAPI.Models;
using FluentValidation;

namespace Diligent.MinimalAPI.Validators
{
    public class ProfessorValidator : AbstractValidator<Professor>
    {
        public ProfessorValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.JMBG).NotEmpty();
            RuleFor(x => x.CourseID).NotNull();
            RuleFor(x => x.YearsOfExperience).NotNull();
        }
    }
}

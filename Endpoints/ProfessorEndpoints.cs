using Diligent.MinimalAPI.Models;
using Diligent.MinimalAPI.Services;
using FluentValidation;
using FluentValidation.Results;

namespace Diligent.MinimalAPI.Endpoints
{
    public static class ProfessorEndpoints
    {
        private const string Tag = "Professors";
        private const string BaseRoute = "professors";

        // Extentions methods:
        // Services DI
        public static void AddProfessorEndpoints(
            this IServiceCollection services)
        {
            services.AddSingleton<IProfessorService, ProfessorService>();
        }

        // Endpoints

        public static void UseProfessorEndpoints(
            this IEndpointRouteBuilder app)
        {
            app.MapPost(BaseRoute, CreateProfessorAsync).WithTags(Tag);
            app.MapGet($"{BaseRoute}/{{index}}", GetProfessorByJMBGAsync).WithTags(Tag);

            app.MapGet($"{BaseRoute}/jovan", ()=>"Joca").WithTags(Tag);

        }

        internal static async Task<IResult> CreateProfessorAsync(Professor professor, IProfessorService professorService,
            IValidator<Professor> validator, ILogger<Program> logger)
        {
            try
            {
                var validationResult = await validator.ValidateAsync(professor);
                if (!validationResult.IsValid)
                {
                    return Results.BadRequest(validationResult.Errors);
                }

                var isCreated = await professorService.CreateAsync(professor);
                return isCreated ? Results.Created($"{BaseRoute}/{professor.JMBG}", professor) : Results.BadRequest();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.InnerException.Message);
                return Results.StatusCode(500);
            }
        }

        internal static async Task<IResult> GetProfessorByJMBGAsync(string JMBG, IProfessorService professorService, ILogger<Program> logger)
        {
            try
            {
                var professor = await professorService.GetByJMBGAsync(JMBG);
                return professor != null ? Results.Ok(professor) : Results.NotFound();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.InnerException.Message);
                return Results.StatusCode(500);
            }
        }
    }
}

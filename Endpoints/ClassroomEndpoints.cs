using Diligent.MinimalAPI.Models;
using Diligent.MinimalAPI.Services;
using Diligent.MinimalAPI.Services.Interfaces;
using FluentValidation;
using FluentValidation.Results;

namespace Diligent.MinimalAPI.Endpoints
{
    public static class ClassroomEndpoints
    {
        private const string Tag = "Classrooms";
        private const string BaseRoute = "clasrooms";

        // Extentions methods:
        // Services DI
        public static void AddClassroomEndpoints(
            this IServiceCollection services)
        {
            services.AddSingleton<IClassroomService, ClassroomService>();
        }

        // Endpoints

        public static void UseClassroomEndpoints(
            this IEndpointRouteBuilder app)
        {
            app.MapPost(BaseRoute, CreateClassroomAsync).WithTags(Tag);
            app.MapGet($"{BaseRoute}/{{identifier}}", GetClassroomByIdentifierAsync).WithTags(Tag);

        }

        internal static async Task<IResult> CreateClassroomAsync(Classroom classroom, IClassroomService classroomService,
            IValidator<Classroom> validator, ILogger<Program> logger)
        {
            try
            {
                var validationResult = await validator.ValidateAsync(classroom);
                if (!validationResult.IsValid)
                {
                    return Results.BadRequest(validationResult.Errors);
                }

                var isCreated = await classroomService.CreateAsync(classroom);
                return isCreated ? Results.Created($"{BaseRoute}/{classroom.Identifier}", classroom) : Results.BadRequest();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.InnerException.Message);
                return Results.StatusCode(500);
            }
        }

        internal static async Task<IResult> GetClassroomByIdentifierAsync(string identifier, IClassroomService classroomService, ILogger<Program> logger)
        {
            try
            {
                var classroom = await classroomService.GetByIdentifierAsync(identifier);
                return classroom != null ? Results.Ok(classroom) : Results.NotFound();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.InnerException.Message);
                return Results.StatusCode(500);
            }
        }
    }
}

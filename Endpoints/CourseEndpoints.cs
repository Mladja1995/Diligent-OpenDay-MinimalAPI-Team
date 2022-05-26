using Diligent.MinimalAPI.Models;
using Diligent.MinimalAPI.Services;
using Diligent.MinimalAPI.Services.Interfaces;
using FluentValidation;
using FluentValidation.Results;

namespace Diligent.MinimalAPI.Endpoints
{
    public static class CourseEndpoints
    {
        private const string Tag = "Courses";
        private const string BaseRoute = "courses";

        public static void AddCourseEndpoints(this IServiceCollection services)
        {
            services.AddSingleton<ICourseService, CourseService>();
        }

        public static void UseCourseEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost(BaseRoute, CreateCourseAsync).WithTags(Tag);
            app.MapGet($"{BaseRoute}/{{code}}", GetCourseByCodeAsync).WithTags(Tag);
            app.MapDelete($"{BaseRoute}/{{code}}", DeleteCourseAsync).WithTags(Tag);
        }

        internal static async Task<IResult> CreateCourseAsync(Course course, ICourseService courseService,
            IValidator<Course> validator, ILogger<Program> logger)
        {
            try
            {
                var validationResult = await validator.ValidateAsync(course);
                if (!validationResult.IsValid)
                {
                    return Results.BadRequest(validationResult.Errors);
                }

                var isCreated = await courseService.CreateAsync(course);
                return isCreated ? Results.Created($"{BaseRoute}/{course.Code}", course) : Results.BadRequest();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.InnerException.Message);
                return Results.StatusCode(500);
            }
        }

        internal static async Task<IResult> GetCourseByCodeAsync(string code, ICourseService courseService, ILogger<Program> logger)
        {
            try
            {
                var course = await courseService.GetByCodeAsync(code);
                return course != null ? Results.Ok(course) : Results.BadRequest();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.InnerException.Message);
                return Results.StatusCode(500);
            }
        }

        internal static async Task<IResult> DeleteCourseAsync(string code, ICourseService courseService, ILogger<Program> logger)
        {
            try
            {
                var course = await courseService.DeleteAsync(code);

                return course ? Results.Ok() : Results.BadRequest();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.InnerException.Message);
                return Results.StatusCode(500);
            }
        }


    }
}

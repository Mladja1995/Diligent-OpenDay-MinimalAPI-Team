using Diligent.MinimalAPI.Models;
using Diligent.MinimalAPI.Services;
using FluentValidation;
using FluentValidation.Results;

namespace Diligent.MinimalAPI.Endpoints
{
    public static class StudentEndpoints
    {
        private const string Tag = "Students";
        private const string BaseRoute = "students";

        // Extentions methods:
        // Services DI
        public static void AddStudentEndpoints(
            this IServiceCollection services)
        {
            services.AddSingleton<IStudentService, StudentService>();
        }

        // Endpoints

        public static void UseStudentEndpoints(
            this IEndpointRouteBuilder app)
        {
            app.MapPost(BaseRoute, CreateStudentAsync).WithTags(Tag);
            app.MapGet($"{BaseRoute}/{{index}}", GetStudentByIndexAsync).WithTags(Tag);

        }

        internal static async Task<IResult> CreateStudentAsync(Student student, IStudentService studentService, 
            IValidator<Student> validator, ILogger<Program> logger)
        {
            try
            {
                var validationResult = await validator.ValidateAsync(student);
                if (!validationResult.IsValid)
                {
                    return Results.BadRequest(validationResult.Errors);
                }

                var isCreated = await studentService.CreateAsync(student);
                return isCreated ? Results.Created($"{BaseRoute}/{student.IndexNum}", student) : Results.BadRequest();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.InnerException.Message);
                return Results.StatusCode(500);
            }
        }

        internal static async Task<IResult> GetStudentByIndexAsync(int index, IStudentService studentService, ILogger<Program> logger)
        {
            try
            {
                var student = await studentService.GetByIndexAsync(index);
                return student != null ? Results.Ok(student) : Results.NotFound();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.InnerException.Message);
                return Results.StatusCode(500);
            }
        }
    }
}

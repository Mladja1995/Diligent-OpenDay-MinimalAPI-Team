using Diligent.MinimalAPI.Models;
using Diligent.MinimalAPI.Services;
using Diligent.MinimalAPI.Services.Interfaces;
using FluentValidation;
using FluentValidation.Results;

namespace Diligent.MinimalAPI.Endpoints
{
    public static class BookEndpoints
    {

        private const string Tag = "Book";
        private const string BaseRoute = "Book";

        public static void AddBookEndpoints(
           this IServiceCollection services)
        {
            services.AddSingleton<IBookService, BookService>();
        }

        public static void UseBookEndpoints(
            this IEndpointRouteBuilder app)
        {
            app.MapPost(BaseRoute, CreateAsync).WithTags(Tag);
            app.MapGet($"{BaseRoute}/{{index}}", GetByIdAsync).WithTags(Tag);
        }

        internal static async Task<IResult> CreateAsync(Book book, IBookService bookService,
          IValidator<Book> validator, ILogger<Program> logger)
        {
            try
            {
                var validationResult = await validator.ValidateAsync(book);
                if (!validationResult.IsValid)
                {
                    return Results.BadRequest(validationResult.Errors);
                }

                var isCreated = await bookService.CreateAsync(book);
                return isCreated ? Results.Created($"{BaseRoute}/{book.Id}", book) : Results.BadRequest();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.InnerException.Message);
                return Results.StatusCode(500);
            }
        }

        internal static async Task<IResult> GetByIdAsync(int id, IBookService bookService, ILogger<Program> logger)
        {
            try
            {
                var book = await bookService.GetByIdAsync(id);
                return book != null ? Results.Ok(book) : Results.NotFound();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.InnerException.Message);
                return Results.StatusCode(500);
            }
        }
    }
}

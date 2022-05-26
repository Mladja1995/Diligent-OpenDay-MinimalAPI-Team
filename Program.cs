using Diligent.MinimalAPI.Database;
using Diligent.MinimalAPI.Endpoints;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<FacultyContext>();

// Swagger setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// End points DI
builder.Services.AddStudentEndpoints();
builder.Services.AddCourseEndpoints();
// Validation
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

// Swagger setup
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});


app.UseStudentEndpoints();
app.UseCourseEndpoints();

app.Run();

using Microsoft.AspNetCore.Mvc;
using OrderBurger.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDatabase(builder.Configuration)
    .AddRepositories()
    .AddApplicationServices()
    .AddAutoMapperProfiles()
    .AddFluentValidators()
    .AddBusinessServices()
    .AddSwaggerDocs();

builder.Services.AddRouting(options =>
    options.LowercaseUrls = true);

builder.Services
    .AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = false;
        options.InvalidModelStateResponseFactory = context =>
        {
            var problemDetails = new ValidationProblemDetails(context.ModelState)
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                Title = "Falha de validação na requisição.",
                Status = StatusCodes.Status400BadRequest,
                Detail = "Um ou mais campos estão inválidos. Corrija e tente novamente.",
                Instance = context.HttpContext.Request.Path
            };

            problemDetails.Extensions["traceId"] = context.HttpContext.TraceIdentifier;

            return new BadRequestObjectResult(problemDetails)
            {
                ContentTypes = { "application/problem+json" }
            };
        };
    });

builder.Services
    .AddHealthChecks()
    .AddDbContextCheck<global::OrderBurger.API.Data.AppDbContext>("database");

builder.Logging
    .ClearProviders()
    .AddConsole()
    .AddDebug();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Order Burger API v1");
        options.RoutePrefix = string.Empty;
    });

    await app.ApplyMigrationsAsync();
}

app.UseHttpsRedirection();
app.MapControllers();
app.MapHealthChecks("/health");

app.Run();

public partial class Program { }
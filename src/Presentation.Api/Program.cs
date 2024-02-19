using Application;
using Infrastructure;
using Presentation.Api;
using Presentation.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPresentation().AddApplication().AddInfrastructure();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthorization();
app.MapControllers();

app.Run();

using Application;
using Infrastructure;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authentication;
using Presentation.Api;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPresentation().AddApplication().AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
        options =>
        {
            options.EnablePersistAuthorization();
        }
    );
}

app.UsePresentation();
app.UseAuthorization();
app.MapControllers();

app.Run();

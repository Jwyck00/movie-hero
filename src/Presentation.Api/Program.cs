using Application;
using Infrastructure;
using Infrastructure.Persistence;
using Presentation.Api;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddPresentation(configuration).AddApplication().AddInfrastructure(configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
    app.UseSwagger();
    app.UseSwaggerUI(
        options =>
        {
            options.EnablePersistAuthorization();
        }
    );
}

app.UsePresentation(configuration);
app.MapControllers();

app.Run();

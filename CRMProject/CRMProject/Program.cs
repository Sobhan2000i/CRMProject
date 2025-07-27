using CRMProject;
using CRMProject.Extensions;


var builder = WebApplication.CreateBuilder(args);

builder.AddDatabase()
    .AddApiServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    await app.ApplyMigrationsAsync();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();

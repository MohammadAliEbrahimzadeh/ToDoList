using Beacon.API;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using ToDoList.Web;


var builder = WebApplication.CreateBuilder(args);

builder.Services
    .InjectAddEndpointsApiExplorer()
    .InjectAddSwaggerGen()
    .InjectControllers()
    .InjectContext(builder.Configuration)
    .InjectUnitOfWork()
    .InjectServices()
    //.InjectFluentValidation()
    .InjectLogger(builder.Configuration)
    .InjectOutputCache();

var app = builder.Build();

app.UseOutputCache();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseMiddleware<ExceptionHandler>();

app.MapControllers();

app.Run();

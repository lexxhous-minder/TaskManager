using Microsoft.EntityFrameworkCore;
using TaskManager.AppDataContext;
using TaskManager.Interfaces;
using TaskManager.Mapping;
using TaskManager.Middleware;
using TaskManager.Models;
using TaskManager.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();
builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings"));
builder.Services.AddSingleton<TaskManagerDbContext>();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile(new AutoMapperProfile());
});
builder.Services.AddScoped<ITaskServices, TaskServices>();
builder.Services.AddScoped<ITaskTypeServices, TaskTypeServices>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddLogging();

var app = builder.Build();
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<TaskManagerDbContext>();
    context.Database.Migrate();
    DataSeeder.SeedTaskTypes(context);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.MapOpenApi();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseExceptionHandler();
app.UseAuthorization();
app.MapControllers();
app.Run();
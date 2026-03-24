using FluentValidation;
using FluentValidation.AspNetCore;
using ToDoListApi.Application.Interfaces;
using ToDoListApi.Application.Services;
using ToDoListApi.Application.Validators;
using ToDoListApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateToDoItemValidator>();

// Application (Service)
builder.Services.AddScoped<IToDoItemService, ToDoItemService>();

// Infrastructure (DbContext, Repository)
builder.Services.AddInfrastructure(builder.Configuration);

// CORS para React
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowReact");
app.UseAuthorization();
app.MapControllers();

app.Run();

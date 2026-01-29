using FluentValidation;
using FluentValidation.AspNetCore;
using HeroManagement.API;
using HeroManagement.Application;
using HeroManagement.Application.DTOs.Validators;
using HeroManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<HeroManagementDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("HeroManagementConnection")));

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddValidatorsFromAssemblyContaining<AtualizarHeroiDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CriarHeroiDtoValidator>();

builder.Services.AddScoped<IHeroiService, HeroiService>();
builder.Services.AddScoped<IHeroiRepository, HeroiRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hero Management API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseCors("AllowAngular");

app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseMiddleware<ApiResponseMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();

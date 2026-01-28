using FluentValidation.AspNetCore;
using HeroManagement.API;
using HeroManagement.Application;
using HeroManagement.Application.DTOs.Validators;
using HeroManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<HeroManagementDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("HeroManagementConnection")));

builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<AtualizarHeroiDtoValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<CriarHeroiDtoValidator>();
    });

builder.Services.AddScoped<IHeroiService, HeroiService>();
builder.Services.AddScoped<IHeroiRepository, HeroiRepository>();

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

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseMiddleware<ApiResponseMiddleware>();

app.MapControllers();

app.Run();

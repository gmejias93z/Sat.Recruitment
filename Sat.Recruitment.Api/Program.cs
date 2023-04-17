using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Data;
using Sat.Recruitment.Api.Database;
using Sat.Recruitment.Api.Extensions;
using Sat.Recruitment.Api.Mapping;
using Sat.Recruitment.Api.Repositories;
using Sat.Recruitment.Api.Services;
using Sat.Recruitment.Api.Validations;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IValidator<UserDto>, UserValidator>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders();
    loggingBuilder.AddSerilog(Log.Logger);
});

builder.Services.AddDbContext<RecruitmentDbContext>();

builder.Services.RegisterMapsterConfiguration();

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.AddGlobalErrorHandler();

app.Run();
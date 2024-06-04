using StarbuckClone.Implementation;
using StarbuckClone.Implementation.UseCases.Commands.Users;
using StarbuckClone.Implementation.UseCases.Logging;
using StarbuckClone.Implementation.UseCases.Queries.AuditLogs;
using StarbuckClone.Implementation.Validators;
using StarbucksClone.Application;
using StarbucksClone.Application.UseCases.Commands.Users;
using StarbucksClone.Application.UseCases.Queries.AuditLogs;
using StarbucksClone.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<SCContext>();
builder.Services.AddTransient<RegisterUserDtoValidator>();
builder.Services.AddTransient<IRegisterUserCommand, EFRegisterUserCommand>();
builder.Services.AddTransient<IUseCaseLogger, DBUseCaseLogger>();
builder.Services.AddTransient<UseCaseHandler>();
builder.Services.AddTransient<ISearchAuditLogsQuery, EFSearchAuditLogs>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

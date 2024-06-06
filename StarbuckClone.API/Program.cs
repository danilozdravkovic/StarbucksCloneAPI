using StarbuckClone.Implementation;
using StarbuckClone.Implementation.UseCases.Commands.ProductCategories;
using StarbuckClone.Implementation.UseCases.Commands.Users;
using StarbuckClone.Implementation.UseCases.Logging;
using StarbuckClone.Implementation.UseCases.Queries.AuditLogs;
using StarbuckClone.Implementation.Validators;
using StarbucksClone.Application;
using StarbucksClone.Application.UseCases.Commands.ProductCategories;
using StarbucksClone.Application.UseCases.Commands.Users;
using StarbucksClone.Application.UseCases.Queries.AuditLogs;
using StarbucksClone.DataAccess;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<SCContext>();
builder.Services.AddTransient<RegisterUserDtoValidator>();
builder.Services.AddTransient<CreateProductCategoryDtoValidator>();
builder.Services.AddTransient<IRegisterUserCommand, EFRegisterUserCommand>();
builder.Services.AddTransient<ICreateProductCategoryCommand, EFCreateProductCategoryCommand>();
builder.Services.AddTransient<IUseCaseLogger, DBUseCaseLogger>();
builder.Services.AddTransient<UseCaseHandler>();
builder.Services.AddTransient<ISearchAuditLogsQuery, EFSearchAuditLogs>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IApplicationActorProvider>(x =>
{
    var accessor = x.GetService<IHttpContextAccessor>();

    var request = accessor.HttpContext.Request;

    var authHeader = request.Headers.Authorization.ToString();

    var context = x.GetService<SCContext>();

    return new DefaultActorProvider();
});
builder.Services.AddTransient<IApplicationActor>(x =>
{
    var accessor = x.GetService<IHttpContextAccessor>();
    if (accessor.HttpContext == null)
    {
        return new UnauthorizedActor();
    }

    return x.GetService<IApplicationActorProvider>().GetActor();
});

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

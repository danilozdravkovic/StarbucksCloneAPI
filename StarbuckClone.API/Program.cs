using StarbuckClone.Implementation;
using StarbuckClone.Implementation.Logging;
using StarbuckClone.Implementation.UseCases.Commands.CartLines;
using StarbuckClone.Implementation.UseCases.Commands.ProductCategories;
using StarbuckClone.Implementation.UseCases.Commands.Products;
using StarbuckClone.Implementation.UseCases.Commands.Users;
using StarbuckClone.Implementation.UseCases.Queries.AuditLogs;
using StarbuckClone.Implementation.UseCases.Queries.ProductCategories;
using StarbuckClone.Implementation.UseCases.Queries.Users;
using StarbuckClone.Implementation.Validators;
using StarbucksClone.Application;
using StarbucksClone.Application.UseCases.Commands.CartLines;
using StarbucksClone.Application.UseCases.Commands.ProductCategories;
using StarbucksClone.Application.UseCases.Commands.Products;
using StarbucksClone.Application.UseCases.Commands.Users;
using StarbucksClone.Application.UseCases.Queries.AuditLogs;
using StarbucksClone.Application.UseCases.Queries.ProductCategories;
using StarbucksClone.Application.UseCases.Queries.Users;
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
builder.Services.AddTransient<UpdateUserAccessDtoValidator>();
builder.Services.AddTransient<CreateProductDtoValidator>();
builder.Services.AddTransient<AddCartLineDtoValidator>();
builder.Services.AddTransient<IRegisterUserCommand, EFRegisterUserCommand>();
builder.Services.AddTransient<ICreateProductCategoryCommand, EFCreateProductCategoryCommand>();
builder.Services.AddTransient<IUpdateUserAccessCommand, EFUpdateUserAccessCommand>();
builder.Services.AddTransient<ICreateProductCommand, EFCreateProductCommand>();
builder.Services.AddTransient<IAddCartLineCommand, EFAddCartLineCommand>();
builder.Services.AddTransient<IUseCaseLogger, DBUseCaseLogger>();
builder.Services.AddTransient<UseCaseHandler>();
builder.Services.AddTransient<ISearchAuditLogsQuery, EFSearchAuditLogsQuery>();
builder.Services.AddTransient<ISearchUsersQuery, EFSearchUsersQuery>();
builder.Services.AddTransient<ISearchProductCategoriesQuery, EFSearchProductCategoriesQuery>();
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

app.UseStaticFiles();

app.Run();

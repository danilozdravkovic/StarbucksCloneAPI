using StarbuckClone.Implementation.UseCases.Commands.Users;
using StarbuckClone.Implementation.Validators;
using StarbuckClone.Implementation;
using StarbucksClone.Application.UseCases.Commands.Users;
using StarbucksClone.Application;
using StarbuckClone.Implementation.Logging;
using StarbuckClone.Implementation.UseCases.Commands.CartLines;
using StarbuckClone.Implementation.UseCases.Commands.ProductCategories;
using StarbuckClone.Implementation.UseCases.Commands.Products;
using StarbucksClone.Application.UseCases.Commands.CartLines;
using StarbucksClone.Application.UseCases.Commands.ProductCategories;
using StarbucksClone.Application.UseCases.Commands.Products;
using StarbuckClone.Implementation.UseCases.Queries.AuditLogs;
using StarbuckClone.Implementation.UseCases.Queries.ProductCategories;
using StarbuckClone.Implementation.UseCases.Queries.Users;
using StarbucksClone.Application.UseCases.Queries.AuditLogs;
using StarbucksClone.Application.UseCases.Queries.ProductCategories;
using StarbucksClone.Application.UseCases.Queries.Users;
using StarbucksClone.Application.UseCases.Commands.Orders;
using StarbuckClone.Implementation.UseCases.Commands.Orders;
using StarbucksClone.Application.UseCases.Queries.CartLines;
using StarbuckClone.Implementation.UseCases.Queries.CartLInes;

namespace StarbuckClone.API.Extensions
{
    public static class ContainerExtensions
    {
        public static void AddUseCases(this IServiceCollection services)
        {
            services.AddTransient<UseCaseHandler>();
            services.AddTransient<IUseCaseLogger, DBUseCaseLogger>();

            services.AddTransient<RegisterUserDtoValidator>();
            services.AddTransient<CreateProductCategoryDtoValidator>();
            services.AddTransient<UpdateUserAccessDtoValidator>();
            services.AddTransient<CreateProductDtoValidator>();
            services.AddTransient<AddCartLineDtoValidator>();
            services.AddTransient<ModifyCartLineDtoValidator>();

            services.AddTransient<IRegisterUserCommand, EFRegisterUserCommand>();
            services.AddTransient<ICreateProductCategoryCommand, EFCreateProductCategoryCommand>();
            services.AddTransient<IUpdateUserAccessCommand, EFUpdateUserAccessCommand>();
            services.AddTransient<ICreateProductCommand, EFCreateProductCommand>();
            services.AddTransient<IAddCartLineCommand, EFAddCartLineCommand>();
            services.AddTransient<ICreateOrderCommand, EFCreateOrderCommand>();
            services.AddTransient<IModifyCartLineCommand, EFModifyCartLineCommand>();
            services.AddTransient<IDeleteCartLineCommand, EFDeleteCartLineDto>();

            services.AddTransient<ISearchAuditLogsQuery, EFSearchAuditLogsQuery>();
            services.AddTransient<ISearchUsersQuery, EFSearchUsersQuery>();
            services.AddTransient<ISearchProductCategoriesQuery, EFSearchProductCategoriesQuery>();
            services.AddTransient<ISearchCartLinesQuery, EFSearchCartLinesQuery>();



        }
    }
}

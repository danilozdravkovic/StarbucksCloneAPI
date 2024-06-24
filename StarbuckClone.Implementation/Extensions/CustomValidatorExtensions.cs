using FluentValidation;
using StarbucksClone.Application.DTO;
using StarbucksClone.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Implementation.Extensions
{
    public static class CustomValidatorExtensions
    {

        public static IRuleBuilderOptions<T, int> SizeIdMustBeValid<T>(this IRuleBuilder<T, int> ruleBuilder, SCContext context)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("Size id is required.")
                .Must(x => context.Sizes.Any(p => p.Id == x && p.IsActive)).WithMessage("Size with given id does not exist.");
        }

        public static IRuleBuilderOptions<T, AddingAddInForCartDto> AddInsMustBeValid<T>(this IRuleBuilder<T, AddingAddInForCartDto> ruleBuilder, SCContext context)
        {
            return ruleBuilder
            .Must(addIn => context.AddIns.Any(x => x.Id == addIn.Id))
            .WithMessage("AddIn contains non-existing id");
        }

        public static IRuleBuilderOptions<T, int> CategoryParentIdMustBeValid<T>(this IRuleBuilder<T, int> ruleBuilder, SCContext context)
        {
            return ruleBuilder
            .NotNull().WithMessage("Category id is required")
                                      .Must(x => context.ProductCategories.Any(p => p.Id == x && p.IsActive)).WithMessage("Category doesn't exist");
        }

        //public static IRuleBuilderOptions<T, string> ImageSrcMustBeValid<T>(this IRuleBuilder<T, string> ruleBuilder)
        //{
        //    return ruleBuilder
        //        .NotNull().WithMessage("Image is required.")
        //        .Must((dto, fileName) =>
        //        {
        //            var path = Path.Combine("wwwroot", "temp", fileName);
        //            return File.Exists(path);
        //        }).WithMessage("File doesn't exist.");
        //}

        public static IRuleBuilderOptions<T, decimal> PriceMustBeValid<T>(this IRuleBuilder<T, decimal> ruleBuilder)
        {
            return ruleBuilder
                .NotNull().WithMessage("Price is required.")
                .GreaterThan(0).WithMessage("Price must be a positive number.");
        }


        public static IRuleBuilderOptions<T, string> FirstNameMustBeValid<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("First name can't be empty.")
                                     .MinimumLength(3).WithMessage("First name must be at lesat 3 characters long.");
        }

        public static IRuleBuilderOptions<T, string> LastNameMustBeValid<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("Last name can't be empty.")
                                     .MinimumLength(3).WithMessage("Last name must be at lesat 3 characters long.");
        }

        //public static IRuleBuilderOptions<T, string> PasswordMustBeValid<T>(this IRuleBuilder<T, string> ruleBuilder)
        //{
        //    return ruleBuilder
        //        .NotEmpty().WithMessage("Password can't be empty.")
        //                            .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{8,}$").WithMessage("Password must be at least 8 characters long, must contain one uppercase  letter,one lowercase letter and one number.");
        //}
        
    }
}

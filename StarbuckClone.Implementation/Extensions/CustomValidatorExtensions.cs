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
    }
}

using FluentValidation;
using StarbuckClone.Implementation.Extensions;
using StarbucksClone.Application.DTO;
using StarbucksClone.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Implementation.Validators
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        private readonly SCContext _context;
        public CreateProductDtoValidator(SCContext context) {

            _context = context;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name).NotNull().WithMessage("Product name is required.")
                                .MinimumLength(2).WithMessage("Product name can't be one letter.")
                                .MaximumLength(50).WithMessage("Product name can't be longer then 50 letters.")
                                .Must(x => !context.Products.Any(p => p.Name == x)).WithMessage("Product name already exists.");

            RuleFor(x => x.ImageSrc).ImageSrcMustBeValid();

            RuleFor(x => x.Calories).NotNull().WithMessage("Calories are required.");

            RuleFor(x => x.CategoryId).CategoryParentIdMustBeValid(_context);

            RuleFor(x => x.InitialPrice).PriceMustBeValid();
        }
    }

    public class ModifyProductDtoValidator : AbstractValidator<ModifyProductDto>
    {
        private readonly SCContext _context;
        public ModifyProductDtoValidator(SCContext context)
        {

            _context = context;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name)
            .NotNull().WithMessage("Product name is required.")
            .MinimumLength(2).WithMessage("Product name can't be one letter.")
            .MaximumLength(50).WithMessage("Product name can't be longer than 50 letters.")
            .Must((dto, name) => BeUniqueName(dto)).WithMessage("Product name already exists.");

            RuleFor(x => x.ImageSrc).ImageSrcMustBeValid();

            RuleFor(x => x.Calories).NotNull().WithMessage("Calories are required.");

            RuleFor(x => x.CategoryId).CategoryParentIdMustBeValid(_context);

            RuleFor(x => x.InitialPrice).PriceMustBeValid();
        }

        private bool BeUniqueName(ModifyProductDto dto)
        {
            return !_context.Products.Any(p => p.Name == dto.Name && p.Id != dto.Id);
        }
    }
}

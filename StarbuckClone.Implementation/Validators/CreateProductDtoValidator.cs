using FluentValidation;
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
        public CreateProductDtoValidator(SCContext context) {
            RuleFor(x => x.Name).NotNull().WithMessage("Product name is required.")
                                .MinimumLength(2).WithMessage("Product name can't be one letter.")
                                .MaximumLength(50).WithMessage("Product name can't be longer then 50 letters.")
                                .Must(x => !context.Products.Any(p => p.Name == x)).WithMessage("Product name already exists.");

            RuleFor(x => x.ImageSrc).NotNull().WithMessage("Image is required.")
                                    .Must((x, fileName) =>
                                    {
                                        var path = Path.Combine("wwwroot", "temp", fileName);

                                        var exists = Path.Exists(path);

                                        return exists;
                                    }).WithMessage("File doesn't exist.");

            RuleFor(x => x.Calories).NotNull().WithMessage("Calories are required.");

            RuleFor(x => x.CategoryId).NotNull().WithMessage("Category id is required")
                                      .Must(x => context.ProductCategories.Any(p => p.Id == x && p.IsActive)).WithMessage("Category doesn't exist");
        }
    }
}

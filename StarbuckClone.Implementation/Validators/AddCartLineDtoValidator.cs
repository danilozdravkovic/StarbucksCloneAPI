using FluentValidation;
using Microsoft.EntityFrameworkCore;
using StarbucksClone.Application.DTO;
using StarbucksClone.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Implementation.Validators
{
    public class AddCartLineDtoValidator : AbstractValidator<AddCartLineDto>
    {
        private readonly SCContext context;
        public AddCartLineDtoValidator(SCContext context) {
            this.context = context;
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product id is required.")
                                     .Must(x => context.Products.Any(p => p.Id == x && p.IsActive)).WithMessage("Product with given id does not exist");

            RuleFor(x => x.SizeId).NotEmpty().WithMessage("Size id is required.")
                                     .Must(x => context.Sizes.Any(p => p.Id == x && p.IsActive)).WithMessage("Size with given id does not exist");

            RuleForEach(x => x.AddIns)
            .Must(BeAValidAddIn)
            .WithMessage("AddIns containes not existing id");

        }

        private bool BeAValidAddIn(AddInForCartDto addIn)
        {
            return context.AddIns.Any(x => x.Id == addIn.Id);
        }
    }
}

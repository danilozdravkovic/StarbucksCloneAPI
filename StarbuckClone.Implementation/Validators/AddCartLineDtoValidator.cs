using FluentValidation;
using Microsoft.EntityFrameworkCore;
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
    public class AddCartLineDtoValidator : AbstractValidator<AddCartLineDto>
    {
        private readonly SCContext _context;
        public AddCartLineDtoValidator(SCContext context) {
            _context = context;
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product id is required.")
                .Must(x => context.Products.Any(p => p.Id == x && p.IsActive)).WithMessage("Product with given id does not exist.");

            RuleFor(x => x.SizeId).SizeIdMustBeValid(_context).When(x => x.SizeId !=0);

            RuleForEach(x => x.AddIns).AddInsMustBeValid(_context);

        }

        
    }

    public class ModifyCartLineDtoValidator : AbstractValidator<ModifyCartLineDto>
    {
        private readonly SCContext _context;
        public ModifyCartLineDtoValidator(SCContext context)
        {
            _context = context;
            CascadeMode = CascadeMode.StopOnFirstFailure;
        
            RuleFor(x => x.SizeId).SizeIdMustBeValid(_context);

            RuleForEach(x => x.AddIns).AddInsMustBeValid(_context);

        }


    }
}

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
    public class CreateProductCategoryDtoValidator : AbstractValidator<CreateProductCategoryDto>
    {
        private SCContext context;

        public CreateProductCategoryDtoValidator(SCContext context) 
        {
            this.context = context;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name).NotNull().WithMessage("Category name is required.")
                                .MinimumLength(2).WithMessage("Category name can't be one letter.")
                                .MaximumLength(50).WithMessage("Category name can't be longer then 50 letters.")
                                .Must(x => !context.ProductCategories.Any(p => p.Name == x)).WithMessage("Category name already exists.");

            RuleFor(x => x.ParentId).Must(DoesCategoryExistsWhenParentIdNotNull).WithMessage("Parent id does not exist.");

            RuleFor(x => x.ChildIds).Must(AllChildrenExist).WithMessage("Some of given child categories doesn't exist in database");
        }

        private bool DoesCategoryExistsWhenParentIdNotNull(int? parentId)
        {
            if (!parentId.HasValue)
            {
                return true;
            }

            return context.ProductCategories.Any(p => p.Id == parentId && p.IsActive);
        }

        private bool AllChildrenExist(IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return true;
            }
            
            int numberOfCategoriesThatExsists = context.ProductCategories.Count(x => x.IsActive && ids.Contains(x.Id));
            return numberOfCategoriesThatExsists == ids.Count();
           
        }
    }
}

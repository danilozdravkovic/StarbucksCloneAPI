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
    public abstract class BaseProductCategoryDtoValidator<T> : AbstractValidator<T>
        where T : BaseProductCategoryDto
    {
        protected readonly SCContext context;

        protected BaseProductCategoryDtoValidator(SCContext context)
        {
            this.context = context;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name).NotNull().WithMessage("Category name is required.")
                                .MinimumLength(2).WithMessage("Category name can't be one letter.")
                                .MaximumLength(50).WithMessage("Category name can't be longer than 50 letters.")
                                .Must(NameIsUnique)
                                .WithMessage("Category name already exists.");

            RuleFor(x => x.ParentId).Must(DoesCategoryExistsWhenParentIdNotNull)
                                    .WithMessage("Parent id does not exist.");

            RuleFor(x => x.ChildIds).Must(AllChildrenExist)
                                    .WithMessage("Some of given child categories don't exist in database");
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

            int numberOfCategoriesThatExist = context.ProductCategories.Count(x => x.IsActive && ids.Contains(x.Id));
            return numberOfCategoriesThatExist == ids.Count();
        }

        private bool NameIsUnique(BaseProductCategoryDto dto, string name)
        {
            if (dto is ModifyProductCategoryDto modifyDto)
            {
                return !context.ProductCategories.Any(p => p.Name == name && p.Id != modifyDto.Id);
            }
            else
            {
                return !context.ProductCategories.Any(p => p.Name == name);
            }
        }
    }

    public class CreateProductCategoryDtoValidator : BaseProductCategoryDtoValidator<CreateProductCategoryDto>
    {
        public CreateProductCategoryDtoValidator(SCContext context) : base(context)
        {
        }
    }

    public class ModifyProductCategoryDtoValidator : BaseProductCategoryDtoValidator<ModifyProductCategoryDto>
    {
        public ModifyProductCategoryDtoValidator(SCContext context) : base(context)
        {
        }
    }
}
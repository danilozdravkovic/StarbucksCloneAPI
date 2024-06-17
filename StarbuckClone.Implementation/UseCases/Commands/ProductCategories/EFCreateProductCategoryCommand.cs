using FluentValidation;
using StarbuckClone.Domain;
using StarbuckClone.Implementation.Validators;
using StarbucksClone.Application.DTO;
using StarbucksClone.Application.UseCases.Commands.ProductCategories;
using StarbucksClone.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Implementation.UseCases.Commands.ProductCategories
{
    public class EFCreateProductCategoryCommand : ICreateProductCategoryCommand
    {
        private SCContext _context;
        private CreateProductCategoryDtoValidator _validator;

        public EFCreateProductCategoryCommand(SCContext context, CreateProductCategoryDtoValidator validator)
        {
            _context = context;
            _validator = validator;
        }
        public int Id => 3;

        public string Name =>"Create new product category";

        public void Execute(CreateProductCategoryDto data)
        {
            _validator.ValidateAndThrow(data);

            ProductCategory category = new ProductCategory
            {
                Name = data.Name,
                ParentId = data.ParentId,
            };

            _context.ProductCategories.Add(category);

            var childCategories = _context.ProductCategories
                                          .Where(p => data.ChildIds.Contains(p.Id))
                                          .ToList();

            category.Children = childCategories;

            _context.SaveChanges();

            
        }
    }
}

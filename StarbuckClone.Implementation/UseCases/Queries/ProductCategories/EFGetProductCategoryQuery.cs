using StarbuckClone.Domain;
using StarbucksClone.Application.DTO;
using StarbucksClone.Application.Exceptions;
using StarbucksClone.Application.UseCases.Queries.ProductCategories;
using StarbucksClone.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Implementation.UseCases.Queries.ProductCategories
{
    public class EFGetProductCategoryQuery : IGetProductCategoryQuery
    {
        public readonly SCContext _context;

        public EFGetProductCategoryQuery(SCContext context)
        {
            _context = context;
        }
        public int Id => 23;

        public string Name => "Get product category";

        public ProductCategoryDto Execute(int search)
        {
            var productCategory = _context.ProductCategories.Where(p=>p.Id==search && p.IsActive).FirstOrDefault();

            if (productCategory==null)
            {
                throw new NotFoundException(typeof(ProductCategory).ToString(), search);
            }

            var productCatToReturn = new ProductCategoryDto
            {
                Id = productCategory.Id,
                Name = productCategory.Name,
            };

            FillChildCategories(productCatToReturn);

            return productCatToReturn;

        }

        private void FillChildCategories(ProductCategoryDto category)
        {
            var id = category.Id;

            category.Children = _context.ProductCategories.Where(p => p.ParentId == id).Select(x => new ProductCategoryDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            foreach (var child in category.Children)
            {
                FillChildCategories(child);
            }
        }
    }
}

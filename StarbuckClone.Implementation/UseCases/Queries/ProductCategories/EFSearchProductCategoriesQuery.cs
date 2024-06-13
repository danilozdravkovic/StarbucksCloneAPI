using StarbuckClone.Domain;
using StarbuckClone.Implementation.Extensions;
using StarbucksClone.Application.DTO;
using StarbucksClone.Application.UseCases.Queries.ProductCategories;
using StarbucksClone.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Implementation.UseCases.Queries.ProductCategories
{
    public class EFSearchProductCategoriesQuery : ISearchProductCategoriesQuery
    {
        public readonly SCContext _context;

        public EFSearchProductCategoriesQuery(SCContext context)
        {
            _context = context;
        }
        public int Id => 7;

        public string Name => "Search product categories";

        public PagedResponse<ProductCategoryDto> Execute(ProductCategorySearchDto search)
        {
            IQueryable<ProductCategory> query = _context.ProductCategories.AsQueryable();

            if (search.WithChildren.HasValue)
            {
                if (search.WithChildren.Value)
                {
                    query = query.Where(c => c.Children.Any());
                }
                else
                {
                    query = query.Where(c => !c.Children.Any());
                }
            }

            if (search.OnlyCategoriesWithoutParent.HasValue)
            {
                if (search.OnlyCategoriesWithoutParent.Value)
                {
                    query = query.Where(x => x.ParentId == null);
                }
            }

            if (search.ParentId.HasValue)
            {
                query = query.Where(x => x.ParentId == search.ParentId) ;
            }

            if (!string.IsNullOrEmpty(search.Name))
            {
                query = query.Where(x => x.Name.ToLower().Contains(search.Name.ToLower())); ;
            }

            var paginatedResult = query.AddPagination(search.Page, search.PerPage, x => new ProductCategoryDto
            {
                Id = x.Id,
                Name = x.Name
            });

            

            foreach(var oneProductCat in paginatedResult.Data){
                this.FillChildCategories(oneProductCat);
            }

            return paginatedResult;
        }

        private void FillChildCategories(ProductCategoryDto category)
        {

            int id = category.Id;

            category.Children = _context.ProductCategories.Where(x => x.ParentId == id).Select(c => new ProductCategoryDto
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();


            foreach (ProductCategoryDto child in category.Children)
            {
                FillChildCategories(child);
            }
        }
    }
}

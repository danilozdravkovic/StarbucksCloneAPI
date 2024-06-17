using Microsoft.EntityFrameworkCore;
using StarbuckClone.Domain;
using StarbuckClone.Implementation.Extensions;
using StarbucksClone.Application.DTO;
using StarbucksClone.Application.UseCases.Queries.Products;
using StarbucksClone.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Implementation.UseCases.Queries.Products
{
    public class EFSearchProductsQuery : ISearchProductsQuery
    {
        public readonly SCContext _context;

        public EFSearchProductsQuery(SCContext context)
        {
            _context = context;
        }
        public int Id => 16;

        public string Name => "Search products";

        public PagedResponse<ProductDto> Execute(ProductSearchDto search)
        {
            IQueryable<Product> query = _context.Products.Include(p => p.Category).Where(p => p.IsActive).AsQueryable();

            if (!string.IsNullOrEmpty(search.Name))
            {
                query = query.Where(x => x.Name.ToLower().Contains(search.Name.ToLower()));
            }

            if (search.CategoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == search.CategoryId.Value);
            }

           

            return query.AddPagination(search.Page, search.PerPage, x => new ProductDto
            {
                Id = x.Id,
                Name=x.Name,
                Category=x.Category.Name,
                Calories=x.Calories,
                ImageSrc=x.ImageSrc,
                Price=x.InitialPrice
            });
        }
    }
}

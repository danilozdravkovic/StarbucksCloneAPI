﻿using StarbuckClone.Domain;
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
            IQueryable<ProductCategory> query = _context.ProductCategories.Where(p=>p.IsActive).AsQueryable();


            if (search.ParentId.HasValue)
            {
                query = query.Where(x => x.ParentId == search.ParentId) ;
            }
            else
            {
                query = query.Where(x => x.ParentId == null);
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

            category.Children = _context.ProductCategories.Where(x => x.ParentId == id && x.IsActive).Select(c => new ProductCategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                ParentId=c.ParentId
            }).ToList();


            foreach (ProductCategoryDto child in category.Children)
            {
                FillChildCategories(child);
            }
        }
    }
}
